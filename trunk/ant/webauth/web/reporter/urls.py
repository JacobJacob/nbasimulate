#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('webauth.web.reporter',
    # clients
    url(r'/daily_reporter/$', 'daily_reporter.generate_report', name='daily-reporter'),
)
