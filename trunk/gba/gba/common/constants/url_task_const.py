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
    CUSTOM_CHECK = 15 # 自定义url检测
    HOST_SEARCH = 16 # 使用host搜索
    AUTO_BLACK_HOST = 17 # 自动提取的黑HOST
    WEBSHIELD_REPORT = 18 #网盾API上报，API为/report_black_url/?url=
    SAFECENTER_SUBSCRIBE = 19 # 安全中心上报的订阅host
    GOOGLE_HOST_SEARCH = 20 # google恶意host搜索
    KEYWORD_SEARCH = 21 # 人工关键词搜索
    SAFECENTER_HOST_SEARCH = 22 # 根据安全中心监控host搜索
    
    CREATE_SNAPSHOT_CHECK = 23 # 自定义生成挂马路径图和快照url检测
    
    # 第三方数据从3开始
    RISING_EYURL = 301 # 瑞星kaka恶意网站爬取
    DEVIL_WOLF_EYURL = 302 # 魔狼军团黑url http://152308.blogbus.com/
    DRAGON_UNION_EYURL = 303 #龙族联盟黑url http://www.chinadforce.com/forumdisplay.php?fid=55&page=1

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
    UrlFromType.CUSTOM_CHECK: u'自定义url检测',
    UrlFromType.CREATE_SNAPSHOT_CHECK: u'生成挂马路径图和快照url检测',
    UrlFromType.HOST_SEARCH: u'根据Host搜索',
    UrlFromType.AUTO_BLACK_HOST: u'自动提取的黑Host',
    UrlFromType.WEBSHIELD_REPORT: u'网盾API上报',
    UrlFromType.SAFECENTER_SUBSCRIBE: u'安全中心订阅Host',
    UrlFromType.GOOGLE_HOST_SEARCH: u'根据google host搜索',
    UrlFromType.SAFECENTER_HOST_SEARCH: u'根据安全中心监控host搜索',
    
    UrlFromType.RISING_EYURL: u'瑞星恶意网址',
    UrlFromType.DEVIL_WOLF_EYURL: u'魔狼军团恶意网址',
    UrlFromType.KEYWORD_SEARCH: u'关键字搜索',
    UrlFromType.DRAGON_UNION_EYURL: u'龙族联盟恶意网址',
}

# url打黑原因
# http://trac.rdev.kingsoft.net/mercury/wiki/WebshieldReason
URL_REASON = {
    '1': u'ie7被丁',
    '2': u'占坑',
    '3': u'微软ms06014漏洞攻击',
    '4': u'检查到利用HeapSpray的恶意代码',
    '5': u'检测到其返回地址是在栈空间',
    '6': u'根据返回地址的页属性不符合要求',
    '7': u'命令行解析',
    '8': u'根据返回地址的页属性不符合要求',
    '9': u'恶意网址',
    '10': u'恶意网址',
    '11': u'恶意网址',
    '12': u'恶意网址',
    '13': u'执行恶意脚本',
    '14': u'执行恶意脚本',
    '15': u'BLOCK_MALICIOUS_SWF',
    '16': u'钓鱼网站',
    '17': u'T第三方ARG检测',
    '18': u'PPlive ARG检测',
    '20': u'广告拦截',
    '22': u'5173钓鱼网站',
    '23': u'触发mpeg2漏洞',
    '24': u'钓鱼网站',
    '25': u'缓冲区攻击',
}

class BlackURLCheckType(ConstantBase):
    """黑url检测类型定义"""
    HOST_MATCH = 2 # host级别
    DOMAIN_MATCH = 1 # domain级别
    ALL_MATCH = 0 # 全url匹配
    
class BlackHost(ConstantBase):
    """黑host相关定义"""
    TODO = 0
    ENABLE = 1
    DISABLE = 2 # 无效
    

REASON_MAP = {}
REASON_MAP['00000001'] = 1
REASON_MAP['00000002'] = 2
REASON_MAP['00000003'] = 3
REASON_MAP['00000004'] = 4
REASON_MAP['00000005'] = 5
REASON_MAP['00000006'] = 6
REASON_MAP['00000007'] = 7
REASON_MAP['00000008'] = 8
REASON_MAP['00000009'] = 9
REASON_MAP['0000000a'] = 10
REASON_MAP['0000000b'] = 11
REASON_MAP['0000000c'] = 12
REASON_MAP['0000000d'] = 13
REASON_MAP['0000000e'] = 14
REASON_MAP['0000000f'] = 15
REASON_MAP['00000010'] = 16
REASON_MAP['00000011'] = 17
REASON_MAP['00000012'] = 18
REASON_MAP['00000015'] = 18 #暂时定的
REASON_MAP['00000018'] = 24
REASON_MAP['00000019'] = 25

REASON_CH = {}
REASON_CH['00000001'] = u'ie7被丁'
REASON_CH['00000002'] = u'占坑'
REASON_CH['00000003'] = u'ms06014'
REASON_CH['00000004'] = u'检查到利用HeapSpray 的恶意代码'
REASON_CH['00000005'] = u'检测到其返回地址是在栈空间'
REASON_CH['00000006'] = u'根据返回地址的页属性不符合要求'
REASON_CH['00000007'] = u'命令行解析'
REASON_CH['00000008'] = u'根据返回地址的页属性不符合要求'
REASON_CH['00000009'] = u'恶意网址库MD5'
REASON_CH['0000000a'] = u'恶意网址库模糊匹配'
REASON_CH['0000000b'] = u'恶意网址库MD5 _Send'
REASON_CH['0000000c'] = u'恶意网址库模糊匹配_send'
REASON_CH['0000000d'] = u'静态脚本地址栏'
REASON_CH['0000000e'] = u'静态脚本地址'
REASON_CH['0000000f'] = u'BLOCK_MALICIOUS_SWF'
REASON_CH['00000010'] = u'网络钓鱼'
REASON_CH['00000011'] = u'第三方ARG检测'
REASON_CH['00000012'] = u'PPlive ARG检测'
REASON_CH['00000015'] = u'挂马测试'
REASON_CH['00000018'] = u'收集钓鱼网址'
REASON_CH['00000019'] = u'收集含有喷堆代码的网页'
    
def get_webauth_reason(reason_id):
    reason_id = '0000' + reason_id[4:]
    print reason_id
    if reason_id not in REASON_MAP:
        return '100'
    return REASON_MAP[reason_id]

def get_webauth_reason_desc(reason_id):
    reason_id = '0000' + reason_id[4:]
    print reason_id
    if reason_id not in REASON_CH:
        return reason_id
    return REASON_CH[reason_id]