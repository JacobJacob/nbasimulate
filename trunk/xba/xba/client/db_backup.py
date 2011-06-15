#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from datetime import datetime

from xba.common import file_utility
from xba.common import log_execption

def backup(db="NewBTP", path="D:\\xba_working\\"):
    file_utility.ensure_dir_exists(path)
    path = os.path.join(path, db + datetime.now().strftime("_%Y_%m_%d.bak"))
    if os.path.exists(path):
        os.remove(path)
    cmd = "sqlcmd -Q \"backup database [%s] to disk='%s'\"" % (db, path)
    print cmd
    try:
        os.system(cmd)
    except:
        log_execption()
        
def restore(db="NewBTP", path="D:\\xba_working\\"):
    path = os.path.join(path, db + datetime.now().strftime("_%Y_%m_%d.bak"))
    cmd = "sqlcmd -Q \"restore database [%s] from disk='%s' with replace\"" % (db, path)
    print cmd
    try:
        os.system(cmd)
    except:
        log_execption()
    
if __name__ == "__main__":
    restore()
    #backup()