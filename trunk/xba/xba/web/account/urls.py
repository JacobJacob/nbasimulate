#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.account',
     url(r'^invitecode', 'views.invitecode', name='invitecode'),
     url(r'^download_invite_code', 'views.download_invite_code', name='download-invite-code'),
)
