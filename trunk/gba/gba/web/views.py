#-*- coding:utf-8 -*-

from gba.web.render import render_to_response
from gba.business.user_roles import UserManager

def index(request):
    content = u'首页'
    return render_to_response(request, "index.html", locals())

def left(request):
    content = u'首页'
    return render_to_response(request, "left.html", locals())

def right(request):
    content = u'首页'
    return render_to_response(request, "right.html", locals())

def admin_top(request):
    
    user_info = UserManager().get_userinfo(request)
    if user_info:
        username = user_info['username']
    
    return render_to_response(request, "admin_top.html", locals())

def development(request):
    return render_to_response(request, "development.html", locals())

