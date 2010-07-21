#-*- coding:utf-8 -*-

import re

from common.spider.base import Spider
from common.util import md5mgr
from paitr.paitr import IE
from business import info
from business import type_mapping

class TianyaSpider(object):
    
    def __init__(self, url, templates, max_page=10):
        """
        @param url:the start spider url
        @param templates: a dict contain the spider info
        @param max_page: the max page for look up 
        """
        self._start_url = url
        self._templates = templates
        self._max_page = max_page
        self._spider = Spider()
    def run(self):
        
        current_page = 1
        ie = IE()
        ie.show()
        
        while current_page < self._max_page:
 
            ie.navigate(self._start_url % current_page)
            links = ie.links()
            for link in links:
                if link.href.startswith('http://bbs.city.tianya.cn/new/TianyaCity/content_life.asp'):
                    content = self._spider.read(link.href)
                    info_dict = self._spider.get_info(content, self._templates['info'])
                    info_dict['link'] = link.href
                    info_dict['link_md5'] = md5mgr.mkmd5fromstr(link.href)
                    print info_dict['title']
                    info_dict['type'] = '%s-%s' % (info_dict['type1'], info_dict['type2'])
                    
                    type_mapping.register(info_dict['type'], 'TY-3Y')
                    
                    del info_dict['type1']
                    del info_dict['type2']
                    if not info_dict['title']:
                        continue
                    if info_dict['content']:
                        info_dict['content'] = info_dict['content'].replace('&nbsp;', ' ')
      
                    info.save_info(info_dict)
                    
            
            current_page += 1 
                    
        ie.close()
            
        
def main():
    templates = {
      'info': ur'''
              ([\S\s]*?：<a[\S\s]*?>(?P<type1>[\S\s]*?)</a>—<a[\S\s]*?>(?P<type2>[\S\s]*?)</a>)?
              ([\S\s]*?<title>(?P<title>.*?)</title>)?
              ([\S\s]*?<DIV\s*class=content[^>]*>(?P<content>[\S\s]*?)</DIV>)?
            '''
    }
    spider = TianyaSpider('http://info.tianya.cn/sort/22/65/0/0/0/%s.shtml', templates, 10)
    spider.run()
    
    
if __name__ == '__main__':
    main()