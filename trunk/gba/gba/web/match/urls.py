#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.match',
    url(r'^$', 'views.profession_tactical', name='profession-tactical'),
    url(r'^professiontacticaldetail/$', 'views.profession_tactical_detail', name='profession-tactical-detail'),
    url(r'^friendlymatch/$', 'views.friendly_match', name='friendly-match'),
    url(r'^matchstat/$', 'views.match_stat', name='match-stat'),
    url(r'^matchdetail/$', 'views.match_detail', name='match-detail'),
)
