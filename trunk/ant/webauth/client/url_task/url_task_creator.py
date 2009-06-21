#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url任务生成客户端"""

import cPickle as pickle
import os
import logging
import shutil
import time

from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import url_task_proxy, url_source_proxy
from datetime import datetime
from webauth.business.priority_logic import url_type2priority
from webauth.config import PathSettings
from webauth.common import log_execption
from webauth.common import urlutil
from webauth.common.constants import UrlFromType, BlackURLCheckType
from webauth.common.single_process import SingleProcess
from webauth.common import json


class UrlTaskCreator(BaseClient):
    def __init__(self):
        BaseClient.__init__(self, 'UrlTaskCreator')
        self.sleep_time = 20 # 1分钟检测一次
        self.status_path = os.path.join(PathSettings.WORKING_FOLDER, "url_task_creator.pkl")
        self.urls = None
        self.__ey_hosts = []
        self.__last_get_ey_hosts_time = None
        
    def before_run(self):
        self.urls = self._get_new_urls()
        begin_time, start_id = self._load_status()
        if not self.urls:
            self.current_info = 'No urls in create time >= %s and id > %s' % (begin_time, start_id)
            return None
        self.current_info = 'Get %s urls create time >= %s and id > %s, id in (%s - %s)' % \
            (len(self.urls), begin_time, start_id, self.urls[0]['id'], self.urls[-1]['id'])
        return self.current_info
    
    @property
    def _ey_hosts(self):
        if self.__last_get_ey_hosts_time is None or \
                (time.time() - self.__last_get_ey_hosts_time) > 1800: # 已获取半小时，则重新获取
            while True:
                try:
                    data = url_source_proxy.get_malicious_hosts()
                    if not data['error'] and data['result']: # 确保恶意库不能为空
                        self.__ey_hosts = data['result']
                        self.__last_get_ey_hosts_time = time.time()
                        break
                except:
                    log_execption()
                self._sleep(10)
        return self.__ey_hosts
        
    def _get_new_urls(self):
        begin_time, start_id = self._load_status()
        print begin_time, start_id
        count = 1000
        while True:
            try:
                data = url_task_proxy.fetch_todo_url(begin_time, start_id, count)
                break
            except:
                log_execption()
                self._sleep(10)
        urls = data['result']
        if not urls:
            return None
        return urls
    
    def run(self):
        begin_time, start_id = self._load_status()
        tasks = []
        ey_hosts = self._ey_hosts
        ey_root_hosts = {}
        no_need_count, bad_count = 0, 0
        for url_data in self.urls:
            from_type = url_data["from_type"]
            priority = url_type2priority(from_type)
            url_data["priority"] = priority
            created_time = url_data['url_created_time']
            if from_type != UrlFromType.RECHECK: # 不存储recheck的id
                if start_id < url_data['id']:
                    start_id = url_data['id']
                if begin_time < created_time:
                    begin_time = created_time
            del url_data['id']
            url_split = urlutil.standardize(url_data['url'])
            if url_split:
                host = url_split.host
                domain = url_split.domain
                # 判断是否是钓鱼url
                if domain in ey_hosts or host in ey_hosts:
#                    url_data['is_fishing'] = True # 钓鱼url
#                    if not url_split.is_host: # 非host级的，不需要
                    url_data['no_need'] = True
                    no_need_count += 1
                    if host not in ey_hosts: # 当前host不存在，则添加，避免漏掉此host
                        ey_root_hosts[host] = ey_hosts[domain] # 拿到ey_type
                tasks.append(url_data)
            else:
                logging.error('bad url: %r' % url_data['url'])
                bad_count += 1
        logging.info('Total: %s, task: %s, bad url: %s, no need: %s, ey host: %s' % \
                    (len(self.urls), len(tasks), bad_count, no_need_count, len(ey_root_hosts)))
        if tasks:
            while True:
                try:
                    data = url_task_proxy.insert_task(tasks)
                    if not data['error']:
                        break
                    self.current_info = data['error']['stack']
                    logging.warning('%r' % data['error'])
                except:
                    log_execption()
                self._sleep(10)
        # 添加有网盾恶意库切割出来的host url
        if ey_root_hosts:
            host_urls = ey_root_hosts.keys()
            logging.info('split %s ey hosts, %s, %s' % \
                        (len(host_urls), host_urls[0], host_urls[-1]))
            while True:
                try:
                    # 添加到url source
                    data = url_source_proxy.add_url(host_urls, UrlFromType.SPLIT_URL)
                    if not data['error']:
                        break
                    self.current_info = data['error']['stack']
                    logging.warning('%r' % data['error'])
                except:
                    log_execption()
                self._sleep(10)
            malicious_hosts = []
            check_type = BlackURLCheckType.HOST_MATCH # host级别
            for host, ey_type in ey_root_hosts.iteritems():
                malicious_hosts.append({'host': host, 
                                        'from_type': UrlFromType.SPLIT_URL, 
                                        'ey_type': ey_type,
                                        'check_type': check_type,
                                        })
            while True:
                try:
                    # 添加到black_url
                    data = url_source_proxy.add_malicious_hosts(malicious_hosts)
                    if not data['error']:
                        effected = data['result'][1]
                        logging.info('add %s ey hosts to black url' % effected)
                        break
                    self.current_info = data['error']['stack']
                    logging.warning('%r' % data['error'])
                except:
                    log_execption()
                self._sleep(10)
        self._save_status(begin_time, start_id)
        self.urls = None
        return 60
    
    def _load_status(self):
        if os.path.exists(self.status_path):
            f = open(self.status_path, "rb")
            try:
                begin_time, start_id = pickle.load(f)
            finally:
                f.close()
        else:
            while True:
                try:
                    data = self.service.get_runtime_data(self.client_type, 0, 'run_status')
                    if not data['error']:
                        if data['result']:
                            begin_time, start_id = json.loads(data['result'])
                        else:
                            begin_time, start_id = datetime(2000, 1, 1), 0
                        break
                except:
                    log_execption()
                self._sleep(10)
        return begin_time, start_id
    
    def _save_status(self, begin_time, start_id):
        backup_path = '%s_old' % self.status_path
        if os.path.exists(self.status_path):
            if os.path.exists(backup_path):
                os.remove(backup_path)
            shutil.copy2(self.status_path, backup_path)
        f = open(self.status_path, "wb")
        try:
            pickle.dump((begin_time, start_id), f, 1)
        finally:
            f.close()
        while True:
            try:
                data_content = json.dumps([begin_time, start_id])
                data = self.service.set_runtime_data(self.client_type, 0, 'run_status', data_content)
                if not data['error']:
                    break
            except:
                log_execption()
            self._sleep(10)

def main():
    mutex_process = SingleProcess("url_task_creator_process")
    mutex_process.check()
    client = UrlTaskCreator()
    client.main()