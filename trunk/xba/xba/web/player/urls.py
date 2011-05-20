#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.player',
     url(r'^player5$', 'views.player5', name='player5'),

)
