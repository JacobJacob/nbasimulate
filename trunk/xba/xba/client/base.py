#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
import traceback
from datetime import datetime

class BaseClient(object):
    
    
    def __init__(self, name):
        self._name = name
    
    def work(self):
        raise "method not implement"
    
    def before_run(self):
        pass
    
    def start(self):
        self.before_run()
        while True:
            try:
                if self.work() == "exist":
                    self.log("start to exist")
                    break
            except:
                self.log(traceback.format_exc())
                break
                
    def log(self, msg):
        print "[%s]%s" % (datetime.now().strftime("%Y-%m-%d %H:%M:%S"), msg)
        
    def sleep(self, seconds=10):
        self.log("start to sleep:%s seconds" % seconds)
        time.sleep(seconds)
                
    