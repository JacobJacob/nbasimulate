#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.entity import Unions, UnionApply, Team, Message, UnionMember, UserInfo
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
    union_id = request.GET.get('union_id')
    datas = {}
    
    if team.union_id:
        has_union = True
    else:
        has_union = False
    
    if not union_id:
        union_id = team.union_id
     
    union = Unions.load(id=team.union_id)
    datas['union'] = union
    if has_union:
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
        remark = request.GET.get('remark')
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
            union_apply.remark =  remark
            
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
    user_info = UserInfo.load(username=team.username)
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
        message.content = u'%s盟盟主%s%s了你的入盟申请' % (union.name, user_info.nickname, u'通过' if result == 1 else u'拒绝')
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
    is_manager = False
    if not union_id:
        union_id = team.union_id
        union = Unions.load(id=union_id)
        if union.leader == team.id:
            is_leader = True
        else:
            union_member = UnionMember.load(team_id=team.id)
            if union_member and union_member.is_manager:
                is_manager = True
        
    infos, total =  UnionMember.paging(page, pagesize, condition='union_id="%s"' % union_id)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1, \
             'is_leader': is_leader, 'is_manager': is_manager}
    
    return render_to_response(request, 'union/union_member.html', datas)

def union_manager_setting(request):
    '''联盟设置管理员'''
    team = request.team
    error = None
    member_id = request.GET.get('member_id')
    opt = int(request.GET.get('opt', 0)) #0是设置1是取消
    i = 0
    while i < 1:
        i += 1
        union = Unions.load(id=team.union_id)
        if not union or union.leader != team.id:
            error = '您不是盟主，不能执行该操作'
            break
        
        union_member = UnionMember.load(id=member_id)
        if not union_member:
            error = '该经理不是您的盟员!'
            break
        
        if opt == 0:
            if union_member.is_manager or union_member.is_leader:
                error = '您不能将其设为管理员'
                break
        
            manager_count = UnionMember.count(condition="union_id='%s' and is_manager=1" % team.union_id)
            if manager_count >=5 :
                error = '您的联盟最多只能设置5位管理员!'
                break
                    
    if request.method == 'GET':
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'union/union_manager_setting.html', {'union_member': union_member, 'opt': opt})
    else:
        if opt == 0:
            success = '管理员设置成功'
        else:
            success = '管理员取消成功'
        if not error:
            union_member.is_manager = 1 if opt == 0 else 0
            message = Message()
            message.type = MessageType.SYSTEM_MSG
            message.from_team_id = 0
            if opt == 0:
                message.title = u'您被设置为联盟管理员'
                message.content = u'您被设置为联盟管理员'
            else:
                message.title = u'您被取消了联盟管理员的职位'
                message.content = u'您被取消了联盟管理员的职位'
            message.to_team_id = union_member.team_id
            
            UnionMember.transaction()
            try:
                union_member.persist()
                message.persist()
                UnionMember.commit()
            except:
                UnionMember.rollback()
                raise
            
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        url = reverse('union-member')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})
             
def union_title_setting(request):
    '''联盟成员封号设置'''
    team = request.team
    error = None
    member_id = request.GET.get('member_id')
    title = request.GET.get('title')
    i = 0
    while i < 1:
        i += 1
        union = Unions.load(id=team.union_id)
        if not union or union.leader != team.id:
            error = '您不是盟主，不能设置封号'
            break
        
        union_member = UnionMember.load(id=member_id)
        if not union_member:
            error = '该经理不是您的盟员!'
            break
               
    if request.method == 'GET':
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'union/union_title_setting.html', {'union_member': union_member})
    else:
        success = '封号成功'
        if not error:
            union_member.title = title 
            message = Message()
            message.type = MessageType.SYSTEM_MSG
            message.from_team_id = 0
            message.title = u'您被联盟盟主封为:%s' % title
            message.content = u'您被联盟盟主封为:%s' % title
            message.to_team_id = union_member.team_id
            
            UnionMember.transaction()
            try:
                union_member.persist()
                message.persist()
                UnionMember.commit()
            except:
                UnionMember.rollback()
                raise
            
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        url = reverse('union-member')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})