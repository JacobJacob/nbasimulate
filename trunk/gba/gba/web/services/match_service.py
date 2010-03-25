#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端基础服务"""

from gba.common.jsonrpcserver import jsonrpc_function
from gba.business.user_roles import login_required, UserManager
from gba.business import match_operator
from gba.entity import Team, Message
from gba.common.constants import MessageType

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

@login_required
@jsonrpc_function
def send_match_request(request, info):
    """保存战术配置"""

    team = UserManager().get_team_info(request)
    user_info = UserManager().get_userinfo(request)
    if not team:
        return 0, u'无法获取球队信息'
    
    guest_team = Team.load(username=info['username'])
    
    if team.id == guest_team.id:
        return 0, u'你无法和自己约战'
    
    message = Message()
    message.type = MessageType.SYSTEM_MSG
    message.from_team_id = 0
    message.to_team_id = guest_team.id
    message.content = u'%s经理向你发送了%s比赛请求，您可以在我的比赛中查看' % (user_info['nickname'], type)
    message.is_new = 1
    
    try:
        match_operator.send_match_request(team.id, guest_team.id, type=1)
        message.persist()
    except:
        return 0, u'服务器异常'
    return 1, None

