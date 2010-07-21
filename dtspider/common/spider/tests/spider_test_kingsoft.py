#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    kingsoft_templates = [
        {'next_level': ur"""
                 <a[^<>]*?href=["']([^<>]*?down[^<>]*?)["'][^<>]*?>
            """,
         'only_downurl': True,
        },
        {'downurl': ur'''href="([^<>]+?(?:\.7z|\.ace|\.arj|\.bz2|\.bzip2|\.cab|\.cpio|\.deb|\.gz|\.gzip|\.iso|\.lha|\.lzh|\.rar|\.rpm|\.split|\.swm|\.tar|\.taz|\.tbz|\.tbz2|\.tgz|\.tpz|\.uue|\.wim|\.z|\.zip|\.xpi|\.wz|\.jar|\.abk|\.bin|\.dll|\.mis|\.exe))"''',
         }
    ]
    
    kingsoft_games_templates = [
        {'next_level': ur"""
                 <a[^<>]*?href=["']([^<>]*?)["'][^<>]*?>客户端下载</a>
            """,
         'only_downurl': True,
        },
        {'downurl': ur'''href="([^<>]+?(?:\.7z|\.ace|\.arj|\.bz2|\.bzip2|\.cab|\.cpio|\.deb|\.gz|\.gzip|\.iso|\.lha|\.lzh|\.rar|\.rpm|\.split|\.swm|\.tar|\.taz|\.tbz|\.tbz2|\.tgz|\.tpz|\.uue|\.wim|\.z|\.zip|\.xpi|\.wz|\.jar|\.abk|\.bin|\.dll|\.mis|\.exe))"''',
         }
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.kingsoft_spider = spider.Spider('http://www.kingsoft.com/download/', 
                                                self.kingsoft_templates)
        self.kingsoft_game_spider = spider.Spider('http://www.kingsoft.com/game/', 
                                                self.kingsoft_games_templates)
        
    def tearDown(self):
        "Hook method for deconstructing the test fixture after testing it."
        pass
#        
    def test_get_urls(self):
#        for level, url, parenturl, response, \
#                    content, downurls, softinfo in self.kingsoft_spider.walk():
#            print url, downurls, parenturl
        
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.kingsoft_game_spider.walk():
            print url, downurls, parenturl
            
#    def test_qqgames(self):
#        urls = ['http://x5.qq.com/information/download_khd.htm']
#        for url in urls:
#            response = self.qqgames_spider.request(url)
#            content = self.qqgames_spider.get_content(response)
#            print content

if __name__ == '__main__':
    unittest.main()
