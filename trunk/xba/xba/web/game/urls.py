#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.game',
     url(r'^gameinfo$', 'views.gameinfo', name='gameinfo'),

)
