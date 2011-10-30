#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.model import Article

from xba.common import staticutil

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
    
    response = render_to_response(request, "article/list.html", locals())
    path = "/article/%s/list/%s.html" % (category, page)
    staticutil.dump(response, path)
    return response

def detail(request, category, id):
    """街球 杯赛"""
    article = Article.load(id=id)
    
    response = render_to_response(request, "article/detail.html", locals())
    path = "/article/%s/detail/%s.html" % (category, id)
    staticutil.dump(response, path)
    return response