#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import random

from xba.config import CLIENT_EXE_PATH

from xba.business import union_cup_manager, club_manager
from xba.common.decorators import ensure_success
from xba.common import cup_util
from xba.common.unioncupladder import UnionCupLadder, CupLadderRoundPair, CupLadderRoundMatch
from base import BaseClient
from xba.config import WEB_ROOT, DOMAINS

#联盟争赛赛处理器

TEAM_COUNT = 5

class UnionCupHandler(BaseClient):
    
    
    CLIENT_NAME = "unioncup_handler"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        
    def work(self):
        
        unioncup_infos = self.get_unioncup_table_toarrage()
        if unioncup_infos:
            for unioncup_info in unioncup_infos:
                self.handle_unioncup(unioncup_info)
        else:
            self.log("not dev cup need set arrage")
            
        unioncup_infos = self.get_run_unioncuptable()
        if unioncup_infos:
            for unioncup_info in unioncup_infos:
                self.handle_unioncup(unioncup_info)
        else:
            self.log("not dev cup need run")
          
        return "exist"
     
    @ensure_success
    def get_unioncup_table_toarrage(self):
        """获取要安排战术的杯赛"""
        return union_cup_manager.get_union_cup_for_arrange()
    
    @ensure_success
    def get_run_unioncuptable(self):
        """获取正在进行的比赛"""    
        return union_cup_manager.get_union_cup_for_run()
    
    def get_ladder_url(self, ladder_url):
        for domain in DOMAINS:
            ladder_url = ladder_url.replace(domain, "")
        return ladder_url
     
    def handle_unioncup(self, unioncup_info):
        unioncup_id = unioncup_info["UnionCupID"]
        logo = "12.gif"
        name = "无道联盟争霸赛"
        round = unioncup_info["Round"]
        self.log("now round is:%s" % round) 
        ladder_url = unioncup_info["LadderURL"]
        save_path = os.path.join(WEB_ROOT, self.get_ladder_url(ladder_url))
        #第一轮，安排赛程
        if round == 0:
            #初始化DeadRound
            union_cup_manager.init_dead_round(TEAM_COUNT)
            alive_reg_infos = self.get_alive_reg_table()
            #print alive_reg_infos
            #没人报名
            if not alive_reg_infos:
                self.log("没有人报名..")
                self.set_status_by_unioncupid(unioncup_id, 3)
                return
            
            #乱序
            random.shuffle(alive_reg_infos)
            
            self.log("unioncup id is:%s" % unioncup_id) 
            union_ids = []
            union_ids_map = {}
            for alive_reg_info in alive_reg_infos:
                union_id = alive_reg_info["UnionID"]
                self.log(union_id)
                union_ids.append(union_id)
                union_ids_map[union_id] = alive_reg_info
                
            _, result_map = cup_util.create_cupLadder(128, union_ids)
            keys = result_map.keys()
            self.log("start sort key")
            keys.sort()
            cupladder = UnionCupLadder(name, logo)
            home_union_id = 0
            for i, key in enumerate(keys):
                value = result_map[key]
                if value > 0:
                    """设置base code"""
                    self.set_code_by_devregid(union_ids_map[value]["UnionCupRegID"], key)
                if i % 2 == 0:
                    home_union_id = value
                    continue
                else:
                    #print union_ids_map[home_union_id]
                    #home_union_id = union_ids_map[home_union_id]["UnionID"]
                    home_union_name = union_ids_map[home_union_id]["UnionName"].strip()
                    if value > 0: 
                        away_union_id = value
                        away_union_name = union_ids_map[value]["UnionName"].strip()
                        round_pair = CupLadderRoundPair(home_union_id, home_union_name, away_union_id, away_union_name)
                        
                        home_union_clubs = union_cup_manager.get_union_club(home_union_id)
                        away_union_clubs = union_cup_manager.get_union_club(away_union_id)
                        
                        #下轮对轮表
                        for i in range(TEAM_COUNT):
                            home_club_id = home_union_clubs[i]['ClubID']
                            away_club_id = away_union_clubs[i]['ClubID']
                            user_id_home, club_name_home = self.get_user_id_club_name_by_club_id(home_club_id)
                            user_id_away, club_name_away = self.get_user_id_club_name_by_club_id(away_club_id)
                            match = CupLadderRoundMatch(user_id_home, club_name_home, user_id_away, club_name_away)
                            round_pair.add_match(match)
                        
                    else:#轮空
                        round_pair = CupLadderRoundPair(home_union_id, home_union_name)
                            
                    cupladder.add_round_pair(0, round_pair)
                    
            cupladder.write(save_path)
            self.set_round_by_unioncupid(unioncup_id, round + 1)
            self.set_status_by_unioncupid(unioncup_id, 1)
        else:
            self.log("start to match update")
            #先打比赛
            
            #取出存活的球队
            alive_reg_infos = self.get_reg_by_unioncupid_end_round(round)
            if len(alive_reg_infos) > 1:
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
                    
                    home_union_id = home_alive_reg_info["UnionID"]
                    away_union_id = away_alive_reg_info["UnionID"]
                    self.log("start to execute union match:home:%s, away:%s" % (home_union_id, away_union_id))
                    self.execute_match(unioncup_id, home_union_id, away_union_id, gain_code, round)
            
            is_last_round, champion_union_id, champion_union_name = self.write_html(unioncup_id, name, logo, save_path, round)            
                
            self.set_round_by_unioncupid(unioncup_id, round + 1)
            if is_last_round: 
                self.finish_unioncup(unioncup_info, champion_union_id, champion_union_name)
                
    def write_html(self, unioncup_id, name, logo, save_path, round):
        """write html"""
        cupladder = UnionCupLadder(name, logo)
        champion_union_id, champion_union_name = None, None
        #将每一轮的赛况输出到html
        is_last_round = False
        for each_round in range(round + 1):
            self.log("start to handle round:%s" % each_round)
            alive_reg_infos = self.get_reg_by_unioncupid_end_round(each_round)
            if len(alive_reg_infos) == 1:
                is_last_round = True
                champion_union_id = alive_reg_infos[0]["UnionID"]
                champion_union_name = alive_reg_infos[0]["UnionName"]
                
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
                    print "error"
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
                home_union_id = home["UnionID"]
                home_union_name = home["UnionName"]    
                if home and away:
                    away_union_id = away["UnionID"]
                    away_union_name = away["UnionName"]
                    unioncup_matchs = self.get_unioncup_match_by_gaincode_unioncupid(unioncup_id, gain_code)
                    matchs = []
                    home_win, away_win = 0, 0
                    #每次对阵是三盘两胜制
                    
                    if unioncup_matchs:
                        for unioncup_match in unioncup_matchs:
                            home_club_id = unioncup_match["ClubAID"]
                            away_club_id =  unioncup_match["ClubBID"] 
                            score_home = unioncup_match["ScoreA"]
                            score_away = unioncup_match["ScoreB"]
                            if score_home > score_away:
                                home_win += 1
                            else:
                                away_win += 1
                            user_id_home, club_name_home = self.get_user_id_club_name_by_club_id(home_club_id)
                            user_id_away, club_name_away = self.get_user_id_club_name_by_club_id(away_club_id)
                            match = CupLadderRoundMatch(user_id_home, club_name_home, user_id_away, club_name_away, unioncup_match) 
                            matchs.append(match)
                    else:
                        home_union_clubs = union_cup_manager.get_union_club(home_union_id)
                        away_union_clubs = union_cup_manager.get_union_club(away_union_id)
                        #下轮对轮表
                        for i in range(TEAM_COUNT):
                            home_club_id = home_union_clubs[i]['ClubID']
                            away_club_id = away_union_clubs[i]['ClubID']
                            user_id_home, club_name_home = self.get_user_id_club_name_by_club_id(home_club_id)
                            user_id_away, club_name_away = self.get_user_id_club_name_by_club_id(away_club_id)
                            match = CupLadderRoundMatch(user_id_home, club_name_home, user_id_away, club_name_away)
                            matchs.append(match)
                     
                    round_pair = CupLadderRoundPair(home_union_id, home_union_name, away_union_id, away_union_name, home_win, away_win, is_last_round)
                    for match in matchs:
                        round_pair.add_match(match)
                else:
                    round_pair = CupLadderRoundPair(home_union_id, home_union_name, is_last_round=is_last_round)
                    
                cupladder.add_round_pair(each_round, round_pair)
                
        cupladder.write(save_path)
        return is_last_round, champion_union_id, champion_union_name 
    
    def get_user_id_club_name_by_club_id(self, club_id):
        club_info = club_manager.get_club_by_id(club_id)
        return club_info["UserID"], club_info["Name"]
            
    def finish_unioncup(self, unioncup_info, union_id, union_name):
        """杯赛完成"""
        unioncupid = unioncup_info["UnionCupID"]
        union_cup_manager.set_cup_champion(unioncupid, union_id, union_name)
        union_cup_manager.set_status_by_cupid(unioncupid, 3)
        #union_cup_manager.reward_unioncup_by_clubid(unioncupid)
        
    def execute_match(self, union_cup_id, home_union_id, away_union_id, gain_code, round):
        """一轮对阵"""
        home_union_teams = union_cup_manager.get_union_club(home_union_id)
        away_union_teams = union_cup_manager.get_union_club(away_union_id)
        for i in range(TEAM_COUNT):
            home_union_team = home_union_teams[i]
            away_union_team = away_union_teams[i]
            home_club_id = home_union_team["ClubID"]
            away_club_id = away_union_team["ClubID"]
            index = i + 1
            
            cmd = "%s %s %s %s %s %s %s %s" % (CLIENT_EXE_PATH, 'unioncup_match_handler', union_cup_id, gain_code, home_club_id, away_club_id, round, index)
            self.call_cmd(cmd)
            
        unioncup_matchs = self.get_unioncup_match_by_gaincode_unioncupid(union_cup_id, gain_code)
        home_win, away_win = 0, 0
        for unioncup_match in unioncup_matchs:
            score_home = unioncup_match["ScoreA"]
            score_away = unioncup_match["ScoreB"]
            if score_home > score_away:
                home_win += 1
            else:
                away_win += 1
                
        if home_win > away_win:
            #设置被淘汰轮次,客队被淘汰
            self.log("客队被淘汰.UnionID[%s]" % away_union_id)
            self.set_dead_roun(away_union_id, round)
        else:
            #设置被淘汰轮次,主队被淘汰
            self.log("主队被淘汰.UnionID[%s]" % home_union_id)
            self.set_dead_roun(home_union_id, round)
            
    @ensure_success
    def set_dead_roun(self, union_id, dead_round):
        return union_cup_manager.set_dead_roun(union_id, dead_round)       
                           
    @ensure_success
    def get_unioncup_match_by_gaincode_unioncupid(self, unioncupid, gain_code):
        """根据GainCode和杯赛ID获取比赛"""
        return union_cup_manager.get_unioncup_match_by_gaincode_unioncupid(unioncupid, gain_code)

    @ensure_success       
    def set_status_by_unioncupid(self, unioncupid, status):
        """设置status"""
        return union_cup_manager.set_status_by_cupid(unioncupid, status)
    
    @ensure_success       
    def get_reg_by_unioncupid_end_round(self, round):
        """获取存活球队"""
        return union_cup_manager.get_alive_reg_table(round+1)
            
    @ensure_success
    def set_round_by_unioncupid(self, unioncupid, round):
        """设置round"""
        return union_cup_manager.set_round_by_cup_id(unioncupid, round)
    
    @ensure_success        
    def set_code_by_devregid(self, regid, code):
        """设置reg code"""
        return union_cup_manager.set_code_by_devregid(regid, code)
                
    def get_alive_reg_table(self):
        return union_cup_manager.get_alive_reg_table(100)    
    

if __name__ == "__main__":
    handler = UnionCupHandler()
    handler.start()
    #ladder_url = 'http://www.113388.net/unioncupLadder/201108/29.htm'
    #save_path = os.path.join(WEB_ROOT, ladder_url.replace(DOMAIN, ""))
    #handler.write_html(29, '哈利与火焰杯',  '16.gif', save_path, 2)
