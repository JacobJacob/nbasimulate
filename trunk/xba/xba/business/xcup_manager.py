#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import random
from xba.config import WEB_ROOT, DOMAIN

from datetime import datetime, timedelta
from xba.common.reward import Reward
from xba.common.cupladder import CupLadder, CupLadderRoundMatch
from xba.common.stringutil import ensure_gbk

match_maps = [
[[1, 6], [2, 5], [3, 4]],
[[1, 5], [6, 4], [2, 3]],
[[1, 4], [5, 3], [6, 2]],
[[1, 3], [4, 2], [5, 6]],
[[1, 2], [3, 6], [4, 5]],
[[4, 3], [5, 2], [6, 1]],
[[5, 1], [3, 2], [6, 4]],
[[4, 1], [3, 5], [2, 6]],
[[3, 1], [4, 2], [6, 5]],
[[2, 1], [6, 3], [5, 4]],
]

from xba.common.sqlserver import connection
from xba.common import cup_util

def arrange_group(xcup_id):
    """小组赛安排"""
    cursor = connection.cursor()
    club_info_map = {}
    try:
        cursor.start_transaction()
        cursor.execute("GetGroupTeamByCategory 1")
        infos = cursor.fetchall()
        for i, info in enumerate(infos):
            xgroup_team_id = info["XGroupTeamID"]
            club_id = info["ClubID"]
            group_index = (i / 6) + 1
            team_index = (i % 6) + 1
            club_infos = club_info_map.get(group_index, [])
            club_infos.append({"team_index": team_index, "club_id": club_id})
            club_info_map[group_index] = club_infos
            sql = "EXEC SetGroup %s, %s, %s" % (xgroup_team_id, group_index, team_index)
            cursor.execute(sql)
        start_time = datetime.strptime(datetime.now().strftime("%Y-%m-%d 16:00:00"), "%Y-%m-%d %H:%M:%S")
        for group_index, club_infos in club_info_map.iteritems():
            for i, match_map in enumerate(match_maps):
                round = i + 1
                for map in match_map:
                    index_a, index_b = map[0], map[1]
                    if index_a > len(club_infos) or index_b > len(club_infos):
                        round += 1
                        continue
                    club_a_info = club_infos[index_a - 1]
                    club_b_info = club_infos[index_b - 1]
                    match_time = (start_time + timedelta(days=round)).strftime("%Y-%m-%d %H:%M:%S")
                    add_group_match(cursor, 1, group_index, round, club_a_info['team_index'], club_b_info['team_index'], \
                                    club_a_info["club_id"], club_b_info["club_id"], match_time)
        set_status_by_xcupid(xcup_id, 1, cursor)
        set_round_by_xcupid(xcup_id, round, cursor)
        cursor.commit()
    except:
        cursor.rollback()
        raise
    finally:
        cursor.close()
        
def add_group_match(cursor, category, group_index, round, teama_index, teamb_index, cluba_id, clubb_id, match_time):
    """添加比赛"""
    sql = "EXEC XGroupAddMatch %s, %s, %s, %s, %s, %s, %s, '%s'" % \
               (category, group_index, round, teama_index, teamb_index, cluba_id, clubb_id, match_time)
    return cursor.execute(sql)

def init_xgame():
    """初始化冠军杯"""
    category = 5
    capacity = 512
    reward_xml = '<Reward><Round>1</Round><Money>200000</Money><Reputation>10</Reputation></Reward>' \
                  '<Reward><Round>2</Round><Money>400000</Money><Reputation>20</Reputation></Reward><Reward>' \
                  '<Round>3</Round><Money>700000</Money><Reputation>30</Reputation></Reward><Reward><Round>4</Round>' \
                  '<Money>800000</Money><Reputation>50</Reputation></Reward><Reward><Round>5</Round><Money>900000</Money>' \
                  '<Reputation>70</Reputation></Reward><Reward><Round>6</Round><Money>1000000</Money><Reputation>90</Reputation>' \
                  '</Reward><Reward><Round>7</Round><Money>1250000</Money><Reputation>110</Reputation></Reward><Reward><Round>8</Round>' \
                  '<Money>2500000</Money><Reputation>150</Reputation></Reward><Reward><Round>9</Round><Money>5000000</Money>' \
                  '<Reputation>200</Reputation></Reward><Reward><Round>100</Round><Money>10000000</Money><Reputation>500</Reputation></Reward>'
    
    intro = u'第一轮：奖金200000，联盟威望10。<br>第二轮：奖金400000，联盟威望20。<br>第三轮：奖金700000，联盟威望30。<br>第四轮：奖金800000，联盟威望50。' \
             '<br>第五轮：奖金900000，联盟威望70。<br>第六轮：奖金1000000，联盟威望90。<br>第七轮：奖金1250000，联盟威望110。<br>第八轮：奖金2500000，联盟威望150。' \
             '<br>第九轮：奖金5000000，联盟威望200。<br>总冠军：奖金10000000，联盟威望500。<br>'
                  
    sql = "EXEC AddGame %s, %s, '%s', '%s'" % (category, capacity, intro, reward_xml)
    sql = ensure_gbk(sql)
    cursor = connection.cursor()
    try:
        cursor.execute(sql)
        cursor.execute("UPDATE BTP_XGAME SET LadderURL='XCupLadder/XResult.htm' WHERE Category=5 AND Status=0")
        cursor.execute("UPDATE BTP_XGAME SET Round=0, Status=0 WHERE XGameID=1")
        cursor.execute("Truncate Table btp_XGroupMatch")
        cursor.execute("Truncate Table btp_XGroupTeam")
        cursor.execute("Truncate Table btp_XCupReg")
        cursor.execute("Truncate Table btp_XCupMatch")
    finally:
        cursor.close()

def get_xgroup_game_by_status(status):
    """根据状态获取xcup,小组赛"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC GetXGroupGameByStatus %s" % status)
        return cursor.fetchone()
    finally:
        cursor.close()
        
def get_xgroup_big_game_by_status(status):
    """根据状态获取xcup,决赛赛"""
    cursor = connection.cursor()
    try:
        cursor.execute("select *, convert(text,RewardXML) as RewardXML from btp_xgame where status = %s and category = 5 " % status)
        return cursor.fetchone()
    finally:
        cursor.close()
        
def set_status_by_xcupid(cup_id, status, cursor=None):
    """设置冠军杯状态"""
    need_close = False;
    if not cursor:
        cursor = connection.cursor()
        need_close = True
    try:
        cursor.execute("EXEC SetStatusByXCupID %s, %s" % (cup_id, status))
    finally:
        if need_close:
            cursor.close()
            
def get_group_match_to_play():
    """获取要打的比赛"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC GetGroupMatchToPlay")
        return cursor.fetchall()
    finally:
        cursor.close()
        
def set_code_by_xregid(reg_id, code):
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC SetCodeByXRegID %s, '%s'" % (reg_id, code))
    finally:
        cursor.close()
        
def get_xcup_match_by_gaincode_xcupid(xcupid, gain_code):
    """根据GainCode和杯赛ID获取比赛"""
    cursor = connection.cursor()
    try:
        sql = "SELECT * FROM BTP_XCupMatch WHERE GainCode = '%s' AND XGameID = %s" % (gain_code, xcupid)
        cursor.execute(sql)
        return cursor.fetchone()
    finally:
        cursor.close() 
        
def set_round_by_xcupid(xcup_id, round, cursor=None):
    """设置轮数"""
    need_close = False
    if not cursor:
        need_close = True
        cursor = connection.cursor()
    try:
        cursor.execute("EXEC SetRoundByXCupID %s, %s" % (xcup_id, round))
    finally:
        if need_close:
            cursor.close()
    
def set_xba_champion(xcup_id, champion_id, champion_name):
    """设置冠军球队"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC SetXBAChampion %s, %s, %s" % (xcup_id, champion_id, champion_name.encode("gbk")))
    finally:
        cursor.close()
    
        
def get_xreg_table_by_cup_id_dead_round(cup_id, dead_round):
    """获取冠军杯存活球员"""
    cursor = connection.cursor()
    try:
        sql = "EXEC GetXRegTableByCupIDEndRound %s, %s" % (cup_id, dead_round)
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        cursor.close()
        
def arrange_xcup(xcup_id, fource=False):
    """安排决赛赛程"""
    cursor = connection.cursor()
    try:
        print "start xcup arrange"
        cursor.execute("select * from btp_xcupreg")
        if cursor.fetchall():
            if fource:
                print "start to delete btp_xcupreg"
                cursor.execute("delete from btp_xcupreg")
            else:
                return
        cursor.execute("EXEC GetGroupSize 1")
        club_count = 0
        added_club_ids = set()
        group_count = cursor.fetchone()[0]
        for group_index in range(1, group_count + 1):
            sql = "EXEC GetGroupTeamByCGIOrderByResult 1, %s" % group_index
            cursor.execute(sql)
            team_infos = cursor.fetchall()
            if team_infos:
                for i, team_info in enumerate(team_infos):
                    if i > 1:
                        break
                    club_id = team_info["ClubID"]
                    added_club_ids.add(club_id)
                    sql = "EXEC AddXCupReg 5, %s" % club_id
                    cursor.execute(sql)
                    club_count += 1
        #补全球队
        add_club_count = 0
        if club_count < 4:
            add_club_count = 4 - club_count
        elif club_count < 8:
            add_club_count = 8 - club_count
        elif club_count < 16:
            add_club_count = 16 - club_count
        elif club_count < 32:
            add_club_count = 32 - club_count
        elif club_count < 64:
            add_club_count = 64 - club_count
        elif club_count < 128:
            add_club_count = 128 - club_count
        elif club_count < 256:
            add_club_count = 256 - club_count
        
        #如果球队不足2, 4, 8...的话，安成绩补足
        cursor.execute("select * from btp_xgroupteam order by win desc, score desc")
        all_club_infos = cursor.fetchall()
        for club_info in all_club_infos:
            club_id = club_info["ClubID"]
            if club_id in added_club_ids:
                continue
            
            added_club_ids.add(club_id)
            sql = "EXEC AddXCupReg 5, %s" % club_id
            cursor.execute(sql)
            add_club_count -= 1
            if add_club_count == 0:
                break
            
        #安排gain_code
        club_ids = []
        club_ids_map = {}
        sql = "EXEC GetXRegTableByCupIDEndRound %s, %s" % (xcup_id, 0)
        print sql
        cursor.execute(sql)
        alive_reg_infos = cursor.fetchall()
        random.shuffle(alive_reg_infos)
        for alive_reg_info in alive_reg_infos:
            club_id = alive_reg_info["ClubID"]
            print club_id
            club_ids.append(club_id)
            club_ids_map[club_id] = alive_reg_info
        
        _, result_map = cup_util.create_cupLadder(512, club_ids)
        keys = result_map.keys()
        keys.sort()
        #写文件
        cursor.execute("select * from btp_xgame where xgameid = %s" % xcup_id)
        cup_info = cursor.fetchone()
        ladder_url = cup_info["LadderURL"]
        save_path = os.path.join(WEB_ROOT, ladder_url.replace(DOMAIN, ""))
        logo = "XBACup/ChampionCup.GIF"
        name = "冠军杯"
        cupladder = CupLadder(name, logo, css_path="../Css/Base.css")
        home_club_id = 0
        for i, key in enumerate(keys):
            value = result_map[key]
            if value > 0:
                """设置base code"""
                cursor.execute("EXEC SetCodeByXRegID %s, '%s'" % (club_ids_map[value]["XCupRegID"], key))
                
            if i % 2 == 0:
                home_club_id = value
            else:
                home_user_id = club_ids_map[home_club_id]["UserID"]
                home_club_name = club_ids_map[home_club_id]["ClubName"].strip()
                if value > 0:
                    away_user_id = club_ids_map[value]["UserID"]
                    away_club_name = club_ids_map[value]["ClubName"].strip()
                    match = CupLadderRoundMatch(6, home_user_id, home_club_name, away_user_id, away_club_name)
                else:
                    match = CupLadderRoundMatch(6, home_user_id, home_club_name)
                        
                cupladder.add_match(0, match)
                
        cupladder.write(save_path)
        set_status_by_xcupid(xcup_id, 1, cursor)
        set_round_by_xcupid(xcup_id, 1)
        #提交事务
        cursor.commit()
    except:
        cursor.rollback()
        raise
    finally:
        cursor.close()
        
def finish_xcup(xcup_info, user_id, club_id, club_name):
    """杯赛完成"""
    xcup_id = xcup_info["XGameID"]
    round = xcup_info["Round"]
    reward_xml = xcup_info["RewardXML"]
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        cursor.execute("EXEC SetXBAChampion %s, %s, '%s' " % (xcup_id, user_id, club_name.encode("gbk")))
        cursor.execute("EXEC SetXBAStatus %s, %s" % (xcup_id, 3))
        #杯赛奖励
        reward = Reward(reward_xml)
        rounds = [i for i in range(round + 1)]
        rounds.append(100)
        for round in rounds:
            sql = "EXEC GetXRegTableByCupIDDeadRound %s, %s" % (xcup_id, round)
            cursor.execute(sql)
            alive_clubs = cursor.fetchall()
            if not alive_clubs:
                continue
        
            reward_info = reward.get_reward(round)
            print "reward info for round:%s[%s]" % (round, reward_info)
            for alive_club in alive_clubs:
                club_id = alive_club["ClubID"]
                user_id = alive_club["UserID"]
                money = reward_info.get("money")
                if money:
                    sql = "EXEC RewardXCupByClubID %s, %s, %s, %s" % (club_id, xcup_id, round, money.money)
                    cursor.execute(sql)
                
                reputation = reward_info.get("reputation")
                if reputation:
                    cursor.execute("select UnionID, NickName from btp_account where UserID = %s" % user_id)
                    info = cursor.fetchone()
                    if round == 100:
                        note = u"冠军杯冠军"
                    else:
                        note = u"冠军杯第%s轮" % round
                    if info and info["UnionID"]:
                        sql = "EXEC AddUnionReputation %s, '%s', %s, %s, '%s'" % \
                                        (user_id, info["NickName"].encode("gbk"), info["UnionID"], reputation.reputation, note.encode("gbk"))
                        print sql
                        cursor.execute(sql)
                      
        cursor.commit()
    except:
        print "rollback"
        cursor.rollback()
        raise
    finally:
        cursor.close()
        
def reward_xcup(xcup_id, round):
    """杯赛完成"""
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        cursor.execute("select *, convert(text,RewardXML) as RewardXML from btp_xgame where XGameID = %s " % xcup_id)
        xcup_info = cursor.fetchone()
        #杯赛奖励
        round = xcup_info["Round"]
        reward_xml = xcup_info["RewardXML"]
        reward = Reward(reward_xml)

        sql = "EXEC GetXRegTableByCupIDDeadRound %s, %s" % (xcup_id, round)
        cursor.execute(sql)
        alive_clubs = cursor.fetchall()
        if not alive_clubs:
            return
    
        reward_info = reward.get_reward(round)
        for alive_club in alive_clubs:
            club_id = alive_club["ClubID"]
            user_id = alive_club["UserID"]
            money = reward_info.get("money")
            if money:
                sql = "EXEC RewardXCupByClubID %s, %s, %s, %s" % (club_id, xcup_id, round, money.money)
                cursor.execute(sql)
            
            reputation = reward_info.get("reputation")
            if reputation:
                cursor.execute("select UnionID, NickName from btp_account where UserID = %s" % user_id)
                info = cursor.fetchone()
                if round == 100:
                    note = u"冠军杯冠军"
                else:
                    note = u"冠军杯第%s轮" % round
                if info and info["UnionID"]:
                    sql = "EXEC AddUnionReputation %s, '%s', %s, %s, '%s'" % \
                                    (user_id, info["NickName"].encode("gbk"), info["UnionID"], reputation.reputation, note.encode("gbk"))
                    print sql
                    cursor.execute(sql)
                      
        cursor.commit()
    except:
        print "rollback"
        cursor.rollback()
        raise
    finally:
        cursor.close()
            
if __name__ == "__main__":
    init_xgame()
    
