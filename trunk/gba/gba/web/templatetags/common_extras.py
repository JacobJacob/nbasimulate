#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from django import template

from gba.common.constants import oten_color_map, AttributeMaps
from gba.common.constants import TacticalSectionTypeMap, MatchStatusMap
from gba.entity import Team, UserInfo

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
    team = Team.load(id=team_id)
    if team:
        return team.name
    
@register.filter
def team_username(team_id):
    team = Team.load(id=team_id)
    if team:
        user_info = UserInfo.load(username=team.username)
        if user_info:
            return user_info.username
    return ''
        
@register.filter
def match_status(status):
    return MatchStatusMap.get(status, u'状态异常')

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