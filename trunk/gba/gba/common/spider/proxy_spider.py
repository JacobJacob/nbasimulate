#!/usr/bin/python
# -*- coding: utf-8 -*-
"""扩展了的爬虫类,实现代理访问方式"""

import urllib2

from common.spider import Spider

class ProxySpider(Spider):
    
    def __init__(self, url, templates, proxy=None, referer=None, remove_html=True,
                 user_agent=None, http_headers=None, timeout=Spider.DEFAULT_TIMEOUT,
                 pass_level=None):
        self.proxy = proxy
        super(ProxySpider, self).__init__(url, templates, referer=None, remove_html=True,
                 user_agent=None, http_headers=None, timeout=Spider.DEFAULT_TIMEOUT,
                 pass_level=None)
    
    def init_spider(self):
        """初始化基础信息,如果代理不为空，则以代理方式访问网络
        ,否则直接访问
        """
        if self.proxy:
            self._init_proxy_spider()
        else:
            super(ProxySpider, self).init_spider()
    
    def _init_proxy_spider(self):
        """初始化基础信息
        """
        self._old_timeout = urllib2.socket.getdefaulttimeout()
        urllib2.socket.setdefaulttimeout(self.timeout)
        proxy_handler = urllib2.ProxyHandler({'http': 'http://%s' % self.proxy})
        self.opener = urllib2.build_opener(proxy_handler, urllib2.HTTPHandler)
        self._caches = set()
        self._same_levels = set()
        self.index = 0