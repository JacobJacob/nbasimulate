#!/usr/bin/python
# -*- coding: utf-8 -*-
"""google pr 值爬取客户端"""

import urllib2
import time
import random
import logging


from webauth.common.spider.proxy_spider import ProxySpider
from webauth.common.constants import ClientType
from webauth.client.rpc_proxy import domain_service_proxy
from webauth.client.domain_info.domain_spider_base import DomainSpiderBase
from webauth.client.domain_info import google_pr_util

class GooglePrSpider(DomainSpiderBase):
    
    def __init__(self):
        url = 'toolbarqueries.google.com'
        templates = [
            {
              'info': ur'''^Rank_\d+:\d+:(?P<google_pr>\d+)'''
            }
        ]
        self._proxy_fail_times = 0
        spider = ProxySpider(url, templates)
        super(GooglePrSpider, self).__init__(ClientType.GOOGLE_PR_SPIDER, spider)

    def run(self):
        """程序主体部分,不断获取域名列表并抓取google pr值
        """
        task_cnt = 0
        while True:
            domain_infos = self._fetch_domain('GOOGLE')
            for domain_info in domain_infos:
                self._get_info(domain_info)
                task_cnt += 1
                if task_cnt % 10 == 0:#每处理10条打印一次日志
                    logging.info('finish %s task' % task_cnt)
                time.sleep(random.randint(5, 10))#延迟
            self._update_domain(domain_infos)
    
    def _get_info(self, domain_info):
        """得到域名的google pr值
        @param domain_info: 字典类型，保存了要爬取的域名
        @return: 字典类型，保存爬取到的数据
        """
        url = google_pr_util.make_url(domain_info['domain'].strip())
        response = self.request(url)
        if not response:
            domain_info['google_pr'] = -1
            return domain_info
        content = self._spider.get_content(response)
        if content == '':#域名未被收录时,返回页面为空白
            domain_info['google_pr'] = 0
            return domain_info
        info = self._spider.get_info(content, self._spider.templates[0]['info'])
        if info:    
            domain_info['google_pr'] = info['google_pr']
        else:
            domain_info['google_pr'] = -1
        return domain_info
        
    def request(self, url):
        """请求url，返回响应的response
        @param url:请求的url
        @return: 响应的response对象 
        """
        failur_times = 0
        while True:
            try:
                return self._spider.request(url)
            except urllib2.URLError, e:
                failur_times += 1
                if failur_times < 3:
                    time.sleep(random.randint(10, 30))
                else:
                    return None
            
def main():
    spider = GooglePrSpider()
    spider.main()
     
if __name__ == '__main__':
    main()
            
