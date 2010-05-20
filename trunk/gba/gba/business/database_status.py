#!/usr/bin/python
# -*- coding: utf-8 -*-
"""数据库状态"""


from gba.common.db import connection

def get_connections():
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall('show full processlist')
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
