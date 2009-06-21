#!/usr/bin/python
# -*- coding: utf-8 -*-
"""domain信息爬取基础客户端"""

import re
import urllib2
import time
import logging
import random

from webauth.common import log_execption
from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import proxy_service_proxy
from webauth.client.rpc_proxy import domain_service_proxy


class DomainSpiderBase(BaseClient):
    
    FORBIDDEN = 'FORBIDDEN' 
    
    def __init__(self, type, spider, round_time=1800):
        super(DomainSpiderBase, self).__init__(type)
        self._proxy = None
        self._spider = spider
        self._round_time = round_time # 爬取一轮的时间间隔
     
    def _fetch_domain(self, task_type='ALEXA'):
        """得到要爬取信息的域名列表
        @param task_type:任务类型,分为ALEXA和GOOGLE 
        @return: 包括字典的列表,每个字典封装一个域名信息
        """
        while True:
            try:
                data = domain_service_proxy.fetch_domain(task_type)
                if not data['error'] and data['result']:
                    return data['result']
                logging.error('fetch domain failure:%s' % data['error'])
            except:
                log_execption()
            self._sleep()
    
    def _ask_proxy(self, task_type='ALEXA'):
        """申请获得一个代理
        @param task_type:任务类型,分为ALEXA和GOOGLE
        @return: 返回一个可用的代理 
        """
        while True:
            data = proxy_service_proxy.ask_for_proxy(task_type, self._proxy)
            self._proxy = data['result']
            self._proxy_fail_times = 0
            if self._proxy:
                return self._proxy
            self._sleep()
        
    def _update_domain(self, domain_info):
        """更新爬取到的域名信息
        @param domain_info:单个字典或多个字典组成的列表,每个字典封装一个域名的信息 
        """
        while True:
            try:
                data = domain_service_proxy.update_domain(domain_info)
                if not data['error']:
                    break
                logging.error('update domain failure:%s' % data['error'])
            except:
                log_execption()
            self._sleep()
    
    def request(self, url):
        """请求url，返回响应的response
                    如果连接被拒绝或超时, 则重新请求一个新的代理,否则直接返回None
        @param url:请求的url
        @return: 响应的response对象 
        """
        failur_times = 0
        fob_retry = 0 
        while True:
            try:
                return self._spider.request(url)
            except urllib2.URLError, e:
                if hasattr(e, 'reason') and 10061 in e.reason:#被封则直接更换代理
                    logging.warning('request refuse')
                    self._spider.proxy = self._ask_proxy()
                    self._spider.init_spider()
                    time.sleep(random.randint(10, 15))
                    continue
                #如果该域名的查询被禁止，则等待一段时间后重试,重试三次不成功则返回，无需更换代理
                if hasattr(e, 'reason') and 10054 in e.reason:
                    fob_retry += 1
                    logging.warning('request reset')
                    if fob_retry >=3:
                        return DomainSpiderBase.FORBIDDEN 
                    else:
                        time.sleep(random.randint(10, 15))
                        continue
                if failur_times < 3:
                    failur_times += 1
                    self._spider.proxy = self._ask_proxy()
                    self._spider.init_spider()
                    time.sleep(random.randint(10, 15))
                else:
                    return None
            