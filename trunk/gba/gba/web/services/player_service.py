#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""

from gba.common.jsonrpcserver import jsonrpc_function
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator

@login_required
@jsonrpc_function
def profession_player_detail(request, player_no):
    team = UserManager().get_team_info(request)
    player_info = player_operator.get_profession_palyer_by_no(player_no)
    players = player_operator.get_profession_player(team.id)
    in_use_nos = []
    for player in players:
        in_use_nos.append(player['player_no'])
        
    return player_info, in_use_nos


