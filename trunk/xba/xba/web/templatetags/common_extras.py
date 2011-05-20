#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from django import template
from xba.common.constants.market import ProfessionMarketCategoryMap

register = template.Library()

@register.filter
def check_attr(attr_oten):
    return 0

@register.filter
def player_category(category):
    """球员类型"""
    return "[%s]%s" % (category, ProfessionMarketCategoryMap.get(category, '未知'))

@register.filter
def club_category(category):
    """球队类型"""
    if category == 3:
        return "街球队"
    elif category == 5:
        return "职业队"
    return "未知"