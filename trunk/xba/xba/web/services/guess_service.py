#!/usr/bin/python
# -*- coding: utf-8 -*-
"""游戏相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.business import guess_manager

@jsonrpc_function
def add_guess(request, type, money_type, namea, nameb, hot, end_time_interval, show_time_interval):
    """添加公告"""
    end_time_interval = int(end_time_interval)
    show_time_interval = int(show_time_interval)
    return guess_manager.add_guess(type, money_type, namea, nameb, hot, end_time_interval, show_time_interval)

@jsonrpc_function
def guess_begin(request, id):
    """平盘"""
    return guess_manager.guess_begin(id)

@jsonrpc_function
def set_guess_result(request, id, result_type, result_text):
    """设置结果"""
    return guess_manager.set_guess_result(id, result_type, result_text)

