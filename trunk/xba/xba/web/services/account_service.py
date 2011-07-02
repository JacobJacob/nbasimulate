#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.business import account_manager

@jsonrpc_function
def add_invite_code(request, count):
    """添加邀请码"""
    return account_manager.add_invite_code(count)

@jsonrpc_function
def assign_invite_code(request, code):
    """分配邀请码"""
    return account_manager.assign_invite_code(code)
