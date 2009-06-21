#-*- coding:utf-8 -*-
from threading import Lock, Condition

from connection_wrapper import Connection
from webauth.config import DjangoSettings
    
class ConnectionPool(object):
    """线程安全的数据库连接池"""
    MAX_COUNT = 20 # 最大同时连接数
    _connections = []
    _hold_count = 20 # 连接允许的持有数
    _conn_locker = Lock()
    _waiter = Condition(Lock())
    _count = 0 # 连接请求计数
    
    def __init__(self):
        raise Exception("only class members can be called")
    
    @classmethod
    def acquire(cls, wait_timeout = 4):
        """请求数据库连接"""
        cls._wait() # 检测是否已达到最大连接数，若达到，则等待直到有可用的连接为止
        cls._conn_locker.acquire()
        try:
            if cls._connections:
                conn = cls._connections.pop(0)
            else:
                conn = Connection(
                                  user = DjangoSettings.DATABASE_USER,
                                  db = DjangoSettings.DATABASE_NAME,
                                  passwd = DjangoSettings.DATABASE_PASSWORD,
                                  host = DjangoSettings.DATABASE_HOST,
                                  port = DjangoSettings.DATABASE_PORT,
                                  charset = "utf8",
                                  unix_socket = DjangoSettings.DATABASE_HOST,
                                  init_command = "set wait_timeout = %s;" % wait_timeout
                                  )
            return conn
        finally:
            cls._conn_locker.release()
    
    @classmethod
    def release(cls, conn):
        if not conn:
            raise Exception("conn is required")
        
        cls._conn_locker.acquire()
        try:
            if len(cls._connections) >= cls._hold_count:
                conn.close()
            else:
                cls._connections.insert(0, conn)
        finally:
            cls._conn_locker.release()
            cls._notify()
    
    @classmethod
    def cursor(cls, wait_timeout = 4):
        """获取当前可用的游标
        
        使用完务必调用close()，以释放数据库连接
        """
        conn = cls.acquire(wait_timeout)
        try:
            c = conn.cursor()
            c._add2pool(cls)
        except:
            conn.close()
            cls.release(conn)
            raise
        return c
    
    @classmethod
    def close(cls):
        """关闭所有连接"""
        cls._conn_locker.acquire()
        try:
            while cls._connections:
                conn = cls._connections.pop(0)
                conn.close()
                cls._notify()
        finally:
            cls._conn_locker.release()

    @classmethod
    def _wait(cls):
        if cls.MAX_COUNT > 0:
            cls._waiter.acquire()
            cls._count += 1 # _count 最大值为 MAX_COUNT + 1
            if cls._count > cls.MAX_COUNT:
                cls._waiter.wait()
            cls._waiter.release()
    
    @classmethod
    def _notify(cls):
        if cls.MAX_COUNT > 0:
            cls._waiter.acquire()
            if cls._count > 0:
                cls._count -= 1
            cls._waiter.notify()
            cls._waiter.release()