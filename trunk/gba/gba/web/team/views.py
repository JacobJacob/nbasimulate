#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from copy import deepcopy
from django.core.urlresolvers import reverse

from gba.business.user_roles import login_required
from gba.entity import TeamStaff, SeasonFinance, AllFinance, TeamArena, TeamAd, LeagueConfig, Friends, Team, ProfessionPlayer
from gba.web.render import render_to_response
from gba.common.constants import StaffStatus, StaffType, FinanceSubType, FinanceType
from gba.common import exception_mgr
from gba.common.config import ad_billboards

@login_required
def season_finance(request, min=False):
    """球队财政"""
    team = request.team
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    infos, total = SeasonFinance.paging(page, pagesize, condition='team_id="%s"' % team.id, order='round desc')

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    if min:
        return render_to_response(request, 'team/season_finance_min.html', datas)
    return render_to_response(request, 'team/season_finance.html', datas)

@login_required
def all_finance(request, min=False):
    """球队财政历史"""
    team = request.team
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    infos, total = AllFinance.paging(page, pagesize, condition='team_id="%s"' % team.id, order='season desc')

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, 'team/all_finance_min.html', datas)

@login_required
def arena_build(request):
    """球员馆建设"""
    team = request.team
    team_arena = TeamArena.load(team_id=team.id)
    datas = {'team_arena': team_arena}
    return render_to_response(request, 'team/arena_build.html', datas)

@login_required
def team_ad(request, min=False):
    """球队广告"""
    team = request.team
    team_ads = deepcopy(ad_billboards)

    selected_ads = TeamAd.query(condition='team_id="%s"' % team.id)
    selected_ad_ids = [selected_ad.round for selected_ad in selected_ads]
    for team_ad in team_ads:
        amount = team_ad['amount']
        team_ad['amount'] = amount * (12 - team.profession_league_evel) #联赛等级越高，广告费越贵
        if team_ad['round'] in selected_ad_ids:
            team_ad['is_selected'] = True
        else:
            team_ad['is_selected'] = False
        
    datas = {'infos': team_ads}
    if min:
        return render_to_response(request, 'team/team_ad_min.html', datas)
    return render_to_response(request, 'team/team_ad.html', datas)

@login_required
def select_ad(request):
    """选择广告"""
    team = request.team
    ad_id = int(request.GET.get('id', 0))
    success = "广告签订成功"
    error = None
    i = 0
    while i < 1:
        i += 1
        if not ad_id or ad_id < 1 or ad_id > 8:
            error = u'广告不存在'
            break
        
        team_arena = TeamArena.load(team_id=team.id)
        if not team_arena:
            error = u'未知异常，联系客服'
            break
        
        selected_ads = TeamAd.query(condition='team_id="%s"' % team.id)
        if len(selected_ads) >= team_arena.level:
            error = u'赞助商不肯投放更多的广告<br/>咱们还是先发展我们的球队吧'
            break
        for selected_ad in selected_ads:
            if selected_ad.round == ad_id:
                error = '您已经签过该广告了'
                break
        
        amount = ad_billboards[ad_id-1]['amount'] * (12 - team.profession_league_evel) #联赛等级越高，广告费越贵
        
        league_config = LeagueConfig.load(id=1)
        
        team_ad = TeamAd()
        team_ad.team_id = team.id
        team_ad.round = ad_id
        team_ad.remain_round = ad_id
        team_ad.amount = amount
        
        #赛季收入明细
        tinance = SeasonFinance()
        tinance.sub_type = FinanceSubType.AD
        tinance.team_id = team.id
        tinance.type = FinanceType.INCOME
        tinance.season = league_config.season
        tinance.round = league_config.round
        tinance.info = u'赞助商广告费'
        tinance.income = amount
        tinance.outlay = 0
        team.funds += amount

        #赛季收入概要
        all_tinance = AllFinance.load(team_id=team.id, season=league_config.season)
        if not all_tinance:
            all_tinance = AllFinance()
            all_tinance.team_id = team.id
            all_tinance.season = league_config.season
            all_tinance.income = 0
            all_tinance.outlay = 0
        all_tinance.income += amount
        
        TeamAd.transaction()
        try:
            team_ad.persist()
            team.persist()
            tinance.persist()
            all_tinance.persist()
            TeamAd.commit()
        except:
            TeamAd.rollback()
            error = u'应用服务器未知异常'
        
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    url = reverse('team-ad-min')
    return render_to_response(request, 'message_update.html', {'success': success, 'url': url})  
    
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
    
@login_required
def team_info(request):
    """球队信息"""
    team = request.team
    datas = {'team': team}
    return render_to_response(request, 'team/team_info.html', datas)

@login_required
def update_team_info(request):
    """更新球队信息"""
    team_name = request.GET.get('team_name', None)
    micro = request.GET.get('hidClothes', None)
    team = request.team
    success = '球队信息更新成功'
    error = None
    i = 0
    while i < 1:
        i += 1
        
        if micro:
            team.micro = micro
        
        if team_name and team_name != team.name:
            check_team = Team.load(name=team_name)
            if check_team:
                error = '该球队名称已被占用'
                break
            else:
                team.name = team_name
        
        try:
            team.persist()
        except:
            exception_mgr.on_except()
            error = '服务器异常'
            break
        
    return render_to_response(request, 'message.html', {'error': error, 'success': success})

@login_required
def friends(request, min=False):
    '''好友列表'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    team = request.team
    infos, total = Friends.paging(page, pagesize, condition='team_id="%s"' % team.id)

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    if min:
        return render_to_response(request, 'team/friends_min.html', datas)
    return render_to_response(request, 'team/friends.html', datas)
 
@login_required
def add_friend(request):
    friend_team_id = request.GET.get('team_id', None)
    team = request.team
    success = '添加好友成功'
    error = None
    i = 0
    while i < 1:
        i += 1
        if not friend_team_id:
            error = '该经理不存在'
            break
        
        friend_team = Team.load(id=friend_team_id)
        if not friend_team:
            error = '该经理不存在'
            break
        
        friend = Friends.load(team_id=team.id, friend_team_id=friend_team_id)
        if friend:
            error = '该经理已经是您的好友'
            break
        
        friend = Friends()
        friend.team_id = team.id
        friend.friend_team_id = friend_team_id
        try:
            friend.persist()
        except:
            exception_mgr.on_except()
            error = '服务器异常'
            break
            
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    url = reverse('friends-min')
    return render_to_response(request, 'message_update.html', {'success': success, 'url': url})

@login_required
def delete_friend(request):
    friend_team_id = request.GET.get('team_id', None)
    team = request.team
    if request.method == 'GET':
        error = None
        i = 0
        while i < 1:
            i += 1
            if not friend_team_id:
                error = '该经理不存在'
                break
            
            friend_team = Team.load(id=friend_team_id)
            if not friend_team:
                error = '该经理不存在'
                break
            
            friend = Friends.load(team_id=team.id, friend_team_id=friend_team_id)
            if not friend:
                error = '该经理不是是您的好友'
                break
             
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'team/delete_friend.html', {'friend_team': friend_team})
    else:
        error = None
        success = '好友删除成功'
        i = 0
        while i < 1:
            i += 1
            if not friend_team_id:
                error = '该经理不存在'
                break
            
            friend_team = Team.load(id=friend_team_id)
            if not friend_team:
                error = '该经理不存在'
                break
            
            friend = Friends.load(team_id=team.id, friend_team_id=friend_team_id)
            if not friend:
                error = '该经理不是是您的好友'
                break
            
            try:
                friend.delete()
            except:
                exception_mgr.on_except()
                error = '删除出错'
                break
             
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        url = reverse('friends-min')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})
    
@login_required
def team_ranking(request, min=False):
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    if page <= 0:
        page = 1
    if page > 10:
        page = 10
    index = (page - 1) * pagesize
    
    infos = Team.query(order='agv_ability desc', limit="%s, %s" % (index, pagesize))
    total = 100
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}

    if min:
        return render_to_response(request, 'team/team_ranking_min.html', datas)
    return render_to_response(request, 'team/team_ranking.html', datas)

@login_required
def player_ranking(request):
    '''球员综合排行榜'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    if page <= 0:
        page = 1
    if page > 10:
        page = 10
    index = (page - 1) * pagesize
    
    infos = ProfessionPlayer.query(order='ability desc', limit="%s, %s" % (index, pagesize))
    total = 100
    
    for i, info in enumerate(infos):
        info.rank = (page-1)*pagesize + i + 1
        
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}

    return render_to_response(request, 'team/player_ranking_min.html', datas)