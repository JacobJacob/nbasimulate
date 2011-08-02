#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection
from xba.common import log_execption

def set_only_one_match():
    """胜者为王挫合"""
    cursor = connection.cursor()
    try:
        sql = "exec SetOnlyOneGame"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
        
def night_update_only_one_game():
    """胜者为王夜间更新"""
    cursor = connection.cursor()
    try:
        sql = "exec NightUpdateOnlyOneGame"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
        
def send_money_by_only_day_point():
    """胜者为王每天奖励"""
    cursor = connection.cursor()
    try:
        sql = "exec SendMoneyByOnlyDayPoint"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
        
def get_onlyone_match_row(user_id):
    """得到胜者报名行"""
    cursor = connection.cursor()
    try:
        sql = "exec GetOnlyOneMatchRow %s" % user_id
        cursor.execute(sql)
        return cursor.fetchone()
    except:
        log_execption()
    finally:
        connection.close()
        
def only_one_match_out(user_id):
    """胜者退出"""
    cursor = connection.cursor()
    try:
        sql = "exec OnlyOneMatchOut %s" % user_id
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
        
def only_one_match_goon(user_id):
    """胜者继续"""
    cursor = connection.cursor()
    try:
        sql = "exec OnlyOneMatchGoOn %s" % user_id
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
        
def only_one_center_reg(user_id):
    """胜者报名"""
    cursor = connection.cursor()
    try:
        sql = "exec OnlyOneCenterReg %s, 0, 0"  % user_id
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
