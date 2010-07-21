#-*- coding:utf-8 -*-
#from connection_pool import ConnectionPool
from connection_wrapper import Connection as __Connection
from dtspider.config import DjangoSettings as __DjangoSettings
connection = __Connection(
                          user = __DjangoSettings.DATABASE_USER,
                          db = __DjangoSettings.DATABASE_NAME,
                          passwd = __DjangoSettings.DATABASE_PASSWORD,
                          host = __DjangoSettings.DATABASE_HOST,
                          port = __DjangoSettings.DATABASE_PORT,
                          charset = "utf8",
                          unix_socket = __DjangoSettings.DATABASE_HOST,
                          init_command = "set wait_timeout = 300"
                          )
del __Connection
del __DjangoSettings
from connection_pool import ConnectionPool

__all__ = ("connection", "ConnectionPool")
