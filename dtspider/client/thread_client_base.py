#!/usr/bin/python
#-*- coding:utf-8 -*-

import re
from datetime import datetime
import time
import logging

from dtspider.client.base import BaseClient
from dtspider.common.constants import ClientType
from dtspider.common.spider.base import Spider
from dtspider.common import md5mgr
from dtspider.common.single_process import SingleProcess
from dtspider.business import zhuhai_info
from dtspider.business import type_mapping
from dtspider.business import spider_log

import sys
reload(sys)
sys.setdefaultencoding('utf-8')


class ZhuhaiSpider(BaseClient):
    
    def __init__(self, url, templates, max_page=10):
        """
        @param url:the start spider url
        @param templates: a dict contain the spider info
        @param max_page: the max page for look up 
        """
        super(ZhuhaiSpider, self).__init__(ClientType.ZHUHAI_SPIDER)
        self._start_url = url
        self._templates = templates
        self._max_page = max_page
        self._spider = Spider()
        
    def run(self):
        
        now = datetime.now()
        self.current_info = '%s:start running' % now.strftime("%Y-%m-%d %H:%M:%S")
        logging.info(self.current_info)
        current_page = 1

        expire_msg_count = 0 #统计过期日期，如果超过三条就不再爬取
        total_info = 0
        total_new_info = 0
        old_msg_count = 0
        
        while current_page < self._max_page:
 
            content = self._spider.read(self._start_url % current_page)
            links = self._spider.get_urls(content, self._templates['url'])
            if not links:
                links = []
            for link in links:
                url = 'http://mark.zhuhai.gd.cn/%s' % link
                info = self._get_info(url)

                if not info['title']:#标题为空的信息跳过
                    continue
                self.current_info = u'url:%s,title:%s' % (url, info['title'])
                logging.info(self.current_info)
                
                now = datetime.now()
                if info['issue_date']:
                    issue_date = '%s-%s' % (datetime.now().year, info['issue_date'])
                    info['issue_date'] = datetime.strptime(issue_date, '%Y-%m-%d %H:%M')
                else:
                    continue
                    
                issue_date = info['issue_date']
                #不是当天的信息不处理
                if now.year == issue_date.year and now.month == issue_date.month \
                                    and now.day - 1 == issue_date.day:
                    pass
                else:
                    expire_msg_count += 1
                    if expire_msg_count >= 3:
                        current_page = self._max_page
                        now = datetime.now()
                        self.current_info = '%s:sleep 180s' % now.strftime("%Y-%m-%d %H:%M:%S")
                        logging.info(self.current_info)
                        return 180
                
                info['type'] = '%s-%s' % (info['type1'], info['type2'])
                
                #注册类别
                type_mapping.register(info['type'], 'ZH-ZH')
                
                info['url'] = url
                info['url_md5'] = md5mgr.mkmd5fromstr(info['url'])
                try:
                    info['price'] = int(info['price'])
                    if info['price'] > 10000000:
                        info['price'] = -1
                except Exception, e:
                    info['price'] = -1 #-1表示面议
                
                if info['effect_date']:
                    if info['effect_date'] == u'长期有效' :
                        info['effect_date'] = datetime(2099, 1, 1)
                    else:
                        info['effect_date'] = datetime.strptime(info['effect_date'], '%Y-%m-%d')
                
                del info['type1']
                del info['type2']
                total_info += 1
                last_id, effect_rows = self._save_info(info)
                if effect_rows == 0:
                    old_msg_count += 1 #如果连续10条信息存在就退出
                    if old_msg_count > 10:
                        old_msg_count = 0
                        now = datetime.now()
                        self.current_info = '%s:old msg > 10 break, sleep 180s' % now.strftime("%Y-%m-%d %H:%M:%S")
                        logging.info(self.current_info)
                        return 180
                else:
                    old_msg_count = 0
                    
                total_new_info += effect_rows
            
            current_page += 1 
        print 'total info:%s;new info:%s' % (total_info, total_new_info)
        log_info = {}
        log_info['log_time'] = datetime.now()
        log_info['msg'] = 'spider total info:%s;new info:%s' % (total_info, total_new_info)
        log_info['program'] = self.__class__.__name__
        spider_log.log(log_info)
        
        now = datetime.now()
        self.current_info = '%s:sleep 180s' % now.strftime("%Y-%m-%d %H:%M:%S")
        return 180
    def _save_info(self, info):
        """save info"""
        while True:
            try:
                return zhuhai_info.save_info(info)
            except:
                self.current_info = u'%s' % traceback.format_exc()
            self._sleep()
                  
    def _get_info(self, url):
        
        content = self._spider.read(url)
        info = self._spider.get_info(content, self._templates['info'])
        return info
    
def main():
    templates = {
      'url': ur"""<A[\s]*?HREF='(infoView\.aspx\?infoId=\d+)'""",
      'info': ur"""
              ([\S\s]*?<DIV[\s]*?ALIGN="center"><STRONG>(?P<title>[\S\s]*?)</STRONG>[\S\s]*?</DIV>)?
              ([\S\s]*?<A[\s]*?HREF=.*?CLASS="heis">(?P<type1>[\S\s]*?)</A>[\S\s]*?<SPAN[\s]*?CLASS="heis">(?P<type2>[\S\s]*?)</SPAN>)?
              ([\S\s]*?<TD[\s]*?CLASS="indetail">(?P<price>[\S\s]*?)</TD>)?
              ([\S\s]*?<TD[\s]*?CLASS="indetail">(?P<issuer>.*?)&nbsp;&nbsp;<IMG)?
              ([\S\s]*?<TD[\s]*?CLASS="indetail">(?P<area>.*)</TD>)?
              ([\S\s]*?<TD[\s]*CLASS="indetail">电话：(?P<phone>.*?)&nbsp;&nbsp;&nbsp;&nbsp;电邮：(?P<email>.*?)</TD>)?
              ([\S\s]*?[>](?P<issue_date>.*?)</ACRONYM>)?
              ([\S\s]*?>(?P<effect_date>.*?)</ACRONYM>)?
              ([\S\s]*?<td>(?P<content>[\S\s]*?)</td>)?
            """
    }
    #房屋出租信息
    spider = ZhuhaiSpider('http://mark.zhuhai.gd.cn/classInfo.aspx?page=%s', templates, 100)
    spider.main()
    
    
if __name__ == '__main__':
    single = SingleProcess('zhuhai spider')
    single.check()
    main()
