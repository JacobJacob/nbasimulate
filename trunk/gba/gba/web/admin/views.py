#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.common.db import connection
from gba.common import json
from gba.business import free_player

def index(request):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    position = request.GET.get('position', 'C')
    order_by = request.GET.get('order_by', 'id')
    order = request.GET.get('order', 'desc')
    
    infos, total = free_player.get_free_palyer(page, pagesize, position, order_by, order)
    
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
    datas = {'id': id}
    return render_to_response(request, 'player/freeplayer_detail.html', datas)

   