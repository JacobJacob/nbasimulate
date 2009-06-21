#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url相关service api"""

from webauth.common.jsonrpcserver import jsonrpc_function
from webauth.common.constants import UrlFromType
from webauth.business import url_source
from webauth.business import black_url
from webauth.business import white_host


@jsonrpc_function
def add_url(request, urls, from_type):
    """新增url
    urls: str or list
    """
    if isinstance(urls, basestring):
        urls = [urls]
    if not from_type: # 来源为空，则是页面提交的
        from_type = UrlFromType.WEB_SUBMIT
    url_source.add_url(urls, from_type)
    return True

@jsonrpc_function
def add_hot_urls(request, urlinfos, from_type):
    """添加热门url"""
    url_source.add_hot_urls(urlinfos, from_type)

@jsonrpc_function
def import_webshield_url(request, data):
    url_source.import_webshield_url(data)

@jsonrpc_function
def get_malicious_hosts(request):
    """获取恶意网址源"""
    return black_url.get_malicious_hosts()

@jsonrpc_function
def add_malicious_hosts(request, eyhost_infos):
    """添加恶意host"""
    return black_url.add_malicious_hosts(eyhost_infos)

@jsonrpc_function
def add_white_host(request, host):
    """添加白名单host"""
    return white_host.save_white_host(host)

@jsonrpc_function
def delete_white_host(request, host):
    """删除白名单host"""
    return white_host.delete_white_host(host)