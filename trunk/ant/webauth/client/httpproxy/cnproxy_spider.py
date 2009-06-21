#!/usr/bin/python
# -*- coding: utf-8 -*-
"""http://www.5uproxy.net 代理爬取"""

import re

from webauth.common.spider import Spider
from webauth.client.httpproxy.proxy_spider_base import ProxySpiderClient, _res


class CnProxySpider(ProxySpiderClient):
    
    def __init__(self):
        url = u'http://www.5uproxy.net/http_fast.html'
        templates = [
            {
              'proxy': r"#ffffff';\">%(blank)s<td width=\"30\">%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td width=\"60\">%(port)s</td>" % _res
            }
        ]
        spider = Spider(url, templates)
        super(CnProxySpider, self).__init__(spider)
    
    def _walk(self, debug = False): # http://51proxy.net/http_fast.html
        def analyse(page):
            tmp = re.findall(r"<tr><td>%(host)s<SCRIPT type=text/javascript>document.write\(\":\"%(portxp)s\)</SCRIPT></td>" % _res, page)
            # z="3";j="4";r="2";l="9";c="0";x="5";i="7";a="6";p="8";s="1";
            dictmap = {}
            for a in re.findall("[a-z]=\"[0-9]\";", page):
                dictmap[a[0]] = a[3]
            # <tr><td>66.98.152.190<SCRIPT type=text/javascript>document.write(":"+a+a+x+j)</SCRIPT></td>
            return [x[0] + ":" + "".join([dictmap[y] for y in x[1].split("+")]) for x in tmp]
        self._get_by_page("http://www.cnproxy.com/proxy1.html",
                        r"<li><a href=\"(proxy.*?\.html)\">",
                        None,
                        analyse,
                        debug)
def main():
    spider = CnProxySpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()
