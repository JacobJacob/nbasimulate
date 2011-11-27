#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from xba.common.sqlserver import connection
from xba.common.stringutil import ensure_gbk

name_map = {
  1: {
    1: u'第一阵容', 
    2: u'第二阵容', 
    3: u'第三阵容', 
    4: u'第四阵容'
  },
  2:{
    1: u'第一阵容', 
    2: u'第二 阵容', 
    3: u'第三阵容', 
    4: u'第四阵容'
  }
}

def_map = {
  1: random.randint(1, 6),
  2: random.randint(1, 6),
  3: random.randint(1, 6),
  4: random.randint(1, 6),
}

off_map = {
  1: random.randint(1, 6),
  2: random.randint(1, 6),
  3: random.randint(1, 6),
  4: random.randint(1, 6),
}

def finish_star_player_vote():
    """完成全明星赛投票"""
    cursor = connection.cursor()
    try:
        cursor.execute("truncate table btp_stararrange5")
        arrange_map = {}
        for zone in (1, 2):
            for pos in (1, 2, 3, 4, 5):
                sql = "select top 3 * from btp_starplayer where zone=%s and pos=%s order by votes desc" % (zone, pos)
                cursor.execute(sql)
                infos = cursor.fetchall()
                for i, info in enumerate(infos):
                    zone_map = arrange_map.get(zone, {})    
                    pos_map = zone_map.get(pos, {})
                    pos_map[i+1] = info
                    zone_map[pos] = pos_map
                    arrange_map[zone] = zone_map
                    
        for zone, zone_map in arrange_map.iteritems():
            club_id = zone
            category = 1
            name = name_map.get(zone).get(1, u'阵容') 
            offense, defense = off_map.get(1), def_map.get(1) 
            cid, pfid, sfid, sgid, pgid = zone_map[1][1]["PlayerID"], zone_map[2][1]["PlayerID"], zone_map[3][1]["PlayerID"], zone_map[4][1]["PlayerID"], zone_map[5][1]["PlayerID"]
            cursor.execute("update btp_starplayer set status=2 where playerid in (%s, %s, %s, %s, %s)" % (cid, pfid, sfid, sgid, pgid))
            sql = u"insert into btp_stararrange5(name, clubid, category, cid, pfid, sfid, sgid, pgid, offense, defense) values('%s', %s, %s, %s, %s, %s, %s, %s, %s, %s)" % \
                   (name, club_id, category, cid, pfid, sfid, sgid, pgid, offense, defense)
            sql = ensure_gbk(sql)
            cursor.execute(sql)
            
            category = 2
            name = name_map.get(zone).get(2, u'阵容')
            offense, defense = off_map.get(2), def_map.get(2)
            sql = u"insert into btp_stararrange5(name, clubid, category, cid, pfid, sfid, sgid, pgid, offense, defense) values('%s', %s, %s, %s, %s, %s, %s, %s, %s, %s)" % \
                   (name, club_id, category, cid, pfid, sfid, sgid, pgid, offense, defense)
            sql = ensure_gbk(sql)
            cursor.execute(sql)
            
            category = 3
            name = name_map.get(zone).get(3, u'阵容')
            offense, defense = off_map.get(3), def_map.get(3)
            cid, pfid, sfid, sgid, pgid = zone_map[1][2]["PlayerID"], zone_map[2][2]["PlayerID"], zone_map[3][2]["PlayerID"], zone_map[4][2]["PlayerID"], zone_map[5][2]["PlayerID"]
            cursor.execute("update btp_starplayer set status=3 where playerid in (%s, %s, %s, %s, %s)" % (cid, pfid, sfid, sgid, pgid))
            sql = u"insert into btp_stararrange5(name, clubid, category, cid, pfid, sfid, sgid, pgid, offense, defense) values('%s', %s, %s, %s, %s, %s, %s, %s, %s, %s)" % \
                   (name, club_id, category, cid, pfid, sfid, sgid, pgid, offense, defense)
            sql = ensure_gbk(sql)
            cursor.execute(sql)
            
            category = 4
            name = name_map.get(zone).get(4, u'阵容')
            offense, defense = off_map.get(4), def_map.get(4)
            sql = u"insert into btp_stararrange5(name, clubid, category, cid, pfid, sfid, sgid, pgid, offense, defense) values('%s', %s, %s, %s, %s, %s, %s, %s, %s, %s)" % \
                   (name, club_id, category, cid, pfid, sfid, sgid, pgid, offense, defense)
            sql = ensure_gbk(sql)
            cursor.execute(sql)
            
            cid, pfid, sfid, sgid, pgid = zone_map[1][3]["PlayerID"], zone_map[2][3]["PlayerID"], zone_map[3][3]["PlayerID"], zone_map[4][3]["PlayerID"], zone_map[5][3]["PlayerID"]
            cursor.execute("update btp_starplayer set status=4 where playerid in (%s, %s, %s, %s, %s)" % (cid, pfid, sfid, sgid, pgid))
            
    finally:
        cursor.close()

def init_star_player():
    """全明星赛候选球员导出"""
    cursor = connection.cursor()
    try:
        cursor.execute("TRUNCATE TABLE BTP_StarPlayer")
        sql = "SELECT TOP 200 * FROM BTP_Player5 ORDER BY Ability DESC"    
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            player_id = info["PlayerID"]
            name = info["Name"].strip()
            club_id = info["ClubID"]
            pos = info["Pos"]

            get_club_sql = "SELECT * FROM BTP_Club WHERE ClubID=%s" % club_id
            cursor.execute(get_club_sql)
            club_info = cursor.fetchone()
            if not club_info:
                continue
            user_id = club_info["UserID"]
            club_name = club_info["Name"]
            get_user_sql = "SELECT * FROM BTP_Account WHERE UserID=%s" % user_id
            cursor.execute(get_user_sql)
            user_info = cursor.fetchone()
            dev_code = user_info["DevCode"]
            dev_code = dev_code.strip()
            
            #判断数据统计是否达到要求
            add = is_add(pos, player_id, dev_code, cursor)
            if add:
                #zone = 1 if dev_code[-1] == "0" else 2
                zone = 1 if user_id % 2 == 0 else 2
                insert_sql = u"INSERT INTO BTP_StarPlayer(PlayerID, Name, ClubID, Pos, Status, Votes, ClubName, UserID, DevCode, Zone)" \
                             "  VALUES('%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s', '%s')" % \
                             (player_id, name, club_id, pos, 1, 0, club_name, user_id, dev_code, zone)
                insert_sql = ensure_gbk(insert_sql)
                #print insert_sql
                cursor.execute(insert_sql)

    finally:
        cursor.close()
        
        
def is_add(pos, player_id, dev_code,cursor):
    """判断技术统计是否达到要求"""
    add = False
    if pos in (1, 2):
        #print "EXEC GetReboundTop12Table '%s'" % dev_code
        cursor.execute("EXEC GetReboundTop12Table '%s'" % dev_code)
        totals = cursor.fetchall()
        for total in totals:
            if player_id == total["PlayerID"]:
                add = True
                break
        if not add:
            cursor.execute("EXEC GetBlockTop12Table '%s'" % dev_code)
            totals = cursor.fetchall()
            for total in totals:
                if player_id == total["PlayerID"]:
                    add = True
                    break
    elif pos in (3, 4):
        cursor.execute("EXEC GetTop12Table '%s'" % dev_code)
        totals = cursor.fetchall()
        for total in totals:
            if player_id == total["PlayerID"]:
                add = True
                break
        if not add:
            cursor.execute("EXEC GetAssistTop12Table '%s'" % dev_code)
            totals = cursor.fetchall()
            for total in totals:
                if player_id == total["PlayerID"]:
                    add = True
                    break
    elif pos == 5:
        cursor.execute("EXEC GetAssistTop12Table '%s'" % dev_code)
        totals = cursor.fetchall()
        for total in totals:
            if player_id == total["PlayerID"]:
                add = True
                break
        if not add:
            cursor.execute("EXEC GetStealTop12Table '%s'" % dev_code)
            totals = cursor.fetchall()
            for total in totals:
                if player_id == total["PlayerID"]:
                    add = True
                    break
        if not add:
            cursor.execute("EXEC GetTop12Table '%s'" % dev_code)
            totals = cursor.fetchall()
            for total in totals:
                if player_id == total["PlayerID"]:
                    add = True
                    break
                
    return add

def add_match(cluba_name, clubb_name, season):
    """添加比赛"""
    cursor = connection.cursor()
    try:
        sql = u"INSERT INTO BTP_StarMatch(ClubNameA, ClubNameB, Season) VALUES('%s', '%s', %s)" % (cluba_name, clubb_name, season)
        sql = ensure_gbk(sql)
        return cursor.execute(sql)
    finally:
        cursor.close()
          
def init_star_match(season):
    """初始化全明星赛数据"""
    cluba_name = u"东部明星队"
    clubb_name = u"西部明星队"
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        sql = u"INSERT INTO BTP_StarMatch(ClubNameA, ClubNameB, Season) VALUES('%s', '%s', %s)" % (cluba_name, clubb_name, season)
        sql = ensure_gbk(sql)
        cursor.execute(sql)
        for table in ["btp_starplayer", "btp_starplayer", "btp_starvoterecord"]:
            cursor.execute("Truncate Table %s" % table)
        cursor.commit()
    except:
        cursor.rollback()
        raise
    finally:
        cursor.close()
   
def finish_star_match(season):
    """添加比赛"""
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        
        sql = "select top 1 * from btp_starmatch where season=%s and status=1" % season
        cursor.execute(sql)
        info = cursor.fetchone()
        if not info:
            print "match not finish or not match"
            return
        
        mvp_player_id = info["MVPPlayerID"]
        
        cursor.execute("update btp_player5 set StarMvpCount = StarMvpCount + 1 where playerid = %s" % mvp_player_id)
        
        cursor.execute("select name, clubid from btp_player5 where playerid = %s" % mvp_player_id)
        player_info = cursor.fetchone()
        
        club_id = player_info["clubid"]
        mvp_player_name = player_info["name"].strip()
        
        cursor.execute("select userid from btp_club where clubid = %s" % club_id)
        user_info = cursor.fetchone()
        
        user_id = user_info["userid"]
        
        honor_sql = u"INSERT INTO BTP_Honor (UserID,SmallLogo,BigLogo,Remark)VALUES(%s, 'Union1.gif','Union1.gif', '全明星赛MVP')" % user_id
        honor_sql = ensure_gbk(honor_sql)
        cursor.execute(honor_sql)
        
        remark = u"恭喜你，您的球员%s在本赛季的全明星赛中获得MVP" % mvp_player_name
        
        message_sql = u"Exec AddNewMessage %s, 2,0,'秘书报告', '%s'" % (user_id, remark)
        message_sql = ensure_gbk(message_sql)
        cursor.execute(message_sql)
        
        cursor.execute("update btp_starmatch set status=2 where season=%s" % season)
        
        cursor.commit()
    
    except:
        cursor.rollback()
        raise
    finally:
        cursor.close()
        
if __name__ == "__main__":
    #finish_star_player_vote()
    #add_match('东部明星队', '西部明星队', 5)
    finish_star_match(5)
