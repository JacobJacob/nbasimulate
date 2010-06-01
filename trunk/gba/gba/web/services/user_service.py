#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关rpc"""

from gba.entity import Team
from gba.common.jsonrpcserver import jsonrpc_function
from gba.business.user_roles import rpc_login_required

@rpc_login_required
@jsonrpc_function
def check_user_exist(request, nickname):
    return 1

@jsonrpc_function
def check_teamname_exist(request, teamname):
    team = Team.load(name=teamname)
    if team:
        return 1
    return 0