#-*- coding:utf-8 -*-

from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral

_LOAD_SQL = '''select host, url, type_mapping from type_url_mapping where type=%s and flg=%s'''

def register(type, flg):
    """注册一个新的类型"""
    cursor = connection.cursor()
    data = {'type': type, 'created_at': ReserveLiteral('now()'), 'flg': flg}
    try:
        cursor.insert(data, 'type_url_mapping', True, 'skip_all')
    finally:
        cursor.close()
        
def get_type_mapping_value(type, flg):
    """根据类型，得到对应的host,url,type_mapping"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone(_LOAD_SQL, (type, flg))
    except:
        return None
    finally:
        cursor.close()
