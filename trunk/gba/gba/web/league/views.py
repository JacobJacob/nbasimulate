#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator, match_operator

from gba.entity import League, LeagueTeams
from gba.common.constants import MatchTypes

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



    
