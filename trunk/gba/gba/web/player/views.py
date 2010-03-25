#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.common import playerutil
from gba.business import player_operator
from gba.business.user_roles import login_required, UserManager
from gba.entity import Team
from gba.common.constants import attributes, hide_attributes, AttributeMaps

@login_required
def index(request):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    position = request.GET.get('position', 'c')
    order_by = request.GET.get('order_by', 'id')
    order = request.GET.get('order', 'asc')
    
    infos, total = player_operator.get_free_palyer(page, pagesize, position, order_by, order)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'position': position, 'order_by': order_by, 
            'order': order}
    
    return render_to_response(request, 'player/free_players.html', datas)

@login_required
def freeplayer_detail(request):
    """free player detail"""
    no = request.GET.get('no', None)
    print no
    if not no:
        return render_to_response(request, 'default_error.html', {'msg': u'球员id为空'})
    player = player_operator.get_free_palyer_by_no(no)
    if not player:
        return render_to_response(request, 'default_error.html', {'msg': u'球员不存在'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]
    
    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps}
    return render_to_response(request, 'player/freeplayer_detail.html', datas)

@login_required
def profession_player(request):
    '''profession player'''

    user_info = UserManager().get_userinfo(request)
    
    team = Team.load(username=user_info['username'])
    
    infos = player_operator.get_profession_player(team.id)
    datas = {'infos': infos}
    datas['nos'] = [i for i in range(30)]
    show_attrs = {}
    for attr in AttributeMaps.keys():
        if attr not in hide_attributes:
            show_attrs[attr] = AttributeMaps[attr]
    
    datas['attrs'] = show_attrs
    
    return render_to_response(request, 'player/profession_player.html', datas)

@login_required
def profession_player_detail(request):
    """profession player detail"""
    no = request.GET.get('no', None)
    if not no:
        return render_to_response(request, 'default_error.html', {'msg': u'球员id为空'})
    player = player_operator.get_profession_palyer_by_no(no)
    if not player:
        return render_to_response(request, 'default_error.html', {'msg': u'球员不存在'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]
    
    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps}
    return render_to_response(request, 'player/profession_player_detail.html', datas)

@login_required
def youth_free_player(request):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    position = request.GET.get('position', 'c')
    order_by = request.GET.get('order_by', 'expired_time')
    order = request.GET.get('order', 'asc')
    
    infos, total = player_operator.get_youth_freepalyer(page, pagesize, position, order_by, order)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'position': position, 'order_by': order_by, 
            'order': order}
    
    return render_to_response(request, 'player/youth_free_players.html', datas)

@login_required
def youth_freeplayer_detail(request):
    """free player detail"""
    no = request.GET.get('no', None)
    print no
    if not no:
        return render_to_response(request, 'default_error.html', {'msg': u'球员id为空'})
    player = player_operator.get_youth_freepalyer_by_no(no)
    if not player:
        return render_to_response(request, 'default_error.html', {'msg': u'球员不存在'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]
    
    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps}
    return render_to_response(request, 'player/youth_freeplayer_detail.html', datas)

@login_required
def youth_player(request):
    '''youth player'''

    user_info = UserManager().get_userinfo(request)
    
    team = Team.load(username=user_info['username'])
    
    infos = player_operator.get_youth_player(team.id)
    datas = {'infos': infos}
    datas['nos'] = [i for i in range(30)]
    
    return render_to_response(request, 'player/youth_player.html', datas)

@login_required
def youth_player_detail(request):
    """youth player detail"""
    no = request.GET.get('no', None)
    if not no:
        return render_to_response(request, 'default_error.html', {'msg': u'球员id为空'})
    player = player_operator.get_profession_palyer_by_no(no)
    if not player:
        return render_to_response(request, 'default_error.html', {'msg': u'球员不存在'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]
    
    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps}
    return render_to_response(request, 'player/profession_player_detail.html', datas)

@login_required
def attention_player(request):
    '''关注球员'''
    team = UserManager().get_team_info(request)
    infos = player_operator.get_attention_player(team.id)
    datas = {'infos': infos}
    return render_to_response(request, 'player/attention_players.html', datas)
