#-*- coding:utf-8 -*-
from django.shortcuts import render_to_response

def index(request):
    content = u'首页'
    return render_to_response("index.html", locals())