#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""

from gba.common.jsonrpcserver import jsonrpc_function
from gba.common.constants import CLIENT_STATUS_NAMES
from gba.business.client import ClientManager

@jsonrpc_function
def register(request, id, client_type, version):
    """注册，返回客户端id"""
    client_id = ClientManager.register(id, client_type, version, request.META['REMOTE_ADDR'])
    return client_id

@jsonrpc_function
def report_status(request, client_id, status, description):
    """客户端状态更新，返回执行命令和辅助参数"""
    cmd, params = ClientManager.update_status(client_id, status, description)
    return {'cmd': cmd, 'params': params}

@jsonrpc_function
def get_client(request, client_id):
    client = ClientManager.get_client(client_id)
    client = client.to_dict()
    client['status_name'] = CLIENT_STATUS_NAMES[client['status']]
    return client

@jsonrpc_function
def set_command(request, client_id, command, params):
    ClientManager.set_command(client_id, command, params)
    return True

@jsonrpc_function
def set_command_to_all(request, command, params):
    ClientManager.set_command_to_all(command, params)
    return True

@jsonrpc_function
def get_msn_account(request):
    return ClientManager.get_msn_account(request.META['REMOTE_ADDR'])

@jsonrpc_function
def get_url_check_status(request):
    clients_info = UrlCheckClientMonitor().get_client_info()
    if clients_info.has_key(request.META['REMOTE_ADDR']):
        return clients_info[request.META['REMOTE_ADDR']]

@jsonrpc_function
def get_runtime_data(request, client_name, process_id, data_key): #已废弃，请调用common_service.get_runtime_data()
    """获取运行时数据"""
    return ClientManager.get_runtime_data(client_name, process_id, data_key)

@jsonrpc_function
def set_runtime_data(request, client_name, process_id, data_key, data_content): #已废弃, 请调用common_service.set_runtime_data()
    """设置运行时数据"""
    return ClientManager.set_runtime_data(client_name, process_id, data_key, data_content)
