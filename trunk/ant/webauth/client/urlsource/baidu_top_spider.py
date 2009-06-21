#!/usr/bin/python
# -*- coding: utf-8 -*-
"""baidu搜索风云榜"""

from webauth.common.spider import Spider
from webauth.client.urlsource.url_spider_base import UrlSpiderClient
from webauth.common.constants import UrlFromType


class BaiduSpider(Spider):
    """增加每页返回的记录数"""
    
    def next_level_finish_callback(self, current_url, next_level_urls):
        return [u'%s&rn=100' % u for u in next_level_urls] # 每页返回100条记录


class BaiduTopSpider(UrlSpiderClient):
    
    def __init__(self):
        url = u'http://top.baidu.com/buzz/top10.html'
        templates = [
            {'next_level': ur"""
                <td>[\S\s]*?
                <a[^<>]*?href=["'](http://www.baidu.com/baidu[^<>]*?)["'][^<>]*?>
                [\S\s]*?</td>
                """,
            },
            {'downurl': ur'''
                <td[^<>]*?class=f[^<>]*?>[\S\s]*?
                <a[^<>]*?href=["'](http://.*?)["'][^<>]*?>''',
              'info': ur'''
              <input\s+name=wd\s+id=kw[^<>]*?value="(?P<keyword>.*?)"[^<>]*?>
              '''
             }
        ]
        spider = BaiduSpider(url, templates)
        super(BaiduTopSpider, self).__init__(UrlFromType.BAIDU_TOP_URL, spider)
        self._exclude_words = ['http://www.baidu.']
            

def main():
    spider = BaiduTopSpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()