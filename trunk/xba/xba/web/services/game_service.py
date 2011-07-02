#!/usr/bin/python
# -*- coding: utf-8 -*-
"""游戏相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.business import game_manager

@jsonrpc_function
def add_announce(request, title, type=0):
    """添加公告"""
    return game_manager.add_announce(title, type)