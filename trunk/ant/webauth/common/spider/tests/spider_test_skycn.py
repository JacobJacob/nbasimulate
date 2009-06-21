#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    skycntemplates = [
        {'same_level': ur"""
                <a\s[^<>]*            # <a title="...."
                href=[\'\"]            # href="
                (all_\d*.html)        #url
                [\'\"]                #"
                [^<>]*>                # xxxx>
                [^<>]*                #a's innerHTML
                </a>                  #end of a
            """,
         'next_level': ur"""
                 <a\s[^<>]*
                 href=[\'\"]
                 (../soft/\d*.html)
                 [\'\"]
                 [^<>]*>
                 [^<>]*
                 </a>
            """,
        },
        {
         'downurl': ur"""
                <a\s[^<>]*?
                href=[\'\"]
                (http://[^<>]*\.skycn\.com/down/[^<>]*?\.(?:\w*?))        #downurl
                [\'\"]
                [^<>]*?>
                [^<>]*?
                </a>
            """,
        'info': ur"""<div[^<>]*?id="softDetail"[^<>]*?>
                    [\s\S]*?
                    <h2[^<>]*?>(?P<name>[^<>]*?)<p><a[^<>]*?>下载地址</a></p></h2>
                    (?:[\s\S]*?
                    <th[^<>]*?>软件大小：</th><td[^<>]*?>(?P<size>[^<>]*?)</td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>更新时间：</th><td[^<>]*?>(?P<last_updated>[^<>]*?)</td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>联\s系\s人：</th><td[^<>]*?><font[^<>]*?title="(?P<email>[^<>]*?)">[\s\S]*?</font></td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>下载次数：</th><td[^<>]*?>(?P<downcount>\d*?)</td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>开\s发\s商：</th><td[^<>]*?><a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>[^<>]*?</a></td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>软件类别：</th><td[^<>]*?>(?P<type>[\s\S]*?)</td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>软件语言：</th><td[^<>]*?>(?P<language>[^<>]*?)</td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>应用平台：</th><td[^<>]*><font[^<>]*?title="(?P<platform>[^<>]*?)">[^<>]*?</font></td>)?
                    (?:[\s\S]*?
                    <th[^<>]*?>软件性质：</th><td[^<>]*?>(?P<copyright>[^<>]*?)</td>)?
                    (?:[\s\S]*?
                    <div[^<>]*?id="intro"[^<>]*?>
                    [\s]*?
                    <b>软件介绍：</b>
                    [\s]*<p>(?P<description>[\s\S]*?)[\s]*</p>[\s]*
                    <div[^<>]*?id="introMore"[^<>]*?>)?
            """
        },
    ]
    
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.skycn_spider = spider.Spider('http://www.skycn.com/new/all.html', 
                                          self.skycntemplates)
        
    def test_skycn(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.skycn_spider.walk():
            print url
            if softinfo:
                print softinfo['name'], softinfo
            print downurls
    
if __name__ == '__main__':
    unittest.main()
