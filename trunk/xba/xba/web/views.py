#-*- coding:utf-8 -*-

from xba.web.render import render_to_response

def index(request):
    '''首页'''
    datas = {}
    return render_to_response(request, "index.html", datas)


