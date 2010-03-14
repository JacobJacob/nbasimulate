#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.match',
    url(r'^$', 'views.profession_tactical', name='profession-tactical'),
    url(r'^professiontacticaldetail/$', 'views.profession_tactical_detail', name='profession-tactical-detail'),
)
