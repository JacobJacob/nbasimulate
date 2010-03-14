#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import logging
from logging import handlers
import errno
import traceback
import sys

import memcache

from gba.config import SYSLOG_HOST, PathSettings, COMMON_CACHE
from gba.common.exception_mgr import format_except


cache = memcache.Client(COMMON_CACHE)

class _InitLog(object):
    """初始化日志系统"""
    def __init__(self, loghost=SYSLOG_HOST):
        self.inited = False
        self.loghost = loghost

    def __call__(self, prefix=''):
        '''初始化 logging
        
        把 WARNING 及以上级别的日志发到 SYSLOG_HOST 的 syslog，DEBUG 及以上的级别发
        到终端。

        第二次调用该函数时不进行任何操作。
        '''
        if self.inited:
            return
        
        return self.force_init(prefix)
    
    def force_init(self, prefix=''):        
        format1 = '%(process)d|%(threadName)s|%(levelname)s|%(pathname)s|%(lineno)s|%(funcName)s|%(message)s'
        format2 = '%(asctime)s|' + format1
        
        [x.close() for x in logging.root.handlers]
        del logging.root.handlers[:]
        logging.root.setLevel(logging.DEBUG)

        handler1 = handlers.SysLogHandler((self.loghost, handlers.SYSLOG_UDP_PORT), 'local1')
        handler1.setLevel(logging.WARNING)
        handler1.setFormatter(logging.Formatter(format1))
        logging.root.addHandler(handler1)
        
        handler3 = logging.StreamHandler()
        handler3.setLevel(logging.DEBUG)
        handler3.setFormatter(logging.Formatter(format2))
        logging.root.addHandler(handler3)

        self.inited = True
        
    def get_local_logfile(self, logdir, prefix=''):
        if prefix and not prefix.endswith('.'):
            prefix += '.'
        
        if not os.path.exists(logdir):
            os.makedirs(logdir)

#        if os.name != 'posix':
#            return os.path.join(logdir, 'log')
            
        i = 1
        while 1:
            logfile = os.path.join(logdir, '%slog.%i' % (prefix, i))
            if self.lock_file(logfile):
                return logfile
            i += 1
            
    def is_locked(self, lockpath):
        if not os.path.exists(lockpath) or os.name != 'posix': # windows 不检测
            return False
        f = open(lockpath)
        try:
            pid = int(f.read().strip())
        finally:
            f.close()
        try:
            os.kill(pid, 0)
        except OSError, e:
            if e.errno in (errno.ESRCH, errno.EPERM):
                return False
            else:
                raise
        return True

    def lock_file(self, fname):
        lockFname = fname + '.lock'
        if self.is_locked(lockFname):
            return False
        f = open(lockFname, 'w')
        try:
            f.write('%d\n' % os.getpid())
        finally:
            f.close()
        return True
        
init_log = _InitLog()

def get_logging():
    init_log()
    return logging

def log_execption(msg = None, track_index = 0):
    """自动记录异常信息"""
    init_log()
    backup = logging.currentframe
    try:
        currentframe = lambda: sys._getframe(5)
        logging.currentframe = currentframe # hook, get the real frame
        logging.error(repr(format_except(msg, track_index)))
    except:
        pass
    finally:
        logging.currentframe = backup
