#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('web.admin',
    # clients
     url(r'^$', 'views.index', name='admin-index'),
#    url(r'^add/$', 'views.add', name='table-add'),
#    url(r'^edit/$', 'views.edit', name='table-edit'),
)
