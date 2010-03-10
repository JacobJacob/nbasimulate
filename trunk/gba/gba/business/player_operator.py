#-*- coding:utf-8 -*-

from gba.common.db import connection

_SELECT_FREE_PLAYER = 'select * from free_player where position="%s" order by %s %s limit %s, %s '
                       
_SELECT_FREE_PLAYER_TOTAL = 'select count(*) as count from free_player where position=%s'

def get_free_palyer(page=1, pagesize=30, position='C', order_by='id', order='desc'):
    '''获取需要人工确认的恶意电话
    '''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    print _SELECT_FREE_PLAYER % (position, order_by, order, index, pagesize)
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_FREE_PLAYER % (position, order_by, order, index, pagesize))
        if rs:
            infos = rs.to_list()
            print _SELECT_FREE_PLAYER_TOTAL % (position, )
            rs = cursor.fetchone(_SELECT_FREE_PLAYER_TOTAL, (position, ))
            print rs
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

_LOAD_FREE_PLAYER = 'select * from free_player where id=%s'

def get_free_palyer_by_id(id):
    '''获取自由球员详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_FREE_PLAYER, (id, ))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()
        
_SELECT_PROFESSION_PLAYER = 'select * from profession_player where team_id=%s order by ability desc'

def get_profession_player(team_id):
    '''获取某队职业球员'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_PROFESSION_PLAYER , (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_LOAD_PROFESSION_PLAYER = 'select * from profession_player where id=%s'

def get_profession_palyer_by_id(id):
    '''获取职业球员详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_PROFESSION_PLAYER, (id, ))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()

if __name__ == '__main__':
    print get_free_palyer()[1]