#-*- coding:utf-8 -*-

from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral

_LOAD_RUNTIME_DATA = '''select value from runtime_data where programe=%s and value_key=%s'''

def insert_runtime_data(data_info):
    """保存运行时数据"""
    data_info['created_at'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(data_info, 'runtime_data', True, ['created_at'])
    finally:
        cursor.close()    
    
    
def load_runtime_data(programe, key):
    """获取运行时数据"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone(_LOAD_RUNTIME_DATA, (programe, key))
    finally:
        cursor.close()  
        
def save_runtime_data(programe, key, value):
    data_info = {'programe': programe, 'value_key': key, 'value': value}
    insert_runtime_data(data_info)    
        
def test():
    runtime_data_info = {'programe': 'test_programe', 'value_key': 'test_key', 'value': 'test_value'}
    insert_runtime_data(runtime_data_info)
    
    data = load_runtime_data('test_programe', 'test_key')
    assert data
    assert data['value']
    assert data['value'] == 'test_value'
    
    runtime_data_info['value'] = 'new test value'
    insert_runtime_data(runtime_data_info)
    data = load_runtime_data('test_programe', 'test_key')
    assert data
    assert data['value']
    assert data['value'] == 'new test value'
    print 'tset is ok'
    
if __name__ == '__main__':
    test()