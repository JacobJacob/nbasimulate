#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.business import game_manager

def gameinfo(request):
    """游戏信息"""
    info = game_manager.game_info()
    return render_to_response(request, "game/gameinfo.html", locals())