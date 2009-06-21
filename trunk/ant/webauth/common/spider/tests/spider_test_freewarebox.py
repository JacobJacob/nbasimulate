#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    
    freewarebox_templates = [
        {'same_level': ur"""
            <a[^<>]*?href="(browse_\w_freeware\.html)"[^<>]*?>\w*</a>
        """,
         'next_level': ur"""
            <p[^<>]*?><a[^<>]*?href="(free_\d*_[^<>]*?-download.html)"[^<>]*?>[^<>]*?</a>[\s\S]*?</p>
        """,
        },
        {'next_level': ur"""
            <p[^<>]*?><strong>Download\spage\s:\s</strong><a[^<>]*?href="([^<>]*?)"[^<>]*?>Download</a></p>
        """,
         'info': """
             <div[^<>]*?id="softview1">\s*?
             <h1><span[^<>]*?>(?P<name>[\s\S]*?)</span>[^<>]*?<span[^<>]*?>Freeware\sVersion\s:\s(?P<version>[^<>]*?)</span></h1>
             (?:[\s\S]*?
             <p[^<>]*?><strong>Author\s:\s</strong>(?P<company>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>OS\s:\s</strong>(?P<platform>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>[^<>]*?license\s:\s</strong>(?P<copyright>[^<>]*?)\s*(?:\([\s\S]*?\))?</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>Added\sdate\s:\s</strong>(?P<last_updated>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>Download\sSize\s:\s</strong>(?P<size>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>Freeware\sdescription\s:\s</strong>(?P<description>[\s\S]*?)</p>)?
         """,
        },
        {'downurl': ur"""
            <a[^<>]*?href="([^<>]*?)"[^<>]*?>Download\shere</a>
        """
        }
    ]
    
    def setUp(self):
        self.freewarebox_spider = spider.Spider('http://www.freewarebox.com/browse_a_freeware.html', 
                                                self.freewarebox_templates)
        
        
        
    def test_spider(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.freewarebox_spider.walk():
            print level, url
            if softinfo:
                for k, v in softinfo.items():
                    print k, v
            print downurls
    
if __name__ == '__main__':
    unittest.main()
