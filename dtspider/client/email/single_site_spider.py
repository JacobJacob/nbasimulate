#-*- coding:utf-8 -*-

import threading
import traceback
import re
import time

from datetime import datetime
from dtspider.common import md5mgr
from dtspider.common.spider import Spider
from dtspider.common.db import connection

class WorkThread(threading.Thread):
    
    
    def __init__(self, name, get_url, put_email):
        super(WorkThread, self).__init__()
        self.name = name
        self.get_url = get_url
        self.put_email = put_email
        self._spider = Spider()
        
    def run(self):
        '''method body'''
        while True:
            url = self.get_url()
            if not url:
                continue
                time.sleep(1)
            emails, title = self.get_emails(url)
            total_count, new_count = self.put_email(emails, title, url)
            if new_count > 0:
                print 'thread:%s put %s new emails, total:%s' % (self.name, new_count, total_count)        
    
    def read_page(self, url):
        '''read page'''
        try:
            content = self._spider.read(url)
            #print content
            return content
        except:
            print traceback.format_exc(3)
            print url
            return ''
    
    def get_emails(self, url):
        """get the emails"""
        content = self.read_page(url)
        
        match = SingleSiteSpider.TITLE_RE.search(content)
        if match:
            d = match.groupdict()
            title = d['title']
        else:
            title = None
        
        emails = SingleSiteSpider.EMAIL_RE.findall(content)
        ret_emails = []
        for email in emails:
            if email.endswith('com') or email.endswith('cn'):
                ret_emails.append(email)
        return ret_emails, title
        
class SingleSiteSpider():
    
    EMAIL_RE = re.compile(ur'''([0-9a-zA-Z\.-]+@[0-9a-zA-Z]+\.[a-zA-Z]*)''', re.VERBOSE)
    TITLE_RE = re.compile(ur'''<title>(?P<title>.*)</title>''', re.VERBOSE)
    
    def __init__(self, start_url, max_level=3):
        self._start_url = start_url
        self._current_level = 1
        self._max_level = max_level
        self._spider = Spider('', [])
        self._url_cache = []
        self._url_cache.append(start_url)
        self._eche_level_urls = {}
        self._total_urls = 0
        self._email_cache = []
        self.thread_started = False
        self.get_url_locker = threading.RLock()
        self.put_email_locker = threading.RLock()
        self.current_url_index = 0
        
    def run(self):
        self.handle_url(self._start_url)
    
    def start_thread(self):
        '''start thread'''
        for i in range(10):
            thread = WorkThread('thread-%s' % i, self.get_url, self.put_email)
            thread.start()
    
    def get_url(self):
        '''a interface for thread to get the url'''
        try:
            self.get_url_locker.acquire()
            if len(self._url_cache) > self.current_url_index:
                url = self._url_cache[self.current_url_index]
                self.current_url_index += 1
                return url
        finally:
            self.get_url_locker.release()
        return None
    
    def put_email(self, emails, title, url):
        '''a interface for the thread to put the url'''
        try:
            self.put_email_locker.acquire()
            new_email_count = 0
            for email in emails:
                if email not in self._email_cache:
                    print '-' * 20, email, '-' * 20
                    self._email_cache.append(email)
                    self._safe_email(email, title, url)
                    new_email_count += 1
        finally:
            self.put_email_locker.release()
        return len(self._email_cache), new_email_count
     
    def _safe_email(self, email, title, url):
        """保存邮箱"""
        cursor = connection.cursor()
        now = datetime.now()
        email_md5 = md5mgr.mkmd5fromstr(email)
        email_info = {'email': email, 'email_md5': email_md5, \
                       'created_at': now, 'key_word': 'tinaya', \
                       'title': title, 'url': url}
        try:
            #cursor.insert(email_info, 'spider_email', True, ['created_at'])
            cursor.insert(email_info, 'spider_email', True)
        finally:
            cursor.close()
                   
    def handle_url(self, url):
        
        #get current page urls
        current_urls = self.get_urls(url)
        #print 'url: %s,current level is:%s, get urls:%s' % (url, self._current_level, len(current_urls))
        current_urls = self.delete_exist(current_urls)
        
        if not self.thread_started:
            self.start_thread()
            self.thread_started = True
        
        #for page_url in current_urls:
            #self.get_emails(page_url)
        self._total_urls += len(current_urls)
        #print 'url:%s,total urls:%s, news:%s' % (url, self._total_urls, len(current_urls))
        if self._current_level in self._eche_level_urls:
            level_urls = self._eche_level_urls[self._current_level]
            level_urls += current_urls
        else:
            self._eche_level_urls[self._current_level] = current_urls
        
        
        if self._current_level + 1 > self._max_level:
            self._current_level -= 1
            #print 'big than max level, out'
            return
        else:
            self._current_level += 1
            for current_url in current_urls:
                self.handle_url(current_url)
    
    def delete_exist(self, urls):
        new_urls = []
        for url in urls:
            if url not in self._url_cache and url.find('tianya.cn') != -1:
                self._url_cache.append(url)
                new_urls.append(url)
        return new_urls
           
    def read_page(self, url):
        try:
            content = self._spider.read(url)
            return content
        except:
            print traceback.format_exc(3)
            print url
            return ''

    def get_urls(self, url):
        content = self.read_page(url)
        try:
            urls = self._spider.get_urls(content, ur'''<a[^<>]*?href=["'](http://.*?)["'][^<>]*?>''')            
            return urls
        except:
            print traceback.format_exc(3)
            return []

    def start(self):
        try:
            self.run()
        except:
            print traceback.format_exc(3)
            
    
            
if __name__ == '__main__':
    spider = SingleSiteSpider(start_url='http://info.tianya.cn/') 
    spider.start()