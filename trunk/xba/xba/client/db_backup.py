#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from datetime import datetime

from xba.config import PathSettings
from xba.common import file_utility
from xba.common import log_execption

def backup(db="NewBTP", file_name=None):
    """备份数据库"""
    if not file_name:
        file_name = db
    file_name = "%s_%s" % (file_name, datetime.now().strftime("%Y_%m_%d_%H_%M_%S"))
    back_dir = os.path.join(PathSettings.DB_BACKUP, datetime.now().strftime("%Y_%m_%d"))
    path = os.path.join(back_dir, "%s.bak" % file_name)
    file_utility.ensure_dir_exists(back_dir)
    if os.path.exists(path):
        os.remove(path)
    cmd = "sqlcmd -Q \"backup database [%s] to disk='%s'\"" % (db, path)
    try:
        os.system(cmd)
    except:
        log_execption()
        
def restore(db="NewBTP", file_name=None):
    """还原数据库"""
    path = os.path.join(PathSettings.DB_BACKUP, file_name)
    cmd = "sqlcmd -Q \"restore database [%s] from disk='%s' with replace\"" % (db, path)
    try:
        os.system(cmd)
    except:
        log_execption()
    
if __name__ == "__main__":
    restore(file_name="2011_07_02/NewBTP_2011_07_02_12_30_31.bak")
    #backup()