#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.business import player5_manager
from xba.business import player3_manager

def player5(request):
    """职业球员"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    category = int(request.REQUEST.get("category", 2))
    total, infos = player5_manager.player_list(page, pagesize, category)
    return render_to_response(request, "player/player5.html", locals())

def player3(request):
    """街球球员"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    category = int(request.REQUEST.get("category", 6))
    total, infos = player3_manager.player_list(page, pagesize, category)
    return render_to_response(request, "player/player3.html", locals())