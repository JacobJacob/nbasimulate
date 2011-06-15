#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from xba.business import dev_match_manager
from xba.business import game_manager
from xba.business import club_manager
from base import BaseClient
from xba.common.decorators import ensure_success


class RoundUpdateHandler(BaseClient):
    

    MATCH_ENGINE_EXE_PATH = "D:\\develop\\xba_workspace\\src\\Client\\Client\\bin\\Debug\\Client.exe"
    
    def __init__(self):
        self._days = -1
        self._turn = -1
        self._season = -1
    
    def work(self):
        self.log("start round update for season:%s, round:%s" % (self._season, self._turn))
        
        #before run
        #self.before_run()
        
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
            
            #after run
            self.after_run()
            
        return "exist"
    
    
    def normal_update_step_one(self):
        """常规数据更新1(赛前)"""
        command = "%s %s %s" % (RoundUpdateHandler.MATCH_ENGINE_EXE_PATH, "round_update_handler", 1)
        self.log("start to run command:%s" % command) 
        os.system(command)
        
    def normal_update_step_two(self):
        """常规数据更新2(赛后)"""
        command = "%s %s %s" % (RoundUpdateHandler.MATCH_ENGINE_EXE_PATH, "round_update_handler", 2)
        self.log("start to run command:%s" % command) 
        os.system(command)
    
    def before_run(self):
        """运行前的初始化"""
        self.log("before run")
        game_info = self.get_game_info()
        self._days = game_info["Days"]
        self._turn = game_info["Turn"]
        self._season = game_info["Season"]
        
    def after_run(self):
        """结束前的收尾动作"""
        self.set_to_next_days()
        if self._days not in (14, 28):
            self.set_to_next_turn()
        
    @ensure_success()
    def set_to_next_days(self):
        """将赛季天数前进一天 """
        return game_manager.set_to_next_days()
    
    @ensure_success()
    def set_to_next_turn(self):
        """将赛季轮数前进一轮 """
        return game_manager.set_to_next_turn()
    
    @ensure_success()
    def get_game_info(self):
        return game_manager.get_game_info()
    
    def dev_match_update(self):
        """比赛更新"""
        self.log("start dev match update....")
        dev_match_infos = self.get_dev_match()
        for dev_match_info in dev_match_infos:
            match_id = dev_match_info["DevMatchID"]
            club_home_score = dev_match_info["ClubHScore"]
            club_away_score = dev_match_info["ClubAScore"]
            if club_home_score > 0 or club_away_score > 0:
                self.log("match had finished")
                continue
            
            command = "%s %s %s" % (RoundUpdateHandler.MATCH_ENGINE_EXE_PATH, "dev_match_handler", match_id)
            self.log("start to run command:%s" % command)
            
            os.system(command)
            
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
            
    @ensure_success()
    def set_club_main_xml(self, club_id, info):
        """设置俱乐部的main xml"""
        self.log("start update club %s's main xml" % club_id)
        return club_manager.set_club_main_xml(club_id, info)
    
    @ensure_success()
    def get_club_info(self, club_id):
        """获取俱乐部info"""
        return club_manager.get_club_by_id(club_id)

    @ensure_success()
    def get_dev_match(self, next=False):
        """获取某轮的比赛列表"""
        turn = self._turn + 1 if next else self._turn
        return dev_match_manager.get_round_dev_matchs(0, turn)
 
if __name__ == "__main__":
    handler = RoundUpdateHandler()
    handler.start()
