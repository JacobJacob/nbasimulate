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
        
    return path
        
def restore(db="NewBTP", file_name=None):
    """还原数据库"""
    print PathSettings.DB_BACKUP
    print file_name
    #path = os.path.join(PathSettings.DB_BACKUP, file_name)
    path = file_name
    print path
    cmd = "sqlcmd -Q \"restore database [%s] from disk='%s' with replace\"" % (db, path)
    try:
        os.system(cmd)
    except:
        log_execption()
    
if __name__ == "__main__":
    restore(file_name="D:\\round_update_handler_2011_10_22_23_59_06.bak")
    #restore(file_name="2011_09_10/round_update_handler_2011_09_10_10_35_03.bak")
    #backup()