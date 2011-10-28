#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.guide',
     url(r'^list', 'views.list', name='guide-list'),

)
