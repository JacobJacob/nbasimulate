#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.user',
    url(r'^loginpage/$', 'views.login_page', name='login-page'),
    url(r'^login/$', 'views.login', name='login'),
    url(r'^registerpage/$', 'views.register_page', name='register-page'),
    url(r'^register/$', 'views.register', name='register'),
    url(r'^onlineuser/$', 'views.online_user', name='online-user'),
)
