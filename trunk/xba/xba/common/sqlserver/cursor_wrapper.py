#-*- coding:utf-8 -*-

from weakref import ref

class Cursor(object):
    def __init__(self, db_cursor, conn):
        self._cursor = db_cursor
        self._conn = ref(conn)
        self._pool = None
        self._closed = False
    
    def __del__(self):
        self.close()
        
    def close(self):
        if not self._closed:
            if self._cursor:
                self._cursor.close()
            if self._conn:
                conn = self._conn
                self._conn = None
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
    