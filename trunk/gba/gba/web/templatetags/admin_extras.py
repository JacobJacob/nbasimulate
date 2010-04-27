#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

import datetime

from django import template

from gba.common.constants import ClientStatus, CLIENT_STATUS_NAMES


register = template.Library()

def _client_status(client_info):
    status = client_info['status']
    if status not in CLIENT_STATUS_NAMES:
        return 'Unkow(%s)' % status
    now =  datetime.datetime.now()
    if client_info['updated_time'] < client_info['last_time'] > 600: # 10分钟没响应的，则当做久无响应处理
        status = ClientStatus.DEATH
    return CLIENT_STATUS_NAMES[status]

@register.filter
def client_status(client_info):
    return _client_status(client_info)

@register.filter
def client_total_info(clients):
    info = {}
    for client in clients:
        status = _client_status(client)
        info[status] = info.get(status, 0) + 1
    return u', '.join([u'%s: %d' % (k, v) for k, v in info.items()])