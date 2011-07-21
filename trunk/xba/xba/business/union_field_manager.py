#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection

def day_update_union_field_game():
    """处理过斯盟战"""
    cursor = connection.cursor()
    try:
        sql = "exec DayUpdateUnionFieldGame"
        cursor.execute(sql)
    finally:
        connection.close()
        
def night_update_delete_union():
    """夜间删除威望小于1的联盟"""
    cursor = connection.cursor()
    try:
        sql = "exec NightUpdateDeleteUnion"
        cursor.execute(sql)
    finally:
        connection.close()
           
if __name__ == "__main__":
    night_update_delete_union()
