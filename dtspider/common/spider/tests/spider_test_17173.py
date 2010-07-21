#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    games17173_templates = [
        {'next_level': ur"""
            <a[^<>]*?href=["'](http://download\.17173[^<>]*?)["'][^<>]*?>
            """,
         'only_downurl': True,
        },
        {'downurl': ur'''href="([^<>]+?(?:\.7z|\.ace|\.arj|\.bz2|\.bzip2|\.cab|\.cpio|\.deb|\.gz|\.gzip|\.iso|\.lha|\.lzh|\.rar|\.rpm|\.split|\.swm|\.tar|\.taz|\.tbz|\.tbz2|\.tgz|\.tpz|\.uue|\.wim|\.z|\.zip|\.xpi|\.wz|\.jar|\.abk|\.bin|\.dll|\.mis|\.exe)[^<>]*?)"''',
         }
    ]
    
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.games17173_spider = spider.Spider('http://news.17173.com/zhuanti/down/index.shtml', 
                                                self.games17173_templates)
        
    def test_get_urls(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.games17173_spider.walk():
            print url, downurls, parenturl

if __name__ == '__main__':
    unittest.main()
