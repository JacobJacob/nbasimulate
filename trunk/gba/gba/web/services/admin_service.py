#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""

from gba.common.jsonrpcserver import jsonrpc_function
from gba.business.user_roles import rpc_login_required
from gba.business import admin_operator
from gba.entity import ActionDesc

@rpc_login_required
@jsonrpc_function
def add_action_desc(request, info):
    return admin_operator.add_action_desc(info)

@rpc_login_required
@jsonrpc_function
def get_action_desc(request, id):
    return ActionDesc.load(id=id)


