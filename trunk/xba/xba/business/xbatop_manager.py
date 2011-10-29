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
        
def set_team_ability():
    """更球队综合排名"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC SetTeamAbility")
    finally:
        cursor.close()
        
def get_top10_account():
    """获取球队综合排名前十的球员"""
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT TOP 8 nickname, teamability FROM BTP_Account ORDER BY teamability DESC")
        return cursor.fetchall()
    finally:
        cursor.close()
        
        