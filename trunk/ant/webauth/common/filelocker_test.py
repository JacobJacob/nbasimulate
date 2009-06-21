#!/usr/bin/python
# -*- coding: utf-8 -*-
"""decorator test
"""
from __future__ import with_statement

import os
import unittest
import threading
import random
import time
import subprocess

from filelocker import FileLocker
    
def start_thread():
    i = func()
    s = random.randint(1, 3)
    time.sleep(s)
    print "thread %s. sleep %s seconds" % (i, s)

count = 0
lock = FileLocker('filelocker_test')

def func():
    with lock:
        global count
        count += 1
        time.sleep(0.1)
        return count
    
class DecoratorTest(unittest.TestCase):
    
    __lock = threading.Lock()
    
    def test_thread(self):
        ts = []
        thread_count = 10
        for i in range(thread_count):
            t = threading.Thread(target = start_thread)
            ts.append(t)
            t.start()
        for t in ts:
            t.join()
        global count
        self.assertEqual(count, thread_count)
        
    def test_process(self):
        if os.path.exists('test_count'):
            os.remove('test_count')
        ps = []
        for i in range(10):
            cmd = 'python %s %d' % (__file__, i)
            print cmd
            p = subprocess.Popen(cmd, shell=True, close_fds=True)
            ps.append(p)
        for p in ps:
            p.wait()
        if os.path.exists('test_count'):
            os.remove('test_count')

def run_process():
    with lock:
        if not os.path.exists('test_count'):
            i = 0
        else:
            with open('test_count', 'rb') as f:
                i = int(f.read())
        i += 1
        with open('test_count', 'wb') as f:
            f.write('%d' % i)
        
    s = random.randint(1, 3)
    time.sleep(s)
    print "process %s. sleep %s seconds" % (i, s)

if __name__ == '__main__':
    import sys
    if len(sys.argv) > 1:
        run_process()
    else:
        unittest.main()