#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.business.user_roles import login_required, UserManager
from gba.business import player_operator, match_operator

from gba.entity import Team, Matchs, ProfessionPlayer, TrainingCenter, \
                       TeamTactical, TeamTacticalDetail, YouthPlayer, \
                       MatchNotInPlayer, UserInfo, TacticalGrade, \
                       TrainingRemain, Message
from gba.common.constants import MatchTypes, DefendTacticalTypeMap, OffensiveTacticalTypeMap
from gba.common.constants import MatchStatus, MatchShowStatus, MessageType, MatchTypeMaps
from gba.common import exception_mgr
from gba.common import playerutil
from gba.common.db.reserve_convertor import ReserveLiteral


@login_required
def friendly_match(request, min=False):
    
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    
    team = UserManager().get_team_info(request)
    infos, total = match_operator.get_match(team.id, MatchTypes.FRIENDLY, page, pagesize)

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
        player_no = stat['player_no']
        player = ProfessionPlayer.load(no=player_no)
        stat['name'] = player.name
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
        player_no = stat['player_no']
        player = ProfessionPlayer.load(no=player_no)
        stat['name'] = player.name
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
    
    #team = UserManager().get_team_info(request)
    match_nodosity_mains = match_operator.get_match_nodosity_main(match_id)
    
    for match_nodosity_main in match_nodosity_mains:
        match_nodosity_details = match_operator.get_match_nodosity_detail(match_nodosity_main['id'])
        print match_nodosity_details
        match_nodosity_main['details'] = match_nodosity_details
        
    datas = {'match_nodosity_mains': match_nodosity_mains}
    return render_to_response(request, 'match/match_detail.html', datas)

@login_required
def training_center(request, min=False):
    '''训练中心'''
    team = request.team
    training_center = TrainingCenter.load(team_id=team.id, status=0)
    remain = 0
    if training_center:
        remain = match_operator.get_training_remain(team.id)
        if remain < 0:
            remain = 0
        remain = remain / 100 * 60
        in_training = True
    else:
        in_training = False
    
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
def youth_tactical(request):
    """青年战术"""

    datas = {}
    team = UserManager().get_team_info(request)
    tactical_details = TeamTacticalDetail.query(condition="team_id=%s and is_youth=1" % team.id, order="seq asc")
    tactical_mains = TeamTactical.query(condition="team_id=%s and is_youth=1" % team.id, order="type asc")
    
    datas['tactical_details'] = tactical_details
    datas['sections'] = [i for i in range(1, 9)]
    
    for tactical_main in tactical_mains:
        for i in range(1, 9):
            setattr(tactical_main, str(i), getattr(tactical_main, 'tactical_detail_%s_id' % i))
            delattr(tactical_main, 'tactical_detail_%s_id' % i)
        datas['match_type_%s' % tactical_main.type] = tactical_main
    
    datas['sort'] = 'W'         
    return render_to_response(request, 'match/youth_tactical.html', datas)

@login_required
def youth_tactical_detail(request):
    """青年战术详细"""
    
    sort = request.GET.get('sort', 'A')
    
    team = UserManager().get_team_info(request)
    
    players = YouthPlayer.query(condition="team_id=%s" % team.id, order='ability desc')
    tactical_info = TeamTacticalDetail.query(condition="team_id=%s and seq='%s' and is_youth=1" % (team.id, sort), limit=1)
    tactical_info = tactical_info[0]
    
    ret_players = []
    tactical_detail_info = {tactical_info.pgid: 'pg_info', tactical_info.sfid: 'sf_info', tactical_info.sgid: 'sg_info', \
                          tactical_info.pfid: 'pf_info', tactical_info.cid: 'c_info'}

    datas = {'sort': sort}
    for player in players:
        if player.no in tactical_detail_info:
            datas[tactical_detail_info[player.no]] = player
        else:
            ret_players.append(player)
            
    datas['infos'] = ret_players      
    datas['tactical_detail_name'] = tactical_info.name
    datas['tactical_info'] = tactical_info
    return render_to_response(request, 'match/youth_tactical_detail.html', datas)

@login_required
def profession_tactical(request, min=False):
    """list"""

    if request.method == 'GET':
        datas = {}
        team = UserManager().get_team_info(request)
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
        user_info = UserManager().get_userinfo(request)
        username = user_info['username']
        team = Team.load(username=username)
        players = player_operator.get_profession_player(team.id)
        tactical_info = match_operator.get_tactical_detail(team.id, seq)
        datas = tactical_info
        datas['infos'] = players
        return render_to_response(request, 'match/profession_tactical_detail.html', datas)
    else:
        success =  u'阵容保存成功'
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
            
            if not match_operator.save_tactical_detail(info):
                error = '战术更新失败'
        
        return render_to_response(request, "message.html", {'success': success, 'error': error})
    
@login_required
def match_accept(request):
    '''接受比赛'''
    success =  u'比赛请求己接受'
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
        
        if match.is_youth == 1:
            url = ''
        else:
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
                tactical_grade.remain_point = point - grades[i-1]
                break
                
    datas = {'infos': tactical_grades}
    return render_to_response(request, "match/tactical_grade.html", datas)

@login_required
def profession_training(request, min=False):
    '''职业训练'''
    team = request.team
    infos = player_operator.get_profession_player(team.id)
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

    player['sd_max_add']  = 3 if player['speed_oten'] >= 3 else player['speed_oten']
    player['tt_max_add']  = 3 if player['bounce_oten'] >= 3 else player['bounce_oten']
    player['qz_max_add']  = 3 if player['strength_oten'] >= 3 else player['strength_oten']
    player['nli_max_add']  = 3 if player['stamina_oten'] >= 3 else player['stamina_oten']
    player['tlan_max_add']  = 3 if player['shooting_oten'] >= 3 else player['shooting_oten']
    player['sf_max_add']  = 3 if player['trisection_oten'] >= 3 else player['trisection_oten']
    player['yq_max_add']  = 3 if player['dribble_oten'] >= 3 else player['dribble_oten']
    player['cq_max_add']  = 3 if player['pass_oten'] >= 3 else player['pass_oten']
    player['lb_max_add']  = 3 if player['backboard_oten'] >= 3 else player['backboard_oten']
    player['qd_max_add']  = 3 if player['steal_oten'] >= 3 else player['steal_oten']
    player['fg_max_add']  = 3 if player['blocked_oten'] >= 3 else player['blocked_oten']
         
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
        if sd_add >  player.speed_oten:
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
        
        total_add =  sd_add + tt_add + qz_add + nli_add + tlan_add + sf_add + yq_add + cq_add + lb_add + qd_add + fg_add
        
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