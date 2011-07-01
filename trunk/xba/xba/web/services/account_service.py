#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.business import account_manager

@jsonrpc_function
def add_invitec_code(request, count):
    return account_manager.add_invitec_code(count)

