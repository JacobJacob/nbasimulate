#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection
from xba.business import game_manager
from xba.common.constants.club import MainXMLTag
from xba.common import main_xml_util
from xba.model import Club
from xba.common.orm import Session

def ensure_gbk(s):
    if isinstance(s, unicode):
        return s.encode("gbk")
    return 

def set_club_main_xml(club_id, map):
    """设置俱乐部的MainXML"""
    club_info = get_club_by_id(club_id)
    cursor = connection.cursor()
    try:
        main_xml = club_info["MainXML"]
        new_main_xml = main_xml_util.build_main_xml(main_xml, map)
        sql = "exec SetMainXMLByClubID %s, '%s'" % (club_id, new_main_xml)
        cursor.execute(ensure_gbk(sql))
    finally:
        connection.close()
        
def get_club_by_user_id(user_id):
    """根据用户ID得到职业队ID"""
    cursor = connection.cursor()
    try:
        sql = "exec GetClubIDByUserID %s" % user_id
        cursor.execute(sql)
        info = cursor.fetchone()
        if info:
            return info["ClubID"]
    finally:
        connection.close()
        
def get_club_by_id(club_id):
    """根据ID查询俱乐部"""
    cursor = connection.cursor()
    try:
        sql = "select *,  convert(text,MainXML) as MainXML from btp_club where clubid = %s " % club_id
        cursor.execute(sql)
        return cursor.fetchone()
    finally:
        cursor.close()
        
def get_all_club(category):
    """得到所有俱乐部"""
    cursor = connection.cursor()
    try:
        sql = "select * from btp_club where category = %s" % category
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        connection.close()
    
def get_dev_match_by_club_id(club_id, round):
    """得到俱乐部当轮比赛行"""
    cursor = connection.cursor()
    try:
        sql = "exec GetDevMRowByClubIDRound %s, %s" % (club_id, round)
        cursor.execute(sql)
        return cursor.fetchone()
    except Exception, e:
        print e.message.decode("gbk")
        raise
    finally:
        connection.close()
    
        
def set_club_main_xmls():
    """批量设置club main xml"""
    game_row = game_manager.get_game_info()
    turn = game_row["Turn"]
    club_infos = get_all_club(5)
    
    for club_info in club_infos:
        club_id = club_info["ClubID"]
        dev_match_info = get_dev_match_by_club_id(club_id, turn)
        if not dev_match_info:
            continue
        
        club_home_id = dev_match_info["ClubHID"]
        club_away_id = dev_match_info["ClubAID"]
        
        club_home_info = get_club_by_id(club_home_id)
        club_home_logo = club_home_info["Logo"]
        club_home_name = club_home_info["Name"]     
        
        club_away_info = get_club_by_id(club_away_id)
        club_away_logo = club_away_info["Logo"]
        club_away_name = club_away_info["Name"]
        
        xml_info = {}
        xml_info[MainXMLTag.NEXT_CLUB_NAME_HOME] = club_home_name
        xml_info[MainXMLTag.NEXT_CLUB_NAME_AWAY] = club_away_name
        xml_info[MainXMLTag.NEXT_CLUB_LOGO_HOME] = club_home_logo
        xml_info[MainXMLTag.NEXT_CLUB_LOGO_AWAT] = club_away_logo
        
        set_club_main_xml(club_id, xml_info)
        
def get_club_list(page, pagesize, category):
    """获取职乐部"""
    session = Session()
    total = session.query(Club).filter(Club.category==category).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Club).filter(Club.category==category).order_by(Club.clubid).offset(index).limit(pagesize).all()
    return total, infos

def fill_player():
    """球员不足8个的补球员"""
    cursor = connection.cursor()
    try:
        
        while True:        
            cursor.execute("select top 1 * from btp_arrange5 where cid=0 or pfid = 0 or sfid = 0 or sgid=0 or pgid = 0")
            info = cursor.fetchone()
            if not info:
                print info
                break
            
            arrangeid = info["Arrange5ID"]
            clubid = info["ClubID"]
            cid = info["CID"]
            pfid = info["PFID"]
            sfid = info["SFID"]
            sgid = info["SGID"]
            pgid = info["PGID"]
            
            need = False
            
            if cid <= 0:
                need = True
                cursor.execute("CreateFillPlayer5 %s" % clubid)
                cursor.execute("select playerid from btp_player5 where clubid = %s and playerid not in (%s,%s,%s,%s,%s)" % (clubid, cid, pfid, sfid, sgid, pgid))
                data = cursor.fetchone()
                cid = data["playerid"]
            
            if pfid <= 0:
                need = True
                cursor.execute("CreateFillPlayer5 %s" % clubid)
                cursor.execute("select playerid from btp_player5 where clubid = %s and playerid not in (%s,%s,%s,%s,%s)" % (clubid, cid, pfid, sfid, sgid, pgid))
                data = cursor.fetchone()
                pfid = data["playerid"]
            
            if sfid <= 0:
                need = True
                cursor.execute("CreateFillPlayer5 %s" % clubid)
                cursor.execute("select playerid from btp_player5 where clubid = %s and playerid not in (%s,%s,%s,%s,%s)" % (clubid, cid, pfid, sfid, sgid, pgid))
                data = cursor.fetchone()
                sfid = data["playerid"]
                
            if sgid <= 0:
                need = True
                cursor.execute("CreateFillPlayer5 %s" % clubid)
                cursor.execute("select playerid from btp_player5 where clubid = %s and playerid not in (%s,%s,%s,%s,%s)" % (clubid, cid, pfid, sfid, sgid, pgid))
                data = cursor.fetchone()
                sgid = data["playerid"]
                
            if pgid <= 0:
                need = True
                cursor.execute("CreateFillPlayer5 %s" % clubid)
                cursor.execute("select playerid from btp_player5 where clubid = %s and playerid not in (%s,%s,%s,%s,%s)" % (clubid, cid, pfid, sfid, sgid, pgid))
                data = cursor.fetchone()
                pgid = data["playerid"]
           
            if need:
                sql = "ReSetArrange5 %s, %s, %s, %s, %s, %s" % (arrangeid, cid, pfid, sfid, sgid, pgid)
                cursor.execute(sql)
            
        cursor.execute("select clubid ,  count(1) as total from btp_player5 where clubid > 0 group by clubid  order by total asc")
        infos = cursor.fetchall()
        print len(infos)
        for info in infos:
            clubid = info["clubid"]
            total = info["total"]
            
            if total < 8:
                add_count = 8 - total
                for _ in range(add_count):
                    sql = "CreateFillPlayer5 %s" % clubid
                    cursor.execute(sql)
        
    finally:
        cursor.close()  
    
        
if __name__ == "__main__":
    fill_player()