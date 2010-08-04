#!/usr/bin/python
# -*- coding: utf-8 -*-
"""proxy 爬取基础客户端"""

import re
import time
import logging
from datetime import datetime

from dtspider.client.base import BaseClient
from dtspider.client.rpc_proxy import proxy_service_proxy
from dtspider.common.constants import ClientType
from dtspider.common import log_execption
from dtspider.common import proxyutil 
from dtspider.common import exception_mgr

_res = {
    "host": r"([0-9a-zA-Z-_]+\.[\.0-9a-zA-Z-_]+)",
    "blank": r"\s*",
    "digit": r"[0-9]+",
    "port": r"([0-9]+)",
    "portxp": r"\+([a-z+]+)",
    "proxy": r"([0-9a-zA-Z-_]+\.[\.0-9a-zA-Z-_]+:[0-9]+)",
    "urlname": r"[0-9a-zA-Z-_%]*?"}

class ProxySpiderClient(BaseClient):
    
    def __init__(self, spider, round_time=432000):
        super(ProxySpiderClient, self).__init__(ClientType.PROXY_SPIDER)
        self._spider = spider
        self._round_time = round_time # 爬取一轮的时间间隔
        
    def run(self):
        self._walk()
        now = datetime.now()
        #每五天爬一次代理
        self.current_info = "%s: sleep 5 day..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return self._round_time
    
    def _visit(self, site, proxy=None):
        """访问一个地址，返回页面的内容
        @param site:要访问的地址
        @param proxy:代理
        @return: 页面的内容  
        """
        try:
            return proxyutil.visit_proxy(site, proxy = proxy, times = 1)
        except Exception, e:
            logging.warning("%s %s -- %s" % (__file__, site, e))
        return ""

    def _get_single(self, url, regular, transform):
        """得到单个页面的代理
        @param url:要获取代理的地址
        @param regular: 页面代理获取正则表达式
        @param transform:方法对像，处理得到的代理，比如 将代理地址与端口连接  lambda tmp: [":".join(x) for x in tmp]
        """
        page = self._visit(url)
        tmp = transform(re.findall(regular, page)) if regular else transform(page)
        self._add_proxys(tmp)
        return tmp
        
    def _get_single_list(self, urls, regular, transform):
        """得到多个地址的代理
        @param urls:要获取代理的地址列表
        @param regular: 页面代理获取正则表达式
        @param transform:方法对像，处理得到的代理，比如 将代理地址与端口连接  lambda tmp: [":".join(x) for x in tmp]
        """
        urls = list(set(urls))
        for url in urls:
            self._get_single(url, regular, transform)
        
    def _get_by_id(self, urltemplet, regular, transform):
        """获取多个页面的代理，每个页面应该只有id不同
        @param urltemplet:页面模板，如 http://www.dheart.net/proxy/index.php?lphydo=list&port=&type=&country=&page=%s
        @param regular:页面代理获取正则表达式
        @param transform:方法对像，处理得到的代理，比如 将代理地址与端口连接  lambda tmp: [":".join(x) for x in tmp] 
        """
        cnt = 0
        while True:
            cnt += 1
            url = urltemplet % cnt   
            tmp = self._get_single(url, regular, transform)
            if debug and cnt >= 3 or not tmp:
                break
            
    def _get_by_page(self, urlseed, _regular, regular, transform):
        """获取页面的代理，包括子页面的代理
        @param urlseed:开始访问的地址
        @param _regular:子页面链接正则表达式
        @param regular:代理获取正则表达式
        @param transform:方法对像，处理得到的代理，比如 将代理地址与端口连接  lambda tmp: [":".join(x) for x in tmp]  
        """
        page = self._visit(urlseed)
        urlroot = urlseed
        while urlroot and not urlroot.endswith("/"):
            urlroot = urlroot[:-1]
        urls = [urlroot + ("".join(x) if isinstance(x, tuple) else x) for x in re.findall(_regular, page)]
        urls = list(set(urls))
        cnt = 0
        for url in urls:
            cnt += 1
            _tmp = self._get_single(url, regular, transform)
            
    def _walk(self):
        """由子类实现"""
        pass    
    def _add_proxys(self, proxys):
        """添加代理,添加前先验证代理的可用性
        @param proxys:列表类型，每个元素为一个代理 
        """
        proxy_infos = []
        if isinstance(proxys, list):
           for proxy in proxys:
               self.current_info = 'start to check proxy:%s' % proxy
               logging.info(self.current_info)
               isok, timeout = proxyutil.check_proxy(proxy)#验证代理有效性
               if isok and timeout <= 120 :#响应时间超过120秒的代理不要了
                   self.current_info = 'proxy:%s is ok,speed:%s' % (proxy, timeout)
                   logging.info(self.current_info)
                   proxy_infos.append({'proxy': proxy, 'timeout':timeout})
                   #proxy_service_proxy.add_proxy({'proxy': proxy, 'timeout':timeout})
               else:
                   self.current_info = 'proxy:%s is not ok' % proxy
                   logging.info(self.current_info)
        while True:
            try:
                proxy_service_proxy.add_proxys(proxy_infos)
                break
            except:
                log_execption()
            self._sleep()
