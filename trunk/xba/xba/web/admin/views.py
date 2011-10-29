#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.model import Article

def create_article(request):
    """创建文章"""
    id = request.REQUEST.get("id", 0)
    if not id:
        id = 0
    else:
        id = int(id)
    if request.method == "POST":
        category = request.REQUEST.get("category")
        title = request.REQUEST.get("title")
        content = request.REQUEST.get("content")
        keyword = request.REQUEST.get("keyword")
        summary = request.REQUEST.get("summary")
        article = Article()
        article.category = category
        article.title = title
        article.content = content
        article.keyword = keyword
        article.summary = summary
        if id > 0:
            article.id = id
        article.persist()
        message = "文章%s成功" % ("更新" if id > 0 else "保存")
    else:
        id = int(request.REQUEST.get("id", 0))
        if id > 0:
            article = Article.load(id=id)
    
    return render_to_response(request, "admin/article_new.html", locals())

def article_list(request):
    """文章列表"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 10))
    category = request.REQUEST.get("category", "notice")
    
    infos, total = Article.paging(page, pagesize, condition=" category = '%s' " % category)
    return render_to_response(request, "admin/article_list.html", locals())