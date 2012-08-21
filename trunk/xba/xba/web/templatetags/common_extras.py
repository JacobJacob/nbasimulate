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

@register.filter
def article_status(status):
    """文章状态"""
    if status == 0:
        return '<font color="red">待发布</font>'
    return '<font color="green">已发布</font>'

@register.filter
def article_link(article):
    """文章链接"""
    return "/article/%s/detail/%s.html" % (article.category, article.id)

@register.filter
def category_name(category):
    """类型名"""
    map = {"notice": "游戏公告", "strategy": "游戏攻略", "guide": "新手指南", "experience": "玩家经验",\
            "nba": "nba新闻", "video": "nba视频", "knowledge": "篮球知识"}
    return map[category]

@register.filter
def team_ability(ability):
    """文章链接"""
    a = float(ability) / 50
    return "%0.1f" % a

@register.filter
def guess_result(result):
    if result == 0:
        return u"进行中"
    elif result == 1:
        return u"平盘中"
    elif result == 3:
        return u"已平盘"
    
    return u"未知"

