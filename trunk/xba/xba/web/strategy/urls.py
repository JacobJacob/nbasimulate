#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.strategy',
     url(r'^list', 'views.list', name='strategy-list'),

)
