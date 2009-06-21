#!/usr/bin/python
# -*- coding: utf-8 -*-
"""alexa 信息爬取客户端"""

import urllib2
import time
import datetime
import random
import logging

from webauth.client.domain_info.domain_spider_base import DomainSpiderBase
from webauth.common.constants import ClientType
from webauth.common.spider.proxy_spider import ProxySpider

class AlexaSpider(DomainSpiderBase):
    
    def __init__(self):
        url = 'http://data.alexa.com/data?cli=10&dat=snbamz&url='
        templates = [
            {
              'info': ur'''
                ([\S\s]*?<TITLE\s*TEXT="(?P<title1>.*?)"\s*/>)?
                ([\S\s]*?<ADDR\s*STREET="(?P<street>.*?)"\s*CITY="(?P<city>.*?)"\s*STATE="(?P<state>.*?)"\s*ZIP="(?P<zip>.*?)"\s*COUNTRY="(?P<country>.*?)"\s*/>)?
                ([\S\s]*?<CREATED\s*DATE=".*"\s*DAY="(?P<day>.*?)"\s*MONTH="(?P<month>.*?)"\s*YEAR="(?P<year>.*?)"\s*/>)?
                ([\S\s]*?<PHONE\s*NUMBER="(?P<phone>.*?)"\s*/>)?
                ([\S\s]*?<OWNER\s*NAME="(?P<owner>.*?)"\s*/>)?
                ([\S\s]*?<EMAIL\s*ADDR="(?P<email>.*?)"\s*/>)?
                ([\S\s]*?<LANG\s*LEX="(?P<language>.*?)"\s*CODE="(?P<encode>.*?)"\s*/>)?
                ([\S\s]*?<LINKSIN\s*NUM="(?P<links>.*?)"\s*/>)?
                ([\S\s]*?<SPEED\s*TEXT="(?P<speed>.*?)"\s*PCT="(?P<pct>.*?)"\s*/>)?
                ([\S\s]*?<POPULARITY\s*URL=".*/"\s*TEXT="(?P<rank>.*?)"\s*/>)?
                ([\S\s]*?<CHILD\s*SRATING="(?P<child>.*?)"\s*/>)?
                ([\S\s]*?<REACH\s*RANK="(?P<reach>.*?)"\s*/>)? 
                ([\S\s]*?<SITE\s*BASE=".*"\s*TITLE="(?P<title>.*?)"\s*DESC="(?P<description>.*?)">)?
                ([\S\s]*?<CAT\s*ID="(?P<category>.*?)"\s*TITLE=".*"\s*CID=".*"\s*/>)?
              '''
             }
        ]
        self._proxy = None 
        self._ask_proxy()
        spider = ProxySpider(url, templates, self._proxy)
        super(AlexaSpider, self).__init__(ClientType.ALEXA_SPIDER, spider)
    
    def run(self):
        """程序主体部分，不断得到domain并爬取alexa信息
        """
        task_cnt = 0
        while True:
            domain_infos = self._fetch_domain('ALEXA')
            for domain_info in domain_infos:
                self._get_info(domain_info)
                task_cnt += 1
                if task_cnt % 10 == 0:#每处理10条打印一次日志
                    logging.info('finish %s task' % task_cnt)
                time.sleep(random.randint(5, 10))#延迟
            self._update_domain(domain_infos)
        
    def _get_info(self, domain_info):
        """得到域名的alexa信息
        @param domain_info:封装了域名信息的字典
        @return: 字典，封装爬取到的域名信息 
        """
        url = '%s%s' % (self._spider.start_url, domain_info['domain'].strip())
        try:
            response = self.request(url)
            if response == DomainSpiderBase.FORBIDDEN:
                domain_info['alexa_rank'] = -1
                return domain_info
            content = self._spider.get_content(response)
        except:
            domain_info['alexa_rank'] = -1
            return domain_info
        info = self._spider.get_info(content, self._spider.templates[0]['info'])
        if info:
            domain_info['alexa_title'] = info['title'] if info['title'] else info['title1']
            domain_info['alexa_desc'] = info['description']
            domain_info['alexa_cat'] = info['category']
            domain_info['alexa_owner'] = info['owner']
            domain_info['alexa_email'] = info['email']
            domain_info['alexa_country'] = info['country']
            domain_info['alexa_linksin'] = info['links']
            domain_info['alexa_speed'] = info['speed']
            domain_info['alexa_pct'] = info['pct']
            domain_info['alexa_child'] = info['child']
            domain_info['alexa_rank'] = info['rank']
            domain_info['alexa_encode'] = info['encode']
            domain_info['alexa_phone'] = info['phone']
            domain_info['alexa_reach'] = info['reach']
            
            if not info['country']:
                info['country'] = ''
            if not info['zip']:
                info['zip'] = ''
            if not info['state']:
                info['state'] = ''
            if not info['city']:
                info['city'] = ''
            if not info['street']:
                info['street'] = ''
                
            domain_info['alexa_addr'] = '%s %s %s %s %s' % (info['street'], info['city'], \
                                                info['state'], info['zip'] ,info['country'])
            if info['year'] and info['month'] and info['day']:
                year = int(info['year'])
                if year < 10:
                    year = 2000 + year
                elif year < 99:
                    year = 1900 + year
                month = int(info['month'])
                day = int(info['day'])
                domain_info['alexa_created'] = datetime.datetime(year, month, day)
            else:
                domain_info['alexa_created'] = None  
        else:
            domain_info['alexa_rank'] = -1
        return domain_info
    
def main():
    spider = AlexaSpider()
    spider.main()
     
if __name__ == '__main__':
    main()
            
