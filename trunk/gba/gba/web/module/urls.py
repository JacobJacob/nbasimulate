#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.module',
    # clients
    url(r'^$', 'views.index', name='module-index'),
    url(r'^(?P<module>\w+)/list/$', 'views.list', name='module-list'),
    url(r'^(?P<module>\w+)/edit/$', 'views.edit', name='module-edit'),
)
