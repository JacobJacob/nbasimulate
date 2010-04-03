#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from datetime import timedelta 

from django import template
from django.core.urlresolvers import reverse

from gba.common.constants import oten_color_map, AttributeMaps
from gba.common.constants import TacticalSectionTypeMap, MatchStatusMap
from gba.entity import Team, UserInfo, ProfessionPlayer

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
def match_status(status, id):
    html = ''
    if status == 0:
        html += '等待中'
    elif status == 1:
        html += '赛前准备中'
    elif status == 2:
        html += '比赛中'
    elif status == 3:
        html += """<a href="%s?match_id=%s" target="_blank">比赛统计</a>""" % (reverse('match-stat'), id)
        html += """<a href="%s?match_id=%s" target="_blank">比赛战报</a>""" % (reverse('match-detail'), id)
    elif status == 4:
        html += '比赛取消'
    return html

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
def position(status):
    '''位置'''
    return u'等待'

@register.filter
def format_number(number):
    '''位置'''
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
    return user_info.nickname

@register.filter
def profession_player_name(player_no):
    player = ProfessionPlayer.load(no=player_no)
    if player:
        return player.name
    return ''