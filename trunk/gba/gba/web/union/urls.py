#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.conf.urls.defaults import url, patterns

urlpatterns = patterns('web.union',
    # clients
    url(r'^union_list/$', 'views.union_list', name='union-list'),
    url(r'^union_list_min/$', 'views.union_list', {'min': min}, name='union-list-min'),
    url(r'^team_union/$', 'views.team_union', name='team-union'),
    url(r'^team_union_min/$', 'views.team_union', {'min': True}, name='team-union-min'),
    url(r'^union_add/$', 'views.union_add', name='union-add'),
)
