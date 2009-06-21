#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    brothersoft_templates = [
        {
         'same_level': ur"""
                 <a[^<>]*?
                 href=[\'\"]
                 (/new_software/\d*\.html)
                 [\'\"]
                 [^<>]*?>
                 [^<>]*?
                 </a>
            """,
            'next_level': ur"""
            <dt><strong><a[^<>]*?
            href=[\'\"]([^<>]*?)
            [\'\"]>[^<>]*?</a></strong>[\s\S]*?</dt>
            """
        },
        {
         'next_level': ur"""
            <dd><img[^<>]*?>[^<>]*?<a[^<>]*?href="([^<>]*?)">Download</a>[^<>]*?</dd>
            """,
            'info': ur"""
                <h2[^<>]*?>(?P<name>[^<>]*?)</h2>
                (?:[\s\S]*?
                <dl><dd><img[^<>]*?>\s<a\s[^<>]*?>Download</a>[^<>]*?\((?P<size>[^<>]*?)\)</dd>)?
                (?:[\s\S]*?
                <li><b>Last\sUpdated:</b>[^<>]*?(?P<last_update>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <li><b>OS:</b>[^<>]*?(?P<platform>[^<>]*?)</li>)?
                (?:[\s\S]*?
                <li><b>Publisher:</b>(?P<company>[\s\S]*?)</li>)?
                (?:[\s\S]*?
                <li><b>Homepage:</b>[^<>]*?<a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>[^<>]*?</a></li>)?
                (?:[\s\S]*?
                <h3>[^<>]*?</h3>&nbsp;Description</div><dl[^<>]*?><dt>(?P<description>[\s\S]*?)</dt></dl>)?
            """
        },
        {
         'downurl': ur"""
            <li><img[^<>]*?>[^<>]*?<a[^<>]*?href="([^<>]*?)"[^<>]*?>[^<>]*?</a></li>
        """,
        }
    ]
    
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.brothersoft_spider = spider.Spider('http://www.brothersoft.com/new_software/', 
                                                self.brothersoft_templates)
        
    def tearDown(self):
        "Hook method for deconstructing the test fixture after testing it."
        pass
    
    def test_get_content(self):
        return
        content, info = self.brothersoft_spider.get_webinfo(self.skycn_spider.start_url)
        self.assertTrue(isinstance(content, basestring))
        print info
        
        content, info = self.brothersoft_spider.get_webinfo(self.huajun_spider.start_url)
        self.assertTrue(isinstance(content, basestring))
        print info
#        
    def test_get_urls(self):
        urls = [
                'http://www.brothersoft.com/numerical-converter-183100.html',
                'http://www.brothersoft.com/3d-ocean-sunset-for-mac-x-188402.html',
                ]
        for url in urls:
            response = self.brothersoft_spider.request(url)
            content = self.brothersoft_spider.get_content(response)
            downurls = self.brothersoft_spider.get_urls(content, 
                                               self.brothersoft_templates[1]['next_level'])
            info = self.brothersoft_spider.get_info(content, 
                                                    self.brothersoft_templates[1]['info'])
            for k, v in info.iteritems():
                print k, v
                
            for du in downurls:
                print du
                response = self.brothersoft_spider.request(du)
                content = self.brothersoft_spider.get_content(response)
                print self.brothersoft_spider.get_urls(content, 
                                    self.brothersoft_templates[2]['downurl'])

if __name__ == '__main__':
    unittest.main()
