#!/usr/bin/python
# -*- coding: utf-8 -*-
"""网关数据拉回

访问超过>=10的有 163275个
访问超过>=100的有 6k个
"""

import os
import logging

from webauth.config import DATA_SYNC_HOST, PathSettings
from webauth.common.ftp_lib import FtpEx as FTP
from webauth.common.file_utility import ensure_dir_exists
from webauth.common.constants import ClientType, UrlFromType
from webauth.common import log_execption
from webauth.common import urlutil
from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import url_source_proxy


class GatewayDataSpider(BaseClient):
    
    def __init__(self):
        super(GatewayDataSpider, self).__init__(ClientType.GATEWAY_DATA_SPIDER)
        # http://trac.rdev.kingsoft.net/mercury/wiki/gateway-internal-data-exchange-center
        self._ftp = FTP(DATA_SYNC_HOST, 'date_mining', 'date_mi@n1i2n3g')
        self._start_path = '/gateway/access'
        self.sleep_time = 60
        self._local_folder = os.path.join(PathSettings.WORKING_FOLDER, 'gateway_data_spider')
        ensure_dir_exists(self._local_folder)
        
    def run(self):
        self._black_url_count = 0
        self._hot_url_count = 0
        self._current_deal_file = None
        self._ftp.ensure_login()
        for root, subs, filepaths in self._ftp.walk(self._start_path):
            for path in filepaths:
                if path.endswith('.done'):
                    self._deal_file(path)
        return 3600 # 一小时拉一次
    
    def _fixed_charset(self, url):
        try:
            url = url.decode('gb2312').encode('utf-8')
        except:
            try:
                url = url.decode('utf-8').encode('utf-8')
            except:
                url = None
        return url
    
    WHITE_HOSTS = set(['item.taobao.com', 'laiba.tianya.cn', 'laiba.cga.com.cn'])
    
    def _deal_file(self, remote_path):
        logging.info('deal %s' % remote_path)
        self._current_deal_file = remote_path
        local_path = os.path.join(self._local_folder, os.path.basename(remote_path))
        self._ftp.download(remote_path, local_path, False)
        hot_urls, black_urls = [], []
        f = open(local_path, 'rb')
        try:
            for line in f:
                line = line.strip()
                if not line:
                    continue
                datas = line.split('\t') # count, url, type
                if len(datas) != 3:
                    continue
                count = int(datas[0])
                if count < 10: # 访问次数少于10次的不要
                    continue
                if count >= 100 or datas[2] in ('3', '4'):
                    url = self._fixed_charset(datas[1])
                    if url is None:
                        continue
                    url_split = urlutil.standardize(url)
                    if url_split is None:
                        continue
                    info = {'url': url,
                            'rank': count}
                    if datas[2] in ('3', '4'):
                        if url_split.host in self.WHITE_HOSTS: # 过滤白名单
                            continue
                        info['keyword'] = 'type:%s' % datas[2] 
                        black_urls.append(info)
                        self._black_url_count += 1
                        if len(black_urls) >= 1000:
                            self._add_urls(black_urls, UrlFromType.GATEWAY_WARNING_URL)
                            black_urls = []
                    else:
                        info['keyword'] = 'hit:%s' % count 
                        hot_urls.append(info)
                        self._hot_url_count += 1
                        if len(hot_urls) >= 1000:
                            self._add_urls(hot_urls, UrlFromType.GATEWAY_HOT_URL)
                            hot_urls = []
            if black_urls:
                self._add_urls(black_urls, UrlFromType.GATEWAY_WARNING_URL)
            if hot_urls:
                self._add_urls(hot_urls, UrlFromType.GATEWAY_HOT_URL) 
        finally:
            f.close()
        os.remove(local_path)
        while True: # 保证已处理的数据已被删除
            try:
                self._ftp.ensure_login()
                self._ftp.delete(remote_path)
                break
            except Exception, e:
                if 'No such file or directory' in e.message: # 删除成功
                    break
                log_execption()
                self._sleep(10)
                
    def _add_urls(self, urls, from_type):
        self.current_info = 'deal with %s, get %s black urls, %s hot urls' % \
            (self._current_deal_file, self._black_url_count, self._hot_url_count)
        logging.info(self.current_info)
        while True:
            try:
                data = url_source_proxy.add_hot_urls(urls, from_type)
                if not data['error']: # 保存成功，则返回
                    break
                self.current_info = data['error']['stack']
                logging.warning(self.current_info)
            except:
                log_execption()
            self._sleep()
    

def main():
    spider = GatewayDataSpider()
    spider.main()