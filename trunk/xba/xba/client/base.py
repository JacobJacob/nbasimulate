#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import time
import traceback
from datetime import datetime

from xba.config import PathSettings

class BaseClient(object):
    
    
    def __init__(self, name):
        self._name = name
        self._log_file = "%s_%s" % (name.lower(), datetime.now().strftime("%Y_%m_%d"))
    
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
        s= "[%s]%s" % (datetime.now().strftime("%Y-%m-%d %H:%M:%S"), msg)
        f = open(os.path.join(PathSettings.LOG, self._log_file), "ab")
        try:
            f.write("%s\n" % s)
            print s
        finally:
            f.close()
        
    def sleep(self, seconds=10):
        self.log("start to sleep:%s seconds" % seconds)
        time.sleep(seconds)
                
    