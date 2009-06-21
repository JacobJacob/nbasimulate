#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from datetime import datetime

from django import template

from webauth.common.constants import ClientStatus, CLIENT_STATUS_NAMES, URL_FROM_TYPE_MAP
from webauth.common.constants.url_task_const import URL_REASON

register = template.Library()

@register.filter
def client_status(client_info):
    status = client_info['status']
    if status not in CLIENT_STATUS_NAMES:
        return 'Unkow(%s)' % status
    now =  datetime.now()
    if client_info['updated_time'] < now and (now - client_info['updated_time']).seconds > 600: # 10分钟没响应的，则当做久无响应处理
        status = ClientStatus.DEATH
    return CLIENT_STATUS_NAMES[status]

@register.filter
def url_from_type(from_type):
    """来源"""
    return '%s(%s)' % (URL_FROM_TYPE_MAP.get(from_type, '未知'), from_type)

@register.filter
def url_result(result):
    """result format: (type, hit_url, reason)
    """
    type, hit_url, reason = result
    result_str = u'安全'
    if type == 3:
        result_str = u'危险'
    elif type == 4:
        result_str = u'警告'
    if hit_url:
        result_str += u', %s' % hit_url
    if reason:
        for info in reason:
            result_str += u', %s %s' % (URL_REASON.get(info[0], u'未知(%s)' % info[0]), 
                                        ' '.join([i for i in info[1:] if i]))
    return result_str