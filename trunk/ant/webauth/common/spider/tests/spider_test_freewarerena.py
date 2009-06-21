#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    
    freewarearena_templates = [
#        {'next_level': ur"""
#        <a[^<>]*?href="(Downloads/c=\d*\.html)">[^<>]*?</a>
#        """,
#        },
#        {'same_level': ur"""
#             <a[^<>]*?href="(Downloads/c=\d*/page=\d*\.html)">\d*</a>
#        """,
#        'next_level': ur"""
#            <a[^<>]*?href="(Downloads/details/id=\d*\.html)">[^<>]*?</a>
#        """,
#        'next_level_callback': u"""
#def next_level_callback(current_url, catch_result):
#    return 'http://freewarearena.com/html/' + catch_result
#""",
#        },
#        {'next_level': ur"""
#            <a[^<>]*?href="(Downloads/get=\d*.html)"[^<>]*?>Download\sNow</a>
#        """,
#        'next_level_callback': u"""
#def next_level_callback(current_url, catch_result):
#    return 'http://freewarearena.com/html/' + catch_result
#""",
#         'info': """
#             <span[^<>]*?><strong[^<>]*?>(?P<name>[^<>]*?)</strong></span>[\s\S]*?<a[^<>]*?>Main\sPage</a>
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>Category</span></td>\s*<td[^<>]*?><a[^<>]*?>(?P<type>[^<>]*?)</a></td>
#             )?
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>Author's\sName</span></td>\s*<td[^<>]*?>(?P<company>[\s\S]*?)</td>
#             )?
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>Homepage</span></td>\s*<td[^<>]*?><a[^<>]*?>(?P<officialweb>[\s\S]*?)</a></td>
#             )?
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>Version</span></td>\s*<td[^<>]*?>(?P<version>[^<>]*?)</td>
#             )?
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>Compatibility</span></td>\s*<td[^<>]*?>(?P<platform>[^<>]*?)</td>
#             )?
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>File\sSize</span></td>\s*<td[^<>]*?>(?P<size>[^<>]*?)</td>
#             )?
#             (?:[\s\S]*?
#             <td[^<>]*?><span[^<>]*?>Last\sUpdated</span></td>\s*<td[^<>]*?><div[^<>]*?>(?P<last_updated>[^<>]*?)</div></td>
#             )?
#             (?:[\s\S]*?
#             <span[^<>]*?><strong[^<>]*?>Description</strong></span><br[^<>]*?/>\s*<div[^<>]*?>(?P<description>[\s\S]*?)</div>
#             )?
#         """,
#        },
#        {'next_level': ur"""
#            <a[^<>]*?href="(Downloads/get=\d*/mirror=\d*\.html)"[^<>]*?>[^<>]*?</a>
#        """,
#        'next_level_callback': u"""
#def next_level_callback(current_url, catch_result):
#    return 'http://freewarearena.com/html/' + catch_result
#""",
#        },
        {'downurl': ur"""
            get\s*it\s*<a[^<>]*?href="([^<>]*?)"[^<>]*?>here</a>
        """
        }
    ]
    
    def setUp(self):
        self.freewarearena_spider = spider.Spider('http://freewarearena.com/html/Downloads.html',
                                                  self.freewarearena_templates)
        
        
    def test_spider(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.freewarearena_spider.walk('http://freewarearena.com/html/Downloads/get=1304/mirror=3180.html'):
            print level, url
            if softinfo:
                for k, v in softinfo.items():
                    print k, v
            print downurls
            
            print content
    
if __name__ == '__main__':
    unittest.main()
