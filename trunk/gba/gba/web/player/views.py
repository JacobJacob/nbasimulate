#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.common import playerutil, exception_mgr
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.business import player_operator
from gba.business.user_roles import login_required, UserManager
from gba.entity import Team, YouthPlayer, FreePlayer, YouthFreePlayer, \
                       ProfessionPlayer, ProPlayerSeasonStatTotal, ProPlayerCareerStatTotal, \
                       DraftPlayer, AttentionPlayer, LeagueConfig, SeasonFinance, AllFinance, \
                       YouthPlayerCareerStatTotal, YouthPlayerSeasonStatTotal
from gba.common.constants import attributes, hide_attributes, AttributeMaps, MarketType, \
                                 FinanceSubType, FinanceType, PlayerStatus

@login_required
def index(request, min=False):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    position = request.GET.get('position', 'c')
    order_by = request.GET.get('order_by', 'id')
    order = request.GET.get('order', 'asc')
    
    infos, total = player_operator.get_free_palyer(page, pagesize, position, order_by, order)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'position': position, 'order_by': order_by, 
            'order': order}
    
    if min:
        return render_to_response(request, 'player/free_players_min.html', datas)
    return render_to_response(request, 'player/free_players.html', datas)

@login_required
def freeplayer_detail(request):
    """free player detail"""
    no = request.GET.get('no', None)
    
    error = None
    
    if not no:
        error = u'获取球员信息出错'
    else:
        player = player_operator.get_free_palyer_by_no(no)
        if not player:
            error = u'获取球员信息出错'

    datas = {'player': player}
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    return render_to_response(request, 'player/common_detail.html', datas)

@login_required
def profession_player(request, min=False):
    '''profession player'''
     
    team = request.team
     
    infos = player_operator.get_profession_player(team.id)
    datas = {'infos': infos}
    
    for info in infos:
        if info['contract'] != None and info['contract'] < 13 and info['is_draft'] == 0:
            info['need_renew'] = 1
        else:
            info['need_renew'] = 0
    
    if min:
        return render_to_response(request, 'player/profession_player_min.html', datas)
    return render_to_response(request, 'player/profession_player.html', datas)

@login_required
def profession_player_detail(request):
    """profession player detail"""
    no = request.GET.get('no', None)
    team = request.team
    if not no:
        return render_to_response(request, 'message.html', {'error': u'球员id为空'})
    player = ProfessionPlayer.load(no=no)
    if not player or player.is_draft == 1:
        return render_to_response(request, 'message.html', {'error': u'球员不存在'})
    if player.team_id != team.id:
        return render_to_response(request, 'message.html', {'error': u'球员不在队中'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]

    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
        
    season_stat_total = ProPlayerSeasonStatTotal.load(player_no=no)
    if not season_stat_total:
        season_stat_total = ProPlayerSeasonStatTotal()
        season_stat_total.player_no = no
        season_stat_total.persist()
        season_stat_total = ProPlayerSeasonStatTotal.load(player_no=no)
    
    career_stat_total = ProPlayerCareerStatTotal.load(player_no=no)
    if not career_stat_total:
        career_stat_total = ProPlayerCareerStatTotal()
        career_stat_total.player_no = no
        career_stat_total.persist()
        career_stat_total = ProPlayerCareerStatTotal.load(player_no=no)
    
    use_nos = []   
    players = ProfessionPlayer.query(condition='team_id="%s"' % team.id)
        
    for p in players:
        use_nos.append(p.no)
        
    free_nos = []
    for i in range(1, 110):
        if i not in use_nos:
            free_nos.append(i)

    datas = {'id': id, 'player': player, 'attributes': attributes_maps, \
             'season_stat_total': season_stat_total, 'career_stat_total': career_stat_total, \
             'free_nos': free_nos}
    
    return render_to_response(request, 'player/player_detail.html', datas)

@login_required
def youth_free_player(request, min=False):
    """list"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    position = request.GET.get('position', 'c')
    order_by = request.GET.get('order_by', 'expired_time')
    order = request.GET.get('order', 'asc')
    
    infos, total = player_operator.get_youth_freepalyer(page, pagesize, position, order_by, order)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
        
        
    for info in infos:
        if info['lave_time'] > 0:
            info['can_bid'] = True
        else:
            info['can_bid'] = False
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'position': position, 'order_by': order_by, 
            'order': order}
    
    if min:
        return render_to_response(request, 'player/youth_free_players_min.html', datas)
    return render_to_response(request, 'player/youth_free_players.html', datas)

@login_required
def youth_freeplayer_detail(request):
    """free player detail"""
    no = request.GET.get('no', None)
    print no
    if not no:
        return render_to_response(request, 'default_error.html', {'msg': u'球员id为空'})
    player = player_operator.get_youth_freepalyer_by_no(no)
    if not player:
        return render_to_response(request, 'default_error.html', {'msg': u'球员不存在'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]
    
    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
    
    datas = {'id': id, 'player': player, 'attributes': attributes_maps}
    return render_to_response(request, 'player/youth_freeplayer_detail.html', datas)

@login_required
def youth_player(request):
    '''youth player'''
    
    team = request.team
    infos = player_operator.get_youth_player(team.id)
    
    for info in infos:
        if info['in_tactical']:
            info['termination'] = False
            info['sell'] = False
            info['promoted'] = False
        else:
            info['termination'] = True
            if info['age'] >= 20:
                info['sell'] = True
                info['promoted'] = True
            else:
                info['sell'] = False
                info['promoted'] = False
            
    datas = {'infos': infos}
    
    return render_to_response(request, 'player/youth_player.html', datas)

@login_required
def youth_player_detail(request):
    """youth player detail"""
    no = request.GET.get('no', None)
    team = request.team
    if not no:
        return render_to_response(request, 'message.html', {'error': u'球员id为空'})
    player = YouthPlayer.load(no=no)
    if not player:
        return render_to_response(request, 'message.html', {'error': u'球员不存在'})
    if player.team_id != team.id:
        return render_to_response(request, 'message.html', {'error': u'球员不在队中'})
    
    playerutil.calcul_otential(player)
    show_attributes = [i for i in attributes if i not in hide_attributes]

    attributes_maps = {}
    for attribute in show_attributes:
        attributes_maps[attribute] = '%s_oten' % attribute
        
    season_stat_total = YouthPlayerSeasonStatTotal.load(player_no=no)
    if not season_stat_total:
        season_stat_total = YouthPlayerSeasonStatTotal()
        season_stat_total.player_no = no
        season_stat_total.persist()
        season_stat_total = YouthPlayerSeasonStatTotal.load(player_no=no)
    
    career_stat_total = YouthPlayerCareerStatTotal.load(player_no=no)
    if not career_stat_total:
        career_stat_total = YouthPlayerCareerStatTotal()
        career_stat_total.player_no = no
        career_stat_total.persist()
        career_stat_total = YouthPlayerCareerStatTotal.load(player_no=no)
    
    use_nos = []   
    players = YouthPlayer.query(condition='team_id="%s"' % team.id)
        
    for p in players:
        use_nos.append(p.no)
        
    free_nos = []
    for i in range(1, 110):
        if i not in use_nos:
            free_nos.append(i)

    datas = {'id': id, 'player': player, 'attributes': attributes_maps, \
             'season_stat_total': season_stat_total, 'career_stat_total': career_stat_total, \
             'free_nos': free_nos}
    
    return render_to_response(request, 'player/player_detail.html', datas)


@login_required
def attention_player(request, min=False):
    '''关注球员'''
    team = UserManager().get_team_info(request)
    infos = player_operator.get_attention_player(team.id)
    datas = {'infos': infos}
    if min:
        return render_to_response(request, 'player/attention_players_min.html', datas)
    return render_to_response(request, 'player/attention_players.html', datas)
    
@login_required
def free_player_bid(request):
    """自由球员出价"""
    
    if request.method == 'GET':
        datas = {}
        error = None
        no = request.GET.get('no')
        if not no:
            error = '球员不存在'
        
        min_price = 1000
        if no:
            player = FreePlayer.load(no=no)
            if not player:
                error = '球员不存在'
            else:
                current_price = player.current_price
                min_price = (current_price * 1.1)
                datas['min_price'] = min_price
                datas['no'] = no
        
        if error:
            return render_to_response(request, 'message.html', {'error': error})  
        return render_to_response(request, 'player/free_player_bid.html', datas)
    else:
        error = None
        success = "竟价成功"
        no = request.GET.get('no')
        price = request.GET.get('price')
        
        if not no or not price:
            error = '出价异常'
    
        if not error:
            print request.team.id
            result = player_operator.freeplayer_auction(request.team.id, no, price)
        
            if result == -1:
                error = '未知异常'
            elif result == -2:
                error = '您的资金不不足'
            elif result == -3:
                error = '您己经出过价了,不能再出价'
            elif result == -4:
                error = '球员不存在'
            elif result == -5:
                error = '您的出价低于该球员最低身价'
        
        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})

@login_required
def youth_free_player_bid(request):
    """自由球员出价"""
    
    if request.method == 'GET':
        datas = {}
        error = None
        no = request.GET.get('no')
        if not no:
            error = '球员不存在'
        
        if no:
            player = YouthFreePlayer.load(no=no)
            if not player:
                error = '球员不存在'
            else:
                datas['no'] = no
        
        if error:
            return render_to_response(request, 'message.html', {'error': error})  
        return render_to_response(request, 'player/youth_free_player_bid.html', datas)
    else:
        error = None
        success = "竟价成功"
        no = request.GET.get('no')
        price = request.GET.get('price')
        
        if not no or not price:
            error = '出价异常'
    
        if not error:
            result = player_operator.youth_freeplayer_auction(request.team.id, no, price)
        
            if result == -1:
                error = '未知异常'
            elif result == -2:
                error = '您的资金不不足'
            elif result == -3:
                error = '您己经出过价了,不能再出价'
            elif result == -4:
                error = '球员不存在'

        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})

@login_required
def add_attention_player(request):
    '''关注球员'''
    if request.method == 'GET':
        success = '球员关注成功'
        error = None
        type = request.GET.get('type')
        no = request.GET.get('no')
        if not (type and no):
            error = '获取信息出错'
        
        if not error:
            result = player_operator.attention_player(request.team.id, no, type)
            if result == 2:
                error = '该球员您已经关注过了'
            elif result == 3:
                error = '最多只能关注10个球员'
            elif result == -1:
                error = '应用服务器异常'
        
        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})

@login_required
def remove_attention_player(request):
    '''取消关注球员'''
    if request.method == 'GET':
        no = request.GET.get('no')
        return render_to_response(request, 'player/remove_attention_player.html', {'no': no})

    else:
        success = '取消关注成功'
        error = None
        no = request.GET.get('no')
        if player_operator.cancel_attention_player(request.team.id, no) == -1:
            error = '应用服务器异常'
        
        if not error:
            url = reverse('attention-player-min')
            return render_to_response(request, 'message_update.html', {'error': error, 'success': success, 'url': url})
        return render_to_response(request, 'message.html', {'error': error, 'success': success})
    
@login_required
def player_detail(request):
    """player detail"""
    no = request.GET.get('no', None)
    type = request.GET.get('type', None)
    from_page = request.GET.get('from_page', None)
    from_id = request.GET.get('from_id', None)
    error = None
    
    if not no or not type:
        error = u'获取球员信息出错'
    else:
        type = int(type)
        if type == 1:
            player = FreePlayer.load(no=no)
        elif type == 2:
            player = ProfessionPlayer.load(no=no)
        elif type == 3:
            player = YouthPlayer.load(no=no)
        elif type == 4:
            player = YouthFreePlayer.load(no=no)
        elif type == 5:
            player = DraftPlayer.load(no=no)
        if not player:
            error = u'获取球员信息出错'
        datas = {'player': player}
        
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if from_page:
        datas['url'] = reverse(from_page)
        datas['from_id'] = from_id
    if type == 5:
        return render_to_response(request, 'player/draft_detail.html', datas)
    return render_to_response(request, 'player/common_detail.html', datas)

@login_required
def player_update(request):
    """player detail"""
    no = request.GET.get('no', None)
    type = request.GET.get('type', None)
    name = request.GET.get('name', None)
    number = request.GET.get('number', None)
    error = None
    if not no or not type:
        error = u'获取球员信息出错'
    else:
        type = int(type)
        if type == 2:
            player = ProfessionPlayer.load(no=no)
        elif type == 3:
            player = YouthPlayer.load(no=no)
   
        if not player:
            error = u'获取球员信息出错'
        player.player_no = number
        player.name = name
        player.persist()
    
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    if type == 2: 
        url = '%s?no=%s' % (reverse('profession-player-detail'), no)
    return render_to_response(request, 'message_update.html', {'success': u'球员信息修改成功', 'sub_url': url})

@login_required
def draft_player(request, min=False):
    """选秀球员"""
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 10))
    position = request.GET.get('position', 'c')
    order_by = request.GET.get('order_by', 'expired_time')
    order = request.GET.get('order', 'asc')
    
    infos, total = DraftPlayer.paging(page, pagesize, condition="position='%s'" % position, order="%s %s" % (order_by, order))
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, \
            'nextpage': page + 1, 'prevpage': page - 1, 'position': position, 'order_by': order_by, 
            'order': order}
    
    if min:
        return render_to_response(request, 'player/draft_players_min.html', datas)
    return render_to_response(request, 'player/draft_players.html', datas)

@login_required
def draft_player_bid(request):
    '''试训'''
    team = request.team
    no = request.GET.get('no')
    error = None
    i = 0
    while i < 1:
        i += 1
        if not no:
            error = '球员不存在'
            break
        
        player = DraftPlayer.load(no=no)
        if not player:
            error = '球员不存在'
            break
        
        if player.status == 1: #1代表现在在其它队试训中
            error = '该球员目前在其它队试训中'
            break
        
        draft_count = ProfessionPlayer.count(condition='team_id="%s" and is_draft=1' % team.id)
        if draft_count >= 2:
            error = '您同一时间最多只能试训2名球员'
            break
            
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if request.method == 'GET':
        return render_to_response(request, 'player/draft_player_bid.html', {'player': player})
    else:
        success = '试训成功'
        pro_player = playerutil.copy_player(player, 'draft_player', 'profession_player')    
        pro_player.is_draft = 1
        pro_player.team_id = team.id
        n = 0
        while n < 100:
            n += 1
            p = ProfessionPlayer.load(team_id=team.id, player_no=n)
            if not p:
                pro_player.player_no = n
                break
        player.status = 1
        player.bid_count += 1
        player.current_team_id = team.id
        
        attention_player = AttentionPlayer()
        attention_player.team_id = team.id
        attention_player.no = no
        attention_player.type = MarketType.DRAFT
        
        DraftPlayer.transaction()
        try:
            pro_player.persist()
            player.persist()
            attention_player.persist()
            DraftPlayer.commit()
        except:
            DraftPlayer.rollback()
            error  = '服务器异常'
            exception_mgr.on_except()
            
        if error:
            return render_to_response(request, 'message.html', {'error': error})
        return render_to_response(request, 'message.html', {'success': success})
    

def pro_player_renew(request):
    '''职业球员续约'''
    team = request.team
    no = request.GET.get('no')
    error = None
    datas = {}
    i = 0 
    while i < 1:
        i += 1
        if not no:
            error = '球员不存在'
            break
        
        player = ProfessionPlayer.load(no=no)
        
        wage = playerutil.calcul_wage(player, ran=False)
        
        datas['wage13'] = int(wage)
        datas['wage26'] = int(wage * 1.2)
        datas['wage39'] = int(wage * 1.4)
        
        if not player:
            error = '球员不存在'
            break
        
        datas['player'] = player
        
        if player.contract >= 13 or player.is_draft == 1:
            error = '该球员不能续约'
            break
        
        if player.team_id != team.id:
            error = '该球员不在您队中'
            break
        
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    
    if request.method == 'GET':
        return render_to_response(request, 'player/pro_player_renew.html', datas)
    else:
        success = '续约成功!'
        round = int(request.GET.get('round'))
        wage = playerutil.calcul_wage(player, ran=False)
        
        if round == 26:
            wage = int(wage * 1.2)
        elif round == 39:
            wage = int(wage * 1.4)
        
        player.wage = wage
        if player.contract != None:
            player.contract += round
        else:
            player.contract = round
        player.persist()
             
        url = reverse('profession-player-min')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})
    
def pro_player_sell(request):
    '''职业球员出售'''
    team = request.team
    no = request.GET.get('no')
    error = None
    datas = {}
    i = 0 
    while i < 1:
        i += 1
        if not no:
            error = '球员不存在'
            break
        
        player = ProfessionPlayer.load(no=no)
        
        if not player:
            error = '球员不存在'
            break
        
        datas['player'] = player

        if player.team_id != team.id:
            error = '该球员不在您队中'
            break
        
        social_status = playerutil.calcul_social_status(player)
        datas['social_status'] = social_status
        
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if request.method == 'GET':
        return render_to_response(request, 'player/pro_player_sell.html', datas)
    else:
        success = '出售成功!'
        
        social_status = playerutil.calcul_social_status(player)
        free_player = playerutil.copy_player(player, 'profession_player', 'free_player') 
        free_player.social_status = social_status
        free_player.expired_time = ReserveLiteral('date_add(now(), interval 2 day)')
        
        league_config = LeagueConfig.load(id=1)
        #赛季支出明细
        tinance = SeasonFinance()
        tinance.sub_type = FinanceSubType.SELL_PLAYER
        tinance.team_id = team.id
        tinance.type = FinanceType.INCOME
        tinance.season = league_config.season
        tinance.round = league_config.round
        tinance.info = u'出售球员%s' % player.name
        tinance.income = social_status
        tinance.outlay = 0
        
        team.funds += social_status
        
        #赛季收入概要
        all_tinance = AllFinance.load(team_id=team.id, season=league_config.season)
        if not all_tinance:
            all_tinance = AllFinance()
            all_tinance.team_id = team.id
            all_tinance.season = league_config.season
            all_tinance.income = 0
            all_tinance.outlay = 0
        all_tinance.income += social_status
        
        FreePlayer.transaction()
        try:
            player.delete()
            free_player.persist()
            all_tinance.persist()
            tinance.persist()
            FreePlayer.commit()
        except:
            FreePlayer.rollback()
            raise
                     
        url = reverse('profession-player-min')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})
    
@login_required
def finish_draft(request):
    '''结束试训'''
    team = request.team
    no = request.GET.get('no')
    error = None
    success = u'球员已经离队'
    i = 0
    while i < 1:
        i += 1
        if not no:
            error = u'球员不存在'
            break
        
        player = DraftPlayer.load(no=no)
        if not player:
            error = u'球员不存在'
            break
        
        pro_player = ProfessionPlayer.load(team_id=team.id, no=player.no)
        if not pro_player:
            error = u'该球员不在您队中试训'
            break
        
        if pro_player.in_tactical:
            error = u'请先将队员移出阵容'
            break
            
    if error:
        return render_to_response(request, 'message.html', {'error': error})

    player.status = 0 #没有试训
    player.current_team_id = 0
    
    DraftPlayer.transaction()
    try:
        pro_player.delete()
        player.persist()
        DraftPlayer.commit()
    except:
        DraftPlayer.rollback()
        raise
    
    url = reverse('profession-player-min')
    return render_to_response(request, 'message_update.html', {'url': url, 'success': success})
        
@login_required    
def youth_player_termination(request):
    '''年轻球员下放'''
    team = request.team
    no = request.GET.get('no')
    datas = {}
    error = None
    i = 0
    while i < 1:
        i += 1
        if not no:
            error = '球员不存在'
            break
        
        player = YouthPlayer.load(no=no)
        if not player:
            error = '球员不存在'
            break
        
        if player.team_id != team.id:
            error = '球员不在您队中'
            break
        
        if player.in_tactical:
            error = '该球员在阵容中'
            break
        
        count = YouthPlayer.count(condition='team_id="%s" and status <> %s' % (team.id, PlayerStatus.HURT))
        print count
        if count <= 8:
            error = '您的场上队员小于8名，不能下放'
            break
            
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if request.method == 'GET':
        social_status = playerutil.calcul_social_status(player)
        datas['social_status'] = int(social_status * 0.7)
        datas['player'] = player
        return render_to_response(request, 'player/youth_player_termination.html', datas)
    else:
        success = u'%s已经被下放!' % player.name
        
        social_status = playerutil.calcul_social_status(player) 
        league_config = LeagueConfig.load(id=1)
        #赛季支出明细
        tinance = SeasonFinance()
        tinance.sub_type = FinanceSubType.TERM_PLAYER
        tinance.team_id = team.id
        tinance.type = FinanceType.INCOME
        tinance.season = league_config.season
        tinance.round = league_config.round
        tinance.info = u'下放球员%s' % player.name
        tinance.income = 0
        tinance.outlay = social_status
        
        team.funds += social_status
        
        #赛季收入概要
        all_tinance = AllFinance.load(team_id=team.id, season=league_config.season)
        if not all_tinance:
            all_tinance = AllFinance()
            all_tinance.team_id = team.id
            all_tinance.season = league_config.season
            all_tinance.income = 0
            all_tinance.outlay = 0
        all_tinance.income += social_status
        
        FreePlayer.transaction()
        try:
            #player.delete()
            all_tinance.persist()
            tinance.persist()
            FreePlayer.commit()
        except:
            FreePlayer.rollback()
            raise               
        url = reverse('youth-player')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})

@login_required    
def youth_player_sell(request):
    '''年轻球员出售'''
    team = request.team
    no = request.GET.get('no')
    datas = {}
    error = None
    i = 0
    while i < 1:
        i += 1
        if not no:
            error = '球员不存在'
            break
        
        player = YouthPlayer.load(no=no)
        if not player:
            error = '球员不存在'
            break
        
        if player.team_id != team.id:
            error = '球员不在您队中'
            break
        
        if player.in_tactical:
            error = '该球员在阵容中'
            break
        
        if player.age < 20:
            error = '该球员年龄不足20,不能出售'
            break
        
        count = YouthPlayer.count(condition='team_id="%s" and status <> %s' % (team.id, PlayerStatus.HURT))
        if count <= 8:
            error = '您的场上队员小于8名，不能出售'
            break
            
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if request.method == 'GET':
        social_status = playerutil.calcul_social_status(player)
        datas['social_status'] = social_status
        datas['player'] = player
        return render_to_response(request, 'player/youth_player_sell.html', datas)
    
@login_required    
def youth_player_promoted(request):
    '''年轻球员提拔'''
    team = request.team
    no = request.GET.get('no')
    error = None
    i = 0
    while i < 1:
        i += 1
        if not no:
            error = '球员不存在'
            break
        
        player = YouthPlayer.load(no=no)
        if not player:
            error = '球员不存在'
            break
        
        if player.team_id != team.id:
            error = '球员不在您队中'
            break
        
        if player.in_tactical:
            error = '该球员在阵容中'
            break
        
        count = YouthPlayer.count(condition='team_id="%s" and status <> %s' % (team.id, PlayerStatus.HURT))
        if count <= 8:
            error = '您的场上队员小于8名，不能提拔'
            break
        
        #判断下有没有选拔卡
            
    if error:
        return render_to_response(request, 'message.html', {'error': error})
    
    if request.method == 'GET':
        return render_to_response(request, 'player/youth_player_promoted.html', {'player': player})
    else:
        success = u'%s已经被提拔到职业队中!' % player.name
        profession_player = playerutil.youth_player_promoted(player)
        
        YouthPlayer.transaction()
        try:
            profession_player.persist()
            player.delete()
            YouthPlayer.commit()
        except:
            YouthPlayer.rollback()
            raise
        
        url = reverse('youth-player')
        return render_to_response(request, 'message_update.html', {'success': success, 'url': url})
        