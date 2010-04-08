#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

import random

from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.common import playerutil
from gba.business import player_operator
from gba.business.user_roles import login_required, UserManager
from gba.entity import Team, YouthPlayer, FreePlayer, YouthFreePlayer, \
                       ProfessionPlayer, ProPlayerSeasonStatTotal, ProPlayerCareerStatTotal
from gba.common.constants import attributes, hide_attributes, AttributeMaps

@login_required
def index(request, min=False):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
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
    
    if min:
        return render_to_response(request, 'player/free_players_min.html', datas)
    return render_to_response(request, 'player/free_players.html', datas)

@login_required
def freeplayer_detail(request):
    """free player detail"""
    no = request.GET.get('no', None)
    
    error = None
    
    if not no:
        error = u'获取球员信息出错'
    else:
        player = player_operator.get_free_palyer_by_no(no)
        if not player:
            error = u'获取球员信息出错'

    datas = {'player': player}
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    return render_to_response(request, 'player/common_detail.html', datas)

@login_required
def profession_player(request, min=False):
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
    
    if min:
        return render_to_response(request, 'player/profession_player_min.html', datas)
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
        
    season_stat_total = ProPlayerSeasonStatTotal.load(player_no=no)
    if not season_stat_total:
        season_stat_total = ProPlayerSeasonStatTotal()
        season_stat_total.player_no = no
        season_stat_total.persist()
        season_stat_total = ProPlayerSeasonStatTotal.load(player_no=no)
    
    career_stat_total = ProPlayerCareerStatTotal.load(player_no=no)
    if not career_stat_total:
        career_stat_total = ProPlayerCareerStatTotal()
        career_stat_total.player_no = no
        career_stat_total.persist()
        career_stat_total = ProPlayerCareerStatTotal.load(player_no=no)
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps, \
             'season_stat_total': season_stat_total, 'career_stat_total': career_stat_total}
    return render_to_response(request, 'player/player_detail.html', datas)

@login_required
def youth_free_player(request, min=False):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
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
    
    if min:
        return render_to_response(request, 'player/youth_free_players_min.html', datas)
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
    player = YouthPlayer.load(no=no)
    if not player:
        return render_to_response(request, 'default_error.html', {'msg': u'球员不存在'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]
    
    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps}
    return render_to_response(request, 'player/youth_player_detail.html', datas)

@login_required
def attention_player(request, min=False):
    '''关注球员'''
    team = UserManager().get_team_info(request)
    infos = player_operator.get_attention_player(team.id)
    datas = {'infos': infos}
    if min:
        return render_to_response(request, 'player/attention_players_min.html', datas)
    return render_to_response(request, 'player/attention_players.html', datas)
    
@login_required
def free_player_bid(request):
    """自由球员出价"""
    
    if request.method == 'GET':
        datas = {}
        error = None
        no = request.GET.get('no')
        if not no:
            error = '球员不存在'
        
        min_price = 1000
        if no:
            player = FreePlayer.load(no=no)
            if not player:
                error = '球员不存在'
            else:
                current_price = player.current_price
                min_price = (current_price * 1.1)
                datas['min_price'] = min_price
                datas['no'] = no
        
        if error:
            return render_to_response(request, 'message.html', {'error': error})  
        return render_to_response(request, 'player/free_player_bid.html', datas)
    else:
        error = None
        success = "竟价成功"
        no = request.GET.get('no')
        price = request.GET.get('price')
        
        if not no or not price:
            error = '出价异常'
    
        if not error:
            print request.team.id
            result = player_operator.freeplayer_auction(request.team.id, no, price)
        
            if result == -1:
                error = '未知异常'
            elif result == -2:
                error = '您的资金不不足'
            elif result == -3:
                error = '您己经出过价了,不能再出价'
            elif result == -4:
                error = '球员不存在'
            elif result == -5:
                error = '您的出价低于该球员最低身价'
        
        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})

@login_required
def add_attention_player(request):
    '''关注球员'''
    if request.method == 'GET':
        success = '球员关注成功'
        error = None
        type = request.GET.get('type')
        no = request.GET.get('no')
        if not (type and no):
            error = '获取信息出错'
        
        if not error:
            result = player_operator.attention_player(request.team.id, no, type)
            if result == 2:
                error = '该球员您已经关注过了'
            elif result == 3:
                error = '最多只能关注10个球员'
            elif result == -1:
                error = '应用服务器异常'
        
        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})

@login_required
def remove_attention_player(request):
    '''取消关注球员'''
    if request.method == 'GET':
        no = request.GET.get('no')
        return render_to_response(request, 'player/remove_attention_player.html', {'no': no})

    else:
        success = '取消关注成功'
        error = None
        no = request.GET.get('no')
        if player_operator.cancel_attention_player(request.team.id, no) == -1:
            error = '应用服务器异常'
        
        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})
    
@login_required
def player_detail(request):
    """player detail"""
    no = request.GET.get('no', None)
    type = request.GET.get('type', None)
    from_page = request.GET.get('from_page', None)
    from_id = request.GET.get('from_id', None)
    error = None
    
    if not no or not type:
        error = u'获取球员信息出错'
    else:
        type = int(type)
        if type == 1:
            player = FreePlayer.load(no=no)
        elif type == 2:
            player = ProfessionPlayer.load(no=no)
        elif type == 3:
            player = YouthPlayer.load(no=no)
        elif type == 4:
            player = YouthFreePlayer.load(no=no)
        if not player:
            error = u'获取球员信息出错'
        datas = {'player': player}
        
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if from_page:
        datas['url'] = reverse(from_page)
        datas['from_id'] = from_id
    return render_to_response(request, 'player/common_detail.html', datas)