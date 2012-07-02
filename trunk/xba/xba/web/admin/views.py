#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.http import HttpResponseRedirect
from django.core.urlresolvers import reverse

from xba.web.render import render_to_response
from xba.model import Article

from xba.business import user_roles
from xba.common.decorators import login_required
from xba.business.user_roles import REDIRECT_FIELD_NAME, SESSION_KEY

@login_required
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

@login_required
def article_list(request):
    """文章列表"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 10))
    category = request.REQUEST.get("category", "notice")
    
    infos, total = Article.paging(page, pagesize, condition=" category = '%s' " % category, order="id desc")
    return render_to_response(request, "admin/article_list.html", locals())

@login_required
def index(request):
    """后台首页"""
    print "index"
    return render_to_response(request, "admin/index.html", locals())

def login(request):
    if not request.POST:
        user = user_roles.get_userinfo(request)
        username = user and user.username or None
        data = {'username': username,
                'post_path': request.get_full_path()}
        return render_to_response(request, 'admin/login.html', data)
    
    # login post back
    username = request.POST.get('username', None)
    password = request.POST.get('password', None)

    if not username or not password:
        error_message = "用户名或者密码不能都不填写，请重新登录。";
        data = {'error_message': error_message,
                'post_path': request.get_full_path()}
        return render_to_response(request, 'admin/login.html', data) 
    
    r, session_id_or_msg = user_roles.login(username, password)
    if r == 0:
        redirect_to = request.GET.get(REDIRECT_FIELD_NAME, reverse('admin-index'))
        response = HttpResponseRedirect(redirect_to)
        response.set_cookie(user_roles.SESSION_KEY, session_id_or_msg)
        return response
    else:
        data = {'username': username,
                'post_path': request.get_full_path(),
                "error_message": session_id_or_msg}
        return render_to_response(request, 'admin/login.html', data)

def logout(request):
    user_roles.logout(request)
    response = HttpResponseRedirect("/admin/login")
    response.delete_cookie(SESSION_KEY)
    return response