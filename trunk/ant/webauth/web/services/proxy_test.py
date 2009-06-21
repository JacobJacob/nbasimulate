#!/usr/bin/python
# -*- coding: utf-8 -*-
"""proxy相关service api"""
import time

from webauth.common.jsonrpclib import ServerProxy

import unittest
class __Test(unittest.TestCase):
    """运行当前测试用例之前应确保服务器己启动
    """
    def test_go(self):
        s = ServerProxy('http://localhost:8888/services/proxy/')
        use_proxys = []
        for i in range(10):
            result = s.ask_for_proxy('http://www.baidu.com')
            proxy = result['result']
            self.assertNotEqual(proxy,None) #确保得到的代理有效
            self.assertFalse(proxy in use_proxys) #确保不会得到用过的代理
            print "success get a proxy:%s" % proxy
            time.sleep(3)
            s.report_proxy(proxy,'google_be_banned')
            use_proxys.append(proxy)
        
        
if __name__ == '__main__':
    unittest.main()
