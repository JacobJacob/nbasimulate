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
        cursor.execute("Truncate Table btp_XGroupMatch")
        cursor.execute("GetGroupTeamByCategory 1")
        infos = cursor.fetchall()
        #乱序
        random.shuffle(infos)
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
        #set_status_by_xcupid(xcup_id, 1, cursor)
        #set_round_by_xcupid(xcup_id, 1, cursor)
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

def add_unioncup(season):
    """初始化冠军杯"""
    capacity = 64
    reward_xml = '<Reward><Round>1</Round><Money>200000</Money><Reputation>10</Reputation></Reward>' \
                  '<Reward><Round>2</Round><Money>400000</Money><Reputation>20</Reputation></Reward><Reward>' \
                  '<Round>3</Round><Money>700000</Money><Reputation>30</Reputation></Reward><Reward><Round>4</Round>' \
                  '<Money>800000</Money><Reputation>50</Reputation></Reward><Reward><Round>5</Round><Money>900000</Money>' \
                  '<Reputation>70</Reputation></Reward><Reward><Round>6</Round><Money>1000000</Money><Reputation>90</Reputation>' \
                  '</Reward><Reward><Round>7</Round><Money>1250000</Money><Reputation>110</Reputation></Reward><Reward><Round>8</Round>' \
                  '<Money>2500000</Money><Reputation>150</Reputation></Reward><Reward><Round>9</Round><Money>5000000</Money>' \
                  '<Reputation>200</Reputation></Reward><Reward><Round>100</Round><Money>10000000</Money><Reputation>500</Reputation></Reward>'
    
    intro = u'杯赛采用联盟的方式参加，每个联盟派出三支队伍，实行五盘三胜制'
   
    matchtime = datetime.now().replace(hour=20, minute=0) + timedelta(hours=48)
    ladderurl = "UnionCupLadder/XResult%s.htm" % season
                   
    sql = "EXEC AddUnionCup %s, %s, '%s', '%s', '%s', '%s'" % (season, capacity, intro, reward_xml, matchtime.strftime("%Y-%m-%d %H:%M:%S"), ladderurl)
    print sql
    sql = ensure_gbk(sql)
    cursor = connection.cursor()
    try:
        cursor.execute("delete from btp_tool where toolid = 1")
        cursor.execute("update btp_union set unioncupcount = 5")
        cursor.execute("delete from btp_unioncupmatch")
        cursor.execute("delete from btp_unioncupreg")
        cursor.execute("delete from btp_unioncupclubreg")
        cursor.execute(sql)
    finally:
        cursor.close()

def get_union_cup_for_arrange():
    """获取要安排战术的杯赛"""
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT * FROM BTP_UnionCup WHERE Status=0 AND MatchTime<GetDate()")
        return cursor.fetchall()
    finally:
        cursor.close()
        

def get_union_cup_for_run():
    """获取正在进行中的杯赛"""
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT * FROM BTP_UnionCup WHERE Status=1 AND MatchTime<'%s' ORDER BY MatchTime ASC" % datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
        return cursor.fetchall()
    finally:
        cursor.close()
        
def set_cup_champion(cup_id, union_id, union_name):
    """设置自定义杯赛冠军"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCup SET ChampionUnionID = %s, ChampionUnionName = '%s' WHERE UnionCupID = %s " % (union_id, union_name.encode("gbk"), cup_id)
        return cursor.execute(sql)
    finally:
        connection.close()     
        
def set_round_by_cup_id(cup_id, round):
    """设置杯赛轮数"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCup SET Round = %s, MatchTime=dateadd(dd, 1, MatchTime) WHERE UnionCupID = %s " % (round, cup_id)
        cursor.execute(sql)
    finally:
        connection.close()
        
def set_status_by_cupid(cup_id, status):
    """设置杯赛状态"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCup SET Status = %s WHERE UnionCupID = %s " % (status, cup_id)
        print sql
        cursor.execute(sql)
    finally:
        connection.close()

def get_alive_reg_table(round):
    """设置杯赛状态"""
    cursor = connection.cursor()
    try:
        sql = "SELECT * FROM BTP_UnionCupReg WHERE deadround >= %s" % round
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        connection.close()
          
def set_code_by_devregid(regid, code):
    """设置报甸code"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCupReg SET BaseCode = '%s' WHERE UnionCupRegID = %s " % (code, regid)
        cursor.execute(sql)
    finally:
        connection.close()
        
def init_dead_round():
    """设置报名red round"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCupReg SET DeadRound = 100 WHERE ClubCount = 5 "
        cursor.execute(sql)
    finally:
        connection.close()
        
def set_dead_round_by_devregid(regid, dead_round):
    """设置报名red round"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCupReg SET DeadRound = %s WHERE UnionCupRegID = %s " % (dead_round, regid)
        cursor.execute(sql)
    finally:
        connection.close()
        
def get_union_club(union_id):
    """获取一个联盟里三支参赛的球队"""
    cursor = connection.cursor()
    try:
        sql = "SELECT * FROM BTP_UnionCupClubReg WHERE UnionID = %s ORDER BY ClubIndex ASC " % union_id
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        connection.close()
        
def get_unioncup_match_by_gaincode_unioncupid(union_cup_id, gain_code):
    """根据gain code和cup id获取比赛"""
    cursor = connection.cursor()
    try:
        sql = """SELECT * FROM BTP_UnionCupMatch WHERE UnionCupID = %s AND GainCode = '%s' ORDER BY "Index" ASC """ % (union_cup_id, gain_code)
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        connection.close()
        
def set_dead_roun(union_id, round):
    """设置被淘汰的轮次"""
    cursor = connection.cursor()
    try:
        sql = "UPDATE BTP_UnionCupReg SET DeadRound = %s  WHERE UnionID = %s " % (round, union_id)
        print sql
        return cursor.execute(sql)
    finally:
        connection.close()
        
if __name__ == "__main__":
    add_unioncup(11)
    