#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
      
        
def set_user_ability_top200():
    """更新职业排名"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC SetUserAbilityTop200")
    finally:
        cursor.close()