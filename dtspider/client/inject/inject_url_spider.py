#!/usr/bin/python
# -*- coding: utf-8 -*-
"""注入url抓取爬取"""
import traceback
from datetime import datetime
import re
import logging
import random

from dtspider.common.spider import Spider
from dtspider.client.base import BaseClient
from dtspider.common.db import connection
from dtspider.common import md5mgr
from dtspider.common.db.reserve_convertor import ReserveLiteral
from dtspider.business import runtime_data
from dtspider.entity import InjectUrlSpiderTask, InjectionUrl


class InjectUrlSpider(BaseClient):
    
    SEARCH_URL = u'''http://www.google.cn/search?q=%s&hl=zh-CN&lr=&num=100&filter=0'''
    URL_RE = re.compile(ur'''<h3[^<>]*?class=r[^<>]*?><a[^<>]*?href="(.*?)"[^<>]*?>[\S\s]*?</a></h3>''', re.VERBOSE)
    PAGE_URL_RE = re.compile(ur'''<a[^<>]*?href="(/search[^<>]*?)"><span[^<>]*?class="csb[^<>]*?ch"[^<>]*?></span>[^<>]*?</a>''', re.VERBOSE)
    KEYWORD_RE = re.compile(ur'<a[^<>]*?class=rsl[^>]*?>(.*?)</a>', re.VERBOSE)

    def __init__(self):
        self._spider = Spider()
        self._current_keyword = None
        self._last_id = 0
        super(InjectUrlSpider, self).__init__('inject_url_spider')
    
    def before_run(self):
        self._load_status()
        self._tasks = self._fetch_task()
        return 'task start'
    
    def run(self):
        
        if not self._tasks:
            now = datetime.now()
            self.current_info = "%s:no tasks now, sleep 60s..." % now.strftime("%Y-%m-%d %H:%M:%S")
            return 60
        self._last_id = self._tasks[-1].id
        for task in self._tasks:
            self._current_keyword = task.keyword
            self._search_key(self._current_keyword)

        self._save_status()  
        
          
    def _search_key(self, keyword):
        now = datetime.now()
        self.current_info = '%s:start to search keyword:%s' % (now.strftime("%Y-%m-%d %H:%M:%S"), keyword)
        logging.info(self.current_info)
        start_url = InjectUrlSpider.SEARCH_URL % keyword
       
        urls, page_urls = self._search_url(start_url, True)
        for page_url in page_urls: 
            now = datetime.now()
            self.current_info = '%s:search url:http://www.google.cn%s' % (now.strftime("%Y-%m-%d %H:%M:%S"), page_url.replace('&amp;', '&'))
            logging.info(self.current_info)
            add_urls, add_page_urls = self._search_url('http://www.google.cn%s' % page_url.replace('&amp;', '&'))

    def _search_url(self, url, need_page=False):
        
        content = self._get_content(url)
        keywords = InjectUrlSpider.KEYWORD_RE.findall(content)
        for keyword in keywords:
            inject_url_spider_task = InjectUrlSpiderTask()
            inject_url_spider_task.keyword = keyword
            inject_url_spider_task.keyword_md5 = md5mgr.mkmd5fromstr(keyword)
            inject_url_spider_task.persist()
        
        urls =  InjectUrlSpider.URL_RE.findall(content)
        for url in urls:
            if url and url.find('asp') != -1 and url.find('?') != -1:
                self._safe_url(url)
                
        page_urls = []
        if need_page:
            page_urls = InjectUrlSpider.PAGE_URL_RE.findall(content)
        self._sleep(random.randint(20,25))  
        return urls, page_urls
  
    
    def _safe_url(self, url):
        """保存邮箱"""
        injection_url = InjectionUrl()
        injection_url.url = url
        injection_url.url_md5 = md5mgr.mkmd5fromstr(url)
        injection_url.persist()
         
    def _get_content(self, url):
        """获取页面内容"""
        try_count = 0
        while True:
            try:
                content = self._spider.read(url)
                return content
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
                try_count += 1
                if try_count >= 3:
                    return ''
            self._sleep()
    
    def _fetch_task(self):
        """fetch task"""
        return InjectUrlSpiderTask.query(condition="id>%s" % self._last_id, limit=100)
            
    def _load_status(self):
        """load status"""
        while True:
            try:
                data = runtime_data.load_runtime_data(self.__class__.__name__, 'last_id')
                if data:
                    self._last_id = int(data['value'])
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()
    
    def _save_status(self):
        """save status"""
        while True:
            try:
                runtime_data.save_runtime_data(self.__class__.__name__, 'last_id', self._last_id)
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()  
    
def main():
    spider = InjectUrlSpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()
