#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('webauth.web.urlinfo',
    # clients
    url(r'^search$', 'views.search', name='url-search'),
    url(r'^whitehosts$', 'views.white_hosts', name='white-hosts'),
)
