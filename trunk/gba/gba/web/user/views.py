#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关视图"""

from django.http import HttpResponseRedirect

from gba.web.render import render_to_response
from gba.business.user_roles import UserManager, login_required
from gba.business import user_operator, match_operator

SESSION_KEY = '_auth_user_id'

def login_page(request):
    next = request.GET.get('next')
    return render_to_response(request, "user/login.html", locals())

def login(request):
    content = u'用户登录'
    
    username = request.POST.get('username')
    password = request.POST.get('password')
    
    user_manager = UserManager()
    success, session_id = user_manager.login(username, password)

    next = request.GET.get('next')
    
    print next    
    if success == 0:
        response = HttpResponseRedirect(next if next else '/')
        response.set_cookie(SESSION_KEY, session_id)
        return response
    else:
        message = session_id
        return render_to_response(request, "user/login_error.html", locals())
    
def register_page(request):
    '''注册页面'''
    return render_to_response(request, "user/register.html", locals())

def register(request):
    '''注册'''
    username = request.POST.get('username')
    password = request.POST.get('password')
    nickname = request.POST.get('nickname')
    teamname = request.POST.get('teamname')
    password_check = request.POST.get('password_check')
    
    user_manager = UserManager()

    success, session_id = user_manager.register_user(username, password, nickname)
    
    if success:
        response = HttpResponseRedirect('/')
        response.set_cookie(SESSION_KEY, session_id)
        match_operator.init_team({'username': username, 'teamname': teamname})
        return response
    else:
        message = session_id
        return render_to_response(request, "user/register_error.html", locals())
    
@login_required
def online_user(request):
    '''在线经理'''
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))

    infos, total = user_operator.get_online_users(page, pagesize)
    
    print infos
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, "user/online_user.html", locals())
    