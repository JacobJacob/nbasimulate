#!/usr/bin/python
# -*- coding: utf-8 -*-
"""proxy相关service api"""

import threading
import datetime

from dtspider.common.filelocker import FileLocker
from dtspider.common.jsonrpcserver import jsonrpc_function
from dtspider.business import http_proxy

_DELTA_TIME1 = datetime.timedelta(days=2) # 一天没有上报，可以认为客户端以死
_DELTA_TIME2 = datetime.timedelta(hours=1) # 一小时没有回报，可以认为客户端没有使用这个 代理
_get_proxy_lock = threading.RLock()

@jsonrpc_function
def add_proxy(request, proxy):
    """添加代理
    """
    return http_proxy.save_proxy(proxy)

@jsonrpc_function
def add_proxys(request, proxys):
    """添加代理
    """
    for proxy in proxys:
        http_proxy.save_proxy(proxy)

@jsonrpc_function
def ask_for_proxy(request, client_type, old_proxy):
    """申请获得一个代理
    """
    _get_proxy_lock.acquire()
    try:
        file_lock = FileLocker('ask_for_proxy')
        file_lock.lock()
        if old_proxy:
            http_proxy.release_proxy(old_proxy)
        idx = 0
        while True:
            proxy_info = http_proxy.get_fast_proxy(idx)
            if not proxy_info: return None # 已没有可用的代理了
            if client_type not in proxy_info['log'] or proxy_info['log'][client_type] + _DELTA_TIME1 < datetime.datetime.now():
                _report_proxy(proxy_info['proxy'], client_type, datetime.datetime.now() - _DELTA_TIME1 + _DELTA_TIME2)
                _report_proxy(proxy_info['proxy'], client_type + "_beginat")
                return proxy_info['proxy']
            idx += 1
    finally:
        file_lock.unlock()
        _get_proxy_lock.release()
        
@jsonrpc_function
def report_proxy(request, proxy, client_type, timex=None):
    """上报代理使用情况
    """
    _report_proxy(proxy, client_type, timex)

def _report_proxy(proxy, client_type, timex=None):
    proxy_obj = http_proxy.load_proxy(proxy)
    if proxy_obj:
        http_proxy.report_log(proxy, client_type, timex)
    
