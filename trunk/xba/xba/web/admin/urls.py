#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.admin',
    url(r'^index', 'views.index', name='admin-index'),
    url(r'^login', 'views.login', name='admin-login'),
    url(r'^logout', 'views.logout', name='admin-logout'),
    url(r'^article_list_create', 'views.create_article', name='create-article'),
    url(r'^article_list', 'views.article_list', name='article-list'),
)
