#!/usr/bin/python
# -*- coding: utf-8 -*-
"""virus spider test
"""

import unittest
from gateway.common_logic import spider

class Spider_test(unittest.TestCase):
    templates = [
        {
         'same_level': ur"""
                 <a[^<>]*?href="([^<>]*?)"[^<>]*?class="pager-next[^<>]*?active"[^<>]*?title="转到下一页">后一页[^<>]*?</a>
            """,
         'next_level': ur"""
                 <h2><a[^<>]*?href="([^<>]*?)"[^<>]*?title="[^<>]*?">[^<>]*?</a></h2>
            """,
        },
        {'info': ur"""
                主页</a></div>[^<>]*?<h2>(?P<name>[^<>]*?)</h2>
        [\s\S]*?
                病毒名称\(中文\):</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<chinese_name>[^<>]*?)</div>
        [\s\S]*?
                病毒别名:</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<alias_name>[^<>]*?)</div>
        [\s\S]*?
        <div[^<>]*?class="field-item-level">[^<>]*?<div[^<>]*?class="field-item">(?P<level>[^<>]*?)</div>
        [\s\S]*?
                病毒类型:</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<category>[^<>]*?)</div>
        [\s\S]*?
                病毒长度:</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<size>[^<>]*?)</div>
        [\s\S]*?
                影响系统:</h5>[^<>]*?<div[^<>]*?class="field-item-platform">[^<>]*?<div[^<>]*?class="field-item">(?P<platform>[^<>]*?)</div>
        (?:[\s\S]*?
                病毒行为:</h5><br[^<>]*?/>[^<>]*?<div[^<>]*?class="content">(?P<behavior>[\s\S]*?)</div>[^<>]*?</div>[^<>]*?<div[^<>]*?class="clear-block\sclear">
        )?
        (?:[\s\S]*?
        <ul[^<>]*?class="links\sinline"[^<>]*?>(?P<tags>[\s\S]*?)</ul>)?
        """
        },
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.spider = spider.Spider('http://vi.duba.net', self.templates)
    
    def test_get_urls(self):
        page_index = 0
        for level, url, parenturl, response, \
                content, downurls, info in self.spider.walk():
            if level==1:
                page_index+= 1
            print page_index, level, url
            if info:
                print info['name']
                if info['tags']:
                    print info['tags'].split('\n')
#                for k, v in info.items():
#                    print k, v

if __name__ == '__main__':
    unittest.main()
