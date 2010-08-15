#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import logging
import random
from datetime import datetime

from dtspider.client.base import BaseClient
from dtspider.entity import AskInfo, AskTypeMapping, RuntimeData
from dtspider.common.net.post import Post

class SosoAskPublisher(BaseClient):
    
    def __init__(self):
        super(SosoAskPublisher, self).__init__('SosoAskPublisher')
        self._last_id = 0
        
    def run(self):
        self.issue_info()
        now = datetime.now()
        self.current_info = "%s: sleep 60s..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return 60
 
    def issue_info(self):
        
        self._load_status()
        infos = AskInfo.query(condition=" status=0 and from_type='soso'", limit=10)
        if not infos:
            return
        self._last_id = infos[-1].id
        for info in infos:
            success = self.publish(info)
            now = datetime.now()
            self.current_info = "%s: message publish %s..." % (now.strftime("%Y-%m-%d %H:%M:%S"), 'success' if success else 'failure')
            self._sleep(random.randint(10, 20))
            logging.info(self.current_info) 
        self._save_status()    
        
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
    
def main():
    spider = SosoAskPublisher()
    spider.main()

if __name__ == '__main__':
    main()
