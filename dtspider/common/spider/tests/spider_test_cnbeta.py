#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    templates = [
        {'next_level': ur"""
                 <link>([^<>]*)</link>
            """,
            'only_downurl': True,
        },
        {
         'downurl': ur"""
            <a[^<>]*?href=['"]([^<>]*?(?:\.7z|\.arj|\.cab|\.gz|\.gzip|\.iso|\.rar|\.tar|\.tbz|\.tbz2|\.tgz|\.z|\.zip|\.bin|\.dll|\.mis|\.exe))['"][^<>]*?>
            """,
        },
    ]

    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.spider = spider.Spider('http://www.cnbeta.com/backend.php', 
                                                self.templates)
        
    def tearDown(self):
        "Hook method for deconstructing the test fixture after testing it."
        pass
#        
    def test_get_urls(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.spider.walk():
            print url, softinfo
            print downurls
            
#    def test_get_content(self):
#        urls = ['http://www.mydown.com/soft/233/233957.html',
#                'http://www.mydown.com/soft/utilitie/cdmake/32/441032.shtml',
#                ]
#        for url in urls:
#            response = self.mydown_spider.request(url)
#            content = self.mydown_spider.get_content(response)
#            info = self.mydown_spider.get_info(content, self.mydown_templates[1]['info'])
#            for key, val in info.iteritems():
#                print key, ':', val
#            print self.mydown_spider.get_urls(content, self.mydown_templates[1]['downurl'])

if __name__ == '__main__':
    unittest.main()
