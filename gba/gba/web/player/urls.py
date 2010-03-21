#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.player',
    # clients
     url(r'^$', 'views.index', name='free-players'),
     url(r'^freeplayer_detail/$', 'views.freeplayer_detail', name='freeplayer-detail'),
     url(r'^profession_player/$', 'views.profession_player', name='profession-player'),
     url(r'^profession_player_detail/$', 'views.profession_player_detail', name='profession-player-detail'),
     url(r'^youthfreeplayer/$', 'views.youth_free_player', name='youth-free-players'),
     url(r'^youth_freeplayer_detail/$', 'views.youth_freeplayer_detail', name='youth-freeplayer-detail'),
#    url(r'^edit/$', 'views.edit', name='table-edit'),
)