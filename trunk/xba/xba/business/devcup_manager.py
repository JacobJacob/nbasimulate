#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime

from xba.common import log_execption
from xba.common.sqlserver import connection

def get_devcup_table_toarrage():
    """得到可以安排赛程的杯赛"""
    cursor = connection.cursor()
    try:
        sql = "exec GetDevCupTableToArrage"
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_reg_by_devcupid_end_round(devcupid, round):
    """得到某轮存活球队"""
    cursor = connection.cursor()
    try:
        sql = "exec GetRegByDevCupIDEndRound %s, %s" % (devcupid, round)
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def reward_devcup_by_clubid(devcupid, club_id, round=100):
    """发奖杯"""
    cursor = connection.cursor()
    try:
        sql = "exec RewardDevCupByClubID %s, %s, %s" % (club_id, devcupid, round)
        print sql
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()  
        
def set_devcup_champion(devcupid, user_id, club_name):
    """设置自定义杯赛冠军"""
    cursor = connection.cursor()
    try:
        sql = "exec SetDevCupChampion %s, %s, '%s'" % (devcupid, user_id, club_name.encode("gbk"))
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()     
    
def set_devclub_dead_round(devcupid, club_id, round):
    """设置报名表的被淘汰轮次"""
    cursor = connection.cursor()
    try:
        sql = "exec SetDevClubDeadRound %s, %s, %s" % (devcupid, club_id, round)
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()        
        
        
        
def get_run_devcuptable():
    """得到可以可以打下一轮的杯赛"""
    cursor = connection.cursor()
    try:
        sql = "exec GetRunDevCupTable '%s'" % datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_alive_reg_table_by_devcupid(devcupid):
    """得到某个杯赛存活球队列表"""
    cursor = connection.cursor()
    try:
        sql = "exec GetAliveRegTableByDevCupID %s" % devcupid
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_round_by_devcupid(devcupid, round):
    """设置杯赛轮数"""
    cursor = connection.cursor()
    try:
        sql = "exec SetRoundByDevCupID %s, %s" % (devcupid, round)
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_status_by_devcupid(devcupid, status):
    """设置杯赛状态"""
    cursor = connection.cursor()
    try:
        sql = "exec SetStatusByDevCupID %s, %s" % (devcupid, status)
        print sql
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_devcup_match_by_gaincode_devcupid(devcupid, gain_code):
    """根据GainCode和杯赛ID获取比赛"""
    cursor = connection.cursor()
    try:
        sql = "SELECT * FROM BTP_DevCupMatch WHERE GainCode = '%s' AND DevCupID = %s" % (gain_code, devcupid)
        cursor.execute(sql)
        return cursor.fetchone()
    except:
        log_execption()
        raise
    finally:
        connection.close() 
    
def set_code_by_devregid(regid, code):
    """设置杯赛报名code"""
    cursor = connection.cursor()
    try:
        sql = "exec SetCodeByDevRegID %s, '%s'" % (regid, code)
        print sql
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()

if __name__ == "__main__":
    a = u"榮光の團 "
    print a.encode("gbk")