#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import time
from datetime import datetime

from xba.common import extract7z
from xba.client import db_backup
from xba.common.ftp_lib import FtpEx

def log(msg):
    s= "[%s]%s" % (datetime.now().strftime("%Y-%m-%d %H:%M:%S.log"), msg)
    print s

def zip(path):
    log("start to zip")
    target_path = extract7z.zip(path)
    log("finish to zip")
    return target_path
 
def upload(path):
    """压缩并上传"""
    i = 0
    while i < 10:
        try:
            ftp = get_ftp()
            log("start to upload:%s" % path)
            ftp.upload(path, "/sxwailyc/db/")
            log("upload finish")
            break
        except:
            log(traceback.format_exc())
            log("upload error")
            time.sleep(60)
        i += 1
        
def get_ftp():
    ftp = FtpEx()
    ftp.host = "222.73.85.199"
    ftp.user = "sxwailyc"
    ftp.password = "EEE2728B29d6cc"
    ftp.set_pasv(False)
    ftp.ensure_login()
    return ftp
 
def main():
    
    path = db_backup.backup()
    path = zip(path)
    upload(path)
    
if __name__ == "__main__":
    upload("D:/data/appdatas/xba_working/sqlserver/backup/2011_10_22\NewBTP_2011_10_22_23_33_03.bak.7z")