#!/usr/bin/python
# -*- coding: utf-8 -*-
"""连接池unittest
"""

import unittest
from connection_pool import ConnectionPool
import threading
import time
import random

COUNT = 0
LOCKER = threading.Lock()
def close_cursor(c):
    global COUNT
    s = random.randint(1, 3)
    LOCKER.acquire()
    COUNT += 1
    i = COUNT
    LOCKER.release()
    time.sleep(s)
    c.close()
    print "close cursor %s. sleep %s seconds" % (i, s)
    
def create_cursor(cursors, ts, tester):
    c = ConnectionPool.cursor()
    cursors.append(c)
    t = threading.Thread(target = close_cursor, args = (c, ))
    t.setDaemon(True)
    t.start()
    ts.append(t)
#    print ConnectionPool._count, ConnectionPool.MAX_COUNT
    tester.assertTrue(ConnectionPool._count <= ConnectionPool.MAX_COUNT + 1)
    tester.assertTrue(len(ConnectionPool._connections) <= ConnectionPool.MAX_COUNT)

class ConnectionPoolTest(unittest.TestCase):
    def test_pool(self):
        cursors = []
        ts = []
        threads_count = 100
        for i in range(threads_count):
            t = threading.Thread(target = create_cursor, args = (cursors, ts, self))
            ts.append(t)
            t.setDaemon(True)
            t.start()
            ts.append(t)
            
        for t in ts:
            t.join()
        self.assertEqual(len(cursors), threads_count)
        self.assertEqual(len(ConnectionPool._connections), ConnectionPool._hold_count)
        self.assertEqual(ConnectionPool._count, 0)
        ConnectionPool.close()
        
if __name__ == '__main__':
    unittest.main()