#!/usr/bin/python
# -*- coding: utf-8 -*-
"""project settings"""

import os

DEBUG = True

class PathSettings:
    if os.name == 'nt':
        WORKING_FOLDER = "e:/appdatas/gba_working"
        if not os.path.exists('d:/'):
            WORKING_FOLDER = "c:/appdatas/gba_working"
    else:
        WORKING_FOLDER = "/data/appdatas/gba_working"
    LOG = WORKING_FOLDER + "/logs"
    EXCEPTION = WORKING_FOLDER + "/exception"
    FILE_LOCKER = WORKING_FOLDER + "/file_locker"
    WEB_STATIC_FILE = WORKING_FOLDER + "/static_file"
    PLANTASKS_FILE = WORKING_FOLDER + "/plantasks.json"
    WEB_LOGS = WORKING_FOLDER + '/nginx/logs' # nginx日志
    RRD_FOLDER = WORKING_FOLDER + '/rrd'
    if not os.path.exists(EXCEPTION):
        os.makedirs(EXCEPTION)
    if not os.path.exists(FILE_LOCKER):
        os.makedirs(FILE_LOCKER)
    if not os.path.exists(WEB_STATIC_FILE):
        os.makedirs(WEB_STATIC_FILE)
    if not os.path.exists(WEB_LOGS):
        os.makedirs(WEB_LOGS)
    if not os.path.exists(RRD_FOLDER):
        os.makedirs(RRD_FOLDER)
    if not os.path.exists(LOG):
        os.makedirs(LOG)
    PROJECT_FOLDER = os.path.dirname(os.path.realpath(__file__))
    
class DjangoSettings:
    DATABASE_ENGINE = 'mysql'           # 'postgresql_psycopg2', 'postgresql', 'mysql', 'sqlite3' or 'oracle'.
    DATABASE_NAME = 'from gba'
    #DATABASE_USER = 'from gba'             # Not used with sqlite3.
    #DATABASE_PASSWORD = 'from gba.123'
    #DATABASE_HOST = '10.20.238.169'
    DATABASE_USER = 'gba'
    DATABASE_PASSWORD = 'gba123'
    DATABASE_HOST = '192.168.1.158'
    DATABASE_PORT = 3306
        
    URL_PREFIX = ''
    FILE_UPLOAD_TEMP_DIR = PathSettings.WORKING_FOLDER + "/file_upload_temp"
    if not os.path.exists(FILE_UPLOAD_TEMP_DIR):
        os.makedirs(FILE_UPLOAD_TEMP_DIR)
    WEB_ROOT = PathSettings.WORKING_FOLDER + '/www' # static-generator
    if not os.path.exists(WEB_ROOT):
        os.makedirs(WEB_ROOT)
    if not os.path.exists(WEB_ROOT + '/rrd'): # rrd image path
        os.makedirs(WEB_ROOT + '/rrd')

COMMON_CACHE = ["192.168.1.158:11211"]
COMMON_CACHE_CMD = 'memcached -d -m 1024 -p 11212 -u from gba'

MinDiskSize = 10 * 1024 * 1024 * 1024

SYSLOG_HOST = '127.0.0.1'
#SERVICE_HOST = '10.20.238.173'
SERVICE_HOST = '127.0.0.1:1000'

# 数据同步服务器
DATA_SYNC_HOST = '10.20.238.194'