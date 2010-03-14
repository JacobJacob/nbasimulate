#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common.constants import MatchStatus, MatchTypes, TacticalGroupTypeMap, TacticalSectionTypeMap
from gba.common import log_execption
from gba.business import player_operator

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

def send_match_request(home_team_id, guest_team_id, type):
    '''发送比赛请求'''
    info = {}
    info['home_team_id'] = home_team_id
    info['guest_team_id'] = guest_team_id
    info['type'] = type
    info['status'] = MatchStatus.SEND
    info['created_time'] = ReserveLiteral('now()')
    info['send_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(info, 'matchs')
    finally:
        cursor.close()
        
def save_tactical_detail(info):
    '''保存战术'''
    info['created_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(info, 'team_tactical_detail', True, ['created_time'])
    except:
        log_execption()
        return False
    finally:
        cursor.close()
    return True

_LOAD_TACTICAL_DETAIL = 'select * from team_tactical_detail where seq=%s and team_id=%s limit 1'

def get_tactical_detail(team_id, seq):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_TACTICAL_DETAIL, (seq, team_id))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()
        
_SELECT_TACTICAL_DETAILS = 'select * from team_tactical_detail where team_id=%s order by seq asc'

def get_tactical_details(team_id):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_DETAILS, (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()

_SELECT_TACTICAL_MAINS = 'select * from team_tactical where team_id=%s order by type asc'
        
def get_tactical_mains(team_id):
    '''获取不同比赛阵容信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_MAINS, (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
def create_team_default_tactical(team_id):
    '''创建球队默认战术'''
    
    players = player_operator.get_profession_player(team_id)
    if len(players) < 5:
        raise '球队人数不足五个, 不能创建'
    
    tactical_infos = {'PG': None, 'SG': None, 'SF': None, 'PF': None, 'C': None}
    in_player_nos = []
    #先按位置排
    for player in players:
        position = player['position']
        if tactical_infos[position] == None:
            tactical_infos[position] = player['no']
            in_player_nos.append(player['no'])
    
    #如果按位置不够人数,则挑高综合的补上
    if len(in_player_nos) < 5:
        for k, v in tactical_infos.items():
            if not v:
                for player in players:
                    if player['no'] not in in_player_nos:
                        tactical_infos[k] = player['no']
                        in_player_nos.append(player['no'])
                        break
                                       
    tactical_details = []
    for seq in 'ABCD':
        tactical_detail = {'team_id': team_id, 'pgid': tactical_infos['PG'], 'sgid': tactical_infos['SG'], \
                       'sfid': tactical_infos['SF'], 'pfid': tactical_infos['PF'], 'cid': tactical_infos['C'], \
                       'created_time': ReserveLiteral('now()')}
        if seq == 'A':
            name = '第一阵容'
        elif seq == 'B':
            name = '第二阵容'
        elif seq == 'C':
            name = '第三阵容'
        else:
            name = '第四阵容'

        tactical_detail['name'] = name
        tactical_detail['seq'] = seq
        tactical_details.append(tactical_detail)
    
    cursor = connection.cursor()
    try:
        cursor.insert(tactical_details, 'team_tactical_detail')
    finally:
        cursor.close()
        
    tactical_details = get_tactical_details(team_id)
    default_tactical_id = tactical_details[0]['id']
    
    tactical_mains = []
    for type in TacticalGroupTypeMap.keys():
        tactical_main = {'team_id': team_id, 'type': type}
        tactical_main['created_time'] = ReserveLiteral('now()')
        tactical_main['tactical_detail_1_id'] = default_tactical_id
        tactical_main['tactical_detail_2_id'] = default_tactical_id
        tactical_main['tactical_detail_3_id'] = default_tactical_id
        tactical_main['tactical_detail_4_id'] = default_tactical_id
        tactical_main['tactical_detail_5_id'] = default_tactical_id
        tactical_main['tactical_detail_6_id'] = default_tactical_id
        tactical_main['tactical_detail_7_id'] = default_tactical_id
        tactical_main['tactical_detail_8_id'] = default_tactical_id
        tactical_mains.append(tactical_main)
    
    cursor = connection.cursor()
    try:
        cursor.insert(tactical_mains, 'team_tactical')
    finally:
        cursor.close()
        
def save_tactical_main(infos):
    '''保存战术设置'''
    for info in infos:
        info['created_time'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        cursor.insert(infos, 'team_tactical', True, ['created_time', ])
    except:
        log_execption()
        return False
    finally:
        cursor.close()
    return True
        
if __name__ == '__main__':
    create_team_default_tactical(1)