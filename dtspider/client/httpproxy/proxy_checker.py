#!/usr/bin/python
# -*- coding: utf-8 -*-
"""proxy 验证客户端,定时对数据库中的代理进行验证，删除无效代理"""

import logging
from datetime import datetime

from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import proxy_service_proxy
from webauth.common.constants import ClientType
from webauth.common import log_execption
from webauth.common import proxyutil 


class ProxyCheckerClient(BaseClient):
    
    def __init__(self, round_time=172800):
        super(ProxyCheckerClient, self).__init__(ClientType.PROXY_SPIDER)
        self._round_time = round_time # 每两天运行一次
    
    def _get_all_proxys(self):
        """得到所有代理列表""" 
        while True:
            try:
                data = proxy_service_proxy.get_all_proxys()
                return data
            except:
                log_execption()
            self._sleep()
    
    def _update_proxys(self, proxy_infos):
        """更新代理
        @param proxy_infos:列表型，每个元素为一个字典，代表一个要更新的代理 
        """
        while True:
            try:
                proxy_service_proxy.add_proxys(proxy_infos)
                break
            except:
                log_execption()
            self._sleep()
    
    def _delete_proxys(self, proxy_infos):
        """删除代理
        @param proxy_infos:列表型，每个元素为一个字典，代表一个要删除的代理 
        """
        while True:
            try:
                proxy_service_proxy.delete_proxys(proxy_infos)
                break
            except:
                log_execption()
            self._sleep()
                
    def run(self):
        proxy_infos = self._get_all_proxys()
        i = 0 
        update_proxy_infos = []
        delete_proxy_infos = []
        for proxy in proxy_infos:
            
            i += 1
            proxy_info = {}
            proxy_info['proxy'] = proxy[0]
            isok, timeout = proxyutil.check_proxy(proxy[0])#验证代理有效性
            if isok and timeout <= 120 : #响应超过120秒的代理不要了
                self.current_info = 'proxy %s is ok ' % proxy[0]
                logging.info(self.current_info)
                proxy_info['timeout'] = timeout
                proxy_info['inuse'] = 0
                update_proxy_infos.append(proxy_info)
            else:
                self.current_info = 'proxy %s is not ok, delete it ' % proxy[0]
                logging.info(self.current_info)
                delete_proxy_infos.append(proxy_info)
            if i % 10 == 0:#每处理十条提交一次
                self._update_proxys(update_proxy_infos)
                self._delete_proxys(delete_proxy_infos)
                update_proxy_infos = []
                delete_proxy_infos = []
                
        self._update_proxys(update_proxy_infos)
        self._delete_proxys(delete_proxy_infos)
        
        now = datetime.now()
        self.current_info = "%s: sleep 48 hour..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return self._round_time
        
def main():
    client = ProxyCheckerClient()
    client.main()
               
if __name__ == '__main__':
    main()
    