#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.common import playerutil
from gba.business import player_operator
from gba.business.user_roles import login_required


def index(request):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    position = request.GET.get('position', 'C')
    order_by = request.GET.get('order_by', 'id')
    order = request.GET.get('order', 'asc')
    
    infos, total = player_operator.get_free_palyer(page, pagesize, position, order_by, order)
    
    print infos
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'position': position, 'order_by': order_by, 
            'order': order}
    
    return render_to_response(request, 'player/free_players.html', datas)

def freeplayer_detail(request):
    """free player detail"""
    id = request.GET.get('id', None)
    player = player_operator.get_free_palyer_by_id(id)
    playerutil.calcul_otential(player)
    datas = {'id': id, 'player': player}
    return render_to_response(request, 'player/freeplayer_detail.html', datas)

@login_required
def profession_player(request):
    '''profession player'''

    team_id = 0
    
    infos = player_operator.get_profession_player(team_id)
    datas = {'infos': infos}
    
    return render_to_response(request, 'player/profession_player.html', datas)

def professioplayer_detail(request):
    """free player detail"""
    id = request.GET.get('id', None)
    player = player_operator.get_free_palyer_by_id(id)
    playerutil.calcul_otential(player)
    datas = {'id': id, 'player': player}
    return render_to_response(request, 'player/freeplayer_detail.html', datas)