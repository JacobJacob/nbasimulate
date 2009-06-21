#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url任务生成客户端"""

from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import url_task_proxy
from webauth.common import log_execption
from webauth.common.single_process import SingleProcess

class UrlTaskExpirer(BaseClient):
    
    def __init__(self):
        BaseClient.__init__(self, 'UrlTaskExpirer')
        self.sleep_time = 60
        
    def run(self):
        while True:
            try:
                data = url_task_proxy.update_exprired_task()
                if data['error']:
                    print data['error']['stack']
                else:
                    break
            except:
                log_execption()
                self._sleep()
        self.current_info = 'update %s exprired tasks.' % data['result']
        print self.current_info
        if data['result'] and data['result'] > 0: # 若有超时任务，则马上进入下一轮循环
            return -1
        return 300 # 5分钟间隔
        
def main():
    mutex_process = SingleProcess("url_task_expirer_process")
    mutex_process.check()
    client = UrlTaskExpirer()
    client.main()
        
        
