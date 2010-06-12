#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator, match_operator

from gba.entity import Team, Matchs, ProfessionPlayer, TrainingCenter, \
                       YouthPlayer, MatchNotInPlayer, UserInfo, TacticalGrade, \
                       TrainingRemain, Message, ChallengePool , MatchNodosityMain, \
                       ChallengeHistory, ChallengeTeam, ChallengeAll, UnionHonor, \
                       UnionHonorDetail, Unions
from gba.common.constants import MatchTypes, DefendTacticalTypeMap, OffensiveTacticalTypeMap
from gba.common.constants import MatchStatus, MatchShowStatus, MessageType, MatchTypeMaps, UnionPrestigeFromType
from gba.common import exception_mgr
from gba.common import playerutil
from gba.common import commonutil
from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral


@login_required
def friendly_match(request, min=False):
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    team = UserManager().get_team_info(request)
    infos, total = match_operator.get_match(team.id, MatchTypes.FRIENDLY, MatchTypes.YOUTH_FRIENDLY, page, pagesize)

    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
        
    for info in infos:
        info['is_home'] = team.id == info['home_team_id']
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    if min:
        return render_to_response(request, 'match/friendly_match_min.html', datas)
    return render_to_response(request, 'match/friendly_match.html', datas)

def _create_total_stat(stat, total_stat):
    total_stat['point2_doom_times'] = total_stat.get('point2_doom_times', 0) + stat['point2_doom_times']
    total_stat['point3_doom_times'] = total_stat.get('point3_doom_times', 0) + stat['point3_doom_times'] 
    total_stat['point1_doom_times'] = total_stat.get('point1_doom_times', 0) + stat['point1_doom_times']
    total_stat['point2_shoot_times'] = total_stat.get('point2_shoot_times', 0) + stat['point2_shoot_times']
    total_stat['point3_shoot_times'] = total_stat.get('point3_shoot_times', 0) + stat['point3_shoot_times'] 
    total_stat['point1_shoot_times'] = total_stat.get('point1_shoot_times', 0) + stat['point1_shoot_times']
    total_stat['offensive_rebound'] = total_stat.get('offensive_rebound', 0) + stat['offensive_rebound']
    total_stat['defensive_rebound'] = total_stat.get('defensive_rebound', 0) + stat['defensive_rebound']
    total_stat['total_rebound'] = total_stat.get('total_rebound', 0) + stat['total_rebound']
    total_stat['assist'] = total_stat.get('assist', 0) + stat['assist']
    total_stat['steals'] = total_stat.get('steals', 0) + stat['steals']
    total_stat['foul'] = total_stat.get('foul', 0) + stat['foul']
    total_stat['block'] = total_stat.get('block', 0) + stat['block']
    total_stat['lapsus'] = total_stat.get('lapsus', 0) + stat['lapsus']
    total_stat['total_point'] = total_stat.get('total_point', 0) + stat['total_point']

def match_stat(request):
    '''比赛统计'''
    match_id = request.GET.get('match_id')
    if not match_id:
        return None
    
    match = Matchs.load(id=match_id)
    
    home_stat = match_operator.get_match_stat(match.home_team_id, match_id)
    
    home_team = Team.load(id=match.home_team_id)
    guest_team = Team.load(id=match.guest_team_id)
    
    home_stat_total = {}
    home_stat_total['times'] = 240
    home_stat_total['name'] = u'合计'
    for stat in home_stat:
        total_point = 0
        for i in range(1, 4):
            total_point += stat.get('point%s_doom_times' % i, 0) * i
        stat['total_point'] = total_point
        stat['total_rebound'] = stat['defensive_rebound'] + stat['offensive_rebound']
        _create_total_stat(stat, home_stat_total)
        
    home_stat = sorted(home_stat, cmp=lambda x, y: cmp(x['total_point'] * 1000 + x['total_rebound'] * 100 + x['assist'] * 10 + x['steals'], \
                                                      y['total_point'] * 1000 + y['total_rebound'] * 100 + y['assist'] * 10 + y['steals']), reverse=True)
    home_stat_main = [] #主队主力
    home_stat_sub = [] #主队替补
    
    for stat in home_stat:
        if stat['is_main'] == 1:
            home_stat_main.append(stat)  
        else:
            home_stat_sub.append(stat)
            
    guest_stat = match_operator.get_match_stat(match.guest_team_id, match_id)
    
    guest_stat_total = {}
    guest_stat_total['times'] = 240
    guest_stat_total['name'] = u'合计'
    for stat in guest_stat:
        total_point = 0
        for i in range(1, 4):
            total_point += stat.get('point%s_doom_times' % i, 0) * i
        stat['total_point'] = total_point
        stat['total_rebound'] = stat['defensive_rebound'] + stat['offensive_rebound']
        _create_total_stat(stat, guest_stat_total)
        
    guest_stat = sorted(guest_stat, cmp=lambda x, y: cmp(x['total_point'] * 1000 + x['total_rebound'] * 100 + x['assist'] * 10 + x['steals'], \
                                                      y['total_point'] * 1000 + y['total_rebound'] * 100 + y['assist'] * 10 + y['steals']), reverse=True)
    
    #guest_stat.append(guest_stat_total)
    #home_stat.append(home_stat_total)
    guest_stat_main = [] #客队主力
    guest_stat_sub = [] #客队替补
    
    for stat in guest_stat:
        if stat['is_main'] == 1:
            guest_stat_main.append(stat)  
        else:
            guest_stat_sub.append(stat)

    home_not_in_players = MatchNotInPlayer.query(condition="match_id=%s and team_id=%s" % (match_id, match.home_team_id))

    guest_not_in_players = MatchNotInPlayer.query(condition="match_id=%s and team_id=%s" % (match_id, match.guest_team_id))

    datas = {'home_stat_main': home_stat_main, 'home_stat_sub': home_stat_sub, 'guest_stat_main': guest_stat_main,
              'guest_stat_sub': guest_stat_sub, 'home_team_name': home_team.name,
              'guest_team_name': guest_team.name, 'home_not_in_players': home_not_in_players,
              'guest_not_in_players': guest_not_in_players, 'guest_stat_total': guest_stat_total,
              'home_stat_total': home_stat_total}
    return render_to_response(request, 'match/match_stat.html', datas)

def match_detail(request):
    '''比赛战报'''
    match_id = request.GET.get('match_id')
    if not match_id:
        return None
    
    match = Matchs.load(id=match_id)
    home_team = Team.load(id=match.home_team_id)
    home_team_name = home_team.name if home_team else ''
    guest_team = Team.load(id=match.guest_team_id)
    guest_team_name = guest_team.name if guest_team else ''
    match_nodosity_mains = match_operator.get_match_nodosity_main(match_id)
    
    for match_nodosity_main in match_nodosity_mains:
        match_nodosity_details = match_operator.get_match_nodosity_detail(match_nodosity_main['id'])
        match_nodosity_main['details'] = match_nodosity_details
        match_nodosity_tactical_details = match_operator.get_match_nodosity_tactical_detail(match_nodosity_main['id'])
        for match_nodosity_tactical_detail in match_nodosity_tactical_details:
            match_nodosity_main[match_nodosity_tactical_detail['position']] = match_nodosity_tactical_detail

    datas = {'match_nodosity_mains': match_nodosity_mains, 'home_team_name': home_team_name, 'guest_team_name': guest_team_name}
    return render_to_response(request, 'match/match_detail.html', datas)

@login_required
def training_center(request, min=False):
    '''训练中心'''
    team = request.team
    training_center = TrainingCenter.load(team_id=team.id, status=0)
    remain = 0
    in_training = False
    if training_center:
        remain = match_operator.get_training_remain(team.id)
        if remain < 0:
            playerutil.finish_training(training_center)            
        else:
            remain = remain / 100 * 60
            in_training = True

    training_remain = TrainingRemain.load(team_id=team.id)
    if not training_remain:
        training_remain = TrainingRemain()
        training_remain.remain_times = 5
        training_remain.team_id = team.id
        training_remain.persist()
       
    training_remain.finish_times = 5 - training_remain.remain_times
        
    datas = {'in_training': in_training, 'training_remain': training_remain, 'remain': remain}
    
    if min:
        return render_to_response(request, 'match/training_center_min.html', datas)
    return render_to_response(request, 'match/training_center.html', datas)
     
def training_center_apply(request):
    '''训练赛报名'''
    team = request.team
    success = '您已经成功的参加了街球训练!'
    error = None
    i = 0 
    while i < 1:
        i += 1
        training_center = TrainingCenter.load(team_id=team.id, status=0)
        if training_center:
            error = '您已经在训练中，无需重复报名'
            break
        training_remain = TrainingRemain.load(team_id=team.id)  
        if training_remain and training_remain.remain_times == 0:
            error = '您今天训练次数已经用完'
            break
          
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    else:
        training_center = TrainingCenter()
        training_center.team_id = team.id
        training_center.status = 0
        training_center.finish_time = ReserveLiteral('date_add(now(), interval 10 minute)')
        training_remain.remain_times -= 1
        
        TrainingRemain.transaction()
        try:
            training_center.persist()
            training_remain.persist()
            TrainingRemain.commit()
        except:
            exception_mgr.on_except()
            TrainingRemain.rollback()
            
    url = reverse('training-center-min')
    return render_to_response(request, 'message_update.html', {'success': success, 'url': url})

@login_required
def youth_tactical(request, min=False):
    """青年战术"""

    if request.method == 'GET':
        datas = {}
        team = request.team
        tactical_details = match_operator.get_tactical_details(team.id, is_youth=True)
        tactical_mains = match_operator.get_tactical_mains(team.id, is_youth=True)
        
        datas['tactical_details'] = tactical_details
        datas['sections'] = [i for i in range(1, 9)]
        
        for tactical_main in tactical_mains:
            for i in range(1, 9):
                tactical_main[i] = tactical_main['tactical_detail_%s_id' % i]
                del tactical_main['tactical_detail_%s_id' % i]
            datas['match_type_%s' % tactical_main['type']] = tactical_main

        if min:
            return render_to_response(request, 'match/youth_tactical_min.html', datas)
        return render_to_response(request, 'match/youth_tactical.html', datas)
    
    else:
        success = u'战术配置保存成功'
        error = None

        team = request.team
        youth_tactical = {'type': 4, 'team_id': team.id, 'is_youth': 1}
        for i in range(1, 9):
            youth_tactical_detail_id = request.GET.get('youth%s' % i)
            if not youth_tactical_detail_id:
                error = '战术设置不完整'
                break
            youth_tactical['tactical_detail_%s_id' % i] = youth_tactical_detail_id

        i = 0
        while i < 1:
            i += 1
            if not match_operator.save_tactical_main((youth_tactical,)):
                error = '战术配置保存失败'
                break
          
    return render_to_response(request, "message.html", {'error': error, 'success': success}) 

@login_required
def youth_tactical_detail(request):
    """青年战术详细"""
    
    if request.method == 'GET':
        seq = request.GET.get('seq', 'A')
        team = request.team
        players = player_operator.get_youth_player(team.id)
        tactical_info = match_operator.get_tactical_detail(team.id, seq, is_youth=True)
        datas = tactical_info
        datas['infos'] = players
        return render_to_response(request, 'match/youth_tactical_detail.html', datas)
    else:
        success = u'阵容保存成功'
        error = None;
        
        i = 1
        while i > 0:
            i -= 1
            cid = request.GET.get('form_c');
            pfid = request.GET.get('form_pf');
            sfid = request.GET.get('form_sf');
            sgid = request.GET.get('form_sg');
            pgid = request.GET.get('form_pg');
            name = request.GET.get('name');
            seq = request.GET.get('seq');
            offensive_tactical_type = request.GET.get('offensive_tactical_type');
            defend_tactical_type = request.GET.get('defend_tactical_type');
            
            team = None
            if hasattr(request, 'team'):
                team = request.team
            if not team:
                error = '战术更新失败,无法获取球队信息'
                break
            
            for p in (cid, pfid, sfid, sgid, pgid):
                if not p:
                    error = '阵容不完整'
                    break
                if not player_operator._check_player_is_in_team(team.id, p, is_youth=True):
                    error = '阵容中有不是您球队的球员，请重新设置'
                    break
            
            if error:
                return render_to_response(request, "message.html", {'success': success, 'error': error})
            
            if not seq:
                error = "保存战术出错"
                break
            
            info = {'team_id': team.id, 'seq': seq, 'cid': cid, 'pfid': pfid, 'sfid': sfid, 'sgid': sgid, 'pgid': pgid,
                     'offensive_tactical_type': offensive_tactical_type, 'defend_tactical_type': defend_tactical_type}

            if not name:
                if seq == 'A':
                    name = '第一节战术'
                elif seq == 'B':
                    name = '第二节战术'
                elif name == 'C':
                    name = '第三节战术'
                else:
                    name = '第四节战术'
            
            info['name'] = name
            info['is_youth'] = 1
            
            if not match_operator.save_tactical_detail(info):
                error = '战术更新失败'
        
        return render_to_response(request, "message.html", {'success': success, 'error': error})

@login_required
def profession_tactical(request, min=False):
    """list"""

    if request.method == 'GET':
        datas = {}
        team = request.team
        tactical_details = match_operator.get_tactical_details(team.id)
        tactical_mains = match_operator.get_tactical_mains(team.id)
        
        datas['tactical_details'] = tactical_details
        datas['sections'] = [i for i in range(1, 9)]
        
        for tactical_main in tactical_mains:
            for i in range(1, 9):
                tactical_main[i] = tactical_main['tactical_detail_%s_id' % i]
                del tactical_main['tactical_detail_%s_id' % i]
            datas['match_type_%s' % tactical_main['type']] = tactical_main

        if min:
            return render_to_response(request, 'match/profession_tactical_min.html', datas)
        return render_to_response(request, 'match/profession_tactical.html', datas)
    
    else:
        success = u'战术配置保存成功'
        error = None

        team = request.team
        profession_tactical = {'type': 1, 'team_id': team.id}
        cup_tactical = {'type': 2, 'team_id': team.id}
        others_tactical = {'type': 3, 'team_id': team.id}
        for i in range(1, 9):
            profession_tactical_detail_id = request.GET.get('profession%s' % i)
            cup_tactical_detail_id = request.GET.get('cup%s' % i)
            others_tactical_detail_id = request.GET.get('others%s' % i)
            if not (profession_tactical_detail_id and cup_tactical_detail_id and others_tactical_detail_id):
                error = '战术设置不完整'
                break
            profession_tactical['tactical_detail_%s_id' % i] = profession_tactical_detail_id
            cup_tactical['tactical_detail_%s_id' % i] = cup_tactical_detail_id
            others_tactical['tactical_detail_%s_id' % i] = others_tactical_detail_id
         
        i = 0
        while i < 1 and not error:
            i += 1
            if not match_operator.save_tactical_main((profession_tactical, cup_tactical, others_tactical)):
                error = '战术配置保存失败'
                break
          
    return render_to_response(request, "message.html", {'error': error, 'success': success}) 
        

@login_required
def profession_tactical_detail(request):
    """free player detail"""
    
    if request.method == 'GET':
        seq = request.GET.get('seq', 'A')
        team = request.team
        players = player_operator.get_profession_player(team.id)
        tactical_info = match_operator.get_tactical_detail(team.id, seq)
        datas = tactical_info
        datas['infos'] = players
        return render_to_response(request, 'match/profession_tactical_detail.html', datas)
    else:
        success = u'阵容保存成功'
        error = None;
        
        i = 1
        while i > 0:
            i -= 1
            cid = request.GET.get('form_c');
            pfid = request.GET.get('form_pf');
            sfid = request.GET.get('form_sf');
            sgid = request.GET.get('form_sg');
            pgid = request.GET.get('form_pg');
            name = request.GET.get('name');
            seq = request.GET.get('seq');
            offensive_tactical_type = request.GET.get('offensive_tactical_type');
            defend_tactical_type = request.GET.get('defend_tactical_type');
            
            team = None
            if hasattr(request, 'team'):
                team = request.team
            if not team:
                error = '战术更新失败,无法获取球队信息'
                break
            
            for p in (cid, pfid, sfid, sgid, pgid):
                if not p:
                    error = '阵容不完整'
                    break
                if not player_operator._check_player_is_in_team(team.id, p):
                    error = '阵容中有不是您球队的球员，请重新设置'
                    break
            
            if error:
                return render_to_response(request, "message.html", {'success': success, 'error': error})
            
            if not seq:
                error = "保存战术出错"
                break
            
            info = {'team_id': team.id, 'seq': seq, 'cid': cid, 'pfid': pfid, 'sfid': sfid, 'sgid': sgid, 'pgid': pgid,
                     'offensive_tactical_type': offensive_tactical_type, 'defend_tactical_type': defend_tactical_type}

            if not name:
                if seq == 'A':
                    name = '第一节战术'
                elif seq == 'B':
                    name = '第二节战术'
                elif name == 'C':
                    name = '第三节战术'
                else:
                    name = '第四节战术'
            
            info['name'] = name
            info['is_youth'] = 0
            
            if not match_operator.save_tactical_detail(info):
                error = '战术更新失败'
        
        return render_to_response(request, "message.html", {'success': success, 'error': error})
    
@login_required
def match_accept(request):
    '''接受比赛'''
    success = u'比赛请求己接受'
    error = None;
        
    team = None
    user_name = None
    if hasattr(request, 'team'):
        team = request.team
        if team:
            user_info = UserInfo.load(username=team.username)
            if user_info:
                user_name = user_info.nickname
    
    match_id = request.GET.get('match_id')
        
    i = 1
    while i > 0:
        i -= 1
        if not team or not user_name:
            error = '登陆信息丢失,请重新登陆!'
            break
        
        if not match_id:
            error = '无法获取比赛信息'
            break
        
        match = Matchs.load(id=match_id)
        if not match or match.status != 0:
            error = '比赛信息异常'
            break
        
#        count = Matchs.count(condition="(home_team_id=%s or guest_team_id=%s) and status <> 3" % (team.id, team.id))
#        if count > 0:
#            error = "你有一场比赛进行中,无法开始"
#            break
#        
#        team_b_id = match.host_team_id if match.host_team_id != team.id else match.guest_team_id
#        count = Matchs.count(condition="(home_team_id=%s or guest_team_id=%s) and status <> 3" % (team_b_id, team_b_id))
#        if count > 0:
#            error = "对方有一场比赛进行中,无法开始"
#            break
        
        match.status = MatchStatus.ACCP
        match.show_status = MatchShowStatus.READY
        match.next_show_status = MatchShowStatus.FIRST
        
        message = Message()
        message.type = MessageType.SYSTEM_MSG
        message.from_team_id = 0
        message.to_team_id = match.home_team_id
        message.title = u'%s经理接收了你的%s约战请求' % (user_name, MatchTypeMaps.get(match.type))
        message.content = u'%s经理接收了你的%s约战请求' % (user_name, MatchTypeMaps.get(match.type))
        message.is_new = 1
        
        Message.transaction()
        try:
            match.persist()
            message.persist()
            Message.commit()
        except:
            error = u'服务器异常'
            Message.rollback()
            break
        
        url = reverse('friendly-match-min')
    
    if error:
        return render_to_response(request, "message.html", {'success': success, 'error': error})
    return render_to_response(request, "message_update.html", {'success': success, 'error': error, 'url': url})

@login_required
def tactical_grade(request):
    '''战术等级'''
    team = request.team
    tactical_grades = TacticalGrade.query(condition='team_id="%s"' % team.id, order='tactical asc')
    if not tactical_grades:
        tactical_grades = []
        for i in range(1, 13):
            tactical_grade = TacticalGrade()
            tactical_grade.tactical = i 
            tactical_grade.team_id = team.id
            tactical_grade.point = 0
            tactical_grades.append(tactical_grade)
        
        try:
            TacticalGrade.inserts(tactical_grades)
        except:
            exception_mgr.on_except()
    
    grades = [0, 2, 4, 8, 16, 32, 64, 128, 256, 512]
    for tactical_grade in tactical_grades:
        if tactical_grade.tactical <= 6:
            tactical_grade.type = u'进攻'
            tactical_grade.style = 'color_4'
            tactical_grade.name = OffensiveTacticalTypeMap.get(tactical_grade.tactical)
        else:
            tactical_grade.type = u'防守'
            tactical_grade.style = 'red'
            tactical_grade.name = DefendTacticalTypeMap.get(tactical_grade.tactical)
        point = tactical_grade.point
        for i, grade in enumerate(grades):
            if point == 0:
                tactical_grade.level = 0
                tactical_grade.max = 2
                tactical_grade.remain_point = 0
                break
            if point <= grade:
                tactical_grade.level = i - 1
                tactical_grade.max = grade
                tactical_grade.remain_point = point - grades[i - 1]
                break
                
    datas = {'infos': tactical_grades}
    return render_to_response(request, "match/tactical_grade.html", datas)

@login_required
def profession_training(request, min=False):
    '''职业训练'''
    team = request.team
    infos = player_operator.get_profession_player(team.id, condition='is_draft=0')
    datas = {'infos': infos}
    
    for info in infos:
        info['can_training'] = True
        if info['power'] < 30 or info['training_locations'] < 200:
            info['can_training'] = False
    if min:
        return render_to_response(request, 'match/profession_training_min.html', datas)
    return render_to_response(request, 'match/profession_training.html', datas)

@login_required
def profession_training_detail(request):
    '''职业训练'''
    no = request.GET.get('no', None)
    team = request.team
    if not no:
        return render_to_response(request, 'message.html', {'error': u'球员id为空'})
    player = player_operator.get_profession_palyer_by_no(no)
    if not player:
        return render_to_response(request, 'message.html', {'error': u'球员不存在'})
    
    if player['team_id'] != team.id:
        return render_to_response(request, 'message.html', {'error': u'该球员不在您队中'})
    
    playerutil.calcul_otential(player)

    player['sd_max_add'] = 3 if player['speed_oten'] >= 3 else player['speed_oten']
    player['tt_max_add'] = 3 if player['bounce_oten'] >= 3 else player['bounce_oten']
    player['qz_max_add'] = 3 if player['strength_oten'] >= 3 else player['strength_oten']
    player['nli_max_add'] = 3 if player['stamina_oten'] >= 3 else player['stamina_oten']
    player['tlan_max_add'] = 3 if player['shooting_oten'] >= 3 else player['shooting_oten']
    player['sf_max_add'] = 3 if player['trisection_oten'] >= 3 else player['trisection_oten']
    player['yq_max_add'] = 3 if player['dribble_oten'] >= 3 else player['dribble_oten']
    player['cq_max_add'] = 3 if player['pass_oten'] >= 3 else player['pass_oten']
    player['lb_max_add'] = 3 if player['backboard_oten'] >= 3 else player['backboard_oten']
    player['qd_max_add'] = 3 if player['steal_oten'] >= 3 else player['steal_oten']
    player['fg_max_add'] = 3 if player['blocked_oten'] >= 3 else player['blocked_oten']
         
    datas = {'player': player}
    return render_to_response(request, 'match/profession_training_detail.html', datas)

@login_required
def profession_training_save(request):
    '''职业训练'''
    no = request.GET.get('no', None)
    team = request.team
    success = '球员训练成功'
    error = None
    i = 0
    while i < 1:
        i += 1
        if not no:
            error = u'球员id为空'
            break
        player = ProfessionPlayer.load(no=no)
        if not player:
            error = u'球员不存在'
            break
        
        playerutil.calcul_otential(player)
        if player.team_id != team.id:
            error = '该球员不在您队中'
            break
            
        sd_add = float(request.GET.get('sd_add', 0))
        if sd_add > player.speed_oten:
            sd_add = player.speed_oten
        player.speed += sd_add
            
        tt_add = float(request.GET.get('tt_add', 0))
        if tt_add > player.bounce_oten:
            tt_add = player.bounce_oten
        player.bounce += tt_add
            
        qz_add = float(request.GET.get('qz_add', 0))
        if qz_add > player.strength_oten:
            qz_add = player.strength_oten
        player.strength += qz_add
            
        nli_add = float(request.GET.get('nli_add', 0))
        if nli_add > player.stamina_oten:
            nli_add = player.stamina_oten
        player.stamina += nli_add
        
        tlan_add = float(request.GET.get('tlan_add', 0))
        if tlan_add > player.shooting_oten:
            tlan_add = player.shooting_oten
        player.shooting += tlan_add
            
        sf_add = float(request.GET.get('sf_add', 0))
        if sf_add > player.trisection_oten:
            sf_add = player.trisection_oten
        player.trisection += sf_add
            
        yq_add = float(request.GET.get('yq_add', 0))
        if yq_add > player.dribble_oten:
            yq_add = player.dribble_oten
        player.dribble += yq_add
            
        cq_add = float(request.GET.get('cq_add', 0))
        if cq_add > player.pass_oten:
            cq_add = player.pass_oten
        old_attr = getattr(player, 'pass')
        old_attr += cq_add
        setattr(player, 'pass', old_attr)
            
        lb_add = float(request.GET.get('lb_add', 0))
        if lb_add > player.backboard_oten:
            lb_add = player.backboard_oten
        player.backboard += lb_add
            
        qd_add = float(request.GET.get('qd_add', 0))
        if qd_add > player.steal_oten:
            qd_add = player.steal_oten
        player.steal += qd_add
        
        fg_add = float(request.GET.get('fg_add', 0))
        if fg_add > player.blocked_oten:
            fg_add = player.blocked_oten
        player.blocked += fg_add
        
        if sd_add > 3 or tt_add > 3 or qz_add > 3 or nli_add > 3 or tlan_add > 3 or sf_add > 3 \
            or yq_add > 3 or cq_add > 3 or lb_add > 3 or qd_add > 3 or fg_add > 3:
            error = u'训练异常'
            break
        
        total_add = sd_add + tt_add + qz_add + nli_add + tlan_add + sf_add + yq_add + cq_add + lb_add + qd_add + fg_add
        
        if player.power - total_add < 30:
            error = u'球队员体不足，不能训练'
            break
        else: 
            player.power = player.power - total_add
        
        if player.training_locations < total_add * 200:
            error = u'球员训练点不足'
            break
        else:
            player.training_locations = player.training_locations - total_add * 200
            
        playerutil.calcul_ability(player)
        try:
            player.persist()
        except:
            exception_mgr.on_except()
            error = '服务器异常'
        
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    url = '%s?no=%s' % (reverse('profession-training-detail'), no)
    return render_to_response(request, 'message_update.html', {'success': success, 'url': url})

@login_required
def challenge_main(request, min=False):
    '''胜者为王'''
    team = request.team
    challenge_pool = ChallengePool.load(team_id=team.id)
    apply = False
    waiting_time = 0
    remain_time = 0
    home_team = None
    guest_team = None
    point_data = None
    entering = False
    statistics = False
    match_nodosity_main = None
    pre_match_nodosity_main = None #上一节比赛，用于显示分数
    show_point = False
    match = None
    finish = False
    win = False
    if challenge_pool:
        print challenge_pool.team_id
        apply = True
        if challenge_pool.status == 1:
            cursor = connection.cursor()
            try:
                rs = cursor.fetchone('select unix_timestamp(now()) - unix_timestamp(start_wait_time)  ' \
                                     'as waiting_time from challenge_pool where team_id="%s"' % team.id)
                if rs:
                    waiting_time = rs['waiting_time']
            finally:
                cursor.close()
        elif challenge_pool.status == 2:
            match = Matchs.load(id=challenge_pool.match_id)
            if match:
                cursor = connection.cursor()
                try:
                    rs = cursor.fetchone('select unix_timestamp(next_status_time) - unix_timestamp(now())  ' \
                                     'as remain_time from matchs where id="%s"' % match.id)
                    if rs:
                        remain_time = rs['remain_time']
                    #如果已经到了下一个状态的时间，则在这自己改状态
                    if remain_time < 0:
                        new_match, interval = commonutil.next_status(match)
                        new_match.id = match.id
                        new_match.persist()
                        match = Matchs.load(id=new_match.id)
                        remain_time = interval
                finally:
                    cursor.close()
            
            
            home_team = Team.load(id=match.home_team_id)
            guest_team = Team.load(id=match.guest_team_id)
            
            seq = 0
            if match.show_status == MatchShowStatus.READY:
                entering = True
            elif match.show_status <= 11 and match.show_status >= 2:
                seq = match.show_status - 1
                if seq >= 2:
                    pre_seq = seq - 1
                    pre_match_nodosity_main = MatchNodosityMain.load(match_id=match.id, seq=pre_seq)
                match_nodosity_main = MatchNodosityMain.load(match_id=match.id, seq=seq)
                
            elif match.show_status == MatchShowStatus.FINISH or match.show_status == MatchShowStatus.STATISTICS:#比赛已经完成
                if match.show_status == MatchShowStatus.FINISH:
                    finish = True
                if match.show_status == MatchShowStatus.STATISTICS:
                    statistics = True
                match_nodosity_main = MatchNodosityMain.query(condition='match_id="%s"' % match.id, order='seq desc', limit=1)
                match_nodosity_main = match_nodosity_main[0]
                
                home_point, guest_point = commonutil.get_point_from_str(match.point)
                if (team.id == match.home_team_id and home_point > guest_point) or \
                        (team.id != match.home_team_id and home_point < guest_point):
                    win = True
            
            if finish or statistics:
                if match_nodosity_main:
                    show_point = True
                    point_data = commonutil.change_point_to_score_card(match_nodosity_main.point)
            else:
                if pre_match_nodosity_main:
                    show_point = True
                    point_data = commonutil.change_point_to_score_card(pre_match_nodosity_main.point)
    
    pool_count = ChallengePool.count()
                
    datas = {'challenge_pool': challenge_pool, 'apply': apply, 'waiting_time': waiting_time, \
             'home_team': home_team, 'guest_team': guest_team, 'entering': entering, \
             'match_nodosity_main': match_nodosity_main, 'finish': finish, 'match': match, \
             'win': win, 'remain_time': remain_time, 'statistics': statistics, 'show_point': show_point, \
             'pool_count': pool_count}
    
    if challenge_pool:
        datas['seq'] = challenge_pool.win_count + 1
    
    if point_data:
        datas.update(point_data)
    if min:
        return render_to_response(request, 'match/challenge_main_min.html', datas)
    return render_to_response(request, 'match/challenge_main.html', datas)

@login_required
def challenge_apply(request):
    '''胜者为王,报名,或者继续'''
    
    team = request.team
    error = None
    
    challenge_pool = ChallengePool.load(team_id=team.id)
    apply = True
    i = 0
    while i < 1:
        i += 1
        if challenge_pool:#是继续的
            #判断一下是不是赢了,赢了才能继续
            match_id = challenge_pool.match_id
            match = Matchs.load(id=match_id)
            if match.show_status != MatchShowStatus.FINISH:
                error = '你有一场比赛正在进行中'
                break
            home_point, guest_point = commonutil.get_point_from_str(match.point)
            if (team.id == match.home_team_id and home_point > guest_point) or \
               (team.id != match.home_team_id and home_point < guest_point):
                if challenge_pool.win_count >= 8:
                    error = '您已经连胜了9场,不能继续了!'
                    break
                challenge_pool.win_count += 1
                challenge_pool.status = 1
                challenge_pool.start_wait_time = ReserveLiteral('now()')
            else:
                apply = False
        else:#是新报名的
            challenge_pool = ChallengePool()
            challenge_pool.win_count = 0
            challenge_pool.start_wait_time = ReserveLiteral('now()')
            challenge_pool.status = 1 #等待中
            challenge_pool.team_id = team.id
            challenge_pool.ability = team.agv_ability
            
            #新报名的时候放一条记录到challenge_team 表中
            challenge_team = ChallengeTeam()
            challenge_team.team_id = team.id
            challenge_team.persist()
            challenge_all = ChallengeAll()
            challenge_all.team_id = team.id
            challenge_all.persist()
            
        try:
            if apply:
                challenge_pool.persist()
            else:
                challenge_pool.delete()
        except:
            exception_mgr.on_except()
            error = '服务器异常'
        
    datas = {'challenge_pool': challenge_pool, 'apply': apply}
    if challenge_pool:
        datas['seq'] = challenge_pool.win_count + 1
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    return render_to_response(request, 'match/challenge_main_min.html', datas)

@login_required
def challenge_out(request):
    '''胜者为王,退出'''
    
    team = request.team
    error = None
    
    challenge_pool = ChallengePool.load(team_id=team.id)
    win = False
    i = 0
    while i < 1:
        i += 1
        if challenge_pool:#
            match_id = challenge_pool.match_id
            match = Matchs.load(id=match_id)
            if match.show_status != MatchShowStatus.FINISH:
                error = '你有一场比赛正在进行中'
                break
            home_point, guest_point = commonutil.get_point_from_str(match.point)
            if (team.id == match.home_team_id and home_point > guest_point) or \
               (team.id != match.home_team_id and home_point < guest_point):
                win = True
        else:#是新报名的
            error = '你没有报名参加比赛'
            break

    datas = {'challenge_pool': challenge_pool, 'apply': False}
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if win:
        #因为win_count 是在点继续的时候加的，所以实际赢的场数应该是 win_count + 1
        win_count = challenge_pool.win_count + 1
        datas['win_count'] = win_count
        return render_to_response(request, 'match/challenge_out.html', datas)
    else:
        try:
            challenge_pool.delete()
        except:
            exception_mgr.on_except()
            error = '服务器异常'
        pool_count = ChallengePool.count()
        datas['pool_count'] = pool_count
        return render_to_response(request, 'match/challenge_main_min.html', datas)
    
@login_required
def challenge_out_confirm(request):
    '''胜者为王,确定退出'''
    
    team = request.team
    error = None
    challenge_pool = ChallengePool.load(team_id=team.id)
    win = False
    i = 0
    while i < 1:
        i += 1
        if challenge_pool:#
            match_id = challenge_pool.match_id
            match = Matchs.load(id=match_id)
            if match.show_status != MatchShowStatus.FINISH:
                error = '你有一场比赛正在进行中'
                break
            home_point, guest_point = commonutil.get_point_from_str(match.point)
            if (team.id == match.home_team_id and home_point > guest_point) or \
               (team.id != match.home_team_id and home_point < guest_point):
                win = True
        else:#是新报名的
            error = '你没有报名参加比赛'
            break

    datas = {'challenge_pool': challenge_pool, 'apply': False}
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if win:
        #因为win_count 是在点继续的时候加的，所以实际赢的场数应该是 win_count + 1
        win_count = challenge_pool.win_count + 1
        point = 0
        if win_count == 3:
            point = 3
        elif win_count == 6:
            point = 6
        elif win_count == 9:
            point = 21
        challenge_team = ChallengeTeam.load(team_id=team.id)
        if not challenge_team:
            challenge_team = ChallengeTeam()
            challenge_team.team_id = team.id
            challenge_team.point = 0
            challenge_team.win_count = win_count
        challenge_team.point += point
        if challenge_team.win_count < win_count:
            challenge_team.win_count = win_count
            
        challenge_all = ChallengeAll.load(team_id=team.id)
        if not challenge_all:
            challenge_all = ChallengeAll()
            challenge_all.team_id = team.id
            challenge_all.point = 0
            challenge_all.win_count = win_count
        challenge_all.point += point
        if challenge_all.win_count < win_count:
            challenge_all.win_count = win_count
        
    ChallengeTeam.transaction()
    try:
        if win:
            challenge_team.persist()
            challenge_all.persist()
        if point > 0 and team.union_id:#联盟威望
            union = Unions.load(id=team.union_id)
            union.prestige += point
            union_honor = UnionHonor.load(team_id=team.id)
            if not union_honor:
                union_honor = UnionHonor()
                union_honor.team_id = team.id
                union_honor.union_id = team.union_id
                union_honor.prestige = 0
            union_honor.prestige += point
            union_honor_detail = UnionHonorDetail()
            union_honor_detail.team_id = team.id
            union_honor_detail.union_id = team.union_id
            union_honor_detail.prestige = point
            union_honor_detail.type = UnionPrestigeFromType.ChallengeMatch
            
            union.persist()
            union_honor.persist()
            union_honor_detail.persist()
            
        challenge_pool.delete()
        ChallengeTeam.commit()
    except:
        ChallengeTeam.rollback()
        exception_mgr.on_except()
        raise
        
    pool_count = ChallengePool.count()
    datas['pool_count'] = pool_count
    return render_to_response(request, 'match/challenge_main_min.html', datas)

@login_required
def team_challenge(request):
    '''胜者为王,我的比赛'''
    
    team = request.team
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    infos, total = ChallengeHistory.paging(page, pagesize, condition='(home_team_id="%s" or guest_team_id="%s") and finish=1' % (team.id, team.id), order='id desc')
        
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    return render_to_response(request, 'match/team_challenge_min.html', datas)

@login_required
def challenge_today_sort(request):
    '''胜者为王，今日排名'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    infos, total = ChallengeTeam.paging(page, pagesize, order='point desc')
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
        
    for i, info in enumerate(infos):
        info.sort = (page - 1) * pagesize + i + 1
        
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, 'match/challenge_today_sort.html', datas)

@login_required
def challenge_all_sort(request):
    '''胜者为王，历史排名'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    infos, total = ChallengeAll.paging(page, pagesize, order='point desc')
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total - 1) / pagesize + 1
        
    for i, info in enumerate(infos):
        info.sort = (page - 1) * pagesize + i + 1
        
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
    
    return render_to_response(request, 'match/challenge_all_sort.html', datas)
