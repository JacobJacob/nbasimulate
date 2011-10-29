#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.business import club_manager

from xba.common.decorators import login_required

@login_required
def club(request):
    """职业球员"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    category = int(request.REQUEST.get("category", 5))
    total, infos = club_manager.get_club_list(page, pagesize, category)
    return render_to_response(request, "club/club.html", locals())