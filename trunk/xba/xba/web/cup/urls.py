#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.cup',
     url(r'^cuplist', 'views.cuplist', name='cuplist'),

)
