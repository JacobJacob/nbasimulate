#!/usr/bin/python
# -*- coding: utf-8 -*-#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Game
from xba.common import log_execption

def get_game_info():
    """获取游戏行"""
    cursor = connection.cursor()
    try:
        sql = "EXEC GetGameRow"
        cursor.execute(sql)
        return cursor.fetchone()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def game_info():
    """获取游戏信息"""
    session = Session()
    return session.query(Game).filter(Game.gameid==1).one()
        
        
def set_to_next_turn():
    """联赛前进一轮"""
    cursor = connection.cursor()
    try:
        sql = "EXEC SetTurn"
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_to_next_days():
    """联赛天数前进一天"""
    cursor = connection.cursor()
    try:
        sql = "EXEC SetDays"
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_season():
    """赛季开始"""
    cursor = connection.cursor()
    try:
        sql = "EXEC SetSeason"
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
 
def add_power_by_online():
    """在线体力恢复"""
    cursor = connection.cursor()
    try:
        sql = "EXEC AddPowerByOnline"
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()       
        
if __name__ == "__main__":
    set_to_next_turn()
