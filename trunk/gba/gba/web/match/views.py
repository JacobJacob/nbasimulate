#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator, match_operator

from gba.entity import Team, Matchs, ProfessionPlayer, TrainingCenter,\
                       TeamTactical, TeamTacticalDetail, YouthPlayer, \
                       MatchNotInPlayer
from gba.common.constants import MatchTypes

@login_required
def friendly_match(request):
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    
    team = UserManager().get_team_info(request)
    infos, total = match_operator.get_match(team.id, MatchTypes.FRIENDLY, page, pagesize)

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, 'match/friendly_match.html', datas)

def _create_total_stat(stat, total_stat):
    total_stat['point2_doom_times'] = total_stat.get('point2_doom_times', 0) + stat['point2_doom_times']
    total_stat['point3_doom_times'] = total_stat.get('point3_doom_times', 0) + stat['point3_doom_times'] 
    total_stat['point1_doom_times'] = total_stat.get('point1_doom_times', 0) + stat['point1_doom_times']
    total_stat['point2_shoot_times'] = total_stat.get('point2_shoot_times', 0) + stat['point2_shoot_times']
    total_stat['point3_shoot_times'] = total_stat.get('point3_shoot_times', 0) + stat['point3_shoot_times'] 
    total_stat['point1_shoot_times'] = total_stat.get('point1_shoot_times', 0) + stat['point1_shoot_times']
    total_stat['offensive_rebound'] = total_stat.get('offensive_rebound', 0) + stat['offensive_rebound']
    total_stat['defensive_rebound'] = total_stat.get('defensive_rebound', 0) + stat['defensive_rebound']
    total_stat['total_rebound'] = total_stat.get('total_rebound', 0) + stat['total_rebound']
    total_stat['assist'] = total_stat.get('assist', 0) + stat['assist']
    total_stat['steals'] = total_stat.get('steals', 0) + stat['steals']
    total_stat['foul'] = total_stat.get('foul', 0) + stat['foul']
    total_stat['block'] = total_stat.get('block', 0) + stat['block']
    total_stat['lapsus'] = total_stat.get('lapsus', 0) + stat['lapsus']
    total_stat['total_point'] = total_stat.get('total_point', 0) + stat['total_point']

def match_stat(request):
    '''比赛统计'''
    match_id = request.GET.get('match_id')
    if not match_id:
        return None
    
    match = Matchs.load(id=match_id)
    
    home_stat = match_operator.get_match_stat(match.home_team_id, match_id)
    
    home_team = Team.load(id=match.home_team_id)
    guest_team = Team.load(id=match.guest_team_id)
    
    home_stat_total = {}
    home_stat_total['times'] = 240
    home_stat_total['name'] = u'合计'
    for stat in home_stat:
        player_no = stat['player_no']
        player = ProfessionPlayer.load(no=player_no)
        stat['name'] = player.name
        total_point = 0
        for i in range(1, 4):
            total_point += stat.get('point%s_doom_times' % i, 0) * i
        stat['total_point'] = total_point
        stat['total_rebound'] = stat['defensive_rebound'] + stat['offensive_rebound']
        _create_total_stat(stat, home_stat_total)
        
    home_stat = sorted(home_stat, cmp=lambda x, y: cmp(x['total_point'] * 1000 + x['total_rebound'] * 100 + x['assist'] * 10 + x['steals'], \
                                                      y['total_point'] * 1000 + y['total_rebound'] * 100 + y['assist'] * 10 + y['steals']), reverse=True)
    
    guest_stat = match_operator.get_match_stat(match.guest_team_id, match_id)
    
    guest_stat_total = {}
    guest_stat_total['times'] = 240
    guest_stat_total['name'] = u'合计'
    for stat in guest_stat:
        player_no = stat['player_no']
        player = ProfessionPlayer.load(no=player_no)
        stat['name'] = player.name
        total_point = 0
        for i in range(1, 4):
            total_point += stat.get('point%s_doom_times' % i, 0) * i
        stat['total_point'] = total_point
        stat['total_rebound'] = stat['defensive_rebound'] + stat['offensive_rebound']
        _create_total_stat(stat, guest_stat_total)
        
    guest_stat = sorted(guest_stat, cmp=lambda x, y: cmp(x['total_point'] * 1000 + x['total_rebound'] * 100 + x['assist'] * 10 + x['steals'], \
                                                      y['total_point'] * 1000 + y['total_rebound'] * 100 + y['assist'] * 10 + y['steals']), reverse=True)
    
    #guest_stat.append(guest_stat_total)
    #home_stat.append(home_stat_total)

    home_not_in_players = MatchNotInPlayer.query(condition="match_id=%s and team_id=%s" %(match_id, match.home_team_id))
    guest_not_in_players = MatchNotInPlayer.query(condition="match_id=%s and team_id=%s" %(match_id, match.guest_team_id))

    datas = {'home_stat': home_stat, 'guest_stat': guest_stat, 'home_team_name': home_team.name,
              'guest_team_name': guest_team.name, 'home_not_in_players': home_not_in_players, 
              'guest_not_in_players': guest_not_in_players, 'guest_stat_total': guest_stat_total,
              'home_stat_total': home_stat_total}
    return render_to_response(request, 'match/match_stat.html', datas)

def match_detail(request):
    '''比赛战报'''
    match_id = request.GET.get('match_id')
    if not match_id:
        return None
    
    #team = UserManager().get_team_info(request)
    match_nodosity_mains = match_operator.get_match_nodosity_main(match_id)
    
    for match_nodosity_main in match_nodosity_mains:
        match_nodosity_details = match_operator.get_match_nodosity_detail(match_nodosity_main['id'])
        print match_nodosity_details
        match_nodosity_main['details'] = match_nodosity_details
        
    datas = {'match_nodosity_mains': match_nodosity_mains}
    return render_to_response(request, 'match/match_detail.html', datas)

@login_required
def training_center(request):
    '''训练中心'''
    
    in_match = 0
    
    team = UserManager().get_team_info(request)
    training_center = TrainingCenter.load(team_id=team.id)
    
    infos = []
    totalpage = 0
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 12))
    if training_center:
        in_match = 1
        infos, total = TrainingCenter.paging(page, pagesize, condition='team_id <> %s' % team.id)
        if total == 0:
            totalpage = 0
        else:
            totalpage = (total - 1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1,\
              'prevpage': page - 1, 'in_match': in_match, 'tab': 1, 'training_center': training_center}
    
    return render_to_response(request, 'match/training_center.html', datas)

@login_required
def youth_tactical(request):
    """青年战术"""

    datas = {}
    team = UserManager().get_team_info(request)
    tactical_details = TeamTacticalDetail.query(condition="team_id=%s and is_youth=1" % team.id, order="seq asc")
    tactical_mains =  TeamTactical.query(condition="team_id=%s and is_youth=1" % team.id, order="type asc")
    
    datas['tactical_details'] = tactical_details
    datas['sections'] = [i for i in range(1, 9)]
    
    for tactical_main in tactical_mains:
        for i in range(1, 9):
            setattr(tactical_main, str(i), getattr(tactical_main, 'tactical_detail_%s_id' % i))
            delattr(tactical_main, 'tactical_detail_%s_id' % i)
        datas['match_type_%s' % tactical_main.type] = tactical_main
    
    datas['sort'] = 'W'         
    return render_to_response(request, 'match/youth_tactical.html', datas)

@login_required
def youth_tactical_detail(request):
    """青年战术详细"""
    
    sort = request.GET.get('sort', 'A')
    
    team = UserManager().get_team_info(request)
    
    players = YouthPlayer.query(condition="team_id=%s" % team.id, order='ability desc')
    tactical_info = TeamTacticalDetail.query(condition="team_id=%s and seq='%s' and is_youth=1" % (team.id, sort), limit=1)
    tactical_info = tactical_info[0]
    
    ret_players = []
    tactical_detail_info = {tactical_info.pgid: 'pg_info', tactical_info.sfid: 'sf_info', tactical_info.sgid: 'sg_info', \
                          tactical_info.pfid: 'pf_info', tactical_info.cid: 'c_info'}

    datas = {'sort': sort}
    for player in players:
        if player.no in tactical_detail_info:
            datas[tactical_detail_info[player.no]] = player
        else:
            ret_players.append(player)
            
    datas['infos'] = ret_players      
    datas['tactical_detail_name'] = tactical_info.name
    datas['tactical_info'] = tactical_info
    return render_to_response(request, 'match/youth_tactical_detail.html', datas)

@login_required
def profession_tactical(request):
    """list"""

    datas = {}
    team = UserManager().get_team_info(request)
    tactical_details = match_operator.get_tactical_details(team.id)
    tactical_mains = match_operator.get_tactical_mains(team.id)
    
    datas['tactical_details'] = tactical_details
    datas['sections'] = [i for i in range(1, 9)]
    
    for tactical_main in tactical_mains:
        for i in range(1, 9):
            tactical_main[i] = tactical_main['tactical_detail_%s_id' % i]
            del tactical_main['tactical_detail_%s_id' % i]
        datas['match_type_%s' % tactical_main['type']] = tactical_main
               
    return render_to_response(request, 'match/profession_tactical.html', datas)

@login_required
def profession_tactical_detail(request):
    """free player detail"""
    
    sort = request.GET.get('sort', 'A')
    
    user_info = UserManager().get_userinfo(request)
    
    username = user_info['username']
    
    team = Team.load(username=username)
    
    players = player_operator.get_profession_player(team.id)
    
    tactical_info = match_operator.get_tactical_detail(team.id, sort)
    
    ret_players = []
    tactical_detail_info = {tactical_info['pgid']: 'pg_info', tactical_info['sfid']: 'sf_info', tactical_info['sgid']: 'sg_info', \
                          tactical_info['pfid']: 'pf_info', tactical_info['cid']: 'c_info'}

    datas = {'sort': sort}
    for player in players:
        if player['no'] in tactical_detail_info:
            datas[tactical_detail_info[player['no']]] = player
        else:
            ret_players.append(player)
            
    datas['infos'] = ret_players      
    datas['tactical_detail_name'] = tactical_info['name']
    datas['tactical_info'] = tactical_info
    return render_to_response(request, 'match/profession_tactical_detail.html', datas)