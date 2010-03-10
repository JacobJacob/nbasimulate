#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.admin',
    # clients
     url(r'^$', 'views.index', name='free-players'),
     url(r'^freeplayer_detail/$', 'views.freeplayer_detail', name='freeplayer-detail'),
     url(r'^profession_player/$', 'views.profession_player', name='profession-player'),
#    url(r'^edit/$', 'views.edit', name='table-edit'),
)
