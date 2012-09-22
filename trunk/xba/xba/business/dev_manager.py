#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common import log_execption
from xba.common.sqlserver import connection
from xba.business import club_manager
from xba.common.constants.dev import DEV_SORT_MONEY_MAP, DEV_SORT_REPUTATION_MAP

def get_dev_clubs(devcode, only_base=False):
    """根据devcode获取所有俱乐部id"""
    cursor = connection.cursor()
    try:
        condition = ""
        if only_base:
            condition = " AND Score <> -9999 "
        sql = "SELECT * FROM BTP_Dev WHERE DevCode='%s' %s ORDER BY Win DESC,Lose DESC,Score DESC,ClubID DESC,DevID ASC" % (devcode, condition)
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        cursor.close()
        
        
def get_delete_count(total, pre_delete):
    print "原剩下的", total - pre_delete
    level = (total - pre_delete) % 14
    if level > 0:
        level = 14 - level
    print "打算留下的", level
    delete = pre_delete - level    
    print "实际删除的", delete
    return delete
    
def delete_from_league_long_not_login():
    """将超过14天没有登陆的球队踢出联赛"""
    cursor = connection.cursor()
    try:
        #先算下大概要删除多少球队
        cursor.execute("select count(1) as total from btp_dev where clubid > 0")
        total_dev_club = cursor.fetchone()["total"]
        
        cursor.execute("select ClubID5 from btp_account where activetime < dateadd(dd, -14, getdate()) order by activetime asc")
        infos = cursor.fetchall()
        if infos:
            club_ids = []
            for info in infos:
                club_id = info["ClubID5"]
                if club_id > 0:
                    cursor.execute("select * from btp_dev where clubid = %s" % club_id)
                    dev_info = cursor.fetchone()
                    if dev_info:
                        club_ids.append(club_id)
        
            pre_delete_count = len(club_ids)
            
            #要删除的俱乐部数
            delete_club_count = get_delete_count(total_dev_club, pre_delete_count)
            for i in range(delete_club_count):
                sql = "DeleteFromLeague %s" % club_ids[i]
                print sql
                cursor.execute(sql)
            
    finally:
        cursor.close()
        
def get_dev_info_by_userid(userid):
    """根据用户ID获取联赛行"""
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT ClubID FROM btp_club WHERE UserID=%s and Category=5" % userid)
        info = cursor.fetchone()
        if info:
            cursor.execute("SELECT * FROM btp_dev WHERE ClubID=%s" % info["ClubID"])
            return cursor.fetchone()
    finally:
        cursor.close()

def delete_log_dev_msg():
    """删除联赛日志"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC DeleteLogDevMsg")
    finally:
        cursor.close()
        
def dev_sort_send_reputation(level, club_id, sort):
    reputation = DEV_SORT_REPUTATION_MAP.get(level, {}).get(sort + 1, 0)
    if reputation <= 0:
        return
    
    club_info = club_manager.get_club_by_id(club_id)
    if not club_info:
        return
    user_id = club_info["UserID"]

    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        
        cursor.execute("SELECT UnionID, Nickname FROM BTP_Account WHERE UserID = %s" % user_id)
        info = cursor.fetchone()
        union_id = info["UnionID"]
        nickname = info["Nickname"]
        if union_id > 0:
            cursor.execute("UPDATE BTP_Union SET  Reputation=Reputation+%s WHERE UnionID=%s AND Reputation>0" % (reputation, union_id))
            nickname = nickname.encode("utf8")
            sql = "INSERT INTO BTP_UnionReputation (UserID,NickName,UnionID,Reputation,Note)VALUES(%s,'%s',%s,%s,'联赛奖励')" % (user_id, nickname, union_id, reputation)
            sql = sql.decode("utf8").encode("gbk")
            cursor.execute(sql)
        
        cursor.commit()
    except:    
        cursor.rollback()
        raise
    finally:
        cursor.close()
        
        
def dev_sort_send_money(level, club_id, sort):
    """联赛奖励"""
    money = DEV_SORT_MONEY_MAP.get(level, {}).get(sort + 1, 0)
    if money <= 0:
        return
    money *= 10000
    club_info = club_manager.get_club_by_id(club_id)
    if not club_info:
        return
    user_id = club_info["UserID"]
    event = "%s级联赛第%s名奖励" % (level, sort + 1)
    content = "恭喜您获得%s级联赛第%s名，奖金%s" % (level, sort + 1, money)
    event = event.decode("utf8").encode("gbk")
    content = content.decode("utf8").encode("gbk")
    sender = "秘书报告".decode("utf8").encode("gbk")
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        sql = "UPDATE BTP_Account SET Money=Money+%s WHERE UserID=%s" % (money, user_id)
        cursor.execute(sql)
        sql = "Exec AddFinance %s,1,5,%s,1,'%s'" % (user_id, money, event)
        cursor.execute(sql)
        sql = "Exec AddNewMessage %s,2,0,'%s','%s'" % (user_id, sender, content)
        cursor.execute(sql)
        cursor.commit()
    except:
        cursor.rollback()
        raise
    finally:
        cursor.close()
    
def upgrade_dev(dev_info):
    """联赛升级"""
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        dev_id = dev_info["DevID"]
        club_id = dev_info["ClubID"]
        dev_code = dev_info["DevCode"].strip()
        if club_id > 0:
            club_info = club_manager.get_club_by_id(club_id)
            user_id = club_info["UserID"]
            update_account_sql = "UPDATE btp_account SET DevCode='%s' WHERE UserID=%s" % (dev_code[:-1], user_id)
            cursor.execute(update_account_sql)
        update_dev_sql = "UPDATE btp_dev SET DevCode='%s', Score=-9999, Levels=Levels - 1 WHERE DevID=%s" % (dev_code[:-1], dev_id)
        cursor.execute(update_dev_sql)
        cursor.commit()
    except:
        cursor.rollback()
        log_execption()
        raise
    finally:
        cursor.close()
        
def degrade_dev(dev_info, sort):
    """联赛降级"""
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        dev_id = dev_info["DevID"]
        club_id = dev_info["ClubID"]
        dev_code = dev_info["DevCode"].strip()
        if sort in (10, 11):
            new_dev_code = "%s0" % dev_code
        elif sort in (12, 13):
            new_dev_code = "%s1" % dev_code
        else:
            print "error"
            return
        if club_id > 0:
            club_info = club_manager.get_club_by_id(club_id)
            user_id = club_info["UserID"]
            update_account_sql = "UPDATE btp_account SET DevCode='%s' WHERE UserID=%s" % (new_dev_code, user_id)
            cursor.execute(update_account_sql)
        update_dev_sql = "UPDATE btp_dev SET DevCode='%s', Score=-9999, Levels=Levels + 1 WHERE DevID=%s" % (new_dev_code, dev_id)
        cursor.execute(update_dev_sql)
        cursor.commit()
    except:
        cursor.rollback()
        log_execption()
        raise
    finally:
        cursor.close()
        
def reward_dev(dev_code, level, sort):
    """联赛奖励"""
    cursor = connection.cursor()
    try:
        sql = "EXEC RewardDev '%s', '%s'" % (dev_code, '%s.%s联赛' % (level, sort + 1))
        sql = sql.decode("utf8").encode("gbk")
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        cursor.close() 
        
def get_dev_table_by_level(level):
    """得到某一等级的所有俱乐部"""
    cursor = connection.cursor()
    try:
        sql = "SELECT DevID,ClubID,DevCode FROM BTP_Dev WHERE Levels=%s ORDER BY DevCode " % level
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_one_empty_dev(level):
    """得到某一等级一支空的联赛球队"""
    cursor = connection.cursor()
    try:
        sql = "SELECT TOP 1 DevID,ClubID,DevCode FROM BTP_Dev WHERE Levels=%s AND ClubID <= 0 ORDER BY DevCode " % level
        cursor.execute(sql)
        return cursor.fetchone()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_dev_table_by_total(level):
    """获取联赛统计"""
    cursor = connection.cursor()
    try:
        sql = "select devcode, sum(1) as total from btp_dev where levels = %s and clubid > 0 group by devcode " % level
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
   
    
def get_last_club_info_by_status(dev_code, club_infos, cursor, active=True):
    """获取最后活的球队"""
    i = len(club_infos)
    while i > 0:
        club_info = club_infos[i - 1]
        club_id = club_info["ClubID"]
        if club_id > 0:
            if club_info["DevCode"] != dev_code:
                if active and is_active(club_id, cursor):
                    return i, club_info
                elif not active and not is_active(club_id, cursor):
                    return i, club_info
            else:
                return 0, None
        i -= 1
        
    return 0, None

def dev_active_club_to_one_place(level):
    """把某一等级的活号放在一起"""
    club_infos = get_dev_table_by_level(level)
    cursor = connection.cursor()
    try:
        for i, club_info in enumerate(club_infos):
            club_id = club_info["ClubID"]
            dev_code = club_info["DevCode"]
            if club_id > 0:
                if is_active(club_id, cursor):
                    continue
                    #print "active %s" % club_id
                    #index, need_club_info = get_last_club_info_by_status(dev_code, club_infos, cursor, False)
                else:
                    print "not active %s" % club_id
                    index, need_club_info = get_last_club_info_by_status(dev_code, club_infos, cursor, True)
                if index < i or not need_club_info:
                    return
                else:
                    print "get need , exchange"
                    exchange_two_dev(club_info, need_club_info)
                    
    finally:
        cursor.close()
   
def is_active(club_id, cursor):
    cursor.execute("SELECT UserID From BTP_Club WHERE ClubID=%s" % club_id)
    info = cursor.fetchone()
    user_id = info["UserID"]
    cursor.execute("SELECT * From BTP_Account WHERE UserID=%s AND ActiveTime > dateadd(dd, -3, getdate()) " % user_id)
    info = cursor.fetchone()
    return info is not None
    
def exchange_two_dev(club_info_one, club_info_two):
    """交换两支球队"""
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        club_id_one = club_info_one["ClubID"]
        club_id_two = club_info_two["ClubID"] 
        dev_id_one = club_info_one["DevID"]
        dev_id_two = club_info_two["DevID"]
        dev_code_one = club_info_one["DevCode"]
        dev_code_two = club_info_two["DevCode"]
        club_info_two["ClubID"] = club_id_one
        club_info_one["ClubID"] = club_id_two
        sql_one = "UPDATE btp_dev SET ClubID = %s WHERE DevID = %s " % (club_id_two, dev_id_one)
        sql_two = "UPDATE btp_dev SET ClubID = %s WHERE DevID = %s " % (club_id_one, dev_id_two)
        print sql_one
        print sql_two
        if club_id_one > 0:
            cursor.execute("select UserID from btp_club where ClubID=%s" % club_id_one)
            userid = cursor.fetchone()["UserID"]
            cursor.execute("update btp_account set devcode='%s' where userid=%s" % (dev_code_two, userid))
        if club_id_two > 0:
            cursor.execute("select UserID from btp_club where ClubID=%s" % club_id_two)
            userid = cursor.fetchone()["UserID"]
            cursor.execute("update btp_account set devcode='%s' where userid=%s" % (dev_code_one, userid))
        
        cursor.execute(sql_one)
        cursor.execute(sql_two)
        cursor.commit()
    except:
        cursor.rollback()
        log_execption()
        raise
    finally:
        cursor.close()
        
def assign_dev():
    """初始化联赛"""
    cursor = connection.cursor()
    try:
        sql = "EXEC AssignDev"
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def reword_mvp_by_devcode(dev_code):
    """联赛MVP奖励"""
    cursor = connection.cursor()
    try:
        sql = "EXEC RewordMVPByDevCode '%s'" % dev_code
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        cursor.close()

def delete_devmessage():
    """删除联赛留言"""
    cursor = connection.cursor()
    try:
        sql = "delete from btp_devmessage"
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        cursor.close()
        
def cretae_sql():
    ABILITYS = ["Attack","Defense","Team"]
    cursor = connection.cursor()
    try:
        sql = "select * from btp_player5 where playerid = 15614"
        cursor.execute(sql)
        info = cursor.fetchone()
        sql = "update btp_player set "
        for ability in ABILITYS:
            sql += "%s=%s  " % (ability, info[ability])
            
        sql += " WHERE PlayerID = 15614" 
        print sql
        print "update btp_account set money = money + 10500000 where userid=287"
        msg_sql = u"EXEC AddMessage '','秘书报告', '尊敬的小白经理您好，由于我们程序上的错误，导致出了一名bug球员并被您买到，为了游戏的公平性，我们特将球员综合更改为正常值，并退还您购买此名球员的所有资金，对您造成的不便深感抱歉.', '小白'"
        print msg_sql
    except:
        log_execption()
        raise
    finally:
        cursor.close()
       
if __name__ == "__main__":
    pass
    #dev_sort_dend_reputation(1, 140, 1)