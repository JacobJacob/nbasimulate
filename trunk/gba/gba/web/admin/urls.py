#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import url, patterns

urlpatterns = patterns('web.admin',
    # clients
    url(r'^$', 'views.index', name='admin-index'),
    url(r'^betchlog/$', 'views.betch_log', name='betch-log'),
    url(r'^actiondesc/$', 'views.action_desc', name='action-desc'),
    url(r'^clients/$', 'views.list', name='client-list'),
    url(r'^engine_status/$', 'views.engine_status', name='engine-status'),
)
