#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.business import player5_manager, player3_manager

@jsonrpc_function
def add_player5(request, count, category,  hours=12, now_point=55, max_point=70):
    return player5_manager.create_player(count, category, int(hours), now_point, max_point)

@jsonrpc_function
def add_player3(request, count, category,  hours=12):
    return player3_manager.create_player(count, category, int(hours))

