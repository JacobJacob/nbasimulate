#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Unit Test for spider.Spider
"""

import unittest
import spider

class SpiderTest(unittest.TestCase):
    """Spider UnitTest Case"""
    
    templates = [
        {'same_level': ur"""
                <a[^<>]*?href="([^/]*.htm)">[^<>]*?</a>
            """,
         'downurl': ur"""
          <TR[^<>]*?><TD[^<>]*?class=cat><B><a[^<>]*?href="([^<>]*?)">[^<>]*?</a></b></TD></TR>
            """,
         'only_downurl': True,
        },
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.spider = spider.Spider('http://jansfreeware.com/outline.html', 
                                          self.templates)
    def test_walk(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.spider.walk():
            print '-' * 60
            print level, url, parenturl
            print downurls

if __name__ == '__main__':
    unittest.main()