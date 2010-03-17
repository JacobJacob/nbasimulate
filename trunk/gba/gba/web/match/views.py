#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator, match_operator

from gba.entity import Team, Matchs, ProfessionPlayer
from gba.common.constants import MatchTypes

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

@login_required
def friendly_match(request):
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    
    team = UserManager().get_team_info(request)
    infos, total = match_operator.get_match(team.id, MatchTypes.FRIENDLY, page, pagesize)

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, 'match/friendly_match.html', datas)

def match_stat(request):
    '''比赛统计'''
    match_id = request.GET.get('match_id')
    if not match_id:
        return None
    
    match = Matchs.load(id=match_id)
    
    home_stat = match_operator.get_match_stat(match.home_team_id, match_id)
    
    for stat in home_stat:
        player_no = stat['player_no']
        player = ProfessionPlayer.load(no=player_no)
        stat['player'] = player
    
    guest_stat = match_operator.get_match_stat(match.guest_team_id, match_id)
    
    for stat in guest_stat:
        player_no = stat['player_no']
        player = ProfessionPlayer.load(no=player_no)
        stat['player'] = player
    
    datas = {'home_stat': home_stat, 'guest_stat': guest_stat}
    return render_to_response(request, 'match/match_stat.html', datas)