#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import url, patterns

urlpatterns = patterns('web.admin',
    # clients
    url(r'^$', 'views.index', name='admin-index'),
    url(r'^betchlog/$', 'views.betch_log', name='betch-log'),
    url(r'^betch_log_json/$', 'views.betch_log_json', name='betch-log-json'),
    url(r'^action_desc/$', 'views.action_desc', name='action-desc'),
    url(r'^action_desc_json/$', 'views.action_desc_json', name='action-desc-json'),
    url(r'^action_name_list_json/$', 'views.action_name_list_json', name='action-name-list-json'),
    url(r'^get_action_desc_json/$', 'views.get_action_desc_json', name='get-action-desc-json'),
    url(r'^action_desc_update/$', 'views.action_desc_update', name='action-desc-update'),
    url(r'^clients/$', 'views.list', name='client-list'),
    url(r'^engine_status/$', 'views.engine_status', name='engine-status'),
    url(r'^engine_status_json/$', 'views.engine_status_json', name='engine-status-json'),
    url(r'^engine_status_cmd/$', 'views.engine_status_cmd', name='engine-status-cmd'),
    url(r'^daily_update_log/$', 'views.daily_update_log', name='daily-update-log'),
    url(r'^daily_update_log_json/$', 'views.daily_update_log_json', name='daily-update-log-json'),
    url(r'^match_list/$', 'views.match_list', name='match-list'),
    url(r'^match_list_json/$', 'views.match_list_json', name='match-list-json'),
    url(r'^error_match/$', 'views.error_match', name='error-match'),
    url(r'^error_match_json/$', 'views.error_match_json', name='error-match-json'),
    url(r'^error_match_restart/$', 'views.error_match_restart', name='error-match-restart'),
    url(r'^league_list/$', 'views.league_list', name='league-list'),
    url(r'^league_list_json/$', 'views.league_list_json', name='league-list-json'),
    url(r'^cup_list/$', 'views.cup_list', name='cup-list'),
    url(r'^cup_list_json/$', 'views.cup_list_json', name='cup-list-json'),
    url(r'^cup_update/$', 'views.cup_update', name='cup-update'),
    url(r'^get_cup_json/$', 'views.get_cup_json', name='get-cup-json'),
    url(r'^list_database_status/$', 'views.list_database_status', name='list-database-status'),
    url(r'^database_status_json/$', 'views.database_status_json', name='databasestatus-json'),
)