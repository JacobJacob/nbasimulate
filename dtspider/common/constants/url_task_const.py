#!/usr/bin/python
# -*- coding: utf-8 -*-
from constant_base import ConstantBase

EXPRIRED_MINUTES = 60

class UrlTaskStatus(ConstantBase):
    TODO = 0
    DOING = 1
    FAILED = 2
    SUCCESS = 3

class UrlFromType(ConstantBase):
    WEBSHIELD = 1 #网盾上报
    IMPORTANT_SITE = 2 #重点网站
    HOT_SITE = 3 #热门网站
    MSN = 4 #MSN查询
    ANTI_MISTAKE = 5 #防误报扫描
    RECHECK = 6 #黑URL重检
    SPLIT_URL = 7 # 由黑url切割得到的url
    INIT = 8 # 初始化
    WEB_SUBMIT = 9 # 页面提交
    BAIDU_TOP_URL = 10 # 百度热门url
    GOOGLE_REBANG_URL = 11 # google热榜url
    GATEWAY_HOT_URL = 12 # 网关查询的热门url
    GATEWAY_WARNING_URL = 13 # 网关查询得到的type=4的警告url
    WEBSHIELD_EYURL = 14 # 网盾毒窝数据

URL_FROM_TYPE_MAP = {
    0: "default",
    UrlFromType.WEBSHIELD: u"网盾上报",
    UrlFromType.IMPORTANT_SITE: u"重点网站",
    UrlFromType.HOT_SITE: u"热门网站",
    UrlFromType.MSN: u"MSN查询",
    UrlFromType.ANTI_MISTAKE: u"防误报扫描",
    UrlFromType.RECHECK: u"黑URL重检",
    UrlFromType.SPLIT_URL: u"黑url切割",
    UrlFromType.INIT: u"初始化",
    UrlFromType.WEB_SUBMIT: u"页面提交",
    UrlFromType.BAIDU_TOP_URL: u"百度热榜",
    UrlFromType.GOOGLE_REBANG_URL: u"google热榜",
    UrlFromType.GATEWAY_HOT_URL: u"网关查询的热榜",
    UrlFromType.GATEWAY_WARNING_URL: u"网关警告url",
    UrlFromType.WEBSHIELD_EYURL: u"网盾毒窝url",
}

# url打黑原因
URL_REASON = {
    '1': u'ie7被丁',
    '2': u'占坑',
    '3': u'试图触发MS06014漏洞',
    '4': u'检查到利用HeapSpray的恶意代码',
    '5': u'检测到其返回地址是在栈空间',
    '6': u'根据返回地址的页属性不符合要求',
    '7': u'命令行解析',
    '8': u'根据返回地址的页属性不符合要求',
    '9': u'访问恶意网址',
    '10': u'访问恶意网址',
    '11': u'访问恶意网址',
    '12': u'访问恶意网址',
    '13': u'执行恶意脚本',
    '14': u'执行恶意脚本',
    '15': u'BLOCK_MALICIOUS_SWF',
    '16': u'钓鱼网站',
    '17': u'T第三方ARG检测',
    '18': u'PPlive ARG检测',
}

class BlackURLCheckType(ConstantBase):
    """黑url检测类型定义"""
    HOST_MATCH = 2 # host级别
    DOMAIN_MATCH = 1 # domain级别
    ALL_MATCH = 0 # 全url匹配