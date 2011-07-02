#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection
from xba.common import log_execption

def update_arrange_lvl(category):
    """更新战术等级"""
    cursor = connection.cursor()
    try:
        cursor.execute("Exec UpdateArrange%sLvl" % category)
    except:
        log_execption()
    finally:
        cursor.close()
        
if __name__ == "__main__":
    pass