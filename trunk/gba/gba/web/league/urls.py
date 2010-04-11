#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.league',
    url(r'^league_rank/$', 'views.league_rank', name='league-rank'),
    url(r'^league_rank_min/$', 'views.league_rank', {'min': True}, name='league-rank-min'),
    url(r'^league_schedule/$', 'views.league_schedule', name='league-schedule'),
    url(r'^league_schedule_min/$', 'views.league_schedule', {'min': True}, name='league-schedule-min'),
    url(r'^pre_schedule/$', 'views.pre_schedule', name='pre-schedule'),
    url(r'^current_schedule/$', 'views.current_schedule', name='current-schedule'),
    url(r'^league_statistics/$', 'views.league_statistics', name='league-statistics'),
    url(r'^team_statistics/$', 'views.team_statistics', name='team-statistics'),
)
