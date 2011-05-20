#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.business import player5_manager

def player5(request):
    """职业球员"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    category = int(request.REQUEST.get("category", 2))
    total, infos = player5_manager.player_list(page, pagesize, category)
    return render_to_response(request, "player/player5.html", locals())