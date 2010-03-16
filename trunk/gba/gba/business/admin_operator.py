#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral

_SELECT_ACTION = 'select * from action_desc where %s limit %s, %s'        
_SELECT_ACTION_TOTAL = 'select count(*) as count from action_desc where %s'

def get_action_desc(action_name=None, page=1, pagesize=15):
    ''''''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    where = '1=1'
    if action_name:
        where = 'action_name="%s"' % action_name
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_ACTION % (where, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_ACTION_TOTAL % where)
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

def add_action_desc(info):
    ''''''
    info['created_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(info, 'action_desc', True, ['created_time', ])
    finally:
        cursor.close()
        
if __name__ == '__main__':
    pass