#-*- coding:utf-8 -*-

import traceback
from datetime import datetime

from gba.common.db import connection
from gba.common import log_execption
from gba.entity import Team, YouthFreeplayerAuctionLog, YouthFreePlayer, FreePlayer, \
                       AttentionPlayer, DraftPlayer
from gba.common.constants import MarketType

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

def get_youth_freepalyer(page=1, pagesize=10, position='C', order_by='expired_time', order='asc'):
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
        
_SELECT_PROFESSION_PLAYER = 'select * from profession_player where team_id=%s order by is_draft asc, ability desc'

def get_profession_player(team_id):
    '''获取某队职业球员'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_PROFESSION_PLAYER , (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_SELECT_YOUTH_PLAYER = 'select * from youth_player where team_id=%s order by ability desc'

def get_youth_player(team_id):
    '''获取年轻球员'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_YOUTH_PLAYER , (team_id, ))
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
        

def _check_player_is_in_team(team_id, no, is_youth=False, cursor=None):
    '''确认某球员在某队中'''
    need_close = False
    if not cursor:
        need_close = True
        cursor = connection.cursor()
    try:
        if is_youth:
            return cursor.fetchone('select 1 from youth_player where team_id=%s and no=%s', (team_id, no))
        else:
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
        
def update_profession_players(team_id, infos):
    '''批量更新职业球员信息，只能更新训练项等信息'''
   
    cursor = connection.cursor()
    try:
        datas = []
        for info in infos:
            data = {}
            no = info['no']
            if _check_player_is_in_team(team_id, no, cursor):
                data['no'] = no
                data['training'] = info.get('training')
                datas.append(data)
        cursor.update(datas, 'profession_player', ['no'])
        return 1
    except:
        log_execption()
        return 0
    finally:
        cursor.close()
        
def youth_freeplayer_auction(team_id, no, price):
    '''年轻球员出价
    @return price: -1 未知异常 - 2 资金不够  1 竞价成功 -3 已经竞过价  -4 球员信息不存在
    '''
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
    
    attention_player = AttentionPlayer()
    attention_player.team_id = team_id
    attention_player.no = no
    attention_player.type = MarketType.YOUTH_FREE
    
    
    YouthFreeplayerAuctionLog.transaction()
    try:
        youth_freeplayer_auction_log.persist()
        youth_freeplayer.persist()
        attention_player.persist()
        YouthFreeplayerAuctionLog.commit()
        return 1
    except:
        log_execption()
        YouthFreeplayerAuctionLog.rollback()
        
def freeplayer_auction(team_id, no, price):
    '''职业球员出价
    @return price:  1 竞价成功   -1 未知异常 - 2 资金不够    -3 已经竞过价  -4 球员信息不存在  -5 出价过底
    '''
    team = Team.load(id=team_id)
    if not team:
        return -1
    funds = team.funds
    hold_funds = team.hold_funds
    if (funds-hold_funds) < int(price):
        return -2

    free_player = FreePlayer.load(no=no)
    if not free_player:
        return -4
    
    current_team_id = free_player.current_team_id
    if current_team_id and current_team_id == team_id:
        return -3
    
    current_price = free_player.current_price
    worth = free_player.worth
    if (current_price and current_price >= int(price)) or worth > price:
        return -5

    free_player.bid_count = free_player.bid_count + 1
    free_player.current_price = price
    free_player.current_team_id = team_id
    
    attention_player = AttentionPlayer()
    attention_player.team_id = team_id
    attention_player.no = no
    attention_player.type = MarketType.FREE
    
    FreePlayer.transaction()
    try:
        free_player.persist()
        attention_player.persist()
        FreePlayer.commit()
        return 1
    except:
        log_execption()
        FreePlayer.rollback()
        
def check_has_auction(username):
    '''验证某个用户是否已经出过价'''
    youth_freeplayerauction_log = YouthFreeplayerAuctionLog.load(username=username)
    if youth_freeplayerauction_log:
        return True
    return False

def get_free_auction_info(username, no):
    '''验证某个用户是否已经在自由球员那出过价,是否已经过了超时时间, 已及要出价球员的身价
    判断hold_funds > 0
    '''
    team = Team.load(username=username)
    free_player = FreePlayer.load(no=no)
    expired_time = free_player.expired_time
    res = 1
    if expired_time < datetime.now():
        res = -1
    elif team.hold_funds > 0:
        res = -2
    return res, free_player.worth, team.funds - (team.hold_funds if team.hold_funds else 0)

def attention_player(team_id, player_no, type):
    '''关注球员
    @return 1 关注成功  2 已经关注过了  3 关注球员超过10个  -1 未知异常
    '''
    
    attention_player = AttentionPlayer.load(no=player_no, team_id=team_id)
    if attention_player:
        return 2
    
    att_count = AttentionPlayer.count(' team_id=%s' % team_id)
    if att_count >= 10:
        return 3
    
    attention_player = AttentionPlayer()
    attention_player.team_id = team_id
    attention_player.no = player_no
    attention_player.type = type
    try:
        attention_player.persist()
    except:
        log_execption()
        return -1
    return 1

def get_player_by_no(table, no):
    '''获取球员'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone('select *, unix_timestamp(expired_time)-unix_timestamp(now()) as lave_time from %s where no="%s"' % (table, no))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()
    
def get_attention_player(team_id):
    '''获取某队关注球员'''
    attention_players = AttentionPlayer.query(condition='team_id=%s' % team_id)
    infos = []
    if attention_players:
        for attention_player in attention_players:
            if attention_player.type == MarketType.FREE:
                #player = FreePlayer.load(no=attention_player.no)
                player = get_player_by_no(table='free_player', no=attention_player.no) 
            elif attention_player.type == MarketType.YOUTH_FREE:
                #player = YouthFreePlayer.load(no=attention_player.no)
                player = get_player_by_no(table='youth_free_player', no=attention_player.no)
            elif attention_player.type == MarketType.DRAFT:
                #player = DraftPlayer.load(no=attention_player.no)
                player = get_player_by_no(table='draft_player', no=attention_player.no)
            player['type'] = attention_player.type
            infos.append(player)
    return infos

def cancel_attention_player(team_id, player_no):
    '''取消关注球员'''
    attention_player = AttentionPlayer.load(no=player_no, team_id=team_id)
    if attention_player:
        try:
            attention_player.delete()
            return 1
        except:
            log_execption()
            return -1
    return -1

if __name__ == '__main__':
    print get_free_palyer()[1]