#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('dtspider.web.monitor',
    # clients
    url(r'^clients/$', 'views.list', name='client-list'),
)
