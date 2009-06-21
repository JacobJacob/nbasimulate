#!/usr/bin/python
# -*- coding: utf-8 -*-
"""http://www.5uproxy.net 代理爬取"""

from webauth.common.spider import Spider
from webauth.client.httpproxy.proxy_spider_base import ProxySpiderClient, _res


class UproxySpider(ProxySpiderClient):
    
    def __init__(self):
        url = u'http://www.5uproxy.net/http_fast.html'
        templates = [
            {
              'proxy': r"#ffffff';\">%(blank)s<td width=\"30\">%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td width=\"60\">%(port)s</td>" % _res
            }
        ]
        spider = Spider(url, templates)
        super(UproxySpider, self).__init__(spider)
    
    def _walk(self): # http://51proxy.net/http_fast.html
        self._get_by_page("http://51proxy.net/http_fast.html", 
                        r"<li><a href=\"(.*?\.html)\"><span>", 
                        r"#ffffff';\">%(blank)s<td width=\"30\">%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td width=\"60\">%(port)s</td>" % _res, 
                        lambda tmp: [":".join(x) for x in tmp])
def main():
    spider = UproxySpider()
    spider.main()
    
            
if __name__ == '__main__':
    main()
