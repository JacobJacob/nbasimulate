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
