#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection

_SELECT_ONLINE_USERS = 'select * from user_info where last_active_time>=date_sub(now(),interval 30 minute) order by id desc limit %s, %s'
                
_SELECT_ONLINE_USERS_TOTAL = 'select count(*) as count from user_info where last_active_time>=date_sub(now(),interval 30 minute)'

def get_online_users(page=1, pagesize=15):
    '''获取比赛记录'''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_ONLINE_USERS % (index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_ONLINE_USERS_TOTAL)
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

        
if __name__ == '__main__':
    pass