#!/usr/bin/python
# -*- coding: utf-8 -*-
"""http://www.cnproxy.com 代理爬取"""

import re

from dtspider.common.spider import Spider
from dtspider.client.httpproxy.proxy_spider_base import ProxySpiderClient, _res

class CnproxySpider(ProxySpiderClient):
    
    def __init__(self):
        url = u'http://www.cnproxy.com/proxy1.html'
        templates = [
            {
              'proxy': r"<li><a href=\"(proxy.*?\.html)\">" % _res
            }
        ]
        spider = Spider(url, templates)
        super(CnproxySpider, self).__init__(spider)
    
    def _walk(self): # http://www.cnproxy.com/proxy1.html
        def analyse(page):
            tmp = re.findall(r"<tr><td>%(host)s<SCRIPT type=text/javascript>document.write\(\":\"%(portxp)s\)</SCRIPT></td>" % _res, page)
            dictmap = {}
            for a in re.findall("[a-z]=\"[0-9]\";", page):
                dictmap[a[0]] = a[3]
            return [x[0] + ":" + "".join([dictmap[y] for y in x[1].split("+")]) for x in tmp]
        self._get_by_page("http://www.cnproxy.com/proxy1.html",
                        r"<li><a href=\"(proxy.*?\.html)\">",
                        None,
                        analyse)
def main():
    spider = CnproxySpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()
