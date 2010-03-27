#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.admin',
    # clients
    url(r'^$', 'views.index', name='admin-index'),
    url(r'^left/$', 'views.left', name='admin-left'),
    url(r'^right/$', 'views.right', name='admin-right'),
    url(r'^admintop/$', 'views.admin_top', name='admin-top'),
    url(r'^betchlog/$', 'views.betch_log', name='betch-log'),
    url(r'^actiondesc/$', 'views.action_desc', name='action-desc'),
)
