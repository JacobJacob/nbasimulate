#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Unit Test for spider.Spider
"""

import unittest
import spider

class SpiderTest(unittest.TestCase):
    """Spider UnitTest Case"""
    
    start = 'http://www.filebuzz.com/'
    template = [
        {'next_level': ur"""
            <img[^<>]*?src="/resources/fbswcat.gif"[^<>]*?>[^<>]*?</td>
            [\s\S]*?
            <td[^<>]*?class="catGroupRef"[^<>]*?><a[^<>]*?href="([^<>]*?)"[^<>]*?>.*?</a></td>
            """,
        },
        {
         'same_level': ur"""
            <a[^<>]*?href="([^<>]*?)"[^<>]*?class="pager"[^<>]*?>.*?</a>
            """,
         'next_level': ur"""
         <div[^<>]*?class="dwnContents"[^<>]*?>[\s\S]*?
         <div[^<>]*?id="links_header"[^<>]*?>[\s\S]*?
         <a[^<>]*?href="([^<>]*?)">.*?</a>
         """,
        },
        {
        'info': ur"""
(?:[\s\S]*?
<img[^<>]*?src="/resources/stars(?P<rate>[^<>]*?).jpg"[^<>]*?>)?
(?:[\s\S]*?
<p[^<>]*?>Program[^<>]*?Name:[^<>]*?<span[^<>]*?>(?P<name>.*?)</span>)?
(?:[\s\S]*?   
<p[^<>]*?>Published[^<>]*?By:[^<>]*?<span[^<>]*?>(?P<author>.*?)</span>)?
(?:[\s\S]*? 
<p[^<>]*?>License[^<>]*?Type:[^<>]*?<span[^<>]*?>(?P<copyright>.*?)</span>)?
(?:[\s\S]*?
<p[^<>]*?>Date[^<>]*?Added:[^<>]*?<span[^<>]*?>(?P<last_updated>.*?)</span>)?
(?:[\s\S]*?  
<a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>Homepage</a>)?
(?:[\s\S]*?  
<h3>.*?Desciption:.*?</h3>
[\s\S]*?
<p[^<>]*?class="publDescr"[^<>]*?>(?P<description>[\s\S]*?)
<p[^<>]*?class="publDescr"[^<>]*?>)?
(?:[\s\S]*?
<span[^<>]*?>[^<>]*?Size:[^<>]*?</span>(?P<size>[^<>]*?)\|)?
(?:[\s\S]*? 
<p[^<>]*?class="publDescr"[^<>]*?><span[^<>]*?>[^<>]*?Platform:[^<>]*?</span>
(?P<platform>[\s\S]*?)
</p>)?
            """,
         'downurl': ur"""
         <a[^<>]*?href="([^<>]*?)"[^<>]*?>Download</a>
        """
        }
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.spider = spider.Spider(self.start, self.template)
    def test_spider(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.spider.walk():
            if softinfo:
                print self.spider.index, level, url, len(downurls), parenturl, downurls
                print softinfo['name'], softinfo
#                for k, v in softinfo.items():
#                    print k, v
            else:
                print self.spider.index, level, url, len(downurls), parenturl

if __name__ == '__main__':
    unittest.main()