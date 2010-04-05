#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.league',
    url(r'^league_rank/$', 'views.league_rank', name='league-rank'),
    url(r'^league_rank_min/$', 'views.league_rank', {'min': True}, name='league-rank-min'),
)
