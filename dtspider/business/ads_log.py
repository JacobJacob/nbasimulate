#-*- coding:utf-8 -*-

from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral

_LOAD_ADS_LOG_SQL = 'select url, url_md5, created_at from ads_log where url_md5=%s'

def log(log_info):
    """将发布广告记录记录保存到数据库中"""
    cursor = connection.cursor()
    log_info['created_at'] = ReserveLiteral('now()')
    try:
        return cursor.insert(log_info, 'ads_log', True, ['created_at'])
    except:
        raise
    finally:
        cursor.close()
        
       
def load_log(url_md5):
    """查找发布广告日志 """
    cursor = connection.cursor()
    try:
        return cursor.fetchone(_LOAD_ADS_LOG_SQL, (url_md5))
    except:
        raise
    finally:
        cursor.close()
    