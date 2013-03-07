#!/usr/bin/python
# -*- coding: utf-8 -*-
"""project settings"""

import os

DEBUG = False

class PathSettings:
    if os.name == 'nt':
        WORKING_FOLDER = "D:/data/appdatas/xba_working"
    else:
        WORKING_FOLDER = "/data/appdatas/xba_working"
    LOG = WORKING_FOLDER + "/logs"
    EXCEPTION = WORKING_FOLDER + "/exception"
    WEBSERVER_LOG_FOLDER = EXCEPTION + "/lighttpd"
    FILE_LOCKER = WORKING_FOLDER + "/file_locker"
    WEB_STATIC_FILE = WORKING_FOLDER + "/static_file"
    PLANTASKS_FILE = WORKING_FOLDER + "/plantasks.json"
    WEB_LOGS = WORKING_FOLDER + '/nginx/logs' # nginx日志
    DB_BACKUP = WORKING_FOLDER + '/sqlserver/backup' # nginx日志
    ROUND_UPDATE_LOCK = WORKING_FOLDER + '/roundupdatelock' # nginx日志
    TRIAN_PLAYER_UPDATE_LOCK = WORKING_FOLDER + '/trianplayerupdatelock' # nginx日志
    PROMOTION_LOG_PATH = WORKING_FOLDER + '/promotion_log' #推广积分日志文件
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
    if not os.path.exists(LOG):
        os.makedirs(LOG)
    if not os.path.exists(DB_BACKUP):
        os.makedirs(DB_BACKUP)
    if not os.path.exists(ROUND_UPDATE_LOCK):
        os.makedirs(ROUND_UPDATE_LOCK)
    if not os.path.exists(TRIAN_PLAYER_UPDATE_LOCK):
        os.makedirs(TRIAN_PLAYER_UPDATE_LOCK)
    if not os.path.exists(PROMOTION_LOG_PATH):
        os.makedirs(PROMOTION_LOG_PATH)
    PROJECT_FOLDER = os.path.dirname(os.path.abspath(__file__))

class DbSetting:
    DATABASE_NAME = 'NewBTP'
    DATABASE_USER = 'sa'
    DATABASE_PASSWORD = '123456'
    DATABASE_HOST = '127.0.0.1'
    #DATABASE_HOST = 'www.113388.net'
    
class DjangoSettings:
    DATABASE_ENGINE = 'mysql'           # 'postgresql_psycopg2', 'postgresql', 'mysql', 'sqlite3' or 'oracle'.
    DATABASE_NAME = 'xba'
    #DATABASE_USER = 'from gba'             # Not used with sqlite3.
    #DATABASE_PASSWORD = 'from gba.123'
    #DATABASE_HOST = '10.20.238.169'
    DATABASE_USER = 'root'
    DATABASE_PASSWORD = '123456'
    DATABASE_HOST = 'localhost'
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

if DEBUG:
    CLIENT_EXE_PATH = "D:\\develop\\xba_workspace\\src\\Client\\Client\\bin\\Release\\Client.exe"
    #DOMAIN = 'http://localhost:34591/xbaweb/'
    DOMAIN = 'http://n1.113388.net/'
    WEB_ROOT = "D:\\develop\\xba_workspace\\xbaweb"
else:
    CLIENT_EXE_PATH = "E:\\xba_app\\xba\\doc\\bin\\Client.exe"
    DOMAIN = 'http://n1.113388.net/'
    WEB_ROOT = "D:\\xba_root\\xbaweb"
    
DOMAINS = ["http://localhost:34591/xbaweb/", "http://n1.113388.net/"]
     
NGINX_ACCESS_LOG = "E:\\nginx-0.8.33\\logs\\access.log"

COMMON_CACHE = ["192.168.1.158:11211"]
SYSLOG_HOST = '127.0.0.1'