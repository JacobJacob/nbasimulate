#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.monitor',
    # clients
    url(r'^clients/$', 'views.list', name='client-list'),
    url(r'^/clients_monitor_open/$', 'views.monitor_open', name='client-monitor-open'),
    url(r'^/clients_monitor/$', 'views.monitor', name='client-monitor'),
)
