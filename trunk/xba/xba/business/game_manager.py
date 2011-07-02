#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Game, Announce
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

def get_announce(page, pagesize):
    """获取公告"""
    session = Session()
    total = session.query(Announce).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Announce).order_by(Announce.id).offset(index).limit(pagesize).all()
    return total, infos

def add_announce(title, type=0):
    """添加公告"""
    session = Session()
    announce = Announce()
    announce.title = title.encode("gbk")
    announce.created_time = datetime.now()
    announce.type = type
    session.add(announce)
    session.commit()
        
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
    total, infos = get_announce(1, 10)
    for info in infos:
        print info.title.decode("gbk")