#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.business.user_roles import login_required
from gba.entity import League, LeagueTeams, LeagueMatchs, LeagueConfig, Team, ProfessionPlayer, ProPlayerSeasonStatTotal
from gba.web.render import render_to_response

@login_required
def season_finance(request, min=False):
    """球队财政"""
    team = request.team
    datas = {}
    if min:
        return render_to_response(request, 'team/season_finance_min.html', datas)
    return render_to_response(request, 'team/season_finance.html', datas)

@login_required
def arena_build(request):
    """球员馆建设"""
    team = request.team
    datas = {}
    return render_to_response(request, 'team/arena_build.html', datas)