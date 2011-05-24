#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.player',
     url(r'^player5$', 'views.player5', name='player5'),
     url(r'^player3$', 'views.player3', name='player3'),
)
