#-*- coding:utf-8 -*-

import traceback

from gba.common.db import connection
from gba.common import log_execption
from gba.entity import Team, YouthFreeplayerAuctionLog, YouthFreePlayer

_SELECT_FREE_PLAYER = 'select *, unix_timestamp(expired_time)-unix_timestamp(now()) as lave_time from free_player where position="%s" order by %s %s limit %s, %s '
                       
_SELECT_FREE_PLAYER_TOTAL = 'select count(*) as count from free_player where position=%s'

def get_free_palyer(page=1, pagesize=30, position='C', order_by='id', order='desc'):
    '''自由球员
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
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_FREE_PLAYER_TOTAL, (position, ))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

_SELECT_YOUTH_FREEPLAYER = 'select *, unix_timestamp(expired_time)-unix_timestamp(now()) as lave_time from youth_free_player where position="%s" order by %s %s limit %s, %s '
                       
_SELECT_YOUTH_FREEPLAYER_TOTAL = 'select count(*) as count from youth_free_player where position=%s'

def get_youth_freepalyer(page=1, pagesize=30, position='C', order_by='id', order='desc'):
    '''年轻自由球员
    '''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_YOUTH_FREEPLAYER % (position, order_by, order, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_YOUTH_FREEPLAYER_TOTAL, (position, ))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

_LOAD_YOUTH_FREE_PLAYER = 'select * from youth_free_player where no=%s'

def get_youth_freepalyer_by_no(no):
    '''获取年轻自由球员详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_YOUTH_FREE_PLAYER, (no, ))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()

_LOAD_FREE_PLAYER = 'select * from free_player where no=%s'

def get_free_palyer_by_no(no):
    '''获取自由球员详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_FREE_PLAYER, (no, ))
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
        
def youth_freeplayer_auction(team_id, no, price):
    '''年轻球员出价
    @return price: -1 未知异常 - 2 资金不够  1 竞价成功 -3 已经竞过价  -4 球员信息不存在
    '''
    print no, price
    team = Team.load(id=team_id)
    if not team:
        return -1
    funds = team.funds
    print funds
    if funds < int(price):
        return -2
    youth_freeplayerauction_log = YouthFreeplayerAuctionLog.load(username=team.username)
    if youth_freeplayerauction_log:
        return -3
    
    youth_freeplayer = YouthFreePlayer.load(no=no)
    if not youth_freeplayer:
        return -4
        
    
    youth_freeplayer_auction_log = YouthFreeplayerAuctionLog()
    youth_freeplayer_auction_log.username = team.username
    youth_freeplayer_auction_log.price = price
    youth_freeplayer_auction_log.player_no = no
    
    youth_freeplayer.bid_count = youth_freeplayer.bid_count + 1
    
    YouthFreeplayerAuctionLog.transaction()
    try:
        youth_freeplayer_auction_log.persist()
        youth_freeplayer.persist()
        YouthFreeplayerAuctionLog.commit()
        return 1
    except:
        log_execption()
        YouthFreeplayerAuctionLog.rollback()
        
def check_has_auction(username):
    '''验证某个用户是否已经出过价'''
    youth_freeplayerauction_log = YouthFreeplayerAuctionLog.load(username=username)
    if youth_freeplayerauction_log:
        return True
    return False
if __name__ == '__main__':
    print get_free_palyer()[1]