#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Sand Box Base Client"""

from xba.client.base import BaseClient
from xba.client import sandbox
import logging

class SanboxBaseClient(BaseClient):
    """需要在沙箱中运行的客户端，都可以继承此类来实现"""
    
    def __init__(self, client_type, cmd_path, default_box_count):
        super(SanboxBaseClient, self).__init__(client_type)
        self.cmd_path = cmd_path
        self.default_box_count = default_box_count
        self.box_count = default_box_count
        
    def before_run(self):
        self.box_count = self.default_box_count
        return "%d Sand box run[%s]" % (self.box_count, self.cmd_path)
    
    def work(self):
        logging.info('start sandbox')
        sandbox.run(self.box_count, self.cmd_path)
        
    def _restart(self):
        sandbox.restart()
        
    def _quit(self):
        sandbox.stop()
        super(SanboxBaseClient, self)._quit()
        
    def _stop(self):
        super(SanboxBaseClient, self)._stop()
        sandbox.stop()
        
    def _svnup(self):
        logging.info('stopping sandbox...')
        sandbox.stop()
        logging.info('success')
        super(SanboxBaseClient, self)._svnup()