#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.business.user_roles import login_required
from gba.entity import League, LeagueTeams, LeagueMatchs
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
    league_team = LeagueTeams.load(team_id=request.team.id)
    league_matchs = LeagueMatchs.query(condition='match_team_home_id=%s or match_team_guest_id=%s' % (league_team.id, league_team.id), order='round asc ')
    datas = {'infos': league_matchs}
    return render_to_response(request, 'league/league_schedule.html', datas)


    
