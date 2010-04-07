#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.business.user_roles import login_required
from gba.entity import League, LeagueTeams, LeagueMatchs, LeagueConfig, Team, ProfessionPlayer, ProPlayerSeasonStatTotal
from gba.web.render import render_to_response

@login_required
def league_rank(request, min=False):
    """联赛排名"""
    team = request.team
    league = League.load(degree=team.profession_league_evel, no=team.profession_league_class)
    league_teams = LeagueTeams.query(condition="league_id='%s'" % league.id, order="rank desc")
    datas = {'infos': league_teams, 'league': league}
    
    for i, league_team in enumerate(league_teams):
        league_team.sort = i + 1
    
    if min:
        return render_to_response(request, 'league/league_rank_min.html', datas)
    return render_to_response(request, 'league/league_rank.html', datas)


@login_required
def league_schedule(request):
    """联赛赛程"""
    team = request.team
    league = League.load(degree=team.profession_league_evel, no=team.profession_league_class)
    league_team = LeagueTeams.load(team_id=team.id)
    league_matchs = LeagueMatchs.query(condition='match_team_home_id=%s or match_team_guest_id=%s' % (league_team.id, league_team.id), order='round asc ')
    datas = {'infos': league_matchs, 'league': league}
    return render_to_response(request, 'league/league_schedule.html', datas)

@login_required
def pre_schedule(request):
    """上轮比赛程"""
    team = request.team
    league_config = LeagueConfig.load(id=1)
    league = League.load(degree=team.profession_league_evel, no=team.profession_league_class)
    league_matchs = LeagueMatchs.query(condition="league_id='%s' and round='%s'" % (league.id, league_config.round))
    datas = {'infos': league_matchs, 'league': league}
    return render_to_response(request, 'league/pre_schedule.html', datas)

@login_required
def current_schedule(request):
    """本轮比赛程"""
    team = request.team
    league_config = LeagueConfig.load(id=1)
    league = League.load(degree=team.profession_league_evel, no=team.profession_league_class)
    league_matchs = LeagueMatchs.query(condition="league_id='%s' and round='%s'" % (league.id, league_config.round+1))
    datas = {'infos': league_matchs, 'season': league_config.season, 'round': league_config.round+1, 'league': league}
    return render_to_response(request, 'league/current_schedule.html', datas)

@login_required
def league_statistics(request):
    """联赛统计,记得做静态化，然后在"""
    type = int(request.GET.get('type', 1))
    league_id = request.GET.get('league_id', None)
    datas = {}
    if league_id:
        print type
        #1平均分 ,2 抢断 3 篮板 , 4 助功 ,5 封盖
        teams = Team.query(league_id=league_id)
        league = League.load(id=league_id)
        player_nos = []
        for team in teams:
            players = ProfessionPlayer.query(condition="team_id='%s'" % team.id)
            for player in players:
                player_nos.append(player.no)
        player_str = ",".join(["'%s'" % no for no in player_nos])
        league_config = LeagueConfig.load(id=1)
        round = league_config.round
        half_round = round / 2 #至少要打了一半才能进统计榜
        order = None
        if type == 1:
            order = 'point_agv desc'
        elif type == 2:
            order = 'steals_agv desc'
        elif type == 3:
            order = 'rebound_agv desc'
        elif type == 4:
            order = 'assist_agv desc'
        elif type == 5:
            order = 'block desc'    
        
        if order:
            statistics = ProPlayerSeasonStatTotal.query(condition="player_no in (%s) and match_total>=%s" % (player_str, half_round), order=order, limit=10)
            for statistic in statistics:
                player = ProfessionPlayer.load(no=statistic.player_no)
                statistic.player = player
        datas['infos'] = statistics
        datas['league'] = league
    
    template = 'league/league_statistics_%s.html' % type
    return render_to_response(request, template, datas)

@login_required
def team_statistics(request):
    """本队球员统计赛统计"""
    team = request.team
    league = League.load(degree=team.profession_league_evel, no=team.profession_league_class)
    player_nos = []
    players = ProfessionPlayer.query(condition="team_id='%s'" % team.id)
    print len(players)
    for player in players:
        player_nos.append(player.no)
    player_str = ",".join(["'%s'" % no for no in player_nos])
    statistics = ProPlayerSeasonStatTotal.query(condition="player_no in (%s) " % player_str, order='point_agv desc')
    for statistic in statistics:
        player = ProfessionPlayer.load(no=statistic.player_no)
        statistic.player = player
    datas = {'infos': statistics, 'league': league}
    return render_to_response(request, 'league/team_statistics.html', datas)