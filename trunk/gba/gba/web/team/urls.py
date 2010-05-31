#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.team',
    url(r'^season_finance/$', 'views.season_finance', name='season-finance'),
    url(r'^all_finance/$', 'views.all_finance', name='all-finance'),
    url(r'^season_finance_min/$', 'views.season_finance', {'min': True}, name='season-finance-min'),
    url(r'^arena_build/$', 'views.arena_build', name='arena-build'),
    url(r'^arena_build_min/$', 'views.arena_build', {'min': True}, name='arena-build-min'),
    url(r'^team_staff/$', 'views.team_staff', name='team-staff'),
    url(r'^team_staff_min/$', 'views.team_staff', {'min': True}, name='team-staff-min'),
    url(r'^hire_staff/$', 'views.hire_staff', name='hire-staff'),
    url(r'^dismiss_staff/$', 'views.dismiss_staff', name='dismiss-staff'),
    url(r'^team_ad/$', 'views.team_ad', name='team-ad'),
    url(r'^team_ad_min/$', 'views.team_ad',{'min': True}, name='team-ad-min'),
    url(r'^select_ad/$', 'views.select_ad', name='select-ad'),
    url(r'^team_info/$', 'views.team_info', name='team-info'),
    url(r'^friends/$', 'views.friends', name='friends'),
    url(r'^friends_min/$', 'views.friends', {'min': True}, name='friends-min'),
    url(r'^add_friend/$', 'views.add_friend', name='add-friend'),
    url(r'^delete_friend/$', 'views.delete_friend', name='delete-friend'),
    url(r'^update_team_info/$', 'views.update_team_info', name='update-team-info'),
    url(r'^team_ranking/$', 'views.team_ranking', name='team-ranking'),
    url(r'^team_ranking_min/$', 'views.team_ranking', {'min': True}, name='team-ranking-min'),
    url(r'^player_ranking/$', 'views.player_ranking', name='player-ranking'),
    url(r'^team_honor/$', 'views.team_honor', name='team-honor'),
    url(r'^team_honor_min/$', 'views.team_honor', {'min': True}, name='team-honor-min'),
    url(r'^arena_update/$', 'views.arena_update', name='arena-update'),
    url(r'^arena_update_save/$', 'views.arena_update_save', name='arena-update-save'),
    url(r'^arena_price_update/$', 'views.arena_price_update', name='arena-price-update'),
    url(r'^register/$', 'views.register_team', name='register-team'),
)