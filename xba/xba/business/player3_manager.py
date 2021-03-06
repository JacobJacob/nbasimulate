#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Player3
from datetime import datetime, timedelta
from xba.common import log_execption


def create_player(count, category, hours):
    """创建球员"""
    
    end_bid_time = datetime.now() + timedelta(hours=hours)
    cursor = connection.cursor()
    try:
        sql = "exec CreatePlayer3 %s, %s, '%s'" % (count, category, end_bid_time.strftime("%Y-%m-%d %H:%M:%S"))
        print sql
        #cursor.execute(sql)
    except:
        log_execption()
    finally:
        cursor.close()
        
def player_list(page, pagesize, category):
    """获取街球球员"""
    session = Session()
    total = session.query(Player3).filter(Player3.category==category).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Player3).filter(Player3.category==category).order_by(Player3.clubid).offset(index).limit(pagesize).all()
    return total, infos

def recover_healthy3():
    """年轻球员受伤及事件更新"""
    cursor = connection.cursor()
    try:
        sql = "exec RecoverHealthy3"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        cursor.close()
        
def player_grow3():
    """年轻球员身高体重增长"""
    cursor = connection.cursor()
    try:
        sql = "exec PlayerGrow3"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        cursor.close()
  
def player3_aging():
    """街头球员老化"""
    cursor = connection.cursor()
    try:
        sql = "exec Player3Aging"
        cursor.execute(sql)
    finally:
        cursor.close()      
        
        
def player_skill_max_up3():
    """年轻球员能力增长"""
    cursor = connection.cursor()
    try:
        sql = "exec PlayerSkillMaxUP3"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        cursor.close()
        
def clear_player3_season():
    """年轻球员赛季统计清除 """
    cursor = connection.cursor()
    try:
        sql = "exec ClearPlayer3Season"
        cursor.execute(sql)
    finally:
        cursor.close()
        
def delete_player3(player_id):
    """删除年轻球员 """
    cursor = connection.cursor()
    try:
        sql = "exec DeletePlayer3 %s" % player_id
        cursor.execute(sql)
    finally:
        cursor.close()

def get_player3_pre_retire():
    """获取要退役的球员"""
    cursor = connection.cursor()
    try:
        sql = "exec Player3PreRetire"
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        cursor.close()
        
def recover_power3():
    """年轻球员体力恢复"""
    cursor = connection.cursor()
    try:
        sql = "exec RecoverPower3"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        cursor.close()

if __name__ == "__main__":
    #end_bid_time = datetime.now() + timedelta(hours=36)
    #create_player(1, 2, end_bid_time.strftime("%Y-%m-%d %H:%M:%S") , 48, 68)
    create_player(10, 6, 5)
