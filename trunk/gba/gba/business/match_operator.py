#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from django.http import HttpResponseRedirect

from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common.constants import MatchStatus, TacticalGroupTypeMap, MatchShowStatus, MatchTypes, \
                                 TacticalGroupType
from gba.common import log_execption
from gba.common import playerutil, exception_mgr, md5mgr
from gba.entity import Team, ProfessionPlayer, League, LeagueTeams, TeamArena, Matchs, ErrorMatch, \
                       MatchNodosityDetail, MatchNodosityMain, MatchNodosityTacticalDetail, \
                       MatchNotInPlayer, MatchStat, InitProfessionPlayer, InitYouthPlayer, YouthPlayer
from gba.business import player_operator

def send_match_request(home_team_id, guest_team_id, type):
    '''发送比赛请求'''
    info = {}
    info['home_team_id'] = home_team_id
    info['guest_team_id'] = guest_team_id
    info['type'] = type
    info['is_youth'] = 1 if type == MatchTypes.YOUTH_FRIENDLY or type == MatchTypes.YOUTH_TRAINING else 0  
    info['status'] = MatchStatus.SEND
    info['created_time'] = ReserveLiteral('now()')
    info['send_time'] = ReserveLiteral('now()')
    info['show_status'] = MatchShowStatus.WAITING
    info['next_show_status'] = MatchShowStatus.READY
    info['next_status_time'] = ReserveLiteral('now()')
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
        cursor.execute('start transaction;')
        cursor.insert(info, 'team_tactical_detail', True, ['created_time'])
        team_id = info['team_id']
        is_youth = info['is_youth']
        if is_youth:
            players = player_operator.get_youth_player(team_id)
        else:
            players = player_operator.get_profession_player(team_id, condition='is_draft=0')
            
        team_tactical_details = get_tactical_details(team_id, is_youth)
        
        on_tactical_nos = {}
        for team_tactical_detail in team_tactical_details:
            on_tactical_nos[team_tactical_detail['cid']] = None
            on_tactical_nos[team_tactical_detail['pfid']] = None
            on_tactical_nos[team_tactical_detail['sfid']] = None
            on_tactical_nos[team_tactical_detail['sgid']] = None
            on_tactical_nos[team_tactical_detail['pgid']] = None
            
        for player in players:
            if player['no'] in on_tactical_nos:
                player['in_tactical'] = 1
            else:
                player['in_tactical'] = 0
                
        if is_youth:
            cursor.insert(players, 'youth_player', True, ['created_time'])
        else:
            cursor.insert(players, 'profession_player', True, ['created_time'])
        cursor.execute('commit;')
    except:
        cursor.execute('rollback;')
        exception_mgr.on_except()
        return False
    finally:
        cursor.close()
    return True

_LOAD_TACTICAL_DETAIL = 'select * from team_tactical_detail where seq=%s and team_id=%s and is_youth=%s limit 1'

def get_tactical_detail(team_id, seq, is_youth=False):
    '''获取战术信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_LOAD_TACTICAL_DETAIL, (seq, team_id, 1 if is_youth else 0))
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

_SELECT_TACTICAL_MAINS = 'select * from team_tactical where team_id=%s and is_youth=%s order by type asc'
        
def get_tactical_mains(team_id, is_youth=False):
    '''获取不同比赛阵容信息'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_TACTICAL_MAINS, (team_id, 1 if is_youth else 0))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
def create_team_default_tactical(team_id, is_youth=False, players=None):
    '''创建球队默认战术'''
    
    if not players:
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
        if isinstance(player, dict):
            position = player['position']
        else:
            position = getattr(player, 'position')
        if tactical_infos[position] == None:
            if isinstance(player, dict):
                no = player['no']
            else:
                no = getattr(player, 'no')
            tactical_infos[position] = no
            in_player_nos.append(no)
    
            
    #如果按位置不够人数,则挑高综合的补上
    if len(in_player_nos) < 5:
        for k, v in tactical_infos.items():
            if not v:
                for player in players:
                    if isinstance(player, dict):
                        no = player['no']
                    else:
                        no = getattr(player, 'no')
                    if no not in in_player_nos:
                        tactical_infos[k] = no
                        in_player_nos.append(no)
                        break
    
    #设置场上状态                                   
    for player in players:
        if isinstance(player, dict):
            no = player['no']
            if no in in_player_nos:
                player['in_tactical'] = 1
        else:
            no = getattr(player, 'no')
            if no in in_player_nos:
                setattr(player, 'in_tactical', 1)
    
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
            if type == TacticalGroupType.YOUTH:
                continue
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
    leagues = League.query(condition='degree>=10 and team_count<14 and status <> 3 ', order='id asc', limit=1)
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
        
        league_team = LeagueTeams.load(league_id=league.id, status=0, team_id= -1)
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
    
_SELECT_MATCH = 'select *, unix_timestamp(next_status_time)-unix_timestamp(now()) as remain_time from matchs where (type=%s or type=%s) and (home_team_id=%s or guest_team_id=%s ) order by id desc limit %s, %s '
                       
_SELECT_MATCH_TOTAL = 'select count(*) as count from matchs where type=%s or type=%s and (home_team_id=%s or guest_team_id=%s ) '

def get_match(team_id, type, type2=None, page=1, pagesize=30):
    '''获取比赛列表
    '''
    if not type2:
        type2 = type
    
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    total = 0
    infos = []
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH % (type, type2, team_id, team_id, index, pagesize))
        if rs:
            infos = rs.to_list()
            rs = cursor.fetchone(_SELECT_MATCH_TOTAL, (type, type2, team_id, team_id))
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
        rs = cursor.fetchall(_SELECT_MATCH_NODOSITY_DETAIL, (match_nodosity_main_id,))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()
        
_SELECT_MATCH_TACTICAL_DETAIL = 'select * from match_nodosity_tactical_detail where match_nodosity_main_id=%s'

def get_match_nodosity_tactical_detail(match_nodosity_main_id):
    '''获取每节的比赛人员详细'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(_SELECT_MATCH_TACTICAL_DETAIL, (match_nodosity_main_id,))
        if rs:
            return rs.to_list()
    finally:
        cursor.close()

_SELECT_TRAININT_REMAIN = 'select (finish_time - now()) as remain from training_center where team_id="%s" and status=0'

def get_training_remain(team_id):
    '''获取训练赛剩余时间'''
    cursor = connection.cursor()
    try:
        rs = cursor.fetchone(_SELECT_TRAININT_REMAIN, (team_id,))
        if rs:
            return rs['remain']
        return 0
    finally:
        cursor.close()
        
def handle_error_match(match_id, delete_error=False):
    '''处理异常比赛
    @param delete_error 是否删除error_match表中的记录: 
    '''
    match = Matchs.load(id=match_id)
    if delete_error:
        error_match = ErrorMatch.load(match_id=match_id)
    match_stats = MatchStat.query(condition='match_id="%s"' % match.id)
    match_nodosity_mains = MatchNodosityMain.query(condition='match_id="%s"' % match.id, order='seq asc')
    match_nodosity_details = MatchNodosityDetail.query(condition='match_id="%s"' % match.id)
    
    match_nodosity_tactical_details = []
    if match_nodosity_mains:
        for match_nodosity_main in match_nodosity_mains:
            details = MatchNodosityTacticalDetail.query(condition='match_nodosity_main_id="%s"' % match_nodosity_main.id)
            match_nodosity_tactical_details += details
    
    match_notin_players = MatchNotInPlayer.query(condition='match_id="%s"' % match_id)
    
    match.status = 1
    match.expired_time = ReserveLiteral('date_add(now(), interval 60 minute)')
    
    Matchs.transaction()
    try:
        if match_stats:
            for match_stat in match_stats:
                match_stat.delete()
        if match_nodosity_mains:
            for match_nodosity_main in match_nodosity_mains:
                match_nodosity_main.delete()
        if match_nodosity_details:
            for match_nodosity_detail in match_nodosity_details:
                match_nodosity_detail.delete()
        if match_nodosity_tactical_details:
            for match_nodosity_tactical_detail in match_nodosity_tactical_details:
                match_nodosity_tactical_detail.delete()
        if match_notin_players:
            for match_notin_player in match_notin_players:
                match_notin_player.delete()
        
        if delete_error:
            error_match.delete()
        match.persist()
        Matchs.commit()
    except:
        Matchs.rollback()
        raise
    
    
def created_init_player_and_tactical(team_id, pro_ids, youth_ids):
    '''根据用户选的球员，创建初始队员, 创建战术
    @param pro_ids: 列表类型，职业球员id
    @param youth_ids:列表类型  街头球员id 
    '''
    pro_players = []
    youth_players = []
    init_pro_players = InitProfessionPlayer.query(condition="id in (%s)" % ','.join(['"%s"' % id for id in pro_ids]))
    init_youth_players = InitYouthPlayer.query(condition="id in (%s)" % ','.join(['"%s"' % id for id in youth_ids]))
    
    for init_pro_player in init_pro_players:
        pro_player = playerutil.copy_player(init_pro_player, 'init_profession_player', 'profession_player')
        no = md5mgr.mkmd5fromstr('%s%s%s' % (team_id, random.random(), init_pro_player.player_no))
        print no
        setattr(pro_player, 'no', no)
        setattr(pro_player, 'team_id', team_id)
        pro_players.append(pro_player)
    
    for init_youth_player in init_youth_players:
        youth_player = playerutil.copy_player(init_youth_player, 'init_youth_player', 'youth_player')
        no = md5mgr.mkmd5fromstr('%s%s%s' % (team_id, random.random(), init_youth_player.player_no))
        setattr(youth_player, 'no', no)
        setattr(youth_player, 'team_id', team_id)
        youth_players.append(youth_player)
        
    ProfessionPlayer.transaction()
    try:
        ProfessionPlayer.inserts(pro_players)
        YouthPlayer.inserts(youth_players)
        ProfessionPlayer.commit()
    except:
        ProfessionPlayer.rollback()
        return False
    
    print len(youth_players)
    print len(pro_players)
    create_team_default_tactical(team_id, is_youth=True, players=youth_players)
    create_team_default_tactical(team_id, is_youth=False, players=pro_players)
    
    return True
    
if __name__ == '__main__':
    created_init_player_and_tactical(9999, ["1","2","12","3","4","5","10","11"], ["6","1","4","9","10","5","8","2"])
