#-*- coding:utf-8 -*-
from datetime import datetime
import time
import unittest

from _mysql_exceptions import ProgrammingError

from dtspider.common import md5mgr
from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral
from dtspider.common.bottle import cache
 
def format_now():
    now = datetime.now()
    return now.strftime("%Y-%m-%d %H:%M:%S")

def info(msg):
    print '[%s]:%s' % (format_now(), msg)

class TableNotExistException(Exception):
    
    def __init__(self, table):
        self._table = table
        
    def __str__(self):
        return "table %s not exist" % self._table

class Persistable(object):
    
    _meta_cache = {}
    _cache = cache

    @classmethod
    def _init_meta(cls):
        if hasattr(cls, 'inited') and getattr(cls, 'inited'):
            return
        info('-' * 100)
        info('start to init meta[%s]...' % cls.__name__)
        info('-' * 100)
        if not hasattr(cls, '_table') or getattr(cls, '_table') is None:
            cls._table = cls._parse_table()
        meta = cls._cache.get(cls._table)
        if meta:
            #try get from cache
            info('meta hit in cache, use it!!!')
            Persistable._meta_cache[cls._table] = meta
            cls.inited = True
            return
            
        if cls._table in Persistable._meta_cache:
            return
        sql = 'show columns from %s'
        cursor = connection.cursor()
        try:
            try:
                data = cursor.fetchall(sql % cls._table)
            except ProgrammingError, err:
                raise TableNotExistException(cls._table)
            meta = Meta(cls._table)
            column_infos = data.to_list()
            for column_info in column_infos:
                field = column_info['Field']
                default = column_info['Default']
                type = column_info['Type']
                extra = column_info['Extra']
                if type.startswith(Column.TYPE_INT):
                    temp_type = type[:len(Column.TYPE_INT)]
                    length = type[len(Column.TYPE_INT)+1:-1]
                    type = temp_type
                elif type.startswith(Column.TYPE_VARCHAR):
                    temp_type = type[:len(Column.TYPE_VARCHAR)]
                    length = type[len(Column.TYPE_VARCHAR)+1:-1]
                    type = temp_type
                else:
                    length = None
                key = column_info['Key']
                if key == 'UNI' or key == 'PRI':
                    is_unique = True
                else:
                    is_unique = False
                    
                column = Column(field=field, type=type, length=length, \
                                is_unique=is_unique, default=default, extra=extra)
                meta.add_column(column)
                cls._cache.set(cls._table, meta)
                Persistable._meta_cache[cls._table] = meta
                    
        finally:
            cursor.close()
        cls.inited = True
           
    @classmethod 
    def _parse_table(cls):
        '''parse the table name'''
        class_name = cls.__name__
        table_name = ''
        for i in range(len(class_name)):
            c = class_name[i:i+1]
            if c.isupper():
                if i != 0:
                    table_name += ('_%s' % c.lower())
                else:
                    table_name += c.lower()
            else:
                table_name += c
        
        return table_name
        
    @classmethod
    def get_meta(cls):
        cls._init_meta()
        meta = Persistable._meta_cache[cls._table]
        return meta
      
    def persist(self):
        '''persist object self'''
        self._init_meta()
        info('start to save object....[table:%s]' % self._table)
        cache_key = self.build_cache_key(self)
        if cache_key:
            self._cache.delete(cache_key)
        data = {}
        update_skip_columns = ['id', 'created_at', 'updated_at']
        cursor = connection.cursor()
        meta = Persistable._meta_cache[self._table]
        columns = meta.columns
        for column in columns:
            field_name = column.field     
            if hasattr(self, field_name):
                data[field_name] = getattr(self, field_name)
            elif field_name == 'created_at':
                data['created_at'] = ReserveLiteral('now()')
            elif field_name == 'created_time':
                data['created_time'] = ReserveLiteral('now()')
                    
        try:
            cursor.insert(data, self._table, True, update_skip_columns)
        finally:
            cursor.close()
        info('finish save object....[table:%s]' % self._table)
        
    @classmethod
    def query(cls, **args):
        '''query objects'''
        cls._init_meta()
        limit = ''
        order = ''
        where = '1=1'
        if 'limit' in args:
            limit = 'limit %s' % args['limit']
        if 'order' in args:
            order = 'order by %s' % args['order']
        if 'condition' in args:
            where = args['condition']
         
        sql =  'select * from %s where %s %s %s ' % (cls._table, where, order, limit)
        info('start to load objects[%s] with sql:%s' % (cls._table, sql))
        cursor = connection.cursor()
        try:
            data = cursor.fetchall(sql)
            if data:
                objs = []
                for row in data:
                    obj = cls._build_object(row)
                    objs.append(obj)
                info('query success, return %s records...' % len(objs))
                return objs
        finally:
            cursor.close()
        info('not record found')
        return []
                   
    @classmethod
    def load(cls, **keys):
        '''load the object'''
        cls._init_meta()
        query_str = ','.join(['%s=%s' % (k, v) for k, v in keys.items()])
        info('start load object with %s[table:%s]' % (query_str, cls._table))
        #load from cache
        obj = cls.load_from_cache(keys)
        if obj:
            return obj
        
        condition = ' and '.join(["%s=%%s" % k for k in keys.keys()])
        param = [keys[k] for k in keys.keys()] 
        sql = 'select * from %s where %s' % (cls._table, condition) 
        cursor = connection.cursor()
        try:
            data = cursor.fetchone(sql, param)
            if data:
                obj = cls._build_object(data)
                cache_key = cls.build_cache_key(obj)
                if cache_key:
                    cls._cache.set(cache_key, obj, 1800)
                info('success load object[%s, id=%s]' % (cls._table, obj.id)) 
                return obj
        finally:
            cursor.close()
        info('load failure[table:%s]' % cls._table)
        return None
    
    @classmethod
    def load_from_cache(cls, parms):
        '''load from cache'''
        cache_key = cls.build_cache_key(parms)
        if not cache_key:
            return None
        obj = cls._cache.get(cache_key)
        if obj:
            info('cache hit:[%s]' % cache_key)
        return obj
     
    @classmethod
    def build_cache_key(cls, parms):
        '''build cache key'''
        if hasattr(cls, 'CACHE_KEY'):
            cache_key_format = cls.CACHE_KEY
        else:
            return None
        cache_keys = cache_key_format.split(':')[1].split(';')
        cache_key_str = '%s:' % cache_key_format.split(':')[0]
        for cache_key in cache_keys:
            if isinstance(parms, dict):
                if cache_key not in parms:
                    return None
                else:
                    cache_key_value = parms[cache_key]
            else:
                if not hasattr(parms, cache_key):
                    return None
                else:
                    cache_key_value = getattr(parms, cache_key)
                    
            cache_key_str += cache_key_value
            cache_key_str += ';'
        cache_key_str = cache_key_str[:-1]
        return cache_key_str
    
    @classmethod
    def count(cls, condition='1=1'):
        '''count the records'''
        cls._init_meta()
        cursor = connection.cursor()
        sql = 'select count(*) as count from %s where %s' % (cls._table, condition)
        try:
            data = cursor.fetchone(sql)
            return data['count']
        finally:
            cursor.close()            
    
    def delete(self, key=None):
        '''delete the object'''
        self._init_meta()
        if not key:
            key = 'id'
        if hasattr(self, key):
            key_value= getattr(self, key)
        else:
            raise 'key value not exist'

        info('start to delete[table:%s, %s=%s]' % (self._table, key, key_value))
        sql = 'delete from %s where %s = %s' % (self._table, key, key_value)
        cache_key = self.build_cache_key(self)
        if cache_key:
            info('start to delete from cache:[%s]' % cache_key)
            self._cache.delete(cache_key)
        cursor = connection.cursor()
        try:
            cursor.execute(sql)
        finally:
            cursor.close()
        info('finish delete')
        
    @classmethod
    def _build_object(cls, data):
        '''build the object'''
        obj = cls()
        meta = Persistable._meta_cache[cls._table]
        columns = meta.columns
        for column in columns:
            if data.has_column(column.field):
                setattr(obj, column.field.lower(), data[column.field])
            else:
                setattr(obj, column.field.lower(), None)
        return obj
    
    @classmethod
    def transaction(cls):
        cursor = connection.cursor()
        info('start transaction...')
        try:
            cursor.execute('start transaction;')
        finally:
            cursor.close()
    
    @classmethod
    def commit(cls):
        cursor = connection.cursor()
        info('commit...')
        try:
            cursor.execute('commit;')
        finally:
            cursor.close()
            
    @classmethod
    def rollback(cls):
        cursor = connection.cursor()
        info('rollback...')
        try:
            cursor.execute('rollback;')
        finally:
            cursor.close()
    
class Meta(object):
    
    def __init__(self, table):
        self._table = table
        self._columns = []
        
    def add_column(self, column):
        self._columns.append(column)
        
    @property
    def columns(self):
        return self._columns
    
    def __str__(self):
        s = '-' * 120 + '\n'
        s += 'table:%s\n' % self._table
        s += 'field' + ' ' * (15-len('field'))    
        s += 'type' + ' ' * (15-len('type'))
        s += 'length' + ' ' * (15-len('length'))
        s += 'is unique' + ' ' * (15-len('is unique'))
        s += 'default' + ' ' * (20-len('default'))
        s += 'extra' + ' ' * (15-len('extra'))
        s += '\n'
        for column in self.columns:
            length = column.length if column.length else ''
            default = column.default if column.default else ''
            extra = column.extra if column.extra else ''
            s += '%s%s%s%s%s%s\n' % \
              (column.field + (' ' * (15-len(column.field))),\
               column.type + (' ' * (15-len(column.type))),\
               length + (' ' * (15-len(length))),\
               str(column.is_unique) + (' ' * (15-len(str(column.is_unique)))),\
               default + (' ' * (20-len(default))),\
               extra + (' ' * (15-len(extra))))
        s += '-' * 120 + '\n'
        return s
    
class Column(object):
    
    TYPE_INT = u'int'
    TYPE_VARCHAR = u'varchar'
    TYPE_DATETIME = u'datetime'
    TYPE_TIMESTAMP = u'timestamp'
    
    def __init__(self, **args):    
        self._args = args
    
    @property
    def field(self):
        if self._args.has_key('field'):
            return self._args['field']
        return None
    
    @property
    def type(self):
        if self._args.has_key('type'):
            return self._args['type']
        return None
    
    @property
    def length(self):
        if self._args.has_key('length'):
            return self._args['length']
        return None
    
    @property
    def is_unique(self):
        if self._args.has_key('is_unique'):
            return self._args['is_unique']
        return None
    
    @property
    def default(self):
        if self._args.has_key('default'):
            return self._args['default']
        return None
    
    @property
    def extra(self):
        if self._args.has_key('extra'):
            return self._args['extra']
        return None

