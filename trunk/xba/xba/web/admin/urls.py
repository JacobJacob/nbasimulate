#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.admin',
    url(r'^article_list_create', 'views.create_article', name='create-article'),
    url(r'^article_list', 'views.article_list', name='article-list'),
)
