#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from datetime import timedelta 

from django import template
from django.core.urlresolvers import reverse

from gba.common.constants import oten_color_map, AttributeMaps, PositioneMap
from gba.common.constants import TacticalSectionTypeMap, MatchStatusMap, StaffMap
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
def match_status(info, id):

    if isinstance(info, dict):
        status = info['status']
        is_home = info['is_home']
    else:
        status = info.status
        is_home = info.is_home
        
    html = ''
    if status == 0:
        print is_home
        if is_home:
            html += '等待中'
        else:
            html += """<a href="%s?match_id=%s" onclick='return show_popup(this);'>接受</a>""" % (reverse('match-accept'), id)
    elif status == 1:
        html += '赛前准备中'
    elif status == 2:
        html += '比赛中'
    elif status == 3:
        html += """<a href="%s?match_id=%s" target="_blank">统计</a>""" % (reverse('match-stat'), id)
        html += """|<a href="%s?match_id=%s" target="_blank">战报</a>""" % (reverse('match-detail'), id)
    elif status == 4:
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
def match_point(info):
    if isinstance(info, dict):
        status = info['status']
        point = info.get('point')
    else:
        status = info.status
        point = info.point
        
    if status == 2 or status == 3:
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
    if league_team:
        return league_team.rank
    return ''

@register.filter
def league_team_to_name(league_team_id):
    '''根据联赛里坑位id,获取实际的球队名'''
    if league_team_id  == -1:
        return u'轮空'
    
    league_team = LeagueTeams.load(id=league_team_id)
    if not league_team:
        return '该经理不存在'
    
    team = Team.load(id=league_team.team_id)
    if team:
        return team.name
    return ''

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