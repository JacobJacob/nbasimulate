#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    
    
    
    crsky_templates = [
        {'same_level': ur"""
            <a[^<>]*?href=(\.\./new/0_\d*?\.html)[^<>]*?>\d*?</a>
        """,
         'next_level': ur"""
            <a[^<>]*?href="(../soft/\d*.html)">[^<>]*?</a>
        """,
        },
        {'next_level': ur"""
            <script[^<>]*?src="(/view_down\.asp?[^<>]*?SoftID=\d*[^<>]*?softname=[^<>]*?)"></script>
        """,
         'info': ur"""
             <title>(?P<name>[^<>]*?)\s-\s霏凡软件站</title>
             (?:[\s\S]*?
                <TD[^<>]*?>软件大小</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<size>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>软件类别</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<type>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>运行环境</TD>[\s]*
                <TD[^<>]*?>(?P<platform>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>授权方式</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<copyright>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>软件等级</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*<IMG[^<>]*?src="[^<>]*?images/(?P<rate>[^<>]*?)star.gif"[^<>]*?></TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>软件语言</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<language>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>整理时间</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<last_updated>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>相关连接</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*<a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>下载次数</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<downcount>[^<>]*?)</TD>
             )?
         """,
        },
        {'downurl': ur"""
            <a[^<>]*?href="(http://[^<>]*?\.crsky\.com/view_down.asp\?down_url=[^<>]*?)"[^<>]*?>[^<>]*?</a>
        """
        }
    ]
    
    
    def setUp(self):
        self.crsky_spider = spider.Spider('http://www.crsky.com/new/0_1.html', 
                                          self.crsky_templates)
        
    def test_crsky(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.crsky_spider.walk():
            print level, url
            if softinfo:
                print softinfo['name'], softinfo
            print downurls
    
if __name__ == '__main__':
    unittest.main()
