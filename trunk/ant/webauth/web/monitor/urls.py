#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('webauth.web.monitor',
    # clients
    url(r'^clients/$', 'client_views.list', name='client-list'),
)
