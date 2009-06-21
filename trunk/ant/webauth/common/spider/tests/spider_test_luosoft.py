#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    qq_templates = [
        {'interval': 7200, 'downurl': ur'''
            <a[^<>]*?href="([^<>]+?)"[^<>]*?><span[^<>]*?>(?:最新版本|密码保护模块下载)</span>
        ''',
         'only_downurl': True,
         }
    ]
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.qq_spider = spider.Spider('http://www.luosoft.com/downcn.htm', 
                                                self.qq_templates)
        
    def tearDown(self):
        "Hook method for deconstructing the test fixture after testing it."
        pass
#        
    def test_get_urls(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.qq_spider.walk():
            print downurls, url, parenturl
            

if __name__ == '__main__':
    unittest.main()
