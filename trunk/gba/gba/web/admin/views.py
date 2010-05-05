#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required
from gba.entity import ClientRunningLog, ActionDesc, EngineStatus, RoundUpdateLog
from gba.business.client import ClientManager
from gba.business.common_client_monitor import CommonClientMonitor
from gba.business import admin_operator

def index(request):
    """list"""
    datas = {}
    return render_to_response(request, 'admin/index.html', datas)

def left(request):
    content = u'首页'
    return render_to_response(request, "admin/left.html", locals())

def right(request):
    content = u'首页'
    return render_to_response(request, "admin/right.html", locals())

def admin_top(request): 
    return render_to_response(request, "admin/admin_top.html", locals())

@login_required
def betch_log(request):
    """日志"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    client_type = request.GET.get('client_type', None)
    date = request.GET.get('date', None)
    
    condition = '1=1'
    if client_type:
        condition += ' and client_type="%s"' % client_type
    if date:
        condition += ' and log_time >="%s" and log_time <= ' % (date, date)
    
    infos, total = ClientRunningLog.paging(page, pagesize, condition=condition)
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'client_type': client_type, 'date': date}
    
    return render_to_response(request, 'admin/betch_log.html', datas)

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
        totalpage = (total -1) / pagesize + 1
    
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
    clients_list = sorted(clients_list, key = lambda d:d[0], reverse = False)
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

def round_update_log(request):
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))


    infos, total = RoundUpdateLog.paging(page, pagesize, order='id desc ')
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1}
    return render_to_response(request, 'admin/round_update_log.html', datas)
