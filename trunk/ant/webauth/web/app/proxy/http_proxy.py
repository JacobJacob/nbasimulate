#!/usr/bin/python
# -*- coding: utf-8 -*-
from datetime import datetime
from webauth.common.db import connection
from webauth.common import proxyutil 

_LOAD_PROXY = """select proxy,log from http_proxy where proxy = %s """
def load_proxy(proxy):
    """查找单个代理，返回信息包括代理本身和使用日志
    """
    cursor = connection.cursor()
    try:
        row = cursor.fetchone(_LOAD_PROXY, (proxy))
    finally:
        cursor.close()
    return row

_SELECT_FAST_PROXYS = """select proxy,log from http_proxy where inuse <> 1 order by timeout asc""" 
def get_fast_proxy(idx=0):
    """获取最快的代理
    """
    cursor = connection.cursor()
    try:
        rows = cursor.fetchall(_SELECT_FAST_PROXYS)
        if rows:
            from webauth.common import json
            proxy_info = {}
            proxy_info['proxy'] = rows[idx][0]
            proxy_info['log'] = json.loads(rows[idx][1]) if rows[idx][1] else {}
            return proxy_info
    except IndexError:
        return None
    finally:
        cursor.close()
        
_SELECT_ALL_PROXY = "select proxy from http_proxy"
def get_all_proxy():
    cursor = connection.cursor()
    try:
        return cursor.fetchall(_SELECT_ALL_PROXY)
    finally:
        cursor.close()

def _update_proxy(proxy_info):
    """更新一个代理
    """
    cursor = connection.cursor()
    proxy_info['modifyat'] = datetime.today()
    try:
      cursor.update(proxy_info, 'http_proxy', 'proxy')
    finally:
      cursor.close()
    
def _insert_proxy(proxy_info):
    """插入一个新的代理
    """
    proxy_info['createat'] = datetime.today()
    cursor = connection.cursor()
    try:
        cursor.insert(proxy_info, 'http_proxy')
    finally:
        cursor.close()
        
def report_log(proxy, client_type, timex=None):
    """添加使用日志
    """
    proxy_obj = load_proxy(proxy)
    if proxy_obj:
        from webauth.common import json
        log = json.loads(proxy_obj[1]) if proxy_obj[1] else {}
        log[client_type] = timex if timex else datetime.now()
        proxy_info = {}
        proxy_info['proxy'] = proxy
        proxy_info['log'] = json.dumps(log)
        _update_proxy(proxy_info)
        
def save_proxy(proxy):
    """保存一个代理，如果已经存在则更新，
         不存在就创建
    """
    isok, ltm = proxyutil.check_proxy(proxy)
    if isok:
        proxy_info = {}
        proxy_info['proxy'] = proxy
        proxy_info['timeout'] = ltm
        proxy_info['verifyat'] = datetime.today()
    
        proxy_obj = load_proxy(proxy)
        if proxy_obj:
           _update_proxy(proxy_info)
        else:
           _insert_proxy(proxy_info)
    return isok

_DELETE_PROXY = """delete from http_proxy where proxy=%s"""
def _delete_proxy(proxy):
    """删除指定的代理
    """
    cursor = connection.cursor()
    try:
        cursor.execute(_DELETE_PROXY, (proxy))
    finally:
        cursor.close()

def check_all_db_proxy():
    """检查当前数据库中所有代理的有效性，删除无效的代理
    """
    proxys = get_all_proxy()
    for proxy in proxys:
        isok, ltm = proxyutil.check_proxy(proxy[0])
        if not isok:
            _delete_proxy(proxy[0])

    

    