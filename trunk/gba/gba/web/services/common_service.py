#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""
from gba.common.jsonrpcserver import jsonrpc_function
from gba.business import common_business

@jsonrpc_function
def get_runtime_config(request, config_key, program=None, ip="all"):
    """获取运行时配置"""
    if not ip:
        ip = "all"
    elif ip == "local":
        ip = request.META["REMOTE_ADDR"]
    return common_business.get_runtime_config(config_key, program, ip)
    
@jsonrpc_function
def get_runtime_data(request, client_name, process_id, data_key):
    """获取运行时数据"""
    return common_business.get_runtime_data(client_name, process_id, data_key)

@jsonrpc_function
def set_runtime_data(request, client_name, process_id, data_key, data_content):
    """设置运行时数据"""
    return common_business.set_runtime_data(client_name, process_id, data_key, data_content)

@jsonrpc_function
def update_task_status(request, name, status, description):
    client_ip = request.META.get('HTTP_X_REAL_IP', request.META['REMOTE_ADDR'])
    r = common_business.save_task_status(name, status, description, client_ip)
    if r is not None:
        return 1
    return 0
