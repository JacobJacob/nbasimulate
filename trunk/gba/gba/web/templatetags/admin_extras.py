#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

import datetime
import logging

from django import template

from dtspider.common.constants import ClientStatus, CLIENT_STATUS_NAMES, \
    URL_FROM_TYPE_MAP, UrlTaskStatus, URL_REASON
from dtspider.common import json, urlutil


register = template.Library()

def _client_status(client_info):
    status = client_info['status']
    if status not in CLIENT_STATUS_NAMES:
        return 'Unkow(%s)' % status
    now =  datetime.datetime.now()
    if client_info['updated_time'] < now and (now - client_info['updated_time']).seconds > 600: # 10分钟没响应的，则当做久无响应处理
        status = ClientStatus.DEATH
    return CLIENT_STATUS_NAMES[status]

@register.filter
def client_status(client_info):
    return _client_status(client_info)

@register.filter
def url_from_type(from_type):
    """来源"""
    return u'%s(%s)' % (URL_FROM_TYPE_MAP.get(from_type, u'未知'), from_type)

@register.filter
def url_result(result):
    """result format: (type, hit_url, reason)
    """
    type, hit_url, reason = result
    result_str = '安全'
    if type == 3:
        result_str = '危险'
    elif type == 4:
        result_str = '警告'
    if hit_url:
        if isinstance(hit_url, unicode):
            hit_url = hit_url.encode('utf-8')
        result_str += ', %s' % hit_url
    if reason:
        result_str += ', %s' % urlutil.get_display_reason(reason)
#        for info in reason:
#            result_str += u', [%s]%s' % (URL_REASON.get(info[0], u'未知(%s)' % info[0]), 
#                                        ' '.join([i for i in info[1:] if i]))
    return result_str

@register.filter
def url_task_status(status):
    if status == UrlTaskStatus.TODO:
        return u'准备'
    if status == UrlTaskStatus.DOING:
        return u'处理中'
    if status == UrlTaskStatus.FAILED:
        return u'失败'
    if status == UrlTaskStatus.SUCCESS:
        return u'成功'

@register.filter
def show_reason(reason):
    if isinstance(reason, basestring) and reason:
        reason = json.loads(reason)
    if not reason:
        return '本次检查安全'
    return urlutil.get_display_reason(reason)
#    if len(reason[0]) >= 3: # 新原因
#        return u', '.join([r[2].replace(u'发现IEXPLORE.EXE', u'').replace(u'成功阻止IEXPLORE.EXE', u'试图').replace(u'，已被成功阻止！', u'').replace(u'，已经被成功阻止', u'')
#                         for r in reason])
#    else:
#        return str(reason)

@register.filter
def show_raw_reason(reason):
    return urlutil.get_raw_display_reason(reason)
    
@register.filter
def client_total_info(clients):
    info = {}
    for client in clients:
        status = _client_status(client)
        info[status] = info.get(status, 0) + 1
    return u', '.join([u'%s: %d' % (k, v) for k, v in info.items()])

@register.filter
def ey_type(url_type):
    """ 1 网马, 2 木马, 3 钓鱼, 4 广告"""
    if url_type == 1:
        return u'网马(1)'
    if url_type == 2:
        return u'木马(2)'
    if url_type == 3:
        return u'钓鱼(3)'
    if url_type == 4:
        return u'广告(4)'
    return u'未知(%s)' % url_type

@register.filter
def black_detail(detail):
    """显示详细的原因"""
    if not detail:
        return None
    if isinstance(detail, basestring):
        detail = eval(detail)
    days = detail['last_time'] - detail['first_time']
    days = days.days
    if days == 0:
        days = 1
    reasons = {}
    for key, val in detail.iteritems():
        if key != 'reason_type' and key.startswith('reason'):
            v = key.replace('reason_', '')
            try:
                reason_val = int(v)
            except ValueError:
                continue
            reasons[v] = val
    host = detail.get('host', detail.get('domain'))
    if isinstance(host, unicode):
        host = host.encode('utf-8')
    details, fishhosts, eyhosts = detail.get('reason_detail', {}), detail.get('fish_hosts', []), detail.get('ey_hosts', [])
    reason_info = urlutil.format_reason(details, eyhosts, fishhosts)
    reason = detail.get('reason', None)
    if reason and reason != 'null':
        root_black = '该站点主页带有恶意行为，其中'
    else:
        root_black = ''
#    ey_hosts = detail.get('ey_hosts', [])
#    if ey_hosts:
#        ey_host_info = u'，恶意攻击托管在以下域名中: %s' % u', '.join(ey_hosts)
#    else:
#        ey_host_info = u''
#    reason_info = u'，'.join([u'%s有 %s 个' % (URL_REASON.get(k, k), val) for k, val in reasons.iteritems()])
    info = '系统在 %d天(%s - %s)里对 <strong>%s</strong>的 %d个URL(%d次)进行了检查，发现%s%d(%d%%)个URL(%d次)是危险的，%s。' % \
        (days, detail['first_time'].strftime('%Y-%m-%d'), detail['last_time'].strftime('%Y-%m-%d'), 
         host, detail['url_count'], detail.get('check_count', 0),
         root_black,
         detail['black_count'], detail['black_rate'], 
         detail.get('check_black_count', 0), reason_info)
    return info
    