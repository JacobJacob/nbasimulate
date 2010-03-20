#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator, match_operator

from gba.entity import Team, Matchs, ProfessionPlayer
from gba.common.constants import MatchTypes

@login_required
def index(request):
    """list"""
    datas = {}         
    return render_to_response(request, 'league/index.html', datas)



    
