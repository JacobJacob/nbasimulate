#-*- coding:utf-8 -*-
#from connection_pool import ConnectionPool
from connection_wrapper import Connection as __Connection

connection = __Connection(
                          user = 'gba',
                          db = 'gba',
                          passwd = 'gba123',
                          host = '192.168.1.158',
                          port = 3306,
                          charset = "utf8",
                          init_command = "set wait_timeout = 300"
                          )
del __Connection
from connection_pool import ConnectionPool

__all__ = ("connection", "ConnectionPool")