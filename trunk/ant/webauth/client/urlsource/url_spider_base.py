#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url 爬取基础客户端"""

import time
import logging

from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import url_source_proxy
from webauth.common.constants import ClientType
from webauth.common import log_execption


class UrlSpiderClient(BaseClient):
    
    def __init__(self, from_type, spider, round_time=1800):
        super(UrlSpiderClient, self).__init__(ClientType.HOT_URL_SPIDER)
        self._spider = spider
        self._from_type = from_type
        self._round_time = round_time # 爬取一轮的时间间隔
        self._exclude_words = []
        
    def run(self):
        key_count, url_count, start_time = 0, 0, time.time()
        for level, url, parenturl, response, \
                    content, downurls, info in self._spider.walk():
            if level == 2:
                key_count += 1
            keyword = info.get('keyword', None)
            if downurls:
                need_urls = []
                for url in downurls:
                    need = True
                    for word in self._exclude_words:
                        if url.startswith(word):
                            need = False
                            break
                    if need:
                        need_urls.append(url)
                url_count += len(need_urls)
                self._add_urls(need_urls, keyword)
            used_time = time.time() - start_time
            self.current_info = u'Current url: %s, keyword: %s. Total keys: %s, urls: %s, use: %.1f secs' % \
                (url, keyword, key_count, url_count, used_time)
            logging.info(self.current_info)
        self._spider.init_spider() # 重新初始化一下
        return self._round_time
    
    def _add_urls(self, urls, keyword):
        urlinfos = []
        for u in urls:
            urlinfos.append({'url': u, 'keyword': keyword})
        while True:
            try:
                data = url_source_proxy.add_hot_urls(urlinfos, self._from_type)
                if not data['error']: # 保存成功，则返回
                    break
                logging.warning('Finish tasks rpr error, %r' % data['error'])
                self.current_info = data['error']['stack']
            except:
                log_execption()
            self._sleep()
            
    def test_spider(self):
        for level, url, parenturl, response, \
                    content, downurls, info in self._spider.walk():
            print level, info.get('keyword', None), url
            print downurls
            print '-' * 60