#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from xba.common.sqlserver import connection
from xba.common.stringutil import ensure_gbk
        
def add_xguess(user_id, club_id, club_name, odds, cursor=None):
    """添加竞猜球队"""
    need_close = False
    if not cursor:
        cursor = connection.cursor()
        need_close = True
    try:
        sql = u"INSERT INTO BTP_XGuess(UserID, ClubID, ClubName, Odds, Status) VALUES(%s, %s, '%s', %0.2f, 0)" % (user_id, club_id, club_name, odds)
        sql = ensure_gbk(sql)
        cursor.execute(sql)
    finally:
        if need_close:
            cursor.close()
        
def add_xgame_team_to_xguess():
    """添加竞猜球队"""
    cursor = connection.cursor()
    try:
        cursor.execute("truncate table BTP_XGuess")
        sql = "EXEC SetTeamAbility"
        cursor.execute(sql)
        sql = "select * from btp_xcupreg"
        cursor.execute(sql)
        reg_infos = cursor.fetchall()
        reg_info_map = {}
        reg_user_ids = []
        for reg_info in reg_infos:
            user_id = reg_info["UserID"]
            reg_user_ids.append(user_id)
            reg_info_map[user_id] = reg_info
            
        sql = "select UserID, TeamAbility from BTP_Account where UserID in (%s) order by TeamAbility Desc" % ",".join(["%s" % id for id in reg_user_ids])
        cursor.execute(sql)
        user_infos = cursor.fetchall()
        base_odds = 3.1
        last_ability = 0
        
        for i, user_info in enumerate(user_infos):
            user_id = user_info["UserID"]
            team_ability = user_info["TeamAbility"]
            reg_info = reg_info_map[user_id]
            club_id = reg_info["ClubID"]
            club_name = reg_info["ClubName"]
            if last_ability == 0:
                seed = 5
            else:
                #因子和两队综合相关有关
                seed = (last_ability - team_ability) / 30
                
            last_ability = team_ability
            odds = base_odds + random.random() * seed
            base_odds = odds
            add_xguess(user_id, club_id, club_name, odds + i * 0.1, cursor)
    finally:
        cursor.close()        
        
        
if __name__ == "__main__":
    #add_xgame_team_to_xguess()
    pass