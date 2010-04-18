#-*- coding:utf-8 -*-
#from connection_pool import ConnectionPool
from connection_wrapper import Connection as __Connection
from gba.config import DjangoSettings

connection = __Connection(
                          user = DjangoSettings.DATABASE_USER,
                          db = DjangoSettings.DATABASE_NAME,
                          passwd = DjangoSettings.DATABASE_PASSWORD,
                          host = DjangoSettings.DATABASE_HOST,
                          port = 3306,
                          charset = "utf8",
                          init_command = "set wait_timeout = 300"
                          )
del __Connection
from connection_pool import ConnectionPool

__all__ = ("connection", "ConnectionPool")