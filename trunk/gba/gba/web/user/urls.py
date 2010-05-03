#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('web.user',
    url(r'^8800/$', 'views.login_page', name='login-page'),
    url(r'^8801/$', 'views.login', name='login'),
    url(r'^8802/$', 'views.register_page', name='register-page'),
    url(r'^8803/$', 'views.register', name='register'),
    url(r'^8804/$', 'views.online_user', name='online-user'),
    url(r'^online_user_min/$', 'views.online_user', {'min': True}, name='online-user-min'),
    url(r'^8805/$', 'views.message', name='message'),
    url(r'^8806/$', 'views.send_match_request', name='send-match-request'),
    url(r'^8807/$', 'views.send_message', name='send-message'),
    url(r'^8808/$', 'views.check_new_message', name='check-new-message'),
    url(r'^8809/$', 'views.user_detail', name='user-detail'),
    url(r'^8810/$', 'views.message', {'min': True}, name='message-min'),
    url(r'^8811/$', 'views.issue_message', name='issue-message'),
    url(r'^8812/$', 'views.out_message', name='out-message'),
    url(r'^8813/$', 'views.message_detail', name='message-detail'),
    url(r'^8814/$', 'views.delete_message', name='delete-message'),
    url(r'^8815/$', 'views.delete_message', {'all': True}, name='delete-all-message'),
)
