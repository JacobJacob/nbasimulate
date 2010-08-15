#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import logging
import random
import re
from datetime import datetime

from dtspider.client.base import BaseClient
from dtspider.entity import AskInfo, AnswerInfo, RuntimeData
from dtspider.common.net.post import Post

class SosoAnswerPublisher(BaseClient):
    
    REMOVE_HTMLTAG_RE = re.compile(r'(<.*?>)', re.I)
    
    def __init__(self):
        super(SosoAnswerPublisher, self).__init__('SosoAnswerPublisher')
        self._last_id = 0
        
    def run(self):
        
        self.issue_answer()
        now = datetime.now()
        self.current_info = "%s: sleep 60s..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return 60
            
    def issue_answer(self):
        self._load_status()
        infos = AnswerInfo.query(condition=" status=0 and id>%s " % self._last_id, order=" id asc ", limit=100)
        if not infos:
            self._last_id = 0
            self._save_status()
            return
        self._last_id = infos[-1].id
        for info in infos:
            self.publish_answer(info)
        self._save_status()
        
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
    spider = SosoAnswerPublisher()
    spider.main()

if __name__ == '__main__':
    #test()
    main()
