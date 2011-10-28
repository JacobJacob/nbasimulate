#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.experience',
     url(r'^list', 'views.list', name='experience-list'),

)
