#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.entity import Unions, UnionApply, Team, Message, UnionMember
from gba.common import exception_mgr
from gba.common.constants import MessageType
from gba.business.client import ClientManager
from gba.business.common_client_monitor import CommonClientMonitor

def union_list(request, min=False):
    """联盟列表"""
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    infos, total = Unions.paging(page, pagesize, order="prestige desc")
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    if min:
        return render_to_response(request, 'union/union_list_min.html', datas)
    return render_to_response(request, 'union/union_list.html', datas)

def team_union(request, min=False):
    """我的联盟"""
    team = request.team
    datas = {}
    
    has_union = False
    if team.union_id:
        has_union = True
        union = Unions.load(id=team.union_id)
        datas['union'] = union
        print union.leader
        print team.id
        print union.leader == team.id 
        datas['is_leader'] = True if union.leader == team.id else False   
        
    if not has_union:
        return render_to_response(request, 'union/team_not_union.html', datas)
    
    if min:
        return render_to_response(request, 'union/team_union_min.html', datas)
    return render_to_response(request, 'union/team_union.html', datas)

@login_required
def union_add(request):
    """创建联盟"""
    
    team = request.team
    
    datas = {}
    if request.method == 'GET':
        return render_to_response(request, 'union/union_add.html', datas)

    else:
        error = None
        success = '联盟创建成功'
        name = request.POST.get('name', '')
        short_name = request.POST.get('short_name', '')
        qq_group = request.POST.get('qq_group', '')
        forum = request.POST.get('forum', '')
        desc = request.POST.get('desc', '')
        
        union = Unions()
        union.name = name
        union.short_name = short_name
        union.qq_group = qq_group
        union.forum = forum
        union.member = 1
        union.leader = team.id
        union.union_desc = desc
        
        union_member = UnionMember()
        union_member.is_leader = 1
        union_member.prestige = 10 #默认10的威望
        union_member.team_id = team.id
        
        try:
            union.persist()
            team.union_id = union.id
            team.persist()
            union_member.union_id = union.id
            union_member.persist()
        except:
            raise
            exception_mgr.on_except()
            error = '服务器异常'
        
        return render_to_response(request, 'union/union_add_message.html', {'success': success, 'error': error})
    

def union_apply(request):
    """申请加入联盟"""
    team = request.team
    if request.method == 'GET':
        error = None
        union_id = request.GET.get('union_id')
        i = 0
        while i < 1:
            i += 1
            if team.union_id:
                error = '您已经加过联盟了'
                break
            
            if not union_id:
                error = '联盟不存在'
                break
        
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'union/union_apply.html', {'union_id': union_id})
    
    else:
        union_id = request.GET.get('union_id')
        print union_id
        print request.GET
        print request.POST
        error = None
        success = '申请成功，请等待审核!'
        i = 0
        while i < 1:
            i += 1
            if team.union_id:
                error = '错误:您已经加过联盟'
                break
            
            union = Unions.load(id=union_id)
            if not union:
                error = '联盟不存在'
                break
            
            union_apply = UnionApply()
            union_apply.union_id = union_id
            union_apply.team_id = team.id
            
            union_apply.persist()
             
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'message.html', {'success': success})
        
def union_announce(request):
    '''联盟公告'''
    team = request.team
    if request.method == 'GET':
        error = None
        i = 0
        while i < 1:
            i += 1
            union = Unions.load(leader=team.id)
            if not union:
                error = '您不是盟主，无权更改联盟公告'
                break
            
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'union/union_announce.html', {'union': union}) 
    
    else:
        error = None
        success = '联盟公告更改成功!'
        announce = request.GET.get('announce')
        i = 0
        while i < 1:
            i += 1
            union = Unions.load(leader=team.id)
            if not union:
                error = '您不是盟主，无权更改联盟公告'
                break
            
            if not announce:
                error = '联盟公告不能为空'
                break
            
            union.announce = announce
            union.persist()
                
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        url = reverse('team-union-min')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url}) 
        
def wait_appove_list(request):
    '''审核会员'''
    team = request.team
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))

    infos, total = UnionApply.paging(page, pagesize, condition='union_id="%s"' % team.union_id)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, 'union/wait_appove_list.html', datas)

def union_member_appove(request):
    '''审核会员'''
    team = request.team
    error = None
    user_info = UserManager().get_userinfo(request)
    result = int(request.GET.get('result'))
    apply_team_id = request.GET.get('team_id')
    success = u'您已经同意了该入盟请求' if result == 1 else u'您已经拒绝了该入盟请求'
    i = 0
    while i < 1:
        i += 1
        union = Unions.load(leader=team.id)
        if not union or union.leader != team.id:
            error = '你不是盟主,无法执行该操作'
            break
        
        union_apply = UnionApply.load(team_id=apply_team_id, union_id=union.id)
        apply_team = Team.load(id=apply_team_id)
        message = Message()
        message.type = MessageType.SYSTEM_MSG
        message.from_team_id = 0
        message.title = u'入盟申请被通过' if result == 1 else u'入盟申请被拒绝'
        message.content = u'%s盟盟主%s%s了你的入盟申请' % (union.name, user_info['nickname'], u'通过' if result == 1 else u'拒绝')
        message.to_team_id = apply_team_id
        
        union_member = UnionMember()
        union_member.prestige = 10 #默认10的威望
        union_member.team_id = apply_team_id
        
        Unions.transaction()
        try:
            union_apply.delete()
            if result == 1:
                apply_team.union_id = union.id
                apply_team.persist()
                union.member += 1
                union.persist()
                union_member.union_id = union.id
                union_member.persist()
                
            message.persist()
            Unions.commit()
        except:
            error = '服务器异常'
            exception_mgr.on_except()
    
    if error:    
        return render_to_response(request, 'message.html', {'error': error})
    url = reverse('wait-appove-list')
    return render_to_response(request, 'message_update.html', {'success': success, 'url': url})

def union_member(request):
    '''联盟盟员'''
    team = request.team
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    union_id = request.GET.get('union_id')
    
    is_leader = False
    is_mamager = False
    if not union_id:
        union_id = team.union_id
        union = Unions.load(id=union_id)
        if union.leader == team.id:
            is_leader = True
        else:
            union_member = UnionMember.load(team_id=team.id)
            if union_member and union_member.is_mamager:
                is_mamager = True
        
    infos, total =  UnionMember.paging(page, pagesize, condition='union_id="%s"' % union_id)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1, \
             'is_leader': is_leader, 'is_mamager': is_mamager}
    
    return render_to_response(request, 'union/union_member.html', datas)