#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""

from gba.common.jsonrpcserver import jsonrpc_function
from gba.business.user_roles import login_required, UserManager
from gba.business import match_operator
from gba.entity import Team

@login_required
@jsonrpc_function
def save_tactical_detail(request, info):
    """保存战术"""
    user_info = UserManager().get_userinfo(request)
    if not user_info:
        return '登录信息丢失，请重新登录'
    username = user_info['username']
    
    team = Team.load(username=username)
    if not team:
        return '战术更新失败,无法获取球队信息'
    
    info['team_id'] = team.id
    if not info.get('name'):
        seq = info['seq']
        if seq == 'A':
            name = '第一节战术'
        elif seq == 'B':
            name = '第二节战术'
        elif name == 'C':
            name = '第三节战术'
        else:
            name = '第四节战术'
        info['name'] = name
        
    if match_operator.save_tactical_detail(info):
        return '战术更新成功'
    
    return '战术更新失败,请联系客服'

@login_required
@jsonrpc_function
def save_tactical_main(request, infos):
    """保存战术配置"""

    team = UserManager().get_team_info(request)
    if not team:
        return '战术更新失败,无法获取球队信息'
    
    try:
        for info in infos:
            info['team_id'] = team.id
            for i in range(1, 9):
                info['tactical_detail_%s_id' % i] = info[str(i)]
                del info[str(i)]
    except:
        return '战术更新失败,服务器发生未知异常'
            
    if match_operator.save_tactical_main(infos):
        return '战术更新成功'
    
    return '战术更新失败,请联系客服'



