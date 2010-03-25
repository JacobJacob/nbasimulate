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

_SELECT_MESSAGE = 'select * from message where to_team_id=%s order by id desc limit %s, %s'
                
_SELECT_MESSAGE_TOTAL = 'select count(*) as count from message where to_team_id=%s'

def get_message(team_id, page=1, pagesize=15):
    '''获取消息'''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MESSAGE % (team_id, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_MESSAGE_TOTAL, (team_id, ))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

_UPDATE_MESSAGE_TO_OLD = 'update message set is_new=0 where id in (%s)'

def update_message_to_old(message_ids):
    '''将消息更新为已读'''
    cursor = connection.cursor()
    try:
        return cursor.execute(_UPDATE_MESSAGE_TO_OLD % ','.join(['%s' % id for id in message_ids]))
    finally:
        cursor.close()

        
if __name__ == '__main__':
    get_message(1)