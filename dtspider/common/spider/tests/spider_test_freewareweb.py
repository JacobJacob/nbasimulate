#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    
    freewareweb_templates = [
        {'next_level': ur"""
        <a[^<>]*?href="([^<>]*?/cgi-bin/[^<>]*?\.cgi\?Category=[^<>]*?)"[^<>]*?>[^<>]*?</a>
        """,
        },
        {'same_level': ur"""
        <a[^<>]*?href="([^<>]*?/cgi-bin/[^<>]*?\.cgi\?Category=[^<>]*?nh=\d*)"[^<>]*?>[^<>]*?</a>
        """,
        'next_level': ur"""
            <a[^<>]*?href="([^<>]*?\.cgi\?ID=\d*)"><font[^<>]*?>Link
        """,
        },
        {'next_level': ur"""
            <B>Download\sNow:</B>[^<>]*?<A[^<>]*?HREF="([^<>]*?)">[^<>]*?</A>
        """,
         'info': """
             <TITLE>FreewareWeb\s*-\s*Freeware:\s*(?P<name>[^<>]*?)\s*-[^<>]*?</TITLE>
             (?:[\s\S]*?
             <B>Homepage[^<>]*?:[^<>]*?</B>\s*<a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>[^<>]*?</a>)?
             (?:[\s\S]*?
             <B>Category[^<>]*?:[^<>]*?</B><A[^<>]*?>(?P<type>[^<>]*?)</a>)?
             (?:[\s\S]*?
             <B>Our\sRating:</B>[^<>]*?<IMG[^<>]*?SRC="/(?P<rate>[\d\.]*).gif"[^<>]*?>)?
             (?:[\s\S]*?
             <B>Description:</B>(?P<description>[\s\S]*?)<p[^<>]*?>[^<>]*?<IMG[^<>]*?SRC="/bluedown.gif"[^<>]*?>)?
             (?:[\s\S]*?
             <B>Download\sNow:</B>\s*<A[^<>]*?>[^<>]*?\[(?P<size>[^<>]*?)\][^<>]*?</A>)?
         """,
        },
        {'downurl': ur"""
            <A[^<>]*?HREF="([^<>]*?)">To\s*Start\s*your
        """
        }
    ]
    
    def setUp(self):
        self.freewareweb_spider = spider.Spider('http://www.freewareweb.com/software.shtml',
                                                self.freewareweb_templates)
        
        
    def test_spider(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.freewareweb_spider.walk():
            print level, url
            if softinfo:
                for k, v in softinfo.items():
                    print k, v
            print downurls
    
if __name__ == '__main__':
    unittest.main()
