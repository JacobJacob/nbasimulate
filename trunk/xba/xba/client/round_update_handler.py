#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from subprocess import Popen, PIPE
from datetime import datetime

from xba.config import CLIENT_EXE_PATH, PathSettings
from xba.business import dev_match_manager
from xba.business import game_manager
from xba.business import club_manager
from xba.business import arrange_manager
from xba.business import account_manager
from xba.business import union_field_manager
from xba.business import cup_manager
from xba.business import only_one_match_manager
from xba.business import player5_manager
from xba.business import player3_manager
from base import BaseClient
from xba.client import db_backup
from xba.common.decorators import ensure_success
from xba.common.constants.club import ClubCategory
from xba.common import single_process

class RoundUpdateHandler(BaseClient):
    
    CLIENT_NAME = "round_update_handler"
    
    def __init__(self):
        super(RoundUpdateHandler, self).__init__(RoundUpdateHandler.CLIENT_NAME)
        self._days = -1
        self._turn = -1
        self._season = -1
    
    def work(self):
        self.log("start round update for season:%s, round:%s" % (self._season, self._turn))
        
                
        self.log("start trian player finish")
        self.trian_player_finish()
        
        if self._days == 14:
            self.log("season truce")
        elif self._days == 28:
            self.log("season finish")
        else:
            #normal update step one
            self.normal_update_step_one()
            
            #match update
            self.dev_match_update()
            
            #after match update handle
            self.update_next_match_info_to_mainxml()
            
            #normal update step two
            self.normal_update_step_two()
            
        #update arrange level
        self.log("start update arrange level")
        self.update_arrange_lvl()
        
        #only one update
        self.log("start only one update")
        self.only_one_update()  
        
        self.log("start update_season_mvp_value")
        self.update_season_mvp_value()

        self.log("start night delete union")
        self.night_update_delete_union()
        
        self.log("start to create player3")
        self.create_player3()
        
        self.log("start to delete online table")
        self.delete_online_table()
        
        self.log("start to delete friend match msg")
        self.delete_fri_match_msg()
        
        self.log("start day add cup")
        self.day_add_cup()
        
        #after run
        self.after_run()
            
        return "exist"
    
    def update_season_mvp_value(self):
        """更新MVP值"""
        return player5_manager.update_season_mvp_value()
    
    def only_one_update(self):
        """胜者为王更新"""
        only_one_match_manager.send_money_by_only_day_point()
        only_one_match_manager.night_update_only_one_game()
    
    def update_arrange_lvl(self):
        """战术等级更新"""
        arrange_manager.update_arrange_lvl(ClubCategory.STREET)
        arrange_manager.update_arrange_lvl(ClubCategory.PROFESSION)
        
    def trian_player_finish(self):
        """试训的球员离队"""
        trian_players = player5_manager.get_trialing_player()
        if trian_players:
            for trian_player in trian_players:
                command = "%s %s %s %s" % (CLIENT_EXE_PATH, "change_player_from_arrange5_handler", trian_player["PlayerID"], trian_player["ClubID"])
                self.call_cmd(command)
                player5_manager.un_set_trial_player(trian_player["ClubID"], trian_player["PlayerID"])
    
    def normal_update_step_one(self):
        """常规数据更新1(赛前)"""
        command = "%s %s %s" % (CLIENT_EXE_PATH, "round_update_handler", 1)
        self.log("start to run command:%s" % command) 
        self.call_cmd(command)
        
    def normal_update_step_two(self):
        """常规数据更新2(赛后)"""
        command = "%s %s %s" % (CLIENT_EXE_PATH, "round_update_handler", 2)
        self.log("start to run command:%s" % command) 
        self.call_cmd(command)
    
    def before_run(self):
        """运行前的初始化"""
        self.log("start to backup database")
        self.back_up()
        
        self.log("before run")
        game_info = self.get_game_info()
        self._days = game_info["Days"]
        self._turn = game_info["Turn"]
        self._season = game_info["Season"]
        
    def back_up(self):
        """备份数据"""
        return db_backup.backup(file_name="round_update_handler")
        
    def after_run(self):
        """结束前的收尾动作"""
        self.set_to_next_days()
        if self._days not in (14, 28):
            self.set_to_next_turn()
        
    @ensure_success
    def set_to_next_days(self):
        """将赛季天数前进一天 """
        return game_manager.set_to_next_days()
    
    @ensure_success
    def set_to_next_turn(self):
        """将赛季轮数前进一轮 """
        return game_manager.set_to_next_turn()
    
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()
    
    def dev_match_update(self):
        """比赛更新"""
        self.log("start dev match update....")
        dev_match_infos = self.get_dev_match()
        for dev_match_info in dev_match_infos:
            if not dev_match_info["ClubHID"] or not dev_match_info["ClubAID"]:
                self.log("one club is empty continue")
                continue
            match_id = dev_match_info["DevMatchID"]
            club_home_score = dev_match_info["ClubHScore"]
            club_away_score = dev_match_info["ClubAScore"]
            if club_home_score > 0 or club_away_score > 0:
                self.log("match had finished")
                continue
            
            command = "%s %s %s" % (CLIENT_EXE_PATH, "dev_match_handler", match_id)
            self.log("start to run command:%s" % command)
            
            self.call_cmd(command)
            
    def update_next_match_info_to_mainxml(self):
        """将下一轮的对阵更新到club的main xml中"""
        if self._turn == 26:
            self.log("now is the last turn of the season, not need to handle next turn")
            return
        dev_match_infos = self.get_dev_match(True)
        for dev_match_info in dev_match_infos:
            club_home_id = dev_match_info["ClubHID"]
            club_away_id = dev_match_info["ClubAID"]
            if not (club_home_id and club_away_id):
                continue
            club_home_info = self.get_club_info(club_home_id)
            club_away_info = self.get_club_info(club_away_id)
            if not club_home_info or not club_away_info:
                print dev_match_info
                raise "error"

            club_home_name = club_home_info["Name"].strip() if club_home_info["Name"] else ""
            club_away_name = club_away_info["Name"].strip() if club_away_info["Name"] else ""
            club_home_logo = club_home_info["Logo"].strip() if club_home_info["Logo"] else ""
            club_away_logo = club_away_info["Logo"].strip() if club_away_info["Logo"] else ""
            next_match_info = {"NClubNameH": club_home_name, "NClubNameA": club_away_name,
                               "NClubLogoH": club_home_logo, "NClubLogoA": club_away_logo}
            self.set_club_main_xml(club_home_id, next_match_info)
            self.set_club_main_xml(club_away_id, next_match_info)
            
    @ensure_success
    def set_club_main_xml(self, club_id, info):
        """设置俱乐部的main xml"""
        self.log("start update club %s's main xml" % club_id)
        return club_manager.set_club_main_xml(club_id, info)
    
    @ensure_success
    def get_club_info(self, club_id):
        """获取俱乐部info"""
        return club_manager.get_club_by_id(club_id)

    @ensure_success
    def get_dev_match(self, next=False):
        """获取某轮的比赛列表"""
        turn = self._turn + 1 if next else self._turn
        return dev_match_manager.get_round_dev_matchs(0, turn)
    
    @ensure_success
    def night_update_delete_union(self):
        """删除威望小于1的联盟"""
        return union_field_manager.night_update_delete_union()
    
    @ensure_success
    def create_player3(self):
        """刷街头球员"""
        return player3_manager.create_player(600, 6, 20)
    
    @ensure_success
    def delete_online_table(self):
        """清空在线表"""
        return account_manager.delete_online_table()
    
    @ensure_success
    def delete_fri_match_msg(self):
        """清空在线聊天刻录"""
        return account_manager.delete_fri_match_msg()
    
    @ensure_success
    def day_add_cup(self):
        """每天刷出杯子"""
        return cup_manager.day_add_cup()
        
    def call_cmd(self, cmd):
        """调用命令"""
        p = Popen(cmd, stdout=PIPE)
        while True:
            line = p.stdout.readline()
            if not line:
                break
            self.log(line.replace("\n", ""))
            
        if p.wait() == 0:
            self.log("call %s success" % cmd)
 
if __name__ == "__main__":
    s = single_process.SingleProcess("RoundUpdateHandler")
    s.check()
    lock_file = os.path.join(PathSettings.ROUND_UPDATE_LOCK, datetime.now().strftime("%Y_%m_%d.lock"))
    if os.path.exists(lock_file):
        print "update had finish"
    else:
        f = open(lock_file, "wb")
        f.close()
        handler = RoundUpdateHandler()
        handler.start()
