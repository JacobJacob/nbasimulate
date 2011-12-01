#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.guess',
    url(r'^guess_list', 'views.list', name='guess-list'),
)
