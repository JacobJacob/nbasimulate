#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.notice',
     url(r'^list', 'views.list', name='notice-list'),

)
