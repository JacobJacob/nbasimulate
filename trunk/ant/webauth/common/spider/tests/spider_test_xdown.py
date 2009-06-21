#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Unit Test for spider.Spider
"""

import unittest
import spider

class SpiderTest(unittest.TestCase):
    """Spider UnitTest Case"""
    
    xdowns_templates = [
#        {'same_level': ur"""
#                <a[^<>]*?href="(Soft_New\d*\.html)">\d*</a>
#            """,
#        'same_level_finish_callback': u"""
#def same_level_finish_callback(current_url, catch_urls):
#    return ['http://www.xdowns.com/soft/special/Soft_New%03d.html' % i for i in range(1, 16)]
#""",
#         'next_level': ur"""
#                 <a[^<>]*?href=["'](/soft/[^<>]*?/Soft_\d+.html)["'][^<>]*?>
#            """,
#        },
        {
         'next_level': ur"""
                <p[^<>]*?>下载地址</p>[\s\S]*?<a[^<>]*?href="(/soft/softdown\.asp\?softid=\d*)"[^<>]*?>
            """,
        'info': ur"""
            <div[^<>]*?><h1[^<>]*?>(?P<name>.*?)</h1></div>
            (?:[\s\S]*?
            <li>软件大小：(?P<size>.*?)</li>)?
            (?:[\s\S]*?
            <li>软件类型：(?P<type>.*?)</li>)?
            (?:[\s\S]*?
            <li>运行环境：(?P<platform>.*?)</li>)?
            (?:[\s\S]*?
            <li>软件语言：(?P<language>.*?)</li>)?
            (?:[\s\S]*?
            <li>相关链接：<a[^<>]*?href=['"](?P<officialweb>[^<>]*?)['"][^<>]*?>[^<>]*?</a></li>)?
            (?:[\s\S]*?
            <li>更新时间：(?P<last_update>.*?)</li>)?
            (?:[\s\S]*?
            <li>推荐指数：<img[^<>]*?src="/skin/xdowns/(?P<rate>.*?)star.gif".*?></li>)?
            (?:[\s\S]*?
            <div[^<>]*?><p>软件介绍</p>(?:[^<>]*?<SCRIPT.*?></SCRIPT>)?.*?</div>[\s\S]*?
            <P>(?P<description>[\s\S]*?)</P>)?
            """,
        },
        {'downurl': ur"""
            <img[^<>]*?src="/skin/xdowns/icon_downloadserver.gif"[^<>]*?>[^<>]*?
            <a[^<>]*?href="([^<>]*?)"[^<>]*?>[^<>]*?</a>
        """},
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.xdowns_spider = spider.Spider('http://www.xdowns.com/soft/4/25/2009/Soft_49949.html', 
                                          self.xdowns_templates)
    def test_xdowns(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.xdowns_spider.walk():
            print '-' * 60
            print softinfo
            if softinfo:
                print level, url, parenturl, len(downurls)
                print softinfo
                for k, v in softinfo.iteritems():
                    print k, ':', v
            else:
                print level, url, parenturl, downurls

if __name__ == '__main__':
    unittest.main()