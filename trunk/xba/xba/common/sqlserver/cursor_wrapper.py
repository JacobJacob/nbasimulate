#-*- coding:utf-8 -*-

from pymssql import OperationalError
from weakref import ref
from xba.common import log_execption

class Cursor(object):
    def __init__(self, db_cursor, conn):
        self._cursor = db_cursor
        self._conn = ref(conn)
        self._pool = None
        self._closed = False
        self._need_close_conn = False
    
    def __del__(self):
        self.close()
        
    def fetchall(self):
        try:
            return self._cursor.fetchall()
        except OperationalError:
            self._need_close_conn = True
            raise
        
    def execute(self, operation, *args):
        try:
            return self._cursor.execute(operation, *args)
        except OperationalError:
            log_execption()
            self._need_close_conn = True
            raise
    
    def fetchone(self):
        try:
            return self._cursor.fetchone()
        except OperationalError:
            self._need_close_conn = True
            raise    
        
    def close(self):
        if not self._closed:
            if self._cursor:
                self._cursor.close()
            if self._conn:
                conn = self._conn
                self._conn = None
                if self._need_close_conn:
                    conn().close()
                else:
                    if self._pool:
                        self._pool.release(conn)
        self._closed = True
        
    def start_transaction(self):
        self._conn().start_transaction()
         
    def rollback(self):
        self._conn().rollback()
        
    def commit(self):
        self._conn().commit()
    
    def __getattr__(self, name):
        return getattr(self._cursor, name)
    