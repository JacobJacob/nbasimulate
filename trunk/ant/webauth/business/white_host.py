#!/usr/bin/python
# -*- coding: utf-8 -*-
"""白名单host"""

from webauth.common import cache
from webauth.common.db import connection
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.md5mgr import mkmd5fromstr


WHITE_HOST_KEY = 'white_hosts_list'

def get_white_hosts_from_db():
    """从数据库获取所有白名单hosts"""
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall('select * from white_host')
        return rs
    finally:
        cursor.close()

def get_white_hosts():
    """获取所有白名单hosts"""
    hosts = cache.get(WHITE_HOST_KEY)
    if hosts is None:
        rs = get_white_hosts_from_db()
        hosts = set()
        for r in rs:
            hosts.add(r['host'].encode('utf-8'))
        cache.set(WHITE_HOST_KEY, hosts, 3600) # 缓存1小时
    return hosts
    
def is_white_host(host, domain=None):
    """判断是否白名单host"""
    hosts = get_white_hosts()
    return host in hosts or domain in hosts

def save_white_host(host):
    host = host.strip()
    if not host:
        return -1, 0
    info = {'host': host, 
            'host_md5': mkmd5fromstr(host),
            'created_time': ReserveLiteral('now()')
            }
    cursor = connection.cursor()
    try:
        lastrowid, row_effected = cursor.insert(info, 'white_host', True, 'skip_all')
        cache.delete(WHITE_HOST_KEY) # 删除cache
        return lastrowid, row_effected
    finally:
        cursor.close()
        
def delete_white_host(host):
    host = host.strip()
    if not host:
        return 0
    cursor = connection.cursor()
    try:
        row_effected = cursor.execute('delete from white_host where host_md5=%s', 
                                      (mkmd5fromstr(host),))
        cache.delete(WHITE_HOST_KEY) # 删除cache
        return row_effected
    finally:
        cursor.close()