#!/usr/bin/python
# -*- coding: utf-8 -*-
"""unit test
"""

import unittest
from white_host import *

class WhiteHostTest(unittest.TestCase):
    
    def test_white_host(self):
        hosts = ['www.testhost-testtesttest1.com',
                 'www.testhost-testtesttest2.com',
                 'testhost-testtesttest3.com',
                 'www.testhost-testtesttest4.com',
                 'testhost-testtesttest4.com',]
        whitehosts = get_white_hosts()
        self.assertTrue(isinstance(whitehosts, set))
        for host in hosts:
            save_white_host(host)
            
        whitehosts = get_white_hosts()
        self.assertTrue(isinstance(whitehosts, set))
        for host in hosts:
            self.assertTrue(host in whitehosts)
            self.assertTrue(is_white_host(host))
        is_white_host('www.testhost-testtesttest3.com', 'testhost-testtesttest4.com')
        
        for host in hosts:
            delete_white_host(host)
            self.assertFalse(is_white_host(host))
        
        whitehosts = get_white_hosts()
        self.assertFalse(whitehosts)
        self.assertTrue(isinstance(whitehosts, set)) 
            

if __name__ == '__main__':
    unittest.main()