#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response, json_response
from gba.business.user_roles import login_required
from gba.entity import ActionDesc, EngineStatus, RoundUpdateLog, Cup , ClientRunningLog, Matchs, ErrorMatch
from gba.business.client import ClientManager
from gba.business.common_client_monitor import CommonClientMonitor
from gba.common.constants import MatchTypeMaps, MatchStatusMap, MatchShowStatusMaps
from gba.business import admin_operator, match_operator

def index(request):
    """list"""
    datas = {}
    return render_to_response(request, 'admin/index.html', datas)

@login_required
def betch_log(request):
    """每天更新日志"""
    return render_to_response(request, 'admin/betch_log_ex.html')

@login_required
def betch_log_json(request):
    """json格式的每天更新日志"""
    start = int(request.POST.get('start', 0))
    limit = int(request.POST.get('limit', 20))
    sort = request.POST.get('sort', 'log_time')
    dir = request.POST.get('dir', 'desc')
    infos, total = ClientRunningLog.paging(pagesize=limit, start=start, order="%s %s" % (sort, dir))
    ret_infos = []
    for info in infos:
        if info.log_time:
            info.log_time = info.log_time.strftime("%Y-%m-%d %H:%M:%S")
        ret_infos.append(info.__dict__)
    return json_response({'infos': ret_infos, 'total': total})

def action_desc(request):
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    action_name = request.GET.get('action_name', '')

    condition = '1=1'
    if action_name:
        condition += ' and action_name="%s"' % action_name
        
    action_names = admin_operator.get_action_names()

    infos, total = ActionDesc.paging(page, pagesize, condition=condition)
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'action_name': action_name,
            'action_names': action_names}
    return render_to_response(request, 'admin/action_desc.html', datas)

def list(request):
    clients = ClientManager.get_clients()
    categories = {}
    # 按类别分类
    for client in clients:
        client_type = client['type']
        if client_type not in categories:
            categories[client_type] = []
        categories[client_type].append(client)
    return render_to_response(request, 'admin/clients.html', {'categories': categories})

def _get_common_client_monitor_info(client_type, monitor_type, time_type, time_length):
    monitor_info = CommonClientMonitor(client_type, monitor_type, time_type, time_length)
    clients_info = monitor_info.get_client_info()
    
    clients_list = [(client_info.client_id, client_info) for id, client_info in clients_info.items()]
    clients_list = sorted(clients_list, key=lambda d:d[0], reverse=False)
    for client_id, client_info in clients_list:
        client_info.total = client_info.count1 + client_info.count2 + client_info.count3 + client_info.count4
        if client_info.is_active:
            client_info.img_label = "active"
        else:
            client_info.img_label = "death"
            
    return clients_list

def engine_status(request):
    '''比赛引擎状态'''
    infos = EngineStatus.query()
    datas = {'infos': infos}
    return render_to_response(request, 'admin/engine_status.html' , datas)

def daily_update_log(request):
    return render_to_response(request, 'admin/daily_update_log_ex.html')

@login_required
def daily_update_log_json(request):
    """json格式的每天更新日志"""
    start = int(request.POST.get('start', 0))
    limit = int(request.POST.get('limit', 20))
    sort = request.POST.get('sort', 'id')
    dir = request.POST.get('dir', 'desc')
    infos, total = RoundUpdateLog.paging(pagesize=limit, start=start, order="%s %s" % (sort, dir))
    ret_infos = []
    for info in infos:
        if info.created_time:
            info.created_time = info.created_time.strftime("%Y-%m-%d %H:%M:%S")
        if info.start_time:
            info.start_time = info.start_time.strftime("%Y-%m-%d %H:%M:%S")
        if info.end_time:
            info.end_time = info.end_time.strftime("%Y-%m-%d %H:%M:%S")
        ret_infos.append(info.__dict__)
    return json_response({'infos': ret_infos, 'total': total})

def cup_list(request):
    '''将杯管理'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    infos, total = Cup.paging(page, pagesize, order='id desc ')
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1}
    return render_to_response(request, 'admin/cup_list.html', datas)

@login_required
def match_list(request):
    """比赛管理"""
    return render_to_response(request, 'admin/match_list_ex.html')

@login_required
def match_list_json(request):
    """json格式的比赛列表"""
    start = int(request.POST.get('start', 0))
    limit = int(request.POST.get('limit', 20))
    sort = request.POST.get('sort', 'id')
    dir = request.POST.get('dir', 'desc')
    infos, total = Matchs.paging(pagesize=limit, start=start, order="%s %s" % (sort, dir))
    ret_infos = []
    for info in infos:
        if info.created_time:
            info.created_time = info.created_time.strftime("%Y-%m-%d %H:%M:%S")
        if info.start_time:
            info.start_time = info.start_time.strftime("%Y-%m-%d %H:%M:%S")
        if info.expired_time:
            info.expired_time = info.expired_time.strftime("%Y-%m-%d %H:%M:%S")
        info.type = MatchTypeMaps.get(info.type, u'未知')
        info.status = MatchStatusMap.get(info.status, u'未知')
        info.show_status = MatchShowStatusMaps.get(info.show_status, u'未知')
        info.next_show_status = MatchShowStatusMaps.get(info.next_show_status, u'未知')
        ret_infos.append(info.__dict__)
    return json_response({'infos': ret_infos, 'total': total})

@login_required
def error_match(request):
    """异常比赛管理"""
    return render_to_response(request, 'admin/error_match_ex.html')

@login_required
def error_match_json(request):
    """json格式的异常比赛列表"""
    start = int(request.POST.get('start', 0))
    limit = int(request.POST.get('limit', 20))
    sort = request.POST.get('sort', 'id')
    dir = request.POST.get('dir', 'desc')
    infos, total = ErrorMatch.paging(pagesize=limit, start=start, order="%s %s" % (sort, dir))
    ret_infos = []
    for info in infos:
        if info.created_time:
            info.created_time = info.created_time.strftime("%Y-%m-%d %H:%M:%S")
        info.type = MatchTypeMaps.get(info.type, u'未知')
        ret_infos.append(info.__dict__)
    return json_response({'infos': ret_infos, 'total': total})

@login_required
def error_match_restart(request):
    """异常比赛重赛"""
    match_id = int(request.POST.get('match_id'))
    result = 'success'
    try:
        match_operator.handle_error_match(match_id, delete_error=True)
    except:
        result = 'error'    
    return json_response(result)