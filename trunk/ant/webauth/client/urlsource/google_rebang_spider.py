#!/usr/bin/python
# -*- coding: utf-8 -*-
"""google热榜"""

import urllib
import re

from webauth.common.spider import Spider
from webauth.client.urlsource.url_spider_base import UrlSpiderClient
from webauth.common.constants import UrlFromType


class GoogleSpider(Spider):
    """
<script type="text/javascript">
        var queries = [         ["成都公交车燃烧视频","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000010892"],                   ["深圳市长许宗衡","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000010963"],                   ["范志毅李倩结婚照","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000034471"],                   ["2009年高考数学试卷","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000008941"],                   ["深圳市长双规","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000010963"],                   ["sarah ziff","1","","",""],                   ["深圳市长 双规","0","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000010963"],                   ["范志毅离婚原因","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000022595"],                                                              ["王雅莉","1","","",""],                   ["依依女郎","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000029052"],                   ["深圳市长被双规","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000010963"],                   ["阎小培","-1","","",""],                   ["高考试题","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000008941"],                   ["彩仙幽欲网","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000016878"],                   ["nba总决赛在线观看","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000000068"],                   ["许宗衡青岛市委书记","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000003601"],                   ["哈娜照片","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000008957"],                   ["王建民 棒球","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000011699"],                   ["verycd","1","","",""],                   ["600562","1","","",""],                   ["追悼会的程序","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000015986"],                   ["600493","-1","","",""],                   ["潘源良","-1","","",""],                   ["商人王雅莉","1","","",""],                   ["不可无一 不可有二","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000018794"],                   ["财政部网站","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000017648"],                                                                                                                             ["我认为中国足球是有希望的","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000005255"],                   ["福气又安康","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000009123"],                   ["2009江苏高考数学","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000008941"],                   ["泸州老窖","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000009772"],                   ["双规","-1","","",""],                   ["佳吉快运货物查询","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000109667"],                   ["板桥体","1","","",""],                   ["广电电子","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000000313"],                   ["许忠衡","-1","","",""],                   ["許宗衡","1","","",""],                   ["中国平安停牌","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000006047"],                   ["连城读书","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000012675"],                   ["李茏怡","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000013125"],                   ["徐宗衡","-1","","",""],                   ["范志毅前妻照片","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000008957"],                   ["600602","-1","","",""],                   ["2009高考","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000008941"],                   ["北京副市长吉林简历","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000015244"],                   ["许中衡","1","","",""],                   ["600637","1","","",""],                   ["总务二科","-1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000109990"],                   ["美女主播情妇李泳","1","","","http://laiba.tianya.cn/laiba/Community?cmm\x3d0000000442"],         ];
        var linktexts = 
        
        
    to http://www.google.cn/search?q=%E8%B4%A2%E6%94%BF%E9%83%A8%E7%BD%91%E7%AB%99&hl=zh-CN&source=billboard_site&cd=12000000&ie=utf8
    """
    
    def get_next_level_urls(self, content, express, callback=None):
        url_pattern = re.compile(express, re.I | re.VERBOSE)
        match = url_pattern.search(content)
        urls = []
        if match:
            queries = match.group(1).strip().split('],')
            for query in queries:
                query = query.strip()
                if query:
                    key = query.split(',', 1)[0][2:-1]
                    if isinstance(key, unicode):
                        key = key.encode('utf-8')
                    url = 'http://www.google.cn/search?q=%s&hl=zh-CN&source=billboard_site&cd=12000000&ie=utf8&num=100' % urllib.quote(key)
                    urls.append(url)
        return urls
        

class GoogleRebangSpider(UrlSpiderClient):
    
    def __init__(self):
        url = u'http://www.google.cn/rebang/detail?bid=12000000'
        
        """
        <h3 class=r><a href="http://vip.v.ifeng.com/news/huawendazhibo/200906/355ad971-6545-43d5-81f0-cc129f76802cdetail.shtml" target=_blank class=l onmousedown="return clk(0,'','','res','16','')">2009-06-07 华闻大直播- <em>视频</em>：<em>成都公交车燃烧</em>全过程- 凤凰宽频- 凤凰 <b>...</b></a></h3>
        
        """
        templates = [
            {'next_level': ur"""
<script[^<>]*?type="text/javascript"[^<>]*?>[\S\s]*?
        var\s*?queries\s*?=\s*?\[(.*?)\];
        [\S\s]*?
        var\s*?linktexts
                """,
            },
            {'downurl': ur'''
                <h3[^<>]*?class=r[^<>]*?><a[^<>]*?href="(.*?)"[^<>]*?>[\S\s]*?</a></h3>
                ''',
            'info': ur'''
            <input[^<>]*?name=q[^<>]*?value="(?P<keyword>.*?)"[^<>]*?>
              '''
             }
        ]
        spider = GoogleSpider(url, templates)
        super(GoogleRebangSpider, self).__init__(UrlFromType.GOOGLE_REBANG_URL, spider)
        self._exclude_words = ['http://news.google.'] # 'http://video.google', 'http://blogsearch.google'

def main():
    spider = GoogleRebangSpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()