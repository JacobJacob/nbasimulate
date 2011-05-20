#!/usr/bin/python
# -*- coding: utf-8 -*-#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Game

def get_game_info():
    """获取游戏行"""
    cursor = connection.cursor()
    try:
        sql = "exec GetGameRow"
        cursor.execute(sql)
        return cursor.fetchone()
    except Exception, e:
        a = e.message.decode("gbk")
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
        sql = "exec SetTurn"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        raise
    finally:
        connection.close()
        
if __name__ == "__main__":
    set_to_next_turn()
