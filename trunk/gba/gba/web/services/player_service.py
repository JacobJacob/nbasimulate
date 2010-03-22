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
    
    print player_info
    print in_use_nos
    return player_info, in_use_nos

@login_required
@jsonrpc_function
def update_profession_player(request, info):
    '''更新职业球员信息，只能更新不位置，号码， 名字等信息'''
    team = UserManager().get_team_info(request)
    return player_operator.update_profession_player(team.id, info)

@login_required
@jsonrpc_function
def youth_freeplayer_auction(request, no, price):
    team = UserManager().get_team_info(request)
    return player_operator.youth_freeplayer_auction(team.id, no, price)

@login_required
@jsonrpc_function
def check_has_auction(request):
    '''验证某个用户是否已经出过价'''
    team = UserManager().get_team_info(request)
    return player_operator.check_has_auction(team.username)

@login_required
@jsonrpc_function
def get_free_auction_info(request, no):
    '''验证某个用户是否已经在自由球员那出过价, 已及要出价球员的身价'''
    team = UserManager().get_team_info(request)
    return player_operator.get_free_auction_info(team.username, no)

@login_required
@jsonrpc_function
def freeplayer_auction(request, no, price):
    '''职业球员出价'''
    team = UserManager().get_team_info(request)
    return player_operator.freeplayer_auction(team.id, no, price)
    