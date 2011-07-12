#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

from django import template
from xba.common.constants.market import ProfessionMarketCategoryMap, StreeMarketCategoryMap
from xba.common.constants.player import PositionMap
from xba.common.constants.account import InviteCodeStatusMap

register = template.Library()

@register.filter
def check_attr(attr_oten):
    return 0

@register.filter
def player5_category(category):
    """球员类型"""
    return "[%s]%s" % (category, ProfessionMarketCategoryMap.get(category, '未知'))

@register.filter
def player3_category(category):
    """球员类型"""
    return "[%s]%s" % (category, StreeMarketCategoryMap.get(category, '未知'))

@register.filter
def club_category(category):
    """球队类型"""
    if category == 3:
        return "街球队"
    elif category == 5:
        return "职业队"
    return "未知"

@register.filter
def position(pos):
    """球员位置"""
    return PositionMap.get(pos, "未知")

@register.filter
def invite_code_status(status):
    """邀请码状态"""
    return InviteCodeStatusMap.get(status, "未知")


