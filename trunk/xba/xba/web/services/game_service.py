#!/usr/bin/python
# -*- coding: utf-8 -*-
"""游戏相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.business import game_manager

@jsonrpc_function
def add_announce(request, title, type=0):
    """添加公告"""
    return game_manager.add_announce(title, type)

@jsonrpc_function
def delete_announce(request, id):
    """删除公告"""
    return game_manager.delete_announce(id)

@jsonrpc_function
def add_system_message(request, content):
    """添加系统消息"""
    return game_manager.add_system_message(content)