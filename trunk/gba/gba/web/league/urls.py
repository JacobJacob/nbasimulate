#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.league',
    url(r'^$', 'views.index', name='league-index'),
)
