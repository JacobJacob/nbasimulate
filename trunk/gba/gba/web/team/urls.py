#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.team',
    url(r'^season_finance/$', 'views.season_finance', name='season-finance'),
    url(r'^arena_build/$', 'views.arena_build', name='arena-build'),
)
