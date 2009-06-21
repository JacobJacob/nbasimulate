#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    mydown_templates = [
        {'next_level': ur"""
                 <link>([^<>]*)</link>
            """,
         'result_start': True,
        },
        {
         'downurl': ur"""
            <a[^<>]*?href=['"]([^<>]*?)['"][^<>]*?><img[^<>]*?src=['"][^<>]*?down2_ico\.gif['"][^<>]*?>
            """,
            'info': ur"""
                <h1>(?P<name>[^<>]*?)</h1>
                (?:[\s\S]*?
                <li><b>文件大小</b>：(?P<size>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <li><b>更新日期</b>：(?P<last_updated>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <li><b>软件版本</b>：(?P<version>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <li><b>适用平台</b>：(?P<platform>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <li><b>软件类型</b>：(?P<copyright>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <div[^<>]*?class="mdccontent"[^<>]*?>\s*<p>\s*
                (?P<description>[\s\S]*?)\s*
                </p>\s*
                </div>)?
            """
        },
    ]

    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.mydown_spider = spider.Spider('http://www.mydown.com/1.xml', 
                                           self.mydown_templates)
#        
    def test_get_urls(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.mydown_spider.walk():
            print url, softinfo
            print downurls
if __name__ == '__main__':
    unittest.main()
