#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from django import template

from gba.common.constants import oten_color_map
from gba.common.constants import TacticalSectionTypeMap
from gba.entity import Team

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
    team = Team.load(id=id)
    if team:
        return team.name
    
@register.filter
def match_status(status):
    return u'等待'