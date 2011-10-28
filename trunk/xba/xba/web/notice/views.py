#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response

def list(request):
    """指手指南列表"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    category = int(request.REQUEST.get("category", 5))
    return render_to_response(request, "strategy/list.html", locals())