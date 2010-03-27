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
