#-*- coding:utf-8 -*-
from django.shortcuts import render_to_response

def index(request):
    content = u'首页'
    return render_to_response("index.html", locals())

def left(request):
    content = u'首页'
    return render_to_response("left.html", locals())

def right(request):
    content = u'首页'
    return render_to_response("right.html", locals())

def admin_top(request):
    return render_to_response("admin_top.html", locals())

def login(request):
    content = u'用户登录'
    return render_to_response("login.html", locals())