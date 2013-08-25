#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.article',
     url(r'^(?P<category>notice|strategy|guide|experience|nba|video|knowledge)/list/(?P<page>\d*)\.html', 'views.list', name='article-list'),
     url(r'^(?P<category>notice|strategy|guide|experience|nba|video|knowledge)/detail/(?P<id>\d*)\.html', 'views.detail', name='detail'),
     url(r'^comment', 'views.comment', name='comment'),
)
