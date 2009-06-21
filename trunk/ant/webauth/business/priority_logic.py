#!/usr/bin/python
# -*- coding: utf-8 -*-
from webauth.common.constants import Priority
from webauth.common.constants import UrlFromType

def url_type2priority(from_type):
    """根据URL来源类型判断url检测任务优先级"""
    if from_type == UrlFromType.ANTI_MISTAKE: # 防误报检测，优先级最高
            return Priority.CRITICAL
    elif from_type in (UrlFromType.WEBSHIELD, # 网盾上报来源
                       UrlFromType.WEB_SUBMIT, # 页面提交
                       ): 
        return Priority.HIGHER
    elif from_type == UrlFromType.SPLIT_URL: #拆分黑URL
        return Priority.HIGHER
    elif from_type == UrlFromType.RECHECK: # 黑URL复检
        return Priority.HIGHER - Priority.STEP
    elif from_type in (UrlFromType.GOOGLE_REBANG_URL, # google 热榜
                       UrlFromType.BAIDU_TOP_URL, # baidu 热榜
                       ):
        return Priority.NORMAL + Priority.STEP + Priority.STEP
    elif from_type == UrlFromType.GATEWAY_WARNING_URL:
        return Priority.NORMAL + Priority.STEP
    elif from_type in (UrlFromType.IMPORTANT_SITE, # 重点监控网站
                       UrlFromType.HOT_SITE, # 热门网站
                       UrlFromType.MSN, # MSN查询网站
                       UrlFromType.GATEWAY_HOT_URL, # 网关查询热门url
                       ): 
        return Priority.NORMAL
    else:
        return Priority.LOWER