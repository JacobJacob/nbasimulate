#-*- coding:utf-8 -*-
#from connection_pool import ConnectionPool
from connection_wrapper import Connection as __Connection
from xba.config import DbSetting as __DbSetting
connection = __Connection(
                          user = __DbSetting.DATABASE_USER,
                          database = __DbSetting.DATABASE_NAME,
                          password = __DbSetting.DATABASE_PASSWORD,
                          host = __DbSetting.DATABASE_HOST,
                          charset = "gbk",
                          as_dict= True
                          )
del __DbSetting
del __Connection