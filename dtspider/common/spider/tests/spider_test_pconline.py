#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    
    pconlinemplates = [
    {
     'next_level': ur"""
             <span\sclass=\"top_name\">
             <a[^<>]*?
             href=[\'\"]
             (http://dl\.pconline\.com\.cn/[^<>]*?.html)
             [\'\"]
             [^<>]*?>
             [\S\s]*?
             </a>
        """,
    },
    {
     'next_level': ur"""
            var\surl\s=\sdocument.URL;\s*?
            url\s=\surl.replace\(\"(.*?)\",\"(.*?)\"\)
        """,
     'next_level_callback': u"""
def next_level_callback(current_url, catch_result):
    return current_url.replace(catch_result[0], catch_result[1])
""",
        'info': ur"""
            <span[^<>]*?>软件名称</span><h1>(?P<name>.*?)</h1>
            [\s\S]*?
            <span[^<>]*?>官方网站</span><span[^<>]*?><a[^<>]*?href=['"](?P<officialweb>[^<>]*?)['"][^<>]*?>[^<>]*?</a></span>
            [\s\S]*?
            <span[^<>]*?>软件大小</span><span[^<>]*?>(?P<size>.*?)</span>
            [\s\S]*?
            <span[^<>]*?>整理日期</span><span[^<>]*?>(?P<last_updated>.*?)</span>
            [\s\S]*?
            <span[^<>]*?>软件授权</span><span[^<>]*?>(?P<copyright>.*?)</span>
            [\s\S]*?
            <span[^<>]*?>软件平台</span><span[^<>]*?>(?P<platform>.*?)</span>
            [\s\S]*?
            <span[^<>]*?>关\s键\s字</span><span[^<>]*?>(?P<keywords>.*?)</span>
            [\s\S]*?
             <div[^<>]*?pContent[^<>]*?>(?P<description>[\s\S]*?)</div>
        """
    },
    {
     'downurl': ur"""
        <dd><a[^<>]*?
        href=\'
        (http://[^<>]*?/filedown\.jsp[^<>]*?)
        \'>[^<>]*?</a></dd>
    """,
    }
]
    
    
    def setUp(self):
        self.pconline_spider = spider.Spider('http://dl.pconline.com.cn/soft_update.html', 
                                             self.pconlinemplates)
        
    def test_spider(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.pconline_spider.walk():
            print level, url
            if softinfo:
                for k, v in softinfo.items():
                    print k, v
            print downurls
    

if __name__ == '__main__':
    unittest.main()
