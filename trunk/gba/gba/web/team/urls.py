#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.team',
    url(r'^season_finance/$', 'views.season_finance', name='season-finance'),
    url(r'^all_finance/$', 'views.all_finance', name='all-finance'),
    url(r'^season_finance_min/$', 'views.season_finance', {'min': True}, name='season-finance-min'),
    url(r'^arena_build/$', 'views.arena_build', name='arena-build'),
    url(r'^team_staff/$', 'views.team_staff', name='team-staff'),
    url(r'^team_staff_min/$', 'views.team_staff', {'min': True}, name='team-staff-min'),
    url(r'^hire_staff/$', 'views.hire_staff', name='hire-staff'),
    url(r'^team_ad/$', 'views.team_ad', name='team-ad'),
    url(r'^team_ad_min/$', 'views.team_ad',{'min': True}, name='team-ad-min'),
    url(r'^select_ad/$', 'views.select_ad', name='select-ad'),
    url(r'^team_info/$', 'views.team_info', name='team-info'),
    url(r'^friends/$', 'views.friends', name='friends'),
    url(r'^friends_min/$', 'views.friends', {'min': True}, name='friends-min'),
    url(r'^add_friend/$', 'views.add_friend', name='add-friend'),
    url(r'^delete_friend/$', 'views.delete_friend', name='delete-friend'),
    url(r'^update_team_info/$', 'views.update_team_info', name='update-team-info'),
)
