#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.player',
    # clients
     url(r'^$', 'views.index', name='free-players'),
     url(r'^freeplayer_detail/$', 'views.freeplayer_detail', name='freeplayer-detail'),
#    url(r'^edit/$', 'views.edit', name='table-edit'),
)
