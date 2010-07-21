#-*- coding:utf-8 -*-

from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral

def log(log_info):
    """将爬取记录保存到数据库中"""
    cursor = connection.cursor()
    log_info['created_at'] = ReserveLiteral('now()')
    try:
        return cursor.insert(log_info, 'spider_log', True, ['created_at'])
    except:
        raise
    finally:
        cursor.close()
    
    