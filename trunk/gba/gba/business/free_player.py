#-*- coding:utf-8 -*-

from gba.common.db import connection

_SELECT_FREE_PLAYER = 'select * from free_player where position="%s" order by %s %s limit %s, %s '
                       
_SELECT_FREE_PLAYER_TOTAL = 'select count(*) as count from free_player where position="%s"'

def get_free_palyer(page=1, pagesize=30, position='C', order_by='id', order='desc'):
    '''获取需要人工确认的恶意电话
    '''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []

    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_FREE_PLAYER % (position, order_by, order, index, pagesize))
        if rs:
            phone_infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_FREE_PLAYER_TOTAL, (position, ))
            total = rs['count']
    finally:
        cursor.close()
        
    return phone_infos, total