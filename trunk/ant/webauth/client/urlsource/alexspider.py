#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Alex爬取"""

import time
from random import randint

from webauth.common.spider import Spider


def main():
    """
    要判断为空的词

category: 该站未被亚马逊分类目录收录
description: 该站点还没有向ALEXA提交任何介绍信息。
author: 不详
country: 不详
charset: 不详
email: 不详
phone: 不详
created_date: 没有记录
speed: 没有记录 Ms      /      没有记录分
address: 不详
    """
    templates = [
        {
          'info': ur'''
在\s*Alexa\s*上综合排名第\s*<b><font[^<>]*?>(?P<rank>.*?)</font></b>\s*位。</div>[\S\s]*?
<td[^<>]*?>站点名称:</td>[\S\s]*?
<td[^<>]*?title="(?P<title>.*?)"[^<>]*?>[\S\s]*?</td>[\S\s]*?
<td[^<>]*?>网站站长:</td>[\S\s]*?
<td[^<>]*?title="(?P<author>.*?)"[^<>]*?>[\S\s]*?</td>[\S\s]*?
<td[^<>]*?>电子信箱:</td>[\S\s]*?
<td[^<>]*?title="(?P<email>.*?)">[\S\s]*?</td>[\S\s]*?
<td[^<>]*?>收录日期:</td>[\S\s]*?
<td[^<>]*?>(?P<created_date>.*?)</td>[\S\s]*?
<td[^<>]*?>所属国家:</td>[\S\s]*?
<td[^<>]*?>(?P<country>.*?)</td>[\S\s]*?
<td[^<>]*?>编码方式:</td>[\S\s]*?
<td[^<>]*?>(?P<charset>.*?)</td>[\S\s]*?
<td[^<>]*?>访问速度:</td>[\S\s]*?
<td[^<>]*?>(?P<speed>.*?)</td>[\S\s]*?
<td[^<>]*?>成人内容:</td>[\S\s]*?
<td[^<>]*?>(?P<child_only>.*?)</td>[\S\s]*?
<td[^<>]*?>反向链接:</td>[\S\s]*?
<td[^<>]*?><a[^<>]*?>(?P<links>.*?)</a>[^<>]*?</td>[\S\s]*?
<td[^<>]*?>联系电话:</td>[\S\s]*?
<td[^<>]*?>(?P<phone>.*?)</td>[\S\s]*?
<td[^<>]*?>详细地址:</td>[\S\s]*?
<td[^<>]*?title="(?P<address>.*?)"[^<>]*?>[\S\s]*?</td>[\S\s]*?
<td[^<>]*?>网站简介:</td>[\S\s]*?
<td[^<>]*?title="(?P<description>.*?)"[^<>]*?>[\S\s]*?</td>[\S\s]*?
<td[^<>]*?>所属目录:</td>[\S\s]*?
<td[^<>]*?title="(?P<category>.*?)"[^<>]*?>[\S\s]*?</td>
          '''
         }
    ]
    url = 'http://alexa.chinaz.com/?Domain='
    spider = Spider(url, templates)
    f = open('hosts.dat', 'rb')
    try:
        for i, host in enumerate(f):
            if i < 10000:
                continue
            q = '%s%s' % (url, host.strip())
            print '-' * 60
            print i, q
            try:
                response = spider.request(q)
                content = spider.get_content(response)
                info = spider.get_info(content, templates[0]['info'])
                print 'Rank:', info.get('rank')
                for k, v in info.items():
                    print u'%s: %s' % (k, v)
            except Exception, e:
                print e
            time.sleep(randint(5, 10))
    finally:
        f.close()
        
if __name__ == '__main__':
    main()