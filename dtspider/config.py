#!/usr/bin/python
# -*- coding: utf-8 -*-
"""project settings"""

import os

DEBUG = True

class PathSettings:
    if os.name == 'nt':
        WORKING_FOLDER = "c:/dtspider_working"
    else:
        WORKING_FOLDER = "/data/appdatas/dtspider_working"
    LOG = WORKING_FOLDER + "/logs"
    EXCEPTION = WORKING_FOLDER + "/exception"
    WEBSERVER_LOG_FOLDER = EXCEPTION + "/lighttpd"
    FILE_LOCKER = WORKING_FOLDER + "/file_locker"
    WEB_STATIC_FILE = WORKING_FOLDER + "/static_file"
    PLANTASKS_FILE = WORKING_FOLDER + "/plantasks.json"
    WEB_LOGS = WORKING_FOLDER + '/nginx/logs' # nginx日志
    if not os.path.exists(EXCEPTION):
        os.makedirs(EXCEPTION)
    if not os.path.exists(FILE_LOCKER):
        os.makedirs(FILE_LOCKER)
    if not os.path.exists(WEB_STATIC_FILE):
        os.makedirs(WEB_STATIC_FILE)
    if not os.path.exists(WEBSERVER_LOG_FOLDER):
        os.makedirs(WEBSERVER_LOG_FOLDER)
    if not os.path.exists(WEB_LOGS):
        os.makedirs(WEB_LOGS)
    PROJECT_FOLDER = os.path.dirname(os.path.abspath(__file__))
    
class DjangoSettings:
    DATABASE_ENGINE = 'mysql'
    DATABASE_NAME = 'ant'
    DATABASE_USER = 'ant'
    DATABASE_PASSWORD = 'ant123'
    DATABASE_HOST = '192.168.1.152'
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

COMMON_CACHE = ["127.0.0.1:11212"]
COMMON_CACHE_CMD = 'memcached -d -m 1024 -p 11212 -u dtspider'

MinDiskSize = 10 * 1024 * 1024 * 1024

SYSLOG_HOST = 'localhost'
SERVICE_HOST = '192.168.1.152:8080'


# 数据同步服务器
DATA_SYNC_HOST = 'localhost'
