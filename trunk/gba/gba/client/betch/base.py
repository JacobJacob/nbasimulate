#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import time
import traceback
from datetime import datetime

from gba.common import json, log_execption
from gba.config import DEBUG
from gba.entity import ClientRunningLog, RuntimeData

def get_ip():
    if os.name != 'nt':
        try:
            ip = os.popen("/sbin/ifconfig | grep 'inet addr' | awk '{print $2}'").read()
            ip = ip[ip.find(':')+1:ip.find('\n')]
        except:
            pass
    else:
        import socket
        ip = socket.gethostbyname(socket.gethostname())
    return ip
    
class BaseBetchClient(object):
    
    def __init__(self):
        self._log = '' 
        self._status = None
        self._time = datetime.now()
        self._data = {}
        
    def start(self):
        self._status = 'start'
        self.append_log('client %s starting' % self.__class__.__name__)
        self.log()
        try:
            self._run()
        except:
            self.append_log(traceback.format_exc(3))
            self._status = 'error'
            self.log()
            raise
        self._status = 'finish'
        self.append_log('client %s finish' % self.__class__.__name__)
        self.log()
       
    def append_log(self, msg):
        if DEBUG:
            print msg
        self._log += '[%s]%s' % (datetime.now(), msg)
        
    def log(self):
        log = ClientRunningLog()
        log.client_name = self.__class__.__name__
        log.log_time = self._time
        log.log = self._log
        log.ip = get_ip()
        log.status = self._status
        ClientRunningLog.transaction()
        try:
            log.persist()
            ClientRunningLog.commit()
        except:
            ClientRunningLog.rollback()
            
    def _run(self):
        pass
    
    
    def save_status(self):
        '''保存状态'''
        while True:
            try:
                runtime_data = RuntimeData()
                runtime_data.programe = self.__class__.__name__
                runtime_data.value_key = 'status'
                runtime_data.value = json.dumps(self._data)
                break
            except:
                log_execption()
                time.sleep(20)
                
    def load_status(self):
        '''加载状态'''
        while True:
            try:
                runtime_data = RuntimeData()
                runtime_data.programe = self.__class__.__name__
                runtime_data.value_key = 'status'
                runtime_data.value = json.dumps(self._data)
                break
            except:
                log_execption()
                time.sleep(20)
                
    def set_status(self, key, value):
        '''设置状态'''
        self._data[key] = value
    
    def get_status(self, key):
        '''获取状态'''
        if self._data.has_key(key):
            return self._data[key]
                
if __name__ == '__main__':
    print get_ip()