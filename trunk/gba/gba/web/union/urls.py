#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import url, patterns

urlpatterns = patterns('web.union',
    # clients
    url(r'^union_list/$', 'views.union_list', name='union-list'),
    url(r'^union_list_min/$', 'views.union_list', {'min': True}, name='union-list-min'),
    url(r'^team_union/$', 'views.team_union', name='team-union'),
    url(r'^team_union_min/$', 'views.team_union', {'min': True}, name='team-union-min'),
    url(r'^union_add/$', 'views.union_add', name='union-add'),
    url(r'^union_apply/$', 'views.union_apply', name='union-apply'),
    url(r'^union_announce/$', 'views.union_announce', name='union-announce'),
    url(r'^wait_appove_list/$', 'views.wait_appove_list', name='wait-appove-list'),
    url(r'^union_member_appove/$', 'views.union_member_appove', name='union-member-appove'),
    url(r'^union_member/$', 'views.union_member', name='union-member'),
    url(r'^union_manager_setting/$', 'views.union_manager_setting', name='union-manager-setting'),
    url(r'^union_title_setting/$', 'views.union_title_setting', name='union-title-setting'),
    url(r'^union_detail/$', 'views.union_detail', name='union-detail'),
    url(r'^union_detail_min/$', 'views.union_detail', {'min': True}, name='union-detail-min'),
    url(r'^union_war_list/$', 'views.union_war_list', name='union-war-list'),
    url(r'^union_war_list_min/$', 'views.union_war_list', {'min': True}, name='union-war-list-min'),
    url(r'^union_war_request/$', 'views.union_war_request', name='union-war-request'),
    url(r'^union_war_accept/$', 'views.union_war_accept', name='union-war-accept'),
    url(r'^union_war_history/$', 'views.union_war_history', name='union-war-history'),
    url(r'^union_event/$', 'views.union_event', name='union-event'),
    url(r'^team_union_war/$', 'views.team_union_war', name='team-union-war'),
)