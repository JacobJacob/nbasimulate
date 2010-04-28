#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required
from gba.entity import Unions
from gba.common import exception_mgr
from gba.business.client import ClientManager
from gba.business.common_client_monitor import CommonClientMonitor

def union_list(request, min=False):
    """联盟列表"""
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    infos, total = Unions.paging(page, pagesize, order="prestige desc")
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    if min:
        return render_to_response(request, 'union/union_list_min.html', datas)
    return render_to_response(request, 'union/union_list.html', datas)

def team_union(request, min=False):
    """我的联盟"""
    team = request.team
    datas = {}
    
    has_union = False
    if team.union_id:
        has_union = True
        
    if not has_union:
        return render_to_response(request, 'union/team_not_union.html', datas)
    
    if min:
        return render_to_response(request, 'union/team_union_min.html', datas)
    return render_to_response(request, 'union/team_union.html', datas)

@login_required
def union_add(request):
    """创建联盟"""
    
    team = request.team
    
    datas = {}
    if request.method == 'GET':
        return render_to_response(request, 'union/union_add.html', datas)

    else:
        error = None
        success = '联盟创建成功'
        name = request.POST.get('name', '')
        short_name = request.POST.get('short_name', '')
        qq_group = request.POST.get('qq_group', '')
        forum = request.POST.get('forum', '')
        desc = request.POST.get('desc', '')
        
        union = Unions()
        union.name = name
        union.short_name = short_name
        union.qq_group = qq_group
        union.forum = forum
        union.leader = team.username
        union.union_desc = desc
        
        try:
            union.persist()
            team.union_id = union.id
            team.persist()
        except:
            raise
            exception_mgr.on_except()
            error = '服务器异常'
        
        return render_to_response(request, 'union/union_add_message.html', {'success': success, 'error': error})