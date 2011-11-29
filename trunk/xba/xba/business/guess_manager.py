#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime, timedelta

from xba.common.sqlserver import connection
from xba.common.stringutil import ensure_gbk

        
def add_xguess(type, namea, nameb, hot, end_time_interval=24, show_time_interval=0):
    """添加竞猜"""
    cursor = connection.cursor()
    try:
        money_type = 0
        end_time = datetime.now() + timedelta(hours=end_time_interval)
        show_time = datetime.now() + timedelta(hours=show_time_interval)
        sql = u"AddGuess '%s', %s, '%s', '%s', %s, '%s', '%s'" % (type, money_type, namea, nameb, hot, end_time.strftime("%Y-%m-%d %H:%M:%S"), show_time.strftime("%Y-%m-%d %H:%M:%S"))
        sql = ensure_gbk(sql)
        cursor.execute(sql)
    finally:
        cursor.close()
            
        
if __name__ == "__main__":
    add_xguess('NBA', '火箭', '湖人', 1)
