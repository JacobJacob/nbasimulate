#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import logging
import random
from datetime import datetime

from dtspider.common import md5mgr
from dtspider.client.base import BaseClient
from dtspider.common.spider import Spider
from dtspider.common.constants import ClientType
from dtspider.entity import SanyaInfo, RuntimeData, SanyaTypeMapping
from dtspider.common.net.post import Post

class SanyaxunfangSpider(BaseClient):
    
    LINK_PATTERN = ur'''<a[\S\s]*?href=../yh/([^>\s]*)[\S\s]*?target=_blank>'''
    CONTENT_PATTERN = ur'''
                            ([\S\s]*?<div[\s]*?id="area"><font[\s]*?style="font-size:16px;font-weight:bold;">\(.*\)(?P<title>[\S\s]*?)</font></div>)?         
                            ([\S\s]*?<div[\s]*?id='area'>类目&nbsp;:&nbsp;(?P<type>[\S\s]*?)</div>)? 
                            ([\S\s]*?<div[\s]*?id='area'>地点&nbsp;:&nbsp;(?P<address>[\S\s]*?)</div>)? 
                            ([\S\s]*?<div[\s]*?id='area'>电话&nbsp;:&nbsp;(?P<phone>[\S\s]*?)</div>)?  
                            ([\S\s]*?<div[\s]*?id="area">日期&nbsp;:&nbsp;(?P<date>[\S\s]*?)</div>)?   
                            ([\S\s]*?<div[\s]*?id="area"><p>(?P<content>[^<]*))?
                        '''
    
    def __init__(self):
        super(SanyaxunfangSpider, self).__init__(ClientType.SANYAXUNFANG_SPIDER)
        self._start_url = 'http://www.sanyaxunfang.com/'
        self._spider = Spider()
        self._last_id = 0
        
    def run(self):
        
        self.spide_info()
        self.issue_info()
        now = datetime.now()
        self.current_info = "%s: sleep 600s..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return 600
    
    def spide_info(self):
        
        content = self.get_content(self._start_url)
        links = self._spider.get_urls(content, SanyaxunfangSpider.LINK_PATTERN)
        for link in links:
            real_url = 'http://www.sanyaxunfang.com/yh/%s' % link
            self._hand_link(real_url)
        
    def issue_info(self):
        
        self._load_status()
        infos = SanyaInfo.query(condition="id>%s and from_type='xunfang'" % self._last_id, limit=10)
        if not infos:
            return
        for info in infos:
            self._last_id = info.id
            success = self.publish(info)
            now = datetime.now()
            self.current_info = "%s: message publish %s..." % (now.strftime("%Y-%m-%d %H:%M:%S"), 'success' if success else 'failure')
            self._sleep(random.randint(100, 200))
            #logging.info(self.current_info) 
        self._save_status()        
    
    def publish(self, info):
        '''publish info'''
        type_mapping = SanyaTypeMapping.load(type=info.type)
        if not type_mapping:
            return False
        data = {'mid': type_mapping.mid}
        data['id'] = ''
        data['filepass'] = '1259474509'
        data['classid'] = type_mapping.classid
        data['title'] = self._encode(info.title)
        data['titlepicfile'] = ''
        data['phone'] = info.phone
        data['enews'] = 'MAddInfo'
        data['smalltext'] =   self._encode(info.content)
        data['myarea'] = '三亚市区'.encode('gb2312')
        data['not_key'] = 1
        data['address'] = self._encode(info.address)
        data['zhongjie'] = '否'.encode('gb2312')
        data['jiage'] = '面议'.encode('gb2312')
        data['xiu'] = '简单装修'.encode('gb2312')
        data['lianxiren'] = info.phone
        data['gotoinfourl'] = 1
        data['editgotoinfourl'] = 1
        post = Post(data, 'www.3-ya.com', '/e/DoInfo/ecms.php')
        success = post.submit(result_keyword=u'成功')
        return success
    
    def _encode(self, s):
        if not s:
            return s
        try:
            return s.encode('gb2312')
        except:
            return s.encode('gb2312', 'replace')
       
    def _load_status(self):
        '''load status'''
        while True:
            try:
                runtime_data = RuntimeData.load(programe=self.__class__.__name__, value_key='last_id')    
                if runtime_data:
                    self._last_id = runtime_data.value
                else:
                    self._last_id = 0
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()
    
    def _save_status(self):
        ''''save status'''
        while True:
            try:
                runtime_data = RuntimeData()
                runtime_data.programe = self.__class__.__name__
                runtime_data.value_key = 'last_id'
                runtime_data.value = self._last_id
                runtime_data.persist()
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()
            
    def _hand_link(self, url):
        content = self.get_content(url)
        info = self._spider.get_info(content, SanyaxunfangSpider.CONTENT_PATTERN)
        for k, v in info.items():
            #print '%s=%s' % (k, v)
            pass
        if not info['title'] or not info['phone'] or not info['content']:
            return False
        sanya_info = SanyaInfo()
        sanya_info.title = info['title']
        sanya_info.type = info['type']
        sanya_info.phone = info['phone']
        sanya_info.address = info['address']
        sanya_info.content = info['content']
        sanya_info.url = url
        sanya_info.from_type = 'xunfang'
        sanya_info.url_md5 = md5mgr.mkmd5fromstr(url)
        
        type_mapping = SanyaTypeMapping()
        type_mapping.type = info['type']
        while True:
            try:
                sanya_info.persist()
                type_mapping.persist()
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
                self._sleep()
        return True
        
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
        info = SanyaInfo.load(id=id)
        self.publish(info)
        

    
def main():
    spider = SanyaxunfangSpider()
    spider.main()
    #print spider.test_issue(1)
    #spider._load_status()
    #spider._hand_link('http://www.sanyaxunfang.com/yh/1259408319.html')
    
if __name__ == '__main__':
    main()
