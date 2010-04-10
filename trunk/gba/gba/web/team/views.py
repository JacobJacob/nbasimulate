#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from django.core.urlresolvers import reverse

from gba.business.user_roles import login_required
from gba.entity import TeamStaff
from gba.web.render import render_to_response
from gba.common.constants import StaffStatus, StaffType
from gba.common import exception_mgr

@login_required
def season_finance(request, min=False):
    """球队财政"""
    team = request.team
    datas = {}
    if min:
        return render_to_response(request, 'team/season_finance_min.html', datas)
    return render_to_response(request, 'team/season_finance.html', datas)

@login_required
def arena_build(request):
    """球员馆建设"""
    team = request.team
    datas = {}
    return render_to_response(request, 'team/arena_build.html', datas)

@login_required
def team_staff(request, min=False):
    """球队职员"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    is_youth = int(request.GET.get('is_youth', 0))
    type = request.GET.get('type', None)
    level = int(request.GET.get('level', 1))
    
    condition = "is_youth=%s and level=%s and status=%s" % (is_youth, level, StaffStatus.NOT_IN_WORD) 
    if type:
        condition += " and type=%s " % type
    
    infos, total = TeamStaff.paging(page, pagesize, condition=condition)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
        
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'is_youth': is_youth, 'level': level, 
            'type': type}
    
    team_staffs = TeamStaff.query(condition="team_id=%s and is_youth=%s" % (request.team.id, is_youth))
    if team_staffs:
        for staff in team_staffs:
            if staff.type == StaffType.TRAINERS:
                datas['trainer'] = staff
            elif staff.type == StaffType.DIETITIAN:
                datas['dietitian'] = staff
            elif staff.type == StaffType.DOCTOR:
                datas['doctor'] = staff
    if min:
        return render_to_response(request, 'team/team_staff_min.html', datas)
    return render_to_response(request, 'team/team_staff.html', datas)

@login_required
def hire_staff(request):
    '''雇佣职员'''
    id = request.GET.get('id')
    if request.method == 'GET':    
        error = None
        i = 0
        while i < 1:
            i += 1
            staff = TeamStaff.load(id=id)
            if not staff or staff.status == StaffStatus.IN_WORK:
                error = '该职员不在市场中'
                break
            
            team_staff = TeamStaff.load(team_id=request.team.id, type=staff.type, is_youth=staff.is_youth)
            if team_staff:
                error = '您已经有了该职位的职员，不能再雇佣了'
                break
            
        if error:
            return render_to_response(request, 'message.html', {'error': error})    
        return render_to_response(request, 'team/hire_staff.html', {'staff': staff, 'fund': staff.round * staff.wave})
    
    else:
        error = None
        success = '职员雇佣成功'
        team = request.team
        i = 0
        while i < 1:
            i += 1
            staff = TeamStaff.load(id=id)
            if not staff or staff.status == StaffStatus.IN_WORK:
                error = '该职员不在市场中'
                break
            
            team_staff = TeamStaff.load(team_id=request.team.id, type=staff.type, is_youth=staff.is_youth)
            if team_staff:
                error = '您已经有了该职位的职员，不能再雇佣了'
                break
            
            fund = staff.wave * staff.round
            if team.funds < fund:
                error = '您的资金不足'
                break
            
            team.funds -= fund
            staff.status = StaffStatus.IN_WORK
            staff.remain_round = staff.round
            staff.team_id = team.id
            
            TeamStaff.transaction()
            try:
                team.persist()
                staff.persist()
                TeamStaff.commit()
            except:
                TeamStaff.rollback()
                exception_mgr()
                error = '雇佣失败，服务器异常'
        
        if error:
            return render_to_response(request, 'message.html', {'error': error, 'success': success})
        url = reverse('team-staff-min')
        return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': '%s?is_youth=%s' % (url, staff.is_youth)})    