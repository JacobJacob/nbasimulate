#!/usr/bin/python
# -*- coding: utf-8 -*-
from datetime import datetime

from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.db import connection
from webauth.common import proxyutil 

_LOAD_PROXY = """select proxy,log from http_proxy where proxy = %s """
_SELECT_FAST_PROXYS = """select proxy,log from http_proxy where inuse <> 1 order by timeout asc""" 
_SELECT_ALL_PROXY = """select proxy from http_proxy"""
_DELETE_PROXY = """delete from http_proxy where proxy=%s"""

def load_proxy(proxy):
    """查找单个代理，返回信息包括代理本身和使用日志
    @param proxy:要查的的代理
    @return: 列表类型 ，包括代理本身以及使用日志
    """
    cursor = connection.cursor()
    try:
        row = cursor.fetchone(_LOAD_PROXY, (proxy))
    finally:
        cursor.close()
    return row

def get_fast_proxy(idx=0):
    """获取最快的代理
    @param idx:序号，将代理排序,取第idx个代理
    @return: 字典类型，封装了代理信息
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
        
def get_all_proxy():
    """得到所有的代理
    @return: 列表类型
    """
    cursor = connection.cursor()
    try:
        return cursor.fetchall(_SELECT_ALL_PROXY)
    finally:
        cursor.close()

def _update_proxy(proxy_info):
    """更新一个代理
    @param proxy_info:封装了代理信息的字典 
    """
    cursor = connection.cursor()
    proxy_info['modifyat'] = ReserveLiteral("now()")
    try:
      cursor.update(proxy_info, 'http_proxy', 'proxy')
    finally:
      cursor.close()
    
def _insert_proxy(proxy_info):
    """插入一个新的代理
    @param proxy_info:封装了代理信息的字典 
    """
    proxy_info['createat'] = ReserveLiteral("now()")
    proxy_info['verifyat'] = ReserveLiteral("now()")
    proxy_info['inuse'] = 0
    cursor = connection.cursor()
    try:
        cursor.insert(proxy_info, 'http_proxy')
    finally:
        cursor.close()
        
def report_log(proxy, client_type, timex=None):
    """添加使用日志
    @param proxy:代理 
    @param client_type:客户端类型,如ALEXA
    @param times:上报的时间  
    """
    proxy_obj = load_proxy(proxy)
    if proxy_obj:
        from webauth.common import json
        log = json.loads(proxy_obj[1]) if proxy_obj[1] else {}
        log[client_type] = timex if timex else ReserveLiteral("now()")
        proxy_info = {}
        proxy_info['proxy'] = proxy
        proxy_info['log'] = json.dumps(log)
        proxy_info['inuse'] = 1
        _update_proxy(proxy_info)
        
def save_proxy(proxy_info):
    """保存一个代理，如果已经存在则更新，
         不存在就创建
    @param proxy_info:封装了代理信息的字典 
    """
    proxy_obj = load_proxy(proxy_info['proxy'])
    if proxy_obj:
       _update_proxy(proxy_info)
    else:
       _insert_proxy(proxy_info)

def _delete_proxy(proxy):
    """删除指定的代理
    @param proxy:要删除的代理 
    """
    cursor = connection.cursor()
    try:
        cursor.execute(_DELETE_PROXY, (proxy))
    finally:
        cursor.close()
def release_proxy(proxy):
    """客户端释放一个代理,该代理的状态将变为可用
    @param proxy:要释放的代理 
    """
    proxy_info = {}
    proxy_info['proxy'] = proxy
    proxy_info['inuse'] = 0
    save_proxy(proxy_info)
    
def check_all_db_proxy():
    """检查当前数据库中所有代理的有效性，删除无效的代理
    """
    proxys = get_all_proxy()
    for proxy in proxys:
        isok, ltm = proxyutil.check_proxy(proxy[0])
        if not isok:
            _delete_proxy(proxy[0])