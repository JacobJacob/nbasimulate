#-*- coding:utf-8 -*-

import pymssql
from xba.common.sqlserver.cursor_wrapper import Cursor
from threading import Lock

class Connection(object):
    def __init__(self, *args, **kwargs):
        """please use host, port, db, user, passwd etc. kwargs to init Connection"""
        self._authInfo = (args, kwargs)
        self._connection = None
        self._locker = Lock()

    def __del__(self):
        self._close()
        
    def close(self):
        self._locker.acquire()
        try:
            self._close()
        finally:
            self._locker.release()
    
    def is_connected(self):
        return self._connection is not None
    
    def ensure_connected(self):
        self._locker.acquire()
        try:
            if not self.is_connected():
                self._connection = pymssql.connect(*self._authInfo[0], **self._authInfo[1])
                self._connection.autocommit(True)
        finally:
            self._locker.release()
        
    def cursor(self, check_conn = True):
        if check_conn or self._connection is None:
            self.ensure_connected()
        c = self._connection.cursor()
        return Cursor(c, self)
    
    def rollback(self):
        self._connection.rollback()
        self._connection.autocommit(True)
        
    def start_transaction(self):
        self._connection.autocommit(False)
        
    def commit(self):
        self._connection.commit()
        self._connection.autocommit(True) 
 
    def _close(self):
        if self._connection:
            self._connection.close()
            self._connection = None

    def __getattr__(self, name):
        return getattr(self._connection, name)

        