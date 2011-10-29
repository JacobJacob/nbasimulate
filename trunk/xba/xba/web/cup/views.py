#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.business import cup_manager

from xba.common.decorators import login_required

@login_required
def cuplist(request):
    """街球 杯赛"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    category = int(request.REQUEST.get("category", 5))
    total, infos = cup_manager.get_cup_list(page, pagesize, category)
    return render_to_response(request, "cup/cuplist.html", locals())