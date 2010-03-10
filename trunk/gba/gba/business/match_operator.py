#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection

_SELECT_MATCH = 'select * from matchs where %s order by id desc limit %s, %s'
                
_SELECT_MATCH_TOTAL = 'select count(*) as total from matchs where %s order by id desc limit %s, %s'

def get_match(team_id, type, page=1, pagesize=15):
    '''获取比赛记录'''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    
    where = 'home_team_id=%s or guest_team_id=%s and type=%s' %  (team_id, team_id, type)
    
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH % (where, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_MATCH_TOTAL, (where, ))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

def send_match_request(guest_team_id, type):
    '''发送比赛请求'''
    pass