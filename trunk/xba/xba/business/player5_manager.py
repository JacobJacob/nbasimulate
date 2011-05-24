#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Player5
from datetime import datetime, timedelta

def create_player(count, category, hours, now_point, max_point):
    """创建球员"""
    end_bid_time = datetime.now() + timedelta(hours=hours)
    cursor = connection.cursor()
    try:
        sql = "exec CreatePlayer5 %s, %s, '%s', %s, %s" % (count, category, end_bid_time.strftime("%Y-%m-%d %H:%M:%S"), now_point, max_point)
        print sql
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def player_list(page, pagesize, category):
    """获取职业球员"""
    session = Session()
    total = session.query(Player5).filter(Player5.category==category).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Player5).filter(Player5.category==category).order_by(Player5.clubid).offset(index).limit(pagesize).all()
    return total, infos

def recover_power5():
    """职业球员体力恢复，将当天训练点清空"""
    cursor = connection.cursor()
    try:
        sql = "exec RecoverPower5"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
        
def update_season_mvp_value():
    """更新球员的MVP值"""
    cursor = connection.cursor()
    try:
        sql = "exec UpdateSeasonMVPValue"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def reset_all_player_shirt():
    """重置球员的球衣销售量"""
    cursor = connection.cursor()
    try:
        sql = "exec ResetAllPlayerShirt"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def reset_all_player_pop():
    """重置球员受欢迎程度"""
    cursor = connection.cursor()
    try:
        sql = "exec ResetAllPlayerPop"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
    
if __name__ == "__main__":
    #update_season_mvp_value()
    reset_all_player_pop()
    reset_all_player_shirt()

