#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.business.user_roles import login_required
from gba.entity import ClientRunningLog, ActionDesc

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
    action_name = request.GET.get('action_name', None)

    condition = '1=1'
    if action_name:
        action_name += ' and action_name="%s"' % action_name

    infos, total = ActionDesc.paging(page, pagesize, condition=condition)
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'client_type': action_name}
    return render_to_response(request, 'admin/action_desc.html', datas)

#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from dtspider.business.client import ClientManager
from dtspider.web.render import render_to_response
from dtspider.business.common_client_monitor import CommonClientMonitor


def list(request):
    clients = ClientManager.get_clients()
    categories = {}
    # 按类别分类
    for client in clients:
        client_type = client['type']
        if client_type not in categories:
            categories[client_type] = []
        categories[client_type].append(client)
    return render_to_response(request, 
                              'monitor/clients.html',
                              {'categories': categories})

def _get_url_check_monitor_info():
    monitor_info = UrlCheckClientMonitor()
    clients_info = monitor_info.get_client_info()

    clients_list = [(client_ip, client_info) for client_ip,client_info in clients_info.items()]
    clients_list = sorted(clients_list, key = lambda d:d[0], reverse = False)

    stat_info = {}
    
    rowcount = 8
    counter = 0
    
    all_check_speed = 0
    all_client_count = 0

    for client_ip, client_info in clients_list:
        if counter % rowcount == 0:
            client_info["new_line"] = True
        if counter % rowcount == rowcount - 1: 
            client_info["end_new_line"] = True
        
        if client_info["is_active"] and client_info["worker_all_active"]:
            client_info["img_label"] = "active"
        else:
            client_info["img_label"] = "death"
        
        for client in client_info["clients_info"]:
            if client.is_active and client.worker_is_active:
                client.img_label = "active"
                all_client_count += 1
                if client.worker_speed:
                    all_check_speed += (1 / client.worker_speed)
            else:
                client.img_label = "death"
                
            client.worker_speed = "%2d" % (int(client.worker_speed + 0.5),)
        counter += 1
    
    stat_info["all_check_speed"] = "%.1f" % (all_check_speed,)
    stat_info["all_client_count"] = str(all_client_count)
    
    return clients_list, stat_info

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

def monitor(request):
    data = {}
    data["url_check_clients_info"], data["url_check_stat_info"] = _get_url_check_monitor_info()
    data["google_pr_spider_info"] = _get_common_client_monitor_info("google_pr_spider" , 1, 2, 1) #1个小时统计数据
    data["alexa_spider_info"] = _get_common_client_monitor_info("alexa_spider", 1, 2, 1) #1个小时统计数据
    data["webshield_url_importer_info"] = _get_common_client_monitor_info("webshield_url_importer", 1, 2, 1) #1个小时统计数据
    
    return render_to_response(request, 
                              'monitor/url_check_monitor.html',
                              data)

def monitor_open(request):
    data = {}
    data["url_check_clients_info"], data["url_check_stat_info"] = _get_url_check_monitor_info() 
    return render_to_response(request, 
                              'monitor/client_monitor.html',
                              data)
   
