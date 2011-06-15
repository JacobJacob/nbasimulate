#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection
from xba.common import log_execption

def set_only_one_match():
    """胜者为王挫合"""
    cursor = connection.cursor()
    try:
        sql = "exec SetOnlyOneGame"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
