#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    qq_templates = [
        {'interval': 36000, 'same_level': ur"""
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
#        'info': ur"""<div[^<>]*?id="softDetail"[^<>]*?>
#                    [\s\S]*?
#                    <h2\sclass="tit">(?P<name>[^<>]*?)<p><a[^<>]*?>下载地址</a></p></h2>
#                    (?:[\s\S]*?
#                    <th[^<>]*?>软件大小：</th><td[^<>]*?>(?P<size>[^<>]*?)</td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>更新时间：</th><td[^<>]*?>(?P<revised_date>[^<>]*?)</td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>联\s系\s人：</th><td[^<>]*?><font[^<>]*?title="(?P<email>[^<>]*?)">[\s\S]*?</font></td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>下载次数：</th><td[^<>]*?>(?P<downcount>\d*?)</td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>开\s发\s商：</th><td[^<>]*?><a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>[^<>]*?</a></td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>软件类别：</th><td[^<>]*?>(?P<type>[\s\S]*?)</td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>软件语言：</th><td[^<>]*?>(?P<language>[^<>]*?)</td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>应用平台：</th><td[^<>]*><font[^<>]*?title="(?P<platform>[^<>]*?)">[^<>]*?</font></td>)?
#                    (?:[\s\S]*?
#                    <th[^<>]*?>软件性质：</th><td[^<>]*?>(?P<copyright>[^<>]*?)</td>)?
#                    (?:[\s\S]*?
#                    <div[^<>]*?id="intro"[^<>]*?>
#                    [\s]*?
#                    <b>软件介绍：</b>
#                    [\s]*<p>(?P<info>[\s\S]*?)[\s]*</p>[\s]*
#                    <div[^<>]*?id="introMore"[^<>]*?>)?
#            """
        },
    ]
    
    qqgames_templates = [
        {'next_level': ur"""
                 <a[^<>]*?href=["']([^<>]*?)["'][^<>]*?>\s*<img[^<>]*?src="[^<>]*?download_22[^<>]*?"[^<>]*?>
            """,
         'only_downurl': True,
        },
        {'downurl': ur'''href="(http://[^<>]+?(?:\.7z|\.ace|\.arj|\.bz2|\.bzip2|\.cab|\.cpio|\.deb|\.gz|\.gzip|\.iso|\.lha|\.lzh|\.rar|\.rpm|\.split|\.swm|\.tar|\.taz|\.tbz|\.tbz2|\.tgz|\.tpz|\.uue|\.wim|\.z|\.zip|\.xpi|\.wz|\.jar|\.abk|\.bin|\.dll|\.mis|\.exe))"''',
         }
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.qq_spider = spider.Spider('http://www.skycn.com/new/all.html', 
                                                self.qq_templates)
        self.qq_2_spider = spider.Spider('http://pc.qq.com/', 
                                                self.qq_templates)
        self.qqgames_spider = spider.Spider('http://game.qq.com/v10/download.shtml', 
                                                self.qqgames_templates)
        
    def tearDown(self):
        "Hook method for deconstructing the test fixture after testing it."
        pass
#        
#    def test_get_urls(self):
#        for level, url, parenturl, response, \
#                    content, downurls, softinfo in self.qq_spider.walk():
#            print downurls, url, parenturl
#        
#        for level, url, parenturl, response, \
#                    content, downurls, softinfo in self.qq_2_spider.walk():
#            print downurls, url, parenturl
#        
#        for level, url, parenturl, response, \
#                    content, downurls, softinfo in self.qqgames_spider.walk():
#            print url, downurls, parenturl
            
    def test_qqgames(self):
        urls = ['http://x5.qq.com/information/download_khd.htm']
        for url in urls:
            response = self.qqgames_spider.request(url)
            content = self.qqgames_spider.get_content(response)
            print content

if __name__ == '__main__':
    unittest.main()
