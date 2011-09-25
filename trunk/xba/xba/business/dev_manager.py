#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common import log_execption
from xba.common.sqlserver import connection
from xba.business import club_manager
from xba.common.constants.dev import DEV_SORT_MONEY_MAP

def get_dev_clubs(devcode):
    """根据devcode获取所有俱乐部id"""
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT * FROM BTP_Dev WHERE DevCode=%s ORDER BY Win DESC,Lose DESC,Score DESC,ClubID DESC,DevID ASC", devcode)
        return cursor.fetchall()
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
    cursor = connection.cursor()
    try:
        cursor.start_transaction()
        sql = "UPDATE BTP_Account SET Money=Money+%s WHERE UserID=%s" % (money, user_id)
        cursor.execute(sql)
        sql = "Exec AddFinance %s,1,5,%s,1,'%s'" % (user_id, money, event)
        print sql
        cursor.execute(sql)
        sql = "Exec AddNewMessage %s,2,0,'秘书报告','%s'" % (user_id, content)
        sql = sql.encode("gbk")
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
       
if __name__ == "__main__":
    infos = get_dev_clubs("0000000")
    for info in infos:
        print info