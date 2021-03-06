#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关视图"""

import os
import shutil

from django.core.urlresolvers import reverse
from django.http import HttpResponseRedirect

from xba.config import PathSettings
from xba.web.render import render_to_response, json_response
from xba.business.user_roles import UserManager, login_required
from xba.business import user_operator, match_operator, player_operator
from xba.entity import Message, Team, ChatMessage, LeagueTeams, OutMessage, UserInfo
from xba.common.constants import MessageType, MatchTypeMaps, MatchTypes
from xba.common import exception_mgr

SESSION_KEY = '_auth_user_id'

def login_page(request):
    next = request.GET.get('next')
    return render_to_response(request, "user/login.html", locals())

def login(request):
    content = u'用户登录'
    
    username = request.POST.get('username')
    password = request.POST.get('password')
    
    user_manager = UserManager()
    success, session_id = user_manager.login(username, password)

    next = request.GET.get('next')
    
    print next    
    if success == 0:
        response = HttpResponseRedirect(next if next else '/')
        response.set_cookie(SESSION_KEY, session_id)
        return response
    else:
        message = session_id
        return render_to_response(request, "user/login_error.html", locals())
    
def register_page(request):
    '''注册页面'''
    return render_to_response(request, "user/register.html", locals())

def register(request):
    '''注册'''
    username = request.POST.get('username')
    password = request.POST.get('password')
    nickname = request.POST.get('nickname')
    teamname = request.POST.get('teamname')
    password_check = request.POST.get('password_check')
    
    user_manager = UserManager()

    success, session_id = user_manager.register_user(username, password, nickname)
    
    if success:
        response = HttpResponseRedirect('/')
        response.set_cookie(SESSION_KEY, session_id)
        match_operator.init_team({'username': username, 'teamname': teamname})
        return response
    else:
        message = session_id
        return render_to_response(request, "user/register_error.html", locals())
    
@login_required
def online_user(request, min=False):
    '''在线经理'''
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    infos, total = user_operator.get_online_users(page, pagesize)
    
    print infos
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1}
        
    if min:
        return render_to_response(request, "user/online_user_min.html", datas)
    return render_to_response(request, "user/online_user.html", datas)

@login_required
def send_match_request(request):
    '''发送约战请求'''
    if request.method == 'GET':
        team_id = request.GET.get('team_id')
        team = Team.load(id=team_id)
        return render_to_response(request, "user/send_match_request.html", locals())
    else:
        success =  u'比赛请求发送成功'
        error = '';
        team = request.team
        
        i = 1
        while i > 0:
            i -= 1 
            print request.GET
            team_id = request.GET.get('team_id')
            type = int(request.GET.get('match_type'))
            is_youth = int(request.GET.get('is_youth', 0))
            
            if type == 1:
                if is_youth == 1:
                    match_type = MatchTypes.YOUTH_TRAINING
                else:
                    match_type = MatchTypes.TRAINING
            else:
                if is_youth == 1:
                    match_type = MatchTypes.YOUTH_FRIENDLY
                else:
                    match_type = MatchTypes.FRIENDLY
            
            user_info = UserInfo.load(username=team.username)
            if not team:
                error = u'登录信息丢失'
                break
                
            guest_team = Team.load(id=team_id)
            if not guest_team:
                error = u'找不到该球队'
                break
                
            if team.id == guest_team.id:
                error = u'你无法和自己约战'
                break
               
            message = Message()
            message.type = MessageType.SYSTEM_MSG
            message.from_team_id = 0
            message.to_team_id = guest_team.id
            message.title = u'比赛请求(%s)' % user_info.nickname
            message.content = u'%s经理向你发送了%s比赛请求，您可以在我的比赛中查看' % (user_info.nickname, MatchTypeMaps.get(match_type))
            message.is_new = 1
    
            try:
                match_operator.send_match_request(team.id, guest_team.id, type=match_type)
                message.persist()
            except:
                error = u'服务器异常'
        
        return render_to_response(request, "message.html", {'success': success, 'error': error})
    
@login_required
def send_message(request):
    '''发送信息''' 
    if request.method == 'GET':
        team_id = request.GET.get('team_id', None)
        if request.method == 'GET':
            error = None
            i = 0
            while i < 1:
                i += 1
                if not team_id:
                    error = '该经理不存在'
                    break
                
                to_team = Team.load(id=team_id)
                if not to_team:
                    error = '该经理不存在'
                    break

            if error:
                return render_to_response(request, 'message.html', {'error': error})
            datas = {'to_team': to_team}
            return render_to_response(request, "user/send_message.html", datas)
    else:
        to_team_id = request.GET.get('team_id', None)
        title = request.GET.get('title', None)
        content = request.GET.get('content', None)
        team = request.team

        success = '信息发送成功'
        error = None
        i = 0
        while i < 1:
            i += 1
            if not (title and content):
                error = '消息不完整'
                break
                
            if not to_team_id:
                error = '该经理不存在'
                break
            
            if to_team_id == team.id:
                error = '您不可以给自己发短信'
                break
            
            to_team = Team.load(id=to_team_id)
            if not to_team:
                error = '该经理不存在'
                break
            
            message = Message()
            out_message = OutMessage()
            
            message.title = title
            message.content = content
            message.from_team_id = team.id
            message.to_team_id = to_team_id
            message.is_new = 1
            message.type = MessageType.PLAYER_MSG
            
            out_message.title = title
            out_message.content = content
            out_message.from_team_id = team.id
            out_message.to_team_id = to_team_id
            
            Message.transaction()
            try:
                message.persist()
                out_message.persist()
                Message.commit()
            except:
                Message.rollback()
                exception_mgr.on_except()
                error = '服务器异常'
            
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, "message.html", {'success': success})
        
@login_required
def message(request, min=False):
    '''消息管理'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    team = UserManager().get_team_info(request)
    
    infos, total = user_operator.get_message(team.id, page, pagesize)
    
    ids = []
    for info in infos:
        ids.append(info['id'])
    
    if ids:    
        user_operator.update_message_to_old(ids)
        
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1}
    
    if min:
        return render_to_response(request, "user/message_min.html", datas)
    return render_to_response(request, "user/message.html", datas)

@login_required
def out_message(request):
    '''发件箱管理'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    team = UserManager().get_team_info(request)
    
    infos, total = OutMessage.paging(page, pagesize, order="id desc")
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, "user/out_message.html", locals())

@login_required
def check_new_message(request):
    '''检查看有没有新的消息'''
    team = UserManager().get_team_info(request)
    message = Message.query(condition="to_team_id=%s and is_new=1" % team.id, limit=1)
    if message:
        return json_response(1)
    return json_response(0)

@login_required
def user_detail(request):
    '''经理信息'''
    team = request.team
    team_id = request.GET.get('id')
    type = int(request.GET.get('type', 1))
    if type == 2:
        league_team = LeagueTeams.load(id=team_id)
        team_id = league_team.team_id
    show_team = Team.load(id=team_id)
    
    if show_team.last_active_time:
        show_team.last_active_time = show_team.last_active_time.strftime("%m-%d")
    
    show_team.is_self = 1 if show_team.id == team.id else 0

    pro_players = player_operator.get_profession_player(team_id)
    youth_players = player_operator.get_youth_player(team_id)
    return render_to_response(request, "user/user_detail.html", locals())
   
@login_required                   
def issue_message(request):
    '''发布消息'''
    team = request.team
    content = request.GET.get('msg', None)
    user_info = UserInfo.load(username=team.username)
    chat_message = ChatMessage()
    chat_message.content = content
    chat_message.username = user_info.nickname
    chat_message.persist()
     
    chat_messages = ChatMessage.query(order='created_time desc', limit=15)
    if chat_messages:
        chat_messages = chat_messages[-1::-1]
    response = render_to_response(request, "user/chat_message.html",{'infos': chat_messages})
    
    path = os.path.join(PathSettings.PROJECT_FOLDER, 'web', 'media', 'static', 'lt.html')
    f = open('%s_' % path, 'wb')
    success = True
    try:
        f.write(response.content)
    except:
        success = False
        exception_mgr.on_except()
    finally:
        f.close()
            
    if success:
        shutil.move('%s_' % path, path)
        
    return response

@login_required
def message_detail(request):
    '''消息详细'''
    id = request.GET.get('id')
    message = Message.load(id=id)
    datas = {'message': message}
    return render_to_response(request, "user/message_detail.html", datas)

@login_required
def delete_message(request, all=False):
    '''消息详细'''
    team = request.team
    i = 0
    error = None
    success = '删除成功'
    while i < 1:
        i += 1
        messages = None
        if all:
            messages = Message.query(condtion="to_team_id=%s" % team.id)
        else:
            msg_ids = request.GET.getlist("ch[]")
            if msg_ids:
                messages = Message.query(condition="id in (%s)" % ','.join(['"%s"' % id for id in msg_ids]))
        if messages:
            for message in messages:
                message.delete()
     
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    url = reverse('message-min')
    return render_to_response(request, "message_update.html", {'success': success, 'url': url})