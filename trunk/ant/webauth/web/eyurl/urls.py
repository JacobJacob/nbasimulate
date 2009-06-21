#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import *

urlpatterns = patterns('webauth.web.eyurl',
    url(r'^/report/$', 'views.report_eyurl', name='report-eyurl'),
)
