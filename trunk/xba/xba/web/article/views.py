#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.model import Article

def list(request, category, page=1):
    """街球 杯赛"""
    page = int(page)
    if page < 1:
        page = 1
    pagesize = 20
    infos, total = Article.paging(page, pagesize, condition="status=1 and category='%s'" % category, order="created_time desc")
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    nextpage = page + 1
    prevpage = page - 1
    
    return render_to_response(request, "article/list.html", locals())

def detail(request, category, id):
    """街球 杯赛"""
    article = Article.load(id=id)
    return render_to_response(request, "article/detail.html", locals())