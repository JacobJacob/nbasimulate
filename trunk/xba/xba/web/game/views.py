#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.web.render import render_to_response
from xba.business import game_manager

from xba.common.decorators import login_required

@login_required
def gameinfo(request):
    """游戏信息"""
    info = game_manager.game_info()
    return render_to_response(request, "game/gameinfo.html", locals())

@login_required
def announce_list(request):
    """游戏公告"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    total, infos = game_manager.get_announce(page, pagesize)
    if infos:
        for info in infos:
            info.title = info.title.decode("utf8")
    return render_to_response(request, "game/announce.html", locals())



