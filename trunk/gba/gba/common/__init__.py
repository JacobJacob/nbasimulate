#!/usr/bin/python
# -*- coding: utf-8 -*-

import logging
from logging import handlers
import errno
import traceback
import sys

import memcache

from gba.web.config import SYSLOG_HOST, PathSettings, COMMON_CACHE

cache = memcache.Client(COMMON_CACHE)

class _InitLog(object):
    """初始化日志系统"""
    def __init__(self):
        self.init('')
    
    def init(self, prefix=''):        
        format = '%(process)d|%(threadName)s|%(levelname)s|%(pathname)s|%(lineno)s|%(funcName)s|%(message)s'
        format = '%(asctime)s|' + format
        
        [x.close() for x in logging.root.handlers]
        del logging.root.handlers[:]
 
        logging.root.setLevel(logging.INFO)
 
        handler = logging.StreamHandler()
        handler.setLevel(logging.DEBUG)
        handler.setFormatter(logging.Formatter(format))
        logging.root.addHandler(handler)
        
init_log = _InitLog()

def log_execption():
    pass

