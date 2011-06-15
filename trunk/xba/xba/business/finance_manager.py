#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common import log_execption
from xba.common.sqlserver import connection
        
def delete_turn_finance(season):
    """删除每天财政"""
    cursor = connection.cursor()
    try:
        sql = "EXEC DeleteTurnFinance %s" % season
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
       
if __name__ == "__main__":
    pass