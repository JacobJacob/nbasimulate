#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import logging
import random
import re
from datetime import datetime

from dtspider.common import md5mgr
from dtspider.client.base import BaseClient
from dtspider.common.spider import Spider
from dtspider.common.constants import ClientType
from dtspider.entity import AskInfo, AnswerInfo, RuntimeData, AskTypeMapping
from dtspider.common.net.post import Post

class SosoAskSpider(BaseClient):
    
    REMOVE_HTMLTAG_RE = re.compile(r'(<.*?>)', re.I)
    LINK_PATTERN = ur'''<a[\S\s]*?href="(/z/q\d*?.htm)"'''
    LIST_LINK_PATTERN = ur'''<dd[\S\s]*?ch=".*?"><a[\S\s]*?href="(/z/c\d*?.htm)">(.*?)</a>'''
    CONTENT_PATTERN = ur'''
                            ([\S\s]*?<div[\S\s]*?class="question_main">[\S\s]*?<h3>(?P<title>[\S\s]*?)</h3>)?         
                            ([\S\s]*?<div[\S\s]*?class="question_con">(?P<content>[\S\s]*?)</div>)? 
                        '''
    ANWSER_PATTERN = ur'''<div[\S\s]*?class="answer_con">([\S\s]*?)</div>''' 

    def __init__(self):
        super(SosoAskSpider, self).__init__('SosoAskSpider')
        self._start_url = 'http://wenwen.soso.com/'
        self._spider = Spider()
        self._last_id = 0
        
    def run(self):
        
        self.spide_info()
        #self.issue_info()
        #self.issue_answer()
        now = datetime.now()
        self.current_info = "%s: sleep 600s..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return 600
    
    def spide_info(self):
        
        content = self.get_content(self._start_url)
        links = self._spider.get_urls(content, SosoAskSpider.LIST_LINK_PATTERN)
        for link in links:
            url, type = link[0], link[1]
            print url, type
            type_mapping = AskTypeMapping()
            type_mapping.type = type
            while True:
                try:
                    type_mapping.persist()
                    break
                except:
                    self.current_info = u'%s' % traceback.format_exc()
                    logging.error(self.current_info)
                    self._sleep()
    
            real_url = 'http://wenwen.soso.com%s' % url
            self._hand_list_link(real_url, type)
        
    def issue_info(self):
        
        infos = AskInfo.query(condition=" status=0 and from_type='soso'", limit=10)
        if not infos:
            return
        for info in infos:
            success = self.publish(info)
            now = datetime.now()
            self.current_info = "%s: message publish %s..." % (now.strftime("%Y-%m-%d %H:%M:%S"), 'success' if success else 'failure')
            #self._sleep(random.randint(100, 200))
            logging.info(self.current_info) 
            
    def issue_answer(self):
        start_id = 0
        infos = AnswerInfo.query(condition=" status=0 and id>%s " % start_id, order=" id asc ", limit=100)
        if not infos:
            return
        start_id = infos[-1].id
        for info in infos:
            self.publish_answer(info)

    def publish(self, info):
        '''publish info'''
        type_mapping = AskTypeMapping.load(type=info.type)
        if not (type_mapping and type_mapping.mid and type_mapping.classid):
            return False
        data = {}
        data['id'] = ''
        data['classid'] = type_mapping.classid
        data['title'] = info.title
        data['not_key'] = 1
        data['uid'] = 29
        data['uname'] = u'三亚小雪'.encode("gb2312")
        data['newstext'] = info.content
        data['point'] = random.randint(10, 50)
        data['enews'] = 'ASKMAddInfo'
        
        post = Post(data, 'www.3-ya.com', '/e/DoInfo/post.php')
        id = post.submit(result_keyword=u'成功', get_content=True, debug=True)
        if id:
            info.status = 1
            info.ask_id = id
            info.persist()
            
        return True
    
    def remove_html(self, v):
        return self.REMOVE_HTMLTAG_RE.sub(u'', v).strip()
    
    def publish_answer(self, info):
        '''publish answer'''
        ask_info = AskInfo.load(url_md5=info.ask_url_md5)
        if not ask_info or not ask_info.ask_id:
            print '问题还没发布'
            return
        else:
            print '开始发布'
        data = {}
        data['id'] = ''
        data['title'] = ask_info.title
        data['content'] = self.remove_html(info.content.replace("<br>", "\n").replace("\\", "").encode('utf8'))
        data['ask_id'] = ask_info.ask_id
        data['enews'] = 'MAddInfo'
        
        post = Post(data, 'www.3-ya.com', '/e/DoInfo/ask.php')
        post.submit(result_keyword=u'成功', get_content=True, debug=True)

        info.status = 1
        info.persist()
            
        return True
        
    def _encode(self, s):
        if not s:
            return s
        try:
            return s.encode('gb2312')
        except:
            return s.encode('gb2312', 'replace')
       
    def _hand_list_link(self, list_url, type):
        
        for i in range(1):
            content = self.get_content('%s?pg=%s' % (list_url, i))
            links = self._spider.get_urls(content, SosoAskSpider.LINK_PATTERN)
            for link in links:
                real_url = 'http://wenwen.soso.com%s' % link
                self._hand_link(real_url, type)
            
    def _hand_link(self, url, type):
        content = self.get_content(url)
        info = self._spider.get_info(content, SosoAskSpider.CONTENT_PATTERN)
        answers = self._spider.get_urls(content, SosoAskSpider.ANWSER_PATTERN)
        
        ask_info = AskInfo()
        ask_info.title = info['title']
        ask_info.content = info['content'] if info['content'] else info['title']
        ask_info.from_type = 'soso'
        ask_info.url = url
        ask_info.type = type
        ask_info.url_md5 = md5mgr.mkmd5fromstr(url)
        #ask_info.status = 0
        ask_info.persist()
        
        if answers:
            for i, answer in enumerate(answers):
                answer_info = AnswerInfo()
                answer_info.content = answer
                answer_info.ask_url_md5 = md5mgr.mkmd5fromstr(url)
                answer_info.status = 0
                answer_info.seq = i
                answer_info.persist()
                
    def get_content(self, url):
        '''read'''
        try:
            content = self._spider.read(url)
            return content
        except:
            self.current_info = u'%s' % traceback.format_exc()
            logging.error(self.current_info)
        return ''
    
    def test_issue(self, id):
        info = AskInfo.load(id=id)
        self.publish(info)
        
        
def test():
    url = 'http://wenwen.soso.com/z/q213787320.htm'
    p = ur'''<div[\S\s]*?class="answer_con">([\S\s]*?)</div>''' 
    spider = Spider()
    content = spider.read(url)
    answers = spider.get_urls(content, p)
    print answers
    
def main():
    spider = SosoAskSpider()
    spider.main()
    #spider.test_issue(1)
    #spider._load_status()
    #spider._hand_link('http://www.sanyaxunfang.com/yh/1259408319.html')
    
if __name__ == '__main__':
    #test()
    main()
