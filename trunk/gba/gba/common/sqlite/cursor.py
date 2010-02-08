#-*- coding:utf-8 -*-
from weakref import ref

class Cursor():
    
    INSERT_MOD_ROLLBACK = 'rollback'
    INSERT_MOD_ABORT  = 'abort'
    INSERT_MOD_FAIL  = 'fail'
    INSERT_MOD_IGNORE  = 'ignore'
    INSERT_MOD_REPLACE = 'replace'
    
    def __init__(self, cursor=None, connection=None):
        self._cursor = cursor
        self._connection = ref(connection)
        self._closed = False
        
    def insert(self, data, table, mode='replace'):
        """insert sql"""
        if not data:
            return 0, 0
        if isinstance(data, dict):
            datas = (data,)
        else:
            datas = data
        columns = []
        values = []
        for k in datas[0].keys():
            columns.append(k)
        if not columns:
            return 0, 0
        for d in datas:
            values.append([d[k] for k in columns])
        if mode:
            sql_header = 'insert or %s into' % mode
        else:
            sql_header = 'insert into '
        sql = '%s %s (%s) values(%s)' % (sql_header, table,
            ','.join(columns), ','.join(['?' for k in columns]))
        #print sql
        return self.executemany(sql, values)
        
    def update(self, data, table, key):
        """update"""
        if not data:
            return 0
        if isinstance(key, basestring):
            keys = (key, )
        else:
            keys = key
        
        if isinstance(data, dict):
            datas = (data,)
        else:
            datas = data
        columns = []
        values = []
        for k in datas[0].keys():
            if k not in keys:
                columns.append(k)
        if not columns:
            return 0
        for d in datas:
            i = [d[k] for k in columns]
            for key in keys:
                i.append(d[key])
            values.append(i)
            
        items = ['%s=?' % k for k in columns]
        sql = 'update %s set %s where %s' % \
            (table, ','.join(items), ' and '.join(['%s=?' % k for k in keys]))
        print sql
        return self.executemany(sql, values)
    
    def fetchone(self, sql, parm=[]):
        """fetch one"""
        self.execute(sql, parm)
        return self.fetchone()
    
    def fetchall(self, sql, parm=[]):
        """fetch all""" 
        self.execute(sql, parm)
        return self._cursor.fetchall()       
        
    def close(self):
        """close the cursor"""
        if not self._closed:
            if self._cursor:
                self._cursor.close()
            if self._connection:
                connection = self._connection
                self._connection = None
        self._closed = True    
        
    def __getattr__(self, name):
        return getattr(self._cursor, name)
