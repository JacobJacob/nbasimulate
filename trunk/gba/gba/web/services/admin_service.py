#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""

from gba.common.jsonrpcserver import jsonrpc_function
from gba.business.user_roles import login_required, UserManager
from gba.business import admin_operator

@login_required
@jsonrpc_function
def add_action_desc(request, info):
    return admin_operator.add_action_desc(info)

