#!/usr/bin/python
# -*- coding: utf-8 -*-
"""domian相关service api"""

import datetime

from webauth.common.jsonrpcserver import jsonrpc_function
from webauth.business import domain_info

@jsonrpc_function
def update_domain(request, domain_infos):
    """更新域名信息
    @param domain_infos: 单个字典或多个字典组成的列表,每个字典封装一个域名的信息 
    """
    return domain_info.update_domain(domain_infos)

@jsonrpc_function
def fetch_domain(request, task_type):
    """获取要爬取信息的域名列表
    @param task_type: 任务类型,分为ALEX和GOOGLE
    """
    return domain_info.fetch_domain(task_type)

    
