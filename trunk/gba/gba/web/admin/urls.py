#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import url, patterns

urlpatterns = patterns('web.admin',
    # clients
    url(r'^$', 'views.index', name='admin-index'),
    url(r'^betchlog/$', 'views.betch_log', name='betch-log'),
    url(r'^betch_log_json/$', 'views.betch_log_json', name='betch-log-json'),
    url(r'^actiondesc/$', 'views.action_desc', name='action-desc'),
    url(r'^clients/$', 'views.list', name='client-list'),
    url(r'^engine_status/$', 'views.engine_status', name='engine-status'),
    url(r'^daily_update_log/$', 'views.daily_update_log', name='daily-update-log'),
    url(r'^daily_update_log_json/$', 'views.daily_update_log_json', name='daily-update-log-json'),
    url(r'^cup_list/$', 'views.cup_list', name='cup-list'),
    url(r'^match_list/$', 'views.match_list', name='match-list'),
    url(r'^match_list_json/$', 'views.match_list_json', name='match-list-json'),
    url(r'^error_match/$', 'views.error_match', name='error-match'),
    url(r'^error_match_json/$', 'views.error_match_json', name='error-match-json'),
    url(r'^error_match_restart/$', 'views.error_match_restart', name='error-match-restart'),
)
