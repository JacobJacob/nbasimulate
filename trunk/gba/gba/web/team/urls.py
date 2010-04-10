#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.team',
    url(r'^season_finance/$', 'views.season_finance', name='season-finance'),
    url(r'^arena_build/$', 'views.arena_build', name='arena-build'),
    url(r'^team_staff/$', 'views.team_staff', name='team-staff'),
    url(r'^team_staff_min/$', 'views.team_staff', {'min': True}, name='team-staff-min'),
    url(r'^hire_staff/$', 'views.hire_staff', name='hire-staff'),
    
)
