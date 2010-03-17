#-*- coding:utf-8 -*-

import traceback

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
        
_LOAD_PROFESSION_PLAYER = 'select * from profession_player where no=%s'

def get_profession_palyer_by_no(no):
    '''获取职业球员详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_PROFESSION_PLAYER, (no, ))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()
        

def _check_player_is_in_team(team_id, no, cursor=None):
    '''确认某球员在某队中'''
    need_close = False
    if not cursor:
        need_close = True
        cursor = connection.cursor()
    try:
        return cursor.fetchone('select 1 from profession_player where team_id=%s and no=%s', (team_id, no))
    finally:
        if need_close:
            cursor.close()

def update_profession_player(team_id, info):
    '''更新职业球员信息，只能更新不位置，号码， 名字等信息
    @return: 1 成功, 0 失败m -1 该球员不在队中
    '''
    data = {}
    no = info['no']
    data['no'] = no
    if info.get('player_no'):
        data['player_no'] = info.get('player_no')
    if info.get('position'):
        data['position'] = info.get('position')
    if info.get('name'):
        data['name'] = info.get('name')
        
    cursor = connection.cursor()
    try:
        if _check_player_is_in_team(team_id, no, cursor):
            cursor.update(data, 'profession_player', ['no'])
            return 1
        else:
            return -1
    except:
        print traceback.format_exc(3)
        return 0
    finally:
        cursor.close()
    
if __name__ == '__main__':
    print get_free_palyer()[1]