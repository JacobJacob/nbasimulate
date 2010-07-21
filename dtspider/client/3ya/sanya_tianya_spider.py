#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import logging
from datetime import datetime
import re

from dtspider.common import md5mgr
from dtspider.client.base import BaseClient
from dtspider.common.spider import Spider
from dtspider.common.constants import ClientType
from dtspider.entity import SanyaInfo, RuntimeData, SanyaTypeMapping
from dtspider.common.net.post import Post

class SanyaTianyaSpider(BaseClient):
    
    LINK_PATTERN = ur'''<a[\s]href='([^>\s]*)'[\s]*?title'''
    CONTENT_PATTERN = ur'''
                            ([\S\s]*?—<[aA][\sS]*?href=[^>]*>(?P<type>[^<]*)<\/[aA]>)?
                            ([\S\s]*?<title>(?P<title>[\S\s]*?)</title>)?           
                            ([\S\s]*?<DIV[\s]*?class=content[\s]*?style="WORD-WRAP:break-word">(?P<content>[\S\s]*?)</DIV>)?
                        '''
                        
    DELETE_IMG_RE = re.compile('<img[^>]*>', re.VERBOSE)
                        
    DETAIL_CONTENT_PATTERN = ur'''
                                 ([\S\s]*?联系人：(?P<contact>[^<]*?)<br>)?
                                 ([\S\s]*?联系方式：(?P<phone>[^<]*?)<br>)?
                                 ([\S\s]*?联系地址：(?P<address>[^<]*?)</DIV>)?
                             '''
    
    def __init__(self):
        super(SanyaTianyaSpider, self).__init__('sanya_tianya_spider')
        self._start_url = 'http://info.tianya.cn/sort/22/65/0/0/0/0.shtml'
        self._spider = Spider(remove_html=False)
        self._last_id = 0
        
    def run(self):
        
        self.spide_info()
        self.issue_info()
        now = datetime.now()
        self.current_info = "%s: sleep 600s..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return 600
    
    def spide_info(self):
        
        content = self.get_content(self._start_url)
        links = self._spider.get_urls(content, SanyaTianyaSpider.LINK_PATTERN)
        for link in links:
            self._hand_link(link)
        
    def issue_info(self):
        
        self._load_status()
        infos = SanyaInfo.query(condition='id>%s' % self._last_id, limit=10)
        if not infos:
            return
        for info in infos:
            self._last_id = info.id
            success = self.publish(info)
            now = datetime.now()
            self.current_info = "%s: message publish %s..." % (now.strftime("%Y-%m-%d %H:%M:%S"), 'success' if success else 'failure')
            logging.info(self.current_info)
        
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
        data['title'] = info.title
        data['titlepicfile'] = ''
        data['mycontact'] = info.phone
        data['enews'] = 'MAddInfo'
        data['smalltext'] =   info.content
        data['myarea'] = u'河东区'
        data['address'] = info.address
        post = Post(data, 'www.3-ya.com', '/e/DoInfo/ecms.php')
        success = post.submit(result_keyword=u'成功')
        return success
       
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
        info = self._spider.get_info(content, SanyaTianyaSpider.CONTENT_PATTERN)
        detail_info = self._spider.get_info(content, SanyaTianyaSpider.DETAIL_CONTENT_PATTERN)
#        for k, v in info.items():
#            print '%s=%s' % (k, v)
#        for k, v in detail_info.items():
#            print '%s=%s' % (k, v)
        if not info['title'] or not detail_info['phone'] or not info['content']:
            return False
        sanya_info = SanyaInfo()
        sanya_info.title = info['title']
        sanya_info.type = info['type']
        sanya_info.phone = detail_info['phone']
        sanya_info.address = detail_info['address']
        sanya_info.content = info['content'].replace('<br>', '\n').replace('&nbsp;', '').replace('<BR>', '\n')
        sanya_info.content = SanyaTianyaSpider.DELETE_IMG_RE.sub('', sanya_info.content)
        sanya_info.url = url
        sanya_info.url_md5 = md5mgr.mkmd5fromstr(url)
        sanya_info.from_type = 'tianya'
        
        #print 'type:%s' % sanya_info.type
        #print 'title:%s' % sanya_info.title
        #print 'content:%s' % sanya_info.content
        #print 'phone:%s' % sanya_info.phone
        
        type_mapping = SanyaTypeMapping()
        type_mapping.type = info['type']
        try:
            sanya_info.persist()
            type_mapping.persist()
            #pass
        except:
             self.current_info = u'%s' % traceback.format_exc()
             logging.error(self.current_info)
             return False
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
    
    
def main():
    spider = SanyaTianyaSpider()
    #spider.main()
    spider.spide_info()
    #spider._load_status()
    #spider._hand_link('http://www.sanyaxunfang.com/yh/1259408319.html')
    
if __name__ == '__main__':
    main()