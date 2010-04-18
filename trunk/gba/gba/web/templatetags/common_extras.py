#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from datetime import timedelta 

from django import template
from django.core.urlresolvers import reverse

from gba.common.constants import oten_color_map, AttributeMaps, PositioneMap
from gba.common.constants import TacticalSectionTypeMap, MatchStatusMap, StaffMap, MatchShowStatus
from gba.entity import Team, UserInfo, ProfessionPlayer, LeagueTeams

register = template.Library()

@register.filter
def check_attr(attr_oten):
    for map_info in oten_color_map:
        if attr_oten >= map_info[0]:
            return map_info[1]
    return 0

@register.filter
def section_name(section):
    return TacticalSectionTypeMap.get(section)

@register.filter
def team_name(team_id):
    '''球队名字'''
    if team_id == -1:
        return u'轮空'
    team = Team.load(id=team_id)
    if team:
        return team.name
    return ''
    
@register.filter
def team_youth_level(team_id):
    '''球队青年队等级'''
    team = Team.load(id=team_id)
    if team:
        return team.youth_league
    return ''
    
@register.filter
def team_username(team_id):
    team = Team.load(id=team_id)
    if team:
        user_info = UserInfo.load(username=team.username)
        if user_info:
            return user_info.username
    return ''

@register.filter
def team_micro(team_id):
    team = Team.load(id=team_id)
    if team and team.micro:
        return team.micro
    return '/site_media/images/team/0.gif'
        
@register.filter
def match_status(info, id):

    if isinstance(info, dict):
        status = info['show_status']
        is_home = info['is_home']
    else:
        status = info.show_status
        is_home = info.is_home
        
    html = ''
    if status == MatchShowStatus.WAITING:
        if is_home:
            html += '等待中'
        else:
            html += """<a href="%s?match_id=%s" onclick='return show_popup(this);'>接受</a>""" % (reverse('match-accept'), id)
    elif status == MatchShowStatus.READY:
        html += '赛前准备中'
    elif status >= MatchShowStatus.FIRST and  status <= MatchShowStatus.OVERTIME_SIX:
        html += '比赛中...'
    elif status == MatchShowStatus.FINISH:
        html += """<a href="%s?match_id=%s" target="_blank">统计</a>""" % (reverse('match-stat'), id)
        html += """|<a href="%s?match_id=%s" target="_blank">战报</a>""" % (reverse('match-detail'), id)
    elif status == MatchShowStatus.CANCEL:
        html += '比赛取消'
    return html

@register.filter
def match_sub_status(sub_status):
    if not sub_status:
        return ''
    if sub_status == 1:
        return '第一节'
    elif sub_status == 2:
        return '第二节'
    elif sub_status == 3:
        return '第三节'
    elif sub_status == 4:
        return '第四节'
    elif sub_status == 5:
        return '第一加时'
    elif sub_status == 6:
        return '第二加时'
    else:
        return '第%s加进进行中' % (sub_status - 4)

@register.filter
def match_show_status(info):
    if isinstance(info, dict):
        show_status = info['show_status']
        created_time = info['created_time']
        remain_time = info['remain_time']
    else:
        show_status = info.show_status
        created_time = info.created_time
        remain_time = info.remain_time
        
    if not remain_time:
        return ''
        
    remain_time = remain_time * 4 #放大四倍
    
    if remain_time >= 60:
        min = remain_time // 60
        level_seconds = remain_time % 60
        remain = '%i分%i秒' % (min, level_seconds)
    else:
        if remain_time > 0:
            remain = '%i秒' % (remain_time)
        else:
            remain = '...'
        
    if not show_status:
        return ''
    
    msg = ''
    
    if show_status == MatchShowStatus.READY:
        msg = '赛前准备中'
    elif show_status == MatchShowStatus.FIRST:
        msg = '第一节'
    elif show_status == MatchShowStatus.SECOND:
        msg = '第二节'
    elif show_status == MatchShowStatus.THIRD:
        msg = '第三节'
    elif show_status == MatchShowStatus.FOURTH:
        msg = '第四节'
    elif show_status == MatchShowStatus.OVERTIME_ONE:
        msg = '第一加时'
    elif show_status == MatchShowStatus.OVERTIME_TWO:
        msg = '第二加时'
    elif show_status == MatchShowStatus.OVERTIME_THREE:
        msg = '第三加时'
    elif show_status == MatchShowStatus.OVERTIME_FOUR:
        msg = '第四加时'
    elif show_status == MatchShowStatus.OVERTIME_FIVE:
        msg = '第五加时'
    elif show_status == MatchShowStatus.OVERTIME_SIX:
        msg = '第六加时'
    elif show_status == MatchShowStatus.STATISTICS:
        msg = '比赛统计中'
    elif show_status == MatchShowStatus.WAITING or show_status == MatchShowStatus.CANCEL or \
         show_status == MatchShowStatus.FINISH:
        return '''<span title="发送邀请时间">%s</span>''' % created_time
    
    return '%s %s' % (msg, remain)

@register.filter
def match_point(info):
    if isinstance(info, dict):
        status = info['status']
        point = info.get('point')
    else:
        status = info.status
        point = info.point
        
    if status == 13:
        return point
    
    return '-- : --'

@register.filter
def position(position):
    '''位置'''
    return PositioneMap.get(position, '')

@register.filter
def format_number(number):
    '''位置'''
    if not number:
        return 'error'
    return '%.1f' % number

@register.filter
def format_int(number):
    '''位置'''
    if not number:
        return 'error'
    return int(number)

@register.filter
def display_attribute(attribute):
    return AttributeMaps.get(attribute, u'未知')

@register.filter
def format_lave_time(seconds):
    '''格式化剩余时间'''
    one_day_seconds = 60 * 60 * 24
    one_hour_seconds = 60 * 60
    one_min_seconds = 60
    if isinstance(seconds, timedelta):
        days = seconds.days
        seconds = days * one_day_seconds + seconds.seconds
        
    if not seconds:
        return ''
    if seconds < 0:
        return u'己截止'

    if seconds >= one_day_seconds:
        days = seconds // one_day_seconds
        level_seconds = seconds % one_day_seconds
        hours = level_seconds // one_hour_seconds 
        return '%i天%i小时' % (days, hours)
    elif seconds >= one_hour_seconds:
        hours = seconds // one_hour_seconds
        level_seconds = seconds % one_hour_seconds
        mins = level_seconds // one_min_seconds 
        return '%i小时%i分' % (hours, mins)  
    elif seconds >= one_min_seconds:
        min = seconds // one_min_seconds
        level_seconds = seconds % one_min_seconds
        return '%i分%i秒' % (min, level_seconds)
    else:
        return '%i秒' % (seconds)
    
@register.filter
def nickname(team_id):
    if team_id == 0:
        return u'系统消息'
    team = Team.load(id=team_id)
    if not team:
        return ''
    user_info = UserInfo.load(username=team.username)
    if user_info:
        return user_info.nickname
    return ''
    
@register.filter
def profession_player_name(player_no):
    player = ProfessionPlayer.load(no=player_no)
    if player:
        return player.name
    return ''

@register.filter
def league_rank(team_id):
    '''球队联赛排名'''
    league_team = LeagueTeams.load(team_id=team_id)
    if league_team and league_team.rank:
        return league_team.rank
    return 1

@register.filter
def league_team_to_name(league_team_id):
    '''根据联赛里坑位id,获取实际的球队名'''
    if league_team_id  == -1:
        return u'轮空'
    
    league_team = LeagueTeams.load(id=league_team_id)
    if not league_team:
        return ''
    if league_team.team_id == -1:
        return u'轮空'
    
    team = Team.load(id=league_team.team_id)
    if team:
        return team.name
    return ''

@register.filter
def league_team_to_team_id(league_team_id):
    '''根据联赛里坑位id,获取实际的球队id'''
    if league_team_id  == -1:
        return u'轮空'
    
    league_team = LeagueTeams.load(id=league_team_id)
    if not league_team:
        return ''
    if league_team.team_id == -1:
        return ''
    return league_team.team_id
    


@register.filter
def point_total(stat):
    '''计算总分'''
    total = 0
    total += (stat.point1_doom_times if stat.point1_doom_times else 0) * 1
    total += (stat.point2_doom_times if stat.point2_doom_times else 0) * 2
    total += (stat.point3_doom_times if stat.point3_doom_times else 0) * 3
    return total

@register.filter
def rebound_total(stat):
    '''计算篮板总数'''
    total = 0
    total += stat.offensive_rebound if stat.offensive_rebound else 0
    total += stat.defensive_rebound if stat.defensive_rebound else 0
    return total

@register.filter
def rebound_agv(stat):
    '''计算篮板平均'''
    if stat.match_total == 0:
        return 0
    total = 0
    total += stat.offensive_rebound if stat.offensive_rebound else 0
    total += stat.defensive_rebound if stat.defensive_rebound else 0
    return '%.1f' % (total / stat.match_total)

@register.filter
def point_agv(stat):
    '''计算平均分'''
    if stat.match_total == 0:
        return 0
    total = 0
    total += (stat.point1_doom_times if stat.point1_doom_times else 0) * 1
    total += (stat.point2_doom_times if stat.point2_doom_times else 0) * 2
    total += (stat.point3_doom_times if stat.point3_doom_times else 0) * 3
    return '%.1f' % (total / stat.match_total)

@register.filter
def assist_agv(stat):
    '''计算助攻平均'''
    if stat.match_total == 0:
        return 0
    return '%.1f' % (stat.assist / stat.match_total)

@register.filter
def steal_agv(stat):
    '''计算抢断平均'''
    if stat.match_total == 0:
        return 0
    return '%.1f' % (stat.steals / stat.match_total)

@register.filter
def block_agv(stat):
    '''计算封盖平均'''
    if stat.match_total == 0:
        return 0
    return '%.1f' % (stat.block / stat.match_total)

@register.filter
def staff_type(type):
    '''职员类型'''
    return StaffMap.get(type, '')

@register.filter
def staff_img(type):
    '''职员类型图片'''
    if type == 1:
        return 'icon_03'
    elif type == 2:
        return 'icon_04'
    elif type == 3:
        return 'img_01'
    
@register.filter
def attr_color(value):
    '''球员属性颜色条'''
    if not value:
        return '#000' 
    elif value >= 90:
        return '#ff8000'
    elif value >= 80:
        return '#0096f1'
    elif value >= 70:
        return '#0096f1'
    elif value >= 60:
        return '#0096f1'
    elif value >= 50:
        return '#00d034'
    else:
        return '#000'
    
@register.filter
def power_color(value):
    '''球员体力颜色条'''
    print 1, value
    if not value:
        return '#000' 
    elif value == 100:
        return '#66cc33'
    elif value >= 70:
        return '#228b22'
    elif value >= 60:
        return '#ff8c00'
    else:
        return '#ff0000'
    
@register.filter
def attr_width(value):
    return int(value*1.6)
