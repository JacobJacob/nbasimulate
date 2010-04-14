#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common.constants import MatchStatus, TacticalGroupTypeMap
from gba.common import log_execption
from gba.common import playerutil
from gba.entity import Team, ProfessionPlayer, League, LeagueTeams, TeamArena
from gba.business import player_operator

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

_LOAD_TACTICAL_DETAIL = 'select * from team_tactical_detail where seq=%s and team_id=%s and is_youth=0 limit 1'

def get_tactical_detail(team_id, seq):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_TACTICAL_DETAIL, (seq, team_id))
        if rs:
            return rs.to_dict()
    finally:
        cursor.close()
        
_SELECT_TACTICAL_DETAILS = 'select * from team_tactical_detail where team_id=%s and is_youth=%s order by seq asc'

def get_tactical_details(team_id, is_youth=False):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_DETAILS, (team_id, 1 if is_youth else 0))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()

_SELECT_TACTICAL_MAINS = 'select * from team_tactical where team_id=%s and is_youth=0 order by type asc'
        
def get_tactical_mains(team_id):
    '''获取不同比赛阵容信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_MAINS, (team_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
def create_team_default_tactical(team_id, is_youth=False):
    '''创建球队默认战术'''
    
    if is_youth:
        players = player_operator.get_youth_player(team_id)
    else:
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
        tactical_detail['is_youth'] = is_youth
        tactical_details.append(tactical_detail)
    
    cursor = connection.cursor()
    try:
        cursor.insert(tactical_details, 'team_tactical_detail')
    finally:
        cursor.close()
        
    tactical_details = get_tactical_details(team_id, is_youth)
    default_tactical_id = tactical_details[0]['id']
    
    tactical_mains = []
    
    if is_youth:
        tactical_main = {'team_id': team_id, 'type': 4, 'is_youth': is_youth} #4是青年比赛
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
    else:
        for type in TacticalGroupTypeMap.keys():
            tactical_main = {'team_id': team_id, 'type': type, 'is_youth': is_youth}
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

def assign_league():
    '''分配联赛'''
    leagues = League.query(condition='degree>=10 and team_count<14', order='id asc', limit=1)
    if leagues:
        league = leagues[0]
        league.team_count += 1
        if league.status == 0 and league.team_count > 2:
            league.status = 1 # 1表示有球队了
        elif league.status == 1 and league.team_count == 14:
            league.status = 2 # 2表示整个联赛己经满了
        league.persist()
        return league
    return None 

def init_team(team_info):
    '''初始化一支球队'''
    
    team = Team()
    team.username = team_info['username']
    team.name = team_info['teamname']
    team.funds = 500000 #50W的初始资金
    
    #先创建球员,8名职业球员1 c , 1pf 2 sf 2 sg 2 pg
    locations = ['C', 'PF', 'SF', 'SF', 'SG', 'SG', 'PG', 'PG']
    ProfessionPlayer.transaction()
    try:
        league = assign_league()
        team.youth_league = 0
        team.profession_league_class = league.no
        team.profession_league_evel = league.degree
        team.persist()
        
        league_team = LeagueTeams.load(league_id=league.id, status=0, team_id=-1)
        if not league_team:
            raise 'league team is not exit'
        league_team.team_id = team.id
        league_team.status = 1 #状态改为了，表示有人了
        league_team.persist()
        
        #创建球馆
        team_arena = TeamArena()
        team_arena.team_id = team.id
        team_arena.level = 1
        team_arena.fare = 20 #票价
        team_arena.fan_count = 1000 #球迷
        team_arena.persist()
        
        
        for i, location in enumerate(locations):
            player = playerutil.create_profession_player(location)
            setattr(player, 'player_no', i + 1)
            setattr(player, 'team_id', team.id)
            youth_player = playerutil.create_youth_player(location)
            setattr(youth_player, 'player_no', i + 1)
            setattr(youth_player, 'team_id', team.id)
            player.persist()
            youth_player.persist()
        ProfessionPlayer.commit()
    except:
        log_execption()
        return False
        ProfessionPlayer.rollback()
       
    #创建默认阵容
    create_team_default_tactical(team.id)
    create_team_default_tactical(team.id, is_youth=True)
    
_SELECT_MATCH = 'select * from matchs where type=%s and (home_team_id=%s or guest_team_id=%s ) order by id desc limit %s, %s '
                       
_SELECT_MATCH_TOTAL = 'select count(*) as count from matchs where type=%s and (home_team_id=%s or guest_team_id=%s ) '

def get_match(team_id, type, page=1, pagesize=30):
    '''获取比赛列表
    '''
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH % (type, team_id, team_id, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_MATCH_TOTAL, (type, team_id, team_id))
            total = rs['count']
    finally:
        cursor.close()
        
    return infos, total

_SELECT_MATCH_STAT = 'select * from match_stat where team_id=%s and match_id=%s'

def get_match_stat(team_id, match_id):
    '''获取比赛统计'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_STAT, (team_id, match_id))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_SELECT_MATCH_NODOSITY_MAIN = 'select * from match_nodosity_main where match_id=%s order by seq asc'

def get_match_nodosity_main(match_id):
    '''获取比赛每节概要'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_NODOSITY_MAIN, (match_id,))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_SELECT_MATCH_NODOSITY_DETAIL = 'select * from match_nodosity_detail where match_nodosity_main_id=%s order by seq asc '

def get_match_nodosity_detail(match_nodosity_main_id):
    '''获取每节的比赛详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_NODOSITY_DETAIL, (match_nodosity_main_id, ))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()

_SELECT_TRAININT_REMAIN = 'select (finish_time - now()) as remain from training_center where team_id="%s" and status=0'

def get_training_remain(team_id):
    '''获取训练赛剩余时间'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_SELECT_TRAININT_REMAIN, (team_id, ))
        if rs:
            return rs['remain']
        return 0
    finally:
        cursor.close()
        
if __name__ == '__main__':
    init_team({'username': '测试用户3', 'teamname': '测试球队3'})