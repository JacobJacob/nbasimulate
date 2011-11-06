#!/usr/bin/python
# -*- coding: utf-8 -*-

from subprocess import Popen, PIPE

import os
import time
import traceback
from datetime import datetime

from xba.config import CLIENT_EXE_PATH
from xba.config import PathSettings

class BaseClient(object):
    
    
    def __init__(self, name):
        self._name = name
        self._log_file = "%s_%s" % (name.lower(), datetime.now().strftime("%Y_%m_%d"))
    
    def work(self):
        raise "method not implement"
    
    def before_run(self):
        pass
        
    def call_cmd(self, cmd):
        """call cmd """
        while True:
            success = self.__call_cmd(cmd)
            if success:
                return True
            else:
                self.log("call cmd failure, sleep and try 10s later")
                self.sleep()
            
    def __call_cmd(self, cmd):
        """调用命令"""
        last_line, line = None, None
        p = Popen(cmd, stdout=PIPE)
        while True:
            last_line =  line
            line = p.stdout.readline()
            if not line:
                break
            self.log(line.replace("\n", ""))
            
        if p.wait() == 0:
            if last_line:
                last_line = last_line.replace("\n", "").replace("\r", "")
            if last_line == "SUCCESS":
                self.log("call %s success" % cmd)
                return True
                   
        self.log("call %s failure" % cmd)
        return False
    
    def start(self):
        self.before_run()
        while True:
            try:
                if self.work() in ("exit", "exist"):
                    self.log("start to exit")
                    break
            except:
                self.log(traceback.format_exc())
                break
                
    def log(self, msg):
        s= "[%s]%s" % (datetime.now().strftime("%Y-%m-%d %H:%M:%S.log"), msg)
        f = open(os.path.join(PathSettings.LOG, self._log_file), "ab")
        try:
            f.write("%s\n" % s)
            print s
        finally:
            f.close()
        
    def sleep(self, seconds=10):
        self.log("start to sleep:%s seconds" % seconds)
        time.sleep(seconds)
                
                
if __name__ == "__main__":
    client = BaseClient("test")
    cmd = "%s %s" % (CLIENT_EXE_PATH, "test_success")
    client.call_cmd(cmd)
    
    cmd = "%s %s" % (CLIENT_EXE_PATH, "test_error")
    client.call_cmd(cmd)
    
    