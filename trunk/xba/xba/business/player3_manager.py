#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Player3
from datetime import datetime, timedelta

def create_player(count, category, hours):
    """创建球员"""
    end_bid_time = datetime.now() + timedelta(hours=hours)
    cursor = connection.cursor()
    try:
        sql = "exec CreatePlayer3 %s, %s, '%s'" % (count, category, end_bid_time.strftime("%Y-%m-%d %H:%M:%S"))
        print sql
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def player_list(page, pagesize, category):
    """获取街球球员"""
    session = Session()
    total = session.query(Player3).filter(Player3.category==category).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Player3).filter(Player3.category==category).order_by(Player3.clubid).offset(index).limit(pagesize).all()
    return total, infos

        
if __name__ == "__main__":
    
    end_bid_time = datetime.now() + timedelta(hours=36)
    create_player(1, 2, end_bid_time.strftime("%Y-%m-%d %H:%M:%S") , 48, 68)

