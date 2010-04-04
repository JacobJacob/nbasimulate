#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.match',
    url(r'^8900$', 'views.profession_tactical', name='profession-tactical'),
    url(r'^8901/$', 'views.profession_tactical_detail', name='profession-tactical-detail'),
    url(r'^8902/$', 'views.friendly_match', name='friendly-match'),
    url(r'^8903/$', 'views.friendly_match', {'min': True}, name='friendly-match-min'),
    url(r'^8904/$', 'views.match_stat', name='match-stat'),
    url(r'^8905/$', 'views.match_detail', name='match-detail'),
    url(r'^8906/$', 'views.training_center', name='training-center'),
    url(r'^8907/$', 'views.youth_tactical', name='youth-tactical'),
    url(r'^8908/$', 'views.youth_tactical_detail', name='youth-tactical-detail'),
    url(r'^8909/$', 'views.match_accept', name='match-accept'),
    url(r'^8910$', 'views.profession_tactical', {'min': True}, name='profession-tactical-min'),
)
