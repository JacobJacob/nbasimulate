#!/usr/bin/python
# -*- coding: utf-8 -*-
"""数据库操作基类
"""

from common import cache as _data_cache
from common.db import connection

_get_cache_table_key = lambda table_name: '%s%s' % (table_name, 
                                                   "$current_table")
class DBOperator(object):
    """数据库操作基类"""
    
    A_TAG = '_a'
    B_TAG = '_b'
    
    CACHE_TIME = 1800 # 30分钟超时，防止有脏数据存在
    
    cache = _data_cache
    
    @classmethod
    def set_cache(cls, key, val, cache_time=CACHE_TIME):
        cls.cache.set(key, val, cache_time)
        
    @classmethod
    def get_cache(cls, key):
        return cls.cache.get(key)
    
    @classmethod
    def delete_cache(cls, key):
        return cls.cache.delete(key)

    @classmethod
    def acquire_connection(cls):
        return connection
    
    @classmethod
    def release_connection(cls, conn):
        pass
    
    @classmethod
    def cursor(cls):
        return connection.cursor()
    
    @classmethod
    def get_current_table(cls, table_name=None):
        """获取当前使用的表名"""
        if table_name is None:
            table_name = cls.TABLENAME
        key = _get_cache_table_key(table_name)
        result = cls.cache.get(key)
        if not result:
            result = '%s%s' % (table_name, cls.A_TAG)
            cls.cache.set(key, result)
        return result
    
    @classmethod
    def get_another_table(cls, table_name=None):
        """获取另外一个表名"""
        if table_name is None:
            table_name = cls.TABLENAME
        current_tag = cls.get_current_table(table_name)
        a_table = '%s%s' % (table_name, cls.A_TAG)
        if current_tag == a_table:
            return '%s%s' % (table_name, cls.B_TAG)
        return a_table
    
    @classmethod
    def shift_table(cls, table_name=None):
        """切换AB数据表"""
        if table_name is None:
            table_name = cls.TABLENAME
        current_table = cls.get_current_table(table_name)
        a_table = '%s%s' % (table_name, cls.A_TAG)
        b_table = '%s%s' % (table_name, cls.B_TAG)
        if current_table == a_table:
            current_table = b_table
        else:
            current_table = a_table
        key = _get_cache_table_key(table_name)
        cls.cache.set(key, current_table)
