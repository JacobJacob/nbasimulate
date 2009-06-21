#!/usr/bin/python
# -*- coding: utf-8 -*-
# Description: spider test
# Create at 2008-10-21
# Author: MK2[fengmk2@gmail.com]

import unittest
import spider
import os

class Spider_test_bingdu(unittest.TestCase):
    mydown_templates = [
        {
         'same_level': ur"""
                 <a[^<>]*?href="([^<>]*?)"[^<>]*?class="pager-next[^<>]*?active"[^<>]*?title="转到下一页">后一页[^<>]*?</a>
            """,

         'next_level': ur"""
                 <h2><a[^<>]*?href="([^<>]*?)"[^<>]*?title="[^<>]*?">[^<>]*?</a></h2>
            """,

        },
        {
         'info': ur"""
                主页</a></div>[^<>]*?<h2>(?P<name>[^<>]*?)</h2>
		[\s\S]*?
		病毒名称\(中文\):</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<alias_name_ch>[^<>]*?)</div>
		[\s\S]*?
		病毒别名:</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<alias_name_en>[^<>]*?)</div>
		[\s\S]*?
		<div[^<>]*?class="field-item-level">[^<>]*?<div[^<>]*?class="field-item">(?P<level>[^<>]*?)</div>
		[\s\S]*?
		病毒类型:</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<type>[^<>]*?)</div>
		[\s\S]*?
		病毒长度:</h5>[^<>]*?<div[^<>]*?class="field-items">[^<>]*?<div[^<>]*?class="field-item">(?P<size>[^<>]*?)</div>
		[\s\S]*?
		影响系统:</h5>[^<>]*?<div[^<>]*?class="field-item-platform">[^<>]*?<div[^<>]*?class="field-item">(?P<effect_os>[^<>]*?)</div>
		[\s\S]*?
		病毒行为:</h5><br[^<>]*?/>[^<>]*?<div[^<>]*?class="content">(?P<depict>[\s\S]*?)</div>[^<>]*?</div>[^<>]*?<div[^<>]*?class="clear-block[^<>]*?clear">
            """
        },
    ]

    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
#        self.mydown_spider = spider.Spider('http://vi.duba.net/node', 
#                                                self.mydown_templates)
        self.mydown_spider = spider.Spider('http://vi.duba.net/node?page=2285', 
                                                self.mydown_templates)
        
    def tearDown(self):
        "Hook method for deconstructing the test fixture after testing it."
        pass
#        
    def test_get_urls(self):
	page_index = 0
        data_file = open(ur'd:\virus_info.txt', "ab")
	try:
		for level, url, parenturl, response, \
			    content, downurls, softinfo in self.mydown_spider.walk():
		    if level==1:
			page_index+= 1
		    print page_index, level, url
		    if softinfo:
			data_file.write(str(softinfo) + os.linesep)
	finally:
		data_file.close()
        
if __name__ == '__main__':
    unittest.main()
