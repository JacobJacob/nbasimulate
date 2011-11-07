#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from xba.config import CLIENT_EXE_PATH
from subprocess import Popen, PIPE

from xba.business import game_manager, xcup_manager, xguess_manager
from xba.common.decorators import ensure_success
from xba.common.cupladder import CupLadder, CupLadderRoundMatch
from base import BaseClient
from xba.config import WEB_ROOT, DOMAIN

STATUS_NEW = 0
STATUS_RUN = 1
STATUS_FINISH = 3

class GroupCupHandler(BaseClient):
    
    CLIENT_NAME = "groupcup_handler"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        self._turn = -1
        
    def work(self):
        
        self.log("turn:%s" % self._turn)
        if self._turn < 3:
            return "exit"
        
        small_group_finish = False
        if self._turn == 3:
            #安排赛程,第三轮安排赛程
            xcup_info = self.get_xgroup_game_by_status(STATUS_NEW)
            if not xcup_info:
                self.log("cup had arrange!!!")
                return "exit"
            cup_id = xcup_info["XGameID"]
            self.log("start to arrange group!!!")
            self.arrange_group(cup_id)
        else:
            xcup_info = self.get_xgroup_game_by_status(STATUS_RUN)
            if not xcup_info:
                self.log("not cup running!!!")
                small_group_finish = True
            else:
                round = xcup_info["Round"]
                xcupid = xcup_info["XGameID"]
                match_infos = self.get_group_match_to_play()
                if not match_infos:
                    self.log("not match!!!!")
                if match_infos:
                    self.log("has:%s matchs" % len(match_infos))
                    has_execute_match = False
                    for match_info in match_infos:
                        #比赛差错效验
                        if round != match_info["Round"]:
                            self.log("the round not match:%s-->%s" % (round, match_info["Round"]))
                            continue
                        has_execute_match = True
                        self.execute_match(match_info["ClubAID"], match_info["ClubBID"], xcupid, 0, round, 1, match_info["XGroupMatchID"])
                    
                    #只有最少打一场比赛时打更新到下一轮
                    if has_execute_match:
                        #更新状态
                        if round < 10:
                            #轮次往前推一轮
                            self.log("xgame set to round++")
                            self.set_round_by_xcupid(xcupid, round + 1)
                        else:
                            #设置小组赛完成
                            self.log("xgame set to finish")
                            self.set_status_by_xgameid(xcupid, STATUS_FINISH)
                            #出线球队注册
                            for_arrange_cup_info = self.get_xgroup_big_game_by_status(STATUS_NEW)
                            if for_arrange_cup_info:
                                xcup_id = for_arrange_cup_info["XGameID"]
                                #球队出线，排定赛程
                                #状态更新为1,轮次更新为2
                                self.arrange_xcup(xcup_id, True)
                    else:
                        self.log("not match exeucte!!!")
        #小杯赛已经打完
        if small_group_finish:
            #决赛比赛
            for_match_cup_info = self.get_xgroup_big_game_by_status(STATUS_RUN)
            if for_match_cup_info:
                self.handle_xgame(for_match_cup_info)
            else:
                self.log("not any cup running!!!")
        
        return "exit"
    
    @ensure_success
    def get_group_match_to_play(self):
        """获取要打的比赛"""
        return xcup_manager.get_group_match_to_play()
    
    @ensure_success
    def arrange_group(self, cup_id):
        """安排赛程"""
        return xcup_manager.arrange_group(cup_id) 
    
    @ensure_success 
    def get_xgroup_game_by_status(self, status):
        """根据状态获取小组赛"""
        return xcup_manager.get_xgroup_game_by_status(status)
   
    @ensure_success 
    def get_xgroup_big_game_by_status(self, status):
        """根据状态获取决赛"""
        return xcup_manager.get_xgroup_big_game_by_status(status)

    def before_run(self):
        """before run"""
        self.log("before run")
        game_info = self.get_game_info()
        self._turn = game_info["Turn"]
        
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()
     
    def handle_xgame(self, xcup_info):
        xcup_id = xcup_info["XGameID"]
        logo = "XBACup/ChampionCup.GIF"
        round = xcup_info["Round"]
        name = "冠军杯"
        self.log("now round is:%s" % round) 
        ladder_url = xcup_info["LadderURL"]
        save_path = os.path.join(WEB_ROOT, ladder_url.replace(DOMAIN, ""))
        #第一轮，安排赛程
        if round == 0:
            return
        else:
            self.log("start to match update")
            #先打比赛
            #取出存活的球队
            alive_reg_infos = self.get_xreg_table_by_cup_id_dead_round(xcup_id, round)
            base_code_map = {}
            for alive_reg_info in alive_reg_infos:
                index = round * -1
                base_code = alive_reg_info["BaseCode"]
                if len(base_code) == 1:
                    continue
                gain_code = base_code[:index]
                self.log("gain code is:%s" % gain_code)
                match_code = base_code[index]
                match_info = base_code_map.get(gain_code, {})
                if int(match_code) == 0:
                    match_info["home"] = alive_reg_info
                else:
                    match_info["away"] = alive_reg_info
                base_code_map[gain_code] = match_info
                    
            for gain_code, match_info in base_code_map.iteritems():
                home_alive_reg_info = match_info.get("home")
                away_alive_reg_info = match_info.get("away")
                if not away_alive_reg_info or not home_alive_reg_info:
                    self.log("empty match")
                    continue
                
                home_club_id = home_alive_reg_info["ClubID"]
                away_club_id = away_alive_reg_info["ClubID"]
                self.log("start to execute match:home:%s, away:%s" % (home_club_id, away_club_id))
                self.execute_match(home_club_id, away_club_id, xcup_id, gain_code, round, 5, 0)
            
            is_last_round, champion_user_id, champion_club_id, champion_club_name = self.write_html(xcup_id, name, logo, save_path, round)            
                
            if is_last_round: 
                self.finish_xcup(xcup_info, champion_user_id, champion_club_id, champion_club_name)
                self.xguess_settled(champion_club_id)
            else:
                self.set_round_by_xcupid(xcup_id, round + 1)
     
    @ensure_success           
    def finish_xcup(self, xcup_info, champion_user_id, champion_club_id, champion_club_name):
        """冠军杯完成"""
        return xcup_manager.finish_xcup(xcup_info, champion_user_id, champion_club_id, champion_club_name)
    
    @ensure_success 
    def xguess_settled(self, champion_club_id):
        """冠军杯冠军平盘"""
        return xguess_manager.xguess_settled(champion_club_id)
                
    def write_html(self, xcup_id, name, logo, save_path, round):
        """write html"""
        cupladder = CupLadder(name, logo, css_path="../Css/Base.css")
        champion_user_id, champion_club_name, champion_club_id = None, None, None
        #将每一轮的赛况输出到html
        is_last_round = False
        for each_round in range(round + 1):
            self.log("start to handle round:%s" % each_round)
            alive_reg_infos = self.get_xreg_table_by_cup_id_dead_round(xcup_id, each_round)
            if len(alive_reg_infos) == 1:
                is_last_round = True
                champion_user_id = alive_reg_infos[0]["UserID"]
                champion_club_name = alive_reg_infos[0]["ClubName"]
                champion_club_id = alive_reg_infos[0]["ClubID"]
                
            self.log("alive _reg team is:%s" % len(alive_reg_infos))
            if is_last_round:
                self.log("is last round")
            gain_code_maps = {}
            for alive_reg_info in alive_reg_infos:
                base_code = alive_reg_info["BaseCode"]
                if len(base_code) == 1:
                    continue
                index = (each_round + 1) * -1
                try:
                    base_code_prefix = base_code[:index]
                    base_code_subfix = base_code[index]
                except:
                    self.log("round index error!!!!")
                    continue
                self.log("base_code:%s, base_code_prefix:%s,  base_code_subfix:%s" % (base_code, base_code_prefix, base_code_subfix))
                info = gain_code_maps.get(base_code_prefix, {})
                if int(base_code_subfix) == 0:
                    info["home"] = alive_reg_info
                else:
                    info["away"] = alive_reg_info
                
                gain_code_maps[base_code_prefix] = info
                    
            gain_codes = gain_code_maps.keys()
            gain_codes.sort()
            
            for gain_code in gain_codes:
                info = gain_code_maps[gain_code]
                home = info.get("home")
                away = info.get("away")
                home_user_id = home["UserID"]
                home_club_name = home["ClubName"]    
                if home and away:
                    away_user_id = away["UserID"]
                    away_club_name = away["ClubName"]
                    xcup_match = self.get_xcup_match_by_gaincode_xcupid(xcup_id, gain_code)
                    match = CupLadderRoundMatch(5, home_user_id, home_club_name, away_user_id, away_club_name, xcup_match, is_last_round)
                else:
                    match = CupLadderRoundMatch(5, home_user_id, home_club_name, is_last_round=is_last_round)
                    
                cupladder.add_match(each_round, match)
                
        cupladder.write(save_path)
        
        return is_last_round, champion_user_id, champion_club_id, champion_club_name
            
    @ensure_success    
    def get_xreg_table_by_cup_id_dead_round(self, cup_id, dead_round):
        """获取存活球员"""
        return xcup_manager.get_xreg_table_by_cup_id_dead_round(cup_id, dead_round)
    
    @ensure_success
    def arrange_xcup(self, xcup_id, fource=True):
        """出线球队注册"""
        return xcup_manager.arrange_xcup(xcup_id, fource)
    
    @ensure_success     
    def execute_match(self, cluba, clubb, xcupid, gain_code, round, category, match_id):
        cmd = "%s %s %s %s %s %s %s %s %s" % (CLIENT_EXE_PATH, 'xcup_match_handler', xcupid, gain_code, cluba, clubb, round, category, match_id)
        self.call_cmd(cmd)
                 
    @ensure_success
    def run_match(self):
        round = 7
        match_infos = self.get_group_match_to_play()
        if not match_infos:
            self.log("not match!!!!")
        if match_infos:
            for match_info in match_infos:
                #比赛差错效验
                if round != match_info["Round"] and False:
                    self.log("the round not match:%s-->%s" % (round, match_info["Round"]))
                    continue
                self.execute_match(match_info["ClubAID"], match_info["ClubBID"], 1, 0, round, 1, match_info["XGroupMatchID"])
    
    @ensure_success
    def get_xcup_match_by_gaincode_xcupid(self, xcup_id, gain_code):
        """根据GainCode和杯赛ID获取比赛"""
        return xcup_manager.get_xcup_match_by_gaincode_xcupid(xcup_id, gain_code)

    @ensure_success       
    def set_status_by_xgameid(self, xcup_id, status):
        """设置status"""
        return xcup_manager.set_status_by_xcupid(xcup_id, status)
            
    @ensure_success
    def set_round_by_xcupid(self, xcup_id, round):
        """设置round"""
        return xcup_manager.set_round_by_xcupid(xcup_id, round)
    
    @ensure_success        
    def set_code_by_xcupregid(self, regid, code):
        """设置reg code"""
        return xcup_manager.set_code_by_xregid(regid, code)
                

if __name__ == "__main__":
    handler = GroupCupHandler()
    handler.start()
    #handler.run_match()