#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from xba.config import CLIENT_EXE_PATH
from subprocess import Popen, PIPE

from xba.business import cup_manager, tool_manager, account_manager
from xba.common.decorators import ensure_success
from xba.common import cup_util
from xba.common.reward import Reward
from xba.common.cupladder import CupLadder, CupLadderRoundMatch
from base import BaseClient
from xba.config import WEB_ROOT, DOMAIN

class CupHandler(BaseClient):
    
    
    CLIENT_NAME = "cup_handler"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        
    def work(self):
        
        cup_infos = self.get_cup_table_toarrage()
        if cup_infos:
            for cup_info in cup_infos:
                self.handle_cup(cup_info)
        else:
            self.log("not cup need set arrage")
            
        cup_infos = self.get_run_cuptable()
        if cup_infos:
            for cup_info in cup_infos:
                self.handle_cup(cup_info)
        else:
            self.log("not cup need run")

        return "exist"
     
    @ensure_success
    def get_cup_table_toarrage(self):
        return cup_manager.get_cup_table_toarrage()
    
    @ensure_success
    def get_run_cuptable(self):
        return cup_manager.get_run_cuptable()
    
    def handle_cup(self, cup_info):
        cup_id = cup_info["CupID"]
        capacity = cup_info["Capacity"]
        category = cup_info["Category"]
        logo = cup_info["BigLogo"]
        name = cup_info["Name"]
        round = cup_info["Round"]
        self.log("now round is:%s" % round) 
        ladder_url = cup_info["LadderURL"]
        save_path = os.path.join(WEB_ROOT, ladder_url.replace(DOMAIN, ""))
        #第一轮，安排赛程
        if round == 0:
            alive_reg_infos = self.get_alive_reg_table_by_cupid(cup_id)
            #没人报名
            if not alive_reg_infos:
                self.set_status_by_cupid(cup_id, 3)
                return 
            
            self.log("cup id is:%s" % cup_id) 
            club_ids = []
            club_ids_map = {}
            for alive_reg_info in alive_reg_infos:
                club_id = alive_reg_info["ClubID"]
                self.log(club_id)
                club_ids.append(club_id)
                club_ids_map[club_id] = alive_reg_info
                
            _, result_map = cup_util.create_cupLadder(capacity, club_ids)
            keys = result_map.keys()
            self.log("start sort key")
            keys.sort()
            cupladder = CupLadder(name, logo, category=3)
            home_club_id = 0
            for i, key in enumerate(keys):
                value = result_map[key]
                if value > 0:
                    """设置base code"""
                    self.set_code_by_regid(club_ids_map[value]["CupRegID"], key)
                if i % 2 == 0:
                    home_club_id = value
                    continue
                else:
                    home_user_id = club_ids_map[home_club_id]["UserID"]
                    home_club_name = club_ids_map[home_club_id]["ClubName"].strip()
                    if value > 0:
                        away_user_id = club_ids_map[value]["UserID"]
                        away_club_name = club_ids_map[value]["ClubName"].strip()
                        match = CupLadderRoundMatch(3, home_user_id, home_club_name, away_user_id, away_club_name)
                    else:
                        match = CupLadderRoundMatch(3, home_user_id, home_club_name)
                            
                    cupladder.add_match(0, match)
                    
            cupladder.write(save_path)
            self.set_round_by_cupid(cup_id, round + 1)
            self.set_status_by_cupid(cup_id, 1)
        else:
            self.log("start to match update")
            #先打比赛
            
            #取出存活的球队
            alive_reg_infos = self.get_reg_by_cupid_end_round(cup_id, round)
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
                self.execute_match(home_club_id, away_club_id, cup_id, gain_code, round, category)
                
            cupladder = CupLadder(name, logo, category=3)
            champion_user_id, champion_club_name, champion_club_id = None, None, None
            #将每一轮的赛况输出到html
            is_last_round = False
            for each_round in range(round + 1):
                self.log("start to handle round:%s" % each_round)
                alive_reg_infos = self.get_reg_by_cupid_end_round(cup_id, each_round)
                if len(alive_reg_infos) == 1 and each_round != 0:
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
                    base_code_prefix = base_code[:index]
                    base_code_subfix = base_code[index]
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
                        devcup_match = self.get_cup_match_by_gaincode_cupid(cup_id, gain_code)
                        match = CupLadderRoundMatch(3, home_user_id, home_club_name, away_user_id, away_club_name, devcup_match, is_last_round)
                    else:
                        match = CupLadderRoundMatch(3, home_user_id, home_club_name, is_last_round=is_last_round)
                        
                    cupladder.add_match(each_round, match)
                    
            cupladder.write(save_path)
                
            self.set_round_by_cupid(cup_id, round + 1)
            if is_last_round: 
                self.finish_devcup(cup_info, champion_user_id, champion_club_id, champion_club_name)
            
    def finish_devcup(self, cup_info, user_id, club_id, club_name):
        """杯赛完成"""
        cupid = cup_info["CupID"]
        round = cup_info["Round"]
        reward_xml = cup_info["RewardXML"]
        cup_manager.set_cup_champion(cupid, user_id, club_name)
        cup_manager.set_status_by_cupid(cupid, 3)
        account_manager.remove_cupids_from_account(cupid)
        #杯赛奖励
        reward = Reward(reward_xml)
        rounds = [i for i in range(round)]
        if round == 1:
            only_money = True
        rounds.append(100)
        for round in rounds:
            alive_clubs = self.get_reg_table_by_cupid_deadround(cupid, round)
            if not alive_clubs:
                continue
            
            reward_info = reward.get_reward(round)
            for alive_club in alive_clubs:
                self.reware(cupid, round, alive_club, reward_info, only_money)
                
    def reware(self, cupid, round, alive_club, reward_info, only_money=False):
        """杯赛奖厉"""
        money = reward_info.get('money')
        score = reward_info.get('score')
        tool = reward_info.get('tool')
        if not money and not score and not tool:
            return
        
        if only_money:
            score = None
            tool = None
            if round == 100:
                round = 1
        
        money_total = money.money if money else 0
        score_total = score.score if score else 0
        self.reward_cup_by_clubid(alive_club["ClubID"], cupid, round, money_total, score_total)
        
        if tool:
            self.log("start to assign tool")
            self.assign_tool_id_by_category_ticket(alive_club["UserID"], tool.category, tool.ticket_category)
            
    @ensure_success        
    def assign_tool_id_by_category_ticket(self, userid, category, ticket_category):
        """发放邀请函"""
        return tool_manager.assign_tool_id_by_category_ticket(userid, category, ticket_category)

    def execute_match(self, cluba, clubb, cupid, gain_code, round, category):
        cmd = "%s %s %s %s %s %s %s %s" % (CLIENT_EXE_PATH, 'cup_match_handler', cupid, gain_code, cluba, clubb, round, category)
        return self.call_cmd(cmd)
    
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
    
    @ensure_success
    def get_cup_match_by_gaincode_cupid(self, cupid, gain_code):
        """根据GainCode和杯赛ID获取比赛"""
        return cup_manager.get_cup_match_by_gaincode_cupid(cupid, gain_code)

    @ensure_success       
    def set_status_by_cupid(self, cupid, status):
        """设置status"""
        return cup_manager.set_status_by_cupid(cupid, status)
    
    @ensure_success       
    def get_reg_by_cupid_end_round(self, cupid, round):
        """获取存活球队"""
        return cup_manager.get_reg_by_cupid_end_round(cupid, round)
    
    @ensure_success 
    def get_reg_table_by_cupid_deadround(self, cupid, round):
        """获得第几轮挂的球队"""
        return cup_manager.get_reg_table_by_cupid_deadround(cupid, round)
    
    @ensure_success 
    def reward_cup_by_clubid(self, clubid, cupid, round, money, score):
        """杯赛奖励"""
        return cup_manager.reward_cup_by_clubid(clubid, cupid, round, money, score)
            
    @ensure_success
    def set_round_by_cupid(self, cupid, round):
        """设置round"""
        return cup_manager.set_round_by_cupid(cupid, round)
    
    @ensure_success        
    def set_code_by_regid(self, regid, code):
        """设置reg code"""
        return cup_manager.set_code_by_regid(regid, code)
                
    def get_alive_reg_table_by_cupid(self, cupid):
        return cup_manager.get_alive_reg_table_by_cupid(cupid)    
    

if __name__ == "__main__":
    handler = CupHandler()
    handler.start()
