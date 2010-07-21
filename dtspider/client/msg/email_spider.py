#!/usr/bin/python
# -*- coding: utf-8 -*-
"""邮箱抓取爬取"""
import traceback
from datetime import datetime

from dtspider.common.spider import Spider
from dtspider.client.base import BaseClient
from dtspider.common.db import connection
from dtspider.common import md5mgr
from dtspider.common.db.reserve_convertor import ReserveLiteral
from dtspider.business import runtime_data

import re
import logging


class EmailSpider(BaseClient):
    
    SEARCH_URL = u'''http://www.google.cn/search?q=%s&hl=zh-CN&lr=&num=100&filter=0'''
    URL_RE = re.compile(ur'''<h3[^<>]*?class=r[^<>]*?><a[^<>]*?href="(.*?)"[^<>]*?>[\S\s]*?</a></h3>''', re.VERBOSE)
    PAGE_URL_RE = re.compile(ur'''<a[^<>]*?href="(/search[^<>]*?)"><span[^<>]*?class="csb[^<>]*?ch"[^<>]*?></span>[^<>]*?</a>''', re.VERBOSE)
    TITLE_RE = re.compile(ur'''<title>(?P<title>.*)</title>''', re.VERBOSE)
    EMAIL_RE = re.compile(ur'''([0-9a-zA-Z\.-]+@[0-9a-zA-Z]+\.[a-zA-Z]*)''', re.VERBOSE)
    
    def __init__(self):
        self._spider = Spider('', [])
        self._current_keyword = None
        self._email_count = 0
        self._last_id = 0
        super(EmailSpider, self).__init__('email_spider')
    
    def before_run(self):
        self._load_status()
        self._task_infos = self._fetch_task()
        return 'task start'
    
    def run(self):
        
        if not self._task_infos:
            now = datetime.now()
            self.current_info = "%s:no tasks now, sleep 60s..." % now.strftime("%Y-%m-%d %H:%M:%S")
            return 60
        self._last_id = self._task_infos[-1]['id']
        for task_info in self._task_infos:
            self._current_keyword = task_info['key_word']
            self._search_key(task_info['key_word'])
            task_info['email_count'] = ReserveLiteral('email_count + %s' % self._email_count)
            now = datetime.now()
            self.current_info = "%s:search key word %s finish, get %s emails..." %\
                   (now.strftime("%Y-%m-%d %H:%M:%S"), self._current_keyword, self._email_count)
            logging.info(self.current_info)
            self._email_count = 0
            self._update_task(task_info)
        
        self._save_status()  
        
          
    def _search_key(self, keyword):
        now = datetime.now()
        self.current_info = '%s:start to search keyword:%s' % (now.strftime("%Y-%m-%d %H:%M:%S"), keyword)
        logging.info(self.current_info)
        start_url = EmailSpider.SEARCH_URL % keyword
       
        urls, page_urls = self._search_url(start_url, True)
        for page_url in page_urls: 
            now = datetime.now()
            self.current_info = '%s:search url:http://www.google.cn%s' % (now.strftime("%Y-%m-%d %H:%M:%S"), page_url.replace('&amp;', '&'))
            logging.info(self.current_info)
            add_urls, add_page_urls = self._search_url('http://www.google.cn%s' % page_url.replace('&amp;', '&'))
            if add_urls:
                urls += add_urls

    def _search_url(self, url, need_page=False):
        
        content = self._get_content(url)
        urls =  EmailSpider.URL_RE.findall(content)
        for url in urls:
            title = self._get_title(url)
            if title:
                pass
                #print '%s%s' % (title, url)
            #print url
        page_urls = []
        if need_page:
            page_urls = EmailSpider.PAGE_URL_RE.findall(content)
            
        return urls, page_urls
    
    def _get_title(self, url):

        content = self._get_content(url)
        match = EmailSpider.TITLE_RE.search(content)
        if match:
            d = match.groupdict()
            title = d['title']
            now = datetime.now()
            self.current_info = '%s title:%s' % (now.strftime('%Y-%m-%d %H:%M:%S'), title)
            logging.info(self.current_info)
        else:
            title = None
            
        emails = self._get_emails(content, url, title)
        return title
    
    def _get_emails(self, content, url, title):
        """get the emails""" 
        emails = EmailSpider.EMAIL_RE.findall(content)
        for email in emails:
            if not email.lower().endswith('com'):
                continue
            self._safe_email(email, url, title)
            self._email_count += 1
        return emails
    
    def _safe_email(self, email, url, title):
        """保存邮箱"""
        cursor = connection.cursor()
        now = datetime.now()
        email_md5 = md5mgr.mkmd5fromstr(email)
        self.current_info = '[%s]\n[title]:%s\n [url]:%s\n [email]:%s' % (now.strftime('%Y-%m-%d %H:%M:%S'), title, url, email)
        email_info = {'email': email, 'email_md5': email_md5, \
                       'created_at': now, 'key_word': self._current_keyword, \
                       'url': url, 'title': title}
        try:
            cursor.insert(email_info, 'spider_email', True, ['created_at'])
        finally:
            cursor.close()
         
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
        sql = 'select id, key_word from email_spider_task where id>%s' % self._last_id
        print sql
        while True:
            cursor = connection.cursor()
            try:
                data = cursor.fetchall(sql)
                if data:
                    return data.to_list()
                else:
                    return []
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            finally:
                cursor.close()
            self._sleep()
    
    def _update_task(self, task_info):
        """update task"""
        cursor = connection.cursor()
        while True:
            try:
                cursor.insert(task_info, 'email_spider_task', True, ['created_at'])
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()
                  
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
    spider = EmailSpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()
