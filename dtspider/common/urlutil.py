#!/usr/bin/python
# -*- coding: utf-8 -*-

import urlparse
import re
import logging
import traceback


def _ensure_str(s):
    """保证s是utf-8编码的str"""
    if isinstance(s, unicode): 
        s = s.encode('utf-8')
    return s

class URLSplitResult(urlparse.SplitResult):
    
    def geturl(self):
        """确保url是utf-8编码的str"""
        url = urlparse.SplitResult.geturl(self)
        return _ensure_str(url)
    
    @property
    def is_domain(self):
        return self.geturl() == self.domain_url
    
    @property
    def is_host(self):
        return self.geturl() == self.host_url
    
    @property
    def domain(self):
        try:
            domain = _host_to_domain(self.hostname)
        except ValueError: # hostname错误，则直接返回hostname
            logging.error('%r, url %r' % (traceback.format_exc(), self.geturl()))
            domain = self.hostname
        return _ensure_str(domain)
        
    @property
    def domain_url(self):
        return 'http://%s/' % self.domain
    
    @property
    def folder(self):
        folder = get_folder(self)
        return _ensure_str(folder)

    @property
    def host(self):
        """主机加端口，忽略80端口"""
#        if not hostname:
#            return None
#        try:
#            p = self.port
#        except:
#            p = None
#        if p and p != 80:
#            hostname = "%s:%s" % (hostname, p)
        return _ensure_str(self.hostname)

    @property
    def host_url(self):
        return 'http://%s/' % self.host

def standardize(url):
    """规范化url: 1.小写 2.去掉锚anchor 3.如果?号之后没有参数，去掉? 4.如果没有路径,加上/ 5.去掉相对路径"""
    url = url.strip()
    if not url:
        return None
    url = url.lower()
    colon_index = url.find("://")
    if colon_index > 0:
        scheme = url[:colon_index]
        if len(scheme) > 5:
            url = "http" + url[colon_index:]
    else:
        url = "http://" + url
    split_result = urlparse.urlsplit(url)
    if not split_result.netloc:
        return None
    host = split_result.hostname
    if not host:
        return None
    if host.endswith('.'):
        host = host[:-1]
    path = split_result.path
    if not path:
        path = "/"
    path = path.replace("/./", "/")
    while True:
        slash_index = path.find("/../")
        if slash_index > -1:
            parent_index = path[:slash_index].rfind("/")
            if parent_index > -1:
                path = path[:parent_index] + path[slash_index + 3:]
            else:
                path = path[slash_index + 3:]
        else:
            break
    try:
        p = split_result.port
    except:
        p = None
    if p and p != 80:
        host = "%s:%s" % (host, p)
    
    r = URLSplitResult(split_result.scheme, host, path, split_result.query, "")
    if _is_bad_url(r):
        return None
    return r

def _is_bad_url(url_split):
    """判断是否是坏url"""
    try:
        host = _host_to_domain(url_split.hostname)
        if host:
            return False
        else:
            return True
    except ValueError:
        return True

#def split_domain_url(url):
#    if isinstance(url, URLSplitResult):
#        split_result = url
#    else:
#        split_result = urlparse.urlsplit(url, "http")
#    if not split_result.netloc:
#        return None
#    return "%s://%s/" % (split_result.scheme, split_result.netloc)
        
def get_folder(url):
    if isinstance(url, URLSplitResult):
        split_result = url
    else:
        split_result = standardize(url)
    if not split_result.netloc:
        return None
    url_path = split_result.path
    if url_path:
        p = url_path.rfind('/', 0, -1)
        url_path = url_path[:p]
    folder = "%s://%s%s/" % (split_result.scheme, split_result.netloc, url_path)
    return _ensure_str(folder)

_IP_RE = re.compile(r'^\d+\.\d+\.\d+\.\d+$', re.I)

def is_ip(host):
    """判定host是否ip"""
    return _IP_RE.match(host) is not None

def _host_to_domain(host): # 进来的 Host 一般都是 url2host 过滤过的，因此可以 认为都 合法
    # 最好的办法是用 Whois 追踪，这里因速度和本地考虑，简单识别：
    # 最后可以有一个表示根域的点（在规范中，最后有一个点的才是完整域名，
    # 但一般认为包括两个以上名字的域名也是完整域名，哪怕它后面没有点）。
    if host is None:
        raise ValueError("host could not be None")
    host = host.lower().strip()
    if host == 'localhost' or is_ip(host):
        return host
    if not host or host.find(".") == -1:
        raise ValueError("Bad Host %r" % host)
    words = host.split('.')
    if len(words) >= 2 and words[-1] in _TOP_DOMAIN and words[-2]:
        return '.'.join(words[-2:])
    elif len(words) >= 2 and words[-1] in _COUNTRY_CODE and words[-2]:
        # D.get(k[,d]) -> D[k] if k in D, else d.  d defaults to None.
        subcodes = _SUB_COUNTRY_CODE.get(words[-1], _SUB_COUNTRY_CODE[''])
        if len(words) >= 3 and words[-2] in subcodes and words[-3]:
            return '.'.join(words[-3:])
        return '.'.join(words[-2:])
    elif len(words) >= 2 and words[-1] and words[-2]:
        return '.'.join(words[-2:])
    raise ValueError("Bad Host %r" % host)

_TOP_DOMAIN = set(['biz','com','edu','gov','info','int','mil','name','net','org','mobi','asia','aero',
                   'cat','coop','jobs','museum','pro','tel','travel', 'onion', # tor 网络的专用域名后缀
                   'arpa','aero', 'arpa', 'asia', 'biz', 'cat', 'com', 'coop', 'edu', 'info', 
                   'int', 'jobs', 'mobi', 'museum', 'name', 'net', 'org', 'pro', 'travel'])
_COUNTRY_CODE = set(['ac','ad','ae','af','ag','ai','al','am','an','ao','aq','ar','as','at','au','aw',
                     'ax','az','ba','bb','bd','be','bf','bg','bh','bi','bj','bm','bn','bo','br','bs',
                     'bt','bw','by','bz','ca','cc','cd','cf','cg','ch','ci','ck','cl','cm','cn','co',
                     'cr','cu','cv','cx','cy','cz','de','dj','dk','dm','do','dz','ec','ee','eg','er',
                     'es','et','eu','fi','fj','fk','fm','fo','fr','ga','gd','ge','gf','gg','gh','gi',
                     'gl','gm','gn','gp','gq','gr','gs','gt','gu','gw','gy','hk','hm','hn','hr','ht',
                     'hu','id','ie','il','im','in','io','iq','ir','is','it','je','jm','jo','jp','ke',
                     'kg','kh','ki','km','kn','kp','kr','kw','ky','kz','la','lb','lc','li','lk','lr',
                     'ls','lt','lu','lv','ly','ma','mc','md','me','mg','mh','mk','ml','mm','mn','mo',
                     'mp','mq','mr','ms','mt','mu','mv','mw','mx','my','mz','na','nc','ne','nf','ng',
                     'ni','nl','no','np','nr','nu','nz','om','pa','pe','pf','pg','ph','pk','pl','pn',
                     'pr','ps','pt','pw','py','qa','re','ro','rs','ru','rw','sa','sb','sc','sd','se',
                     'sg','sh','si','sk','sl','sm','sn','sr','st','sv','sy','sz','tc','td','tf','tg',
                     'th','tj','tk','tl','tm','tn','to','tr','tt','tv','tw','tz','ua','ug','uk','us',
                     'uy','uz','va','vc','ve','vg','vi','vn','vu','wf','ws','ye','za','zm','zw','yu',
                     # http://www.cnnic.cn/html/Dir/2003/10/20/0906.htm   67 个
                     'ac', 'ad', 'ag', 'am', 'as', 'at', 'be', 'bg', 'bm', 'bn',
                     'by', 'bz', 'ca', 'cc', 'cd', 'cg', 'ch', 'cl', 'cz', 'de',
                     'dk', 'fo', 'fr', 'ge', 'gg', 'gh', 'hm', 'hu', 'ie', 'in',
                     'is', 'gg', 'je', 'jp', 'ky', 'li', 'lk', 'lu', 'lt', 'lv',
                     'ly', 'mc', 'md', 'mn', 'ms', 'na', 'nc', 'nf', 'nl', 'no',
                     'nu', 'ph', 'pn', 're', 'ro', 'ru', 'sh', 'st', 'tc', 'tf',
                     'tj', 'to', 'tp', 'vg', 'vi', 'vu', 'ws',
                     # http://www.gatevalve.cn/business/global-domain-name.html
                     'cs', 'eh', 'gb', 'sj', 'so', 'su', 'zr', 'mf', 'bl', 'um', 'bv', 'yt', 'pm']) 
# http://www.iana.org/domains/root/db/
iana_country_code = ['ac', 'ad', 'ae', 'af', 'ag', 'ai', 'al', 'am', 'an', 'ao', 'aq', 'ar', 'as', 'at', 'au', 'aw', 'ax', 'az', 'ba', 'bb', 'bd', 'be', 'bf', 'bg', 'bh', 'bi', 'bj', 'bl', 'bm', 'bn', 'bo', 'br', 'bs', 'bt', 'bv', 'bw', 'by', 'bz', 'ca', 'cc', 'cd', 'cf', 'cg', 'ch', 'ci', 'ck', 'cl', 'cm', 'cn', 'co', 'cr', 'cu', 'cv', 'cx', 'cy', 'cz', 'de', 'dj', 'dk', 'dm', 'do', 'dz', 'ec', 'ee', 'eg', 'eh', 'er', 'es', 'et', 'eu', 'fi', 'fj', 'fk', 'fm', 'fo', 'fr', 'ga', 'gb', 'gd', 'ge', 'gf', 'gg', 'gh', 'gi', 'gl', 'gm', 'gn', 'gp', 'gq', 'gr', 'gs', 'gt', 'gu', 'gw', 'gy', 'hk', 'hm', 'hn', 'hr', 'ht', 'hu', 'id', 'ie', 'il', 'im', 'in', 'io', 'iq', 'ir', 'is', 'it', 'je', 'jm', 'jo', 'jp', 'ke', 'kg', 'kh', 'ki', 'km', 'kn', 'kp', 'kr', 'kw', 'ky', 'kz', 'la', 'lb', 'lc', 'li', 'lk', 'lr', 'ls', 'lt', 'lu', 'lv', 'ly', 'ma', 'mc', 'md', 'me', 'mf', 'mg', 'mh', 'mk', 'ml', 'mm', 'mn', 'mo', 'mp', 'mq', 'mr', 'ms', 'mt', 'mu', 'mv', 'mw', 'mx', 'my', 'mz', 'na', 'nc', 'ne', 'nf', 'ng', 'ni', 'nl', 'no', 'np', 'nr', 'nu', 'nz', 'om', 'pa', 'pe', 'pf', 'pg', 'ph', 'pk', 'pl', 'pm', 'pn', 'pr', 'ps', 'pt', 'pw', 'py', 'qa', 're', 'ro', 'rs', 'ru', 'rw', 'sa', 'sb', 'sc', 'sd', 'se', 'sg', 'sh', 'si', 'sj', 'sk', 'sl', 'sm', 'sn', 'so', 'sr', 'st', 'su', 'sv', 'sy', 'sz', 'tc', 'td', 'tf', 'tg', 'th', 'tj', 'tk', 'tl', 'tm', 'tn', 'to', 'tp', 'tr', 'tt', 'tv', 'tw', 'tz', 'ua', 'ug', 'uk', 'um', 'us', 'uy', 'uz', 'va', 'vc', 've', 'vg', 'vi', 'vn', 'vu', 'wf', 'ws', 'ye', 'yt', 'yu', 'za', 'zm', 'zw']
iana_generic_restricted = ['biz', 'name', 'pro']
iana_infrastructure = ['arpa']
iana_sponsored = ['aero', 'asia', 'cat', 'coop', 'edu', 'gov', 'int', 'jobs', 'mil', 'mobi', 'museum', 'tel', 'travel']
iana_generic = ['com', 'info', 'net', 'org']
iana_all = iana_generic_restricted + iana_infrastructure + iana_sponsored + iana_generic


__list = ['com', 'net', 'org', 'edu', 'gov', 'mil', 'ac', 'co', 'ne', 'or', 'ed', 'go']
# 目前已知的都在这里了，进最大努力匹配，保证正确的绝对能过的同时，尽力找出最多的不能过的域名
_SUB_COUNTRY_CODE = {
        '':   set(__list),
        'cn': set(__list +
                  ['bj', 'fj', 'sh', 'jx', 'tj', 'sd', 'cq', 'ha', 'he', 'hb', 'sx',
                   'hn', 'nm', 'gd', 'ln', 'gx', 'jl', 'hi', 'hl', 'sc', 'js', 'gz',
                   'zj', 'yn', 'ah', 'xz', 'sn', 'tw', 'gs', 'hk', 'qh', 'mo', 'nx',
                   'xj', 'ce']),
        # http://www.cnnic.cn/html/Dir/2003/10/20/0906.htm
        # 允许注册二级域名的国家和地区，共67 个
        'ac': set(__list),
        'ad': set(__list),
        'ag': set(__list),
        'am': set(__list),
        'as': set(__list),
        'at': set(__list),
        'be': set(__list),
        'bg': set(__list),
        'bm': set(__list),
        'bn': set(__list),
        'by': set(__list),
        'bz': set(__list),
        'ca': set(__list),
        'cc': set(__list),
        'cd': set(__list),
        'cg': set(__list),
        'ch': set(__list),
        'cl': set(__list),
        'cz': set(__list),
        'de': set(__list),
        'dk': set(__list),
        'fo': set(__list),
        'fr': set(__list),
        'ge': set(__list),
        'gg': set(__list),
        'gh': set(__list),
        'hm': set(__list),
        'hu': set(__list),
        'ie': set(__list),
        'in': set(__list),
        'is': set(__list),
        'gg': set(__list),
        'je': set(__list),
        'jp': set(__list),
        'ky': set(__list),
        'li': set(__list),
        'lk': set(__list),
        'lu': set(__list),
        'lt': set(__list),
        'lv': set(__list),
        'ly': set(__list),
        'mc': set(__list),
        'md': set(__list),
        'mn': set(__list),
        'ms': set(__list),
        'na': set(__list),
        'nc': set(__list),
        'nf': set(__list),
        'nl': set(__list),
        'no': set(__list),
        'nu': set(__list),
        'ph': set(__list),
        'pn': set(__list),
        're': set(__list),
        'ro': set(__list),
        'ru': set(__list),
        'sh': set(__list),
        'st': set(__list),
        'tc': set(__list),
        'tf': set(__list),
        'tj': set(__list),
        'to': set(__list),
        'tp': set(__list),
        'vg': set(__list),
        'vi': set(__list),
        'vu': set(__list),
        'ws': set(__list),}

# _TOP_DOMAIN _COUNTRY_CODE
#print _COUNTRY_CODE - set(iana_country_code)
#print _TOP_DOMAIN - set(iana_all)

## http://www.gatevalve.cn/business/global-domain-name.html
#if False: sss = """AT .at 奥地利
#AD .ad 安道尔
#AE .ae 阿联酋
#AF .af 阿富汗
#AI .ai 安奎拉
#AL .al 阿尔巴尼亚
#AM .am 亚美尼亚
#AO .ao 安哥拉
#AQ .aq 南极洲
#AR .ar 阿根廷
#AS .as 美属萨摩亚群岛
#AU .au 澳大利亚
#AZ .az 阿塞拜疆
#BA .ba 波斯尼亚和黑塞哥维那
#BB .bb 巴巴多斯
#BD .bd 孟加拉
#BE .be 比利时
#BF .bf 布基那法索
#BG .bg 保加利亚
#BH .bh 巴林
#BI .bi 布隆迪
#BJ .bj 贝宁
#BM .bm 百慕大
#BN .bn 文莱
#BO .bo 玻利维亚
#BR .br 巴西
#BS .bs 巴哈马
#BT .bt 不丹
#BW .bw 博茨瓦纳
#BZ .bz 伯利兹
#CA .ca 加拿大
#CF .cf 中非共和国
#CG .cg 刚果
#CH .ch 瑞士
#CI .ci 象牙海岸
#CK .ck 库克群岛
#CL .cl 智利
#CM .cm 喀麦隆
#CN .cn 中国
#CO .co 哥伦比亚
#CR .cr 哥斯达黎加
#CS .cs 捷克斯洛伐克（前）
#CU .cu 古巴
#CV .cv 佛得角群岛
#CY .cy 塞浦路斯
#CZ .cz 捷克共和国
#DE .de 德国
#DJ .dj 吉布提
#DK .dk 丹麦
#DM .dm 多米尼加
#DO .do 多米尼加共和国
#DZ .dz 阿尔及利亚
#EC .ec 厄瓜多尔
#EE .ee 爱沙尼亚
#EG .eg 埃及
#EH .eh 西撒哈拉
#ER .er 厄立特利亚
#ES .es 西班牙
#ET .et 埃塞俄比亚
#FI .fi 芬兰
#FJ .fj 斐济
#FK .fk 马尔维那斯群岛
#FM .fm 密克罗尼西亚
#FR .fr 法国
#GA .ga 加蓬
#GB .gb 英国
#GD .gd 格林纳达
#GE .ge 乔治亚
#GF .gf 法属圭亚那
#GH .gh 加纳
#GI .gi 直布罗陀
#GL .gl 格陵兰（岛）
#GM .gm 赞比亚
#GN .gn 几内亚
#GP .gp 瓜德罗普
#GQ .gq 赤道几内亚
#GR .gr 希腊
#GT .gt 危地马拉
#GU .gu 关岛
#GW .gw 几内亚比绍
#GY .gy 圭亚那
#HK .hk 香港
#HN .hn 洪都拉斯
#HR .hr 克罗地亚
#HT .ht 海地
#HU .hu 匈牙利
#ID .id 印度尼西亚
#IE .ie 爱尔兰
#IL .il 以色列
#IN .in 印度
#IQ .iq 伊拉克
#IR .ir 伊朗
#IS .is 冰岛
#IT .it 意大利
#JM .jm 牙买加
#JO .jo 约旦
#JP .jp 日本
#KE .ke 肯尼亚
#KH .kh 柬埔寨
#KM .km 科摩罗群岛
#KR .kr 韩国
#KP .kp 朝鲜
#KW .kw 科威特
#KY .ky 开曼群岛
#KZ .kz 哈萨克斯坦
#LA .la 老挝
#LB .lb 黎巴嫩
#LC .lc 圣路西亚
#LI .li 列支敦士登
#LK .lk 斯里兰卡
#LR .lr 利比里亚
#LS .ls 莱索托
#LT .lt 立陶宛
#LU .lu 卢森堡
#LV .lv 拉托维亚
#LY .ly 利比亚
#MA .ma 摩洛哥
#MC .mc 摩纳哥
#MD .md 摩尔多瓦
#ME .me 黑山
#MG .mg 马达加斯加
#MH .mh 马绍尔群岛
#ML .ml 马里
#MN .mn 蒙古
#MO .mo 澳门
#MP .mp 南马利亚那群岛
#MQ .mq 马提尼克岛
#MR .mr 毛里塔尼亚
#MS .ms 蒙特塞拉特克岛
#MT .mt 马耳他
#MU .mu 毛里求斯
#MV .mv 马尔代夫
#MW .mw 马拉维
#MX .mx 墨西哥
#MY .my 马来西亚
#MZ .mz 莫桑比克
#NA .na 纳米比亚
#NC .nc 新喀里多尼亚岛
#NE .ne 尼日尔
#NG .ng 尼日利亚
#NI .ni 尼加拉瓜
#NL .nl 荷兰
#NO .no 挪威
#NP .np 尼泊尔
#NR .nr 瑙鲁
#NU .nu 纽埃岛
#NZ .nz 新西兰
#OM .om 阿曼
#PA .pa 巴拿马
#PE .pe 秘鲁
#PF .pf 法属玻利尼西亚
#PG .pg 巴布亚新几内亚
#PH .ph 菲律宾
#PK .pk 巴基斯坦
#PL .pl 波兰
#PR .pr 波多黎哥
#PT .pt 葡萄牙
#PY .py 巴拉圭
#QA .qa 卡塔尔
#RE .re 留尼汪岛
#RO .ro 罗马尼亚
#RS .rs 塞尔维亚
#RU .ru 俄罗斯
#RW .rw 卢旺达
#SA .sa 沙特阿拉伯
#Sb .sb 所罗门群岛
#SC .sc 塞舌尔
#SD .sd 苏丹
#SE .se 瑞典
#SG .sg 新加坡
#SH .sh 圣赫勒拿岛
#SI .si 斯洛文尼亚
#SJ .sj 斯瓦巴德群岛
#SK .sk 斯洛伐克
#SL .sl 塞拉利昂
#SM .sm 圣马力诺
#SN .sn 塞内加尔
#SO .so 索马里
#SR .sr 苏里南
#ST .st 圣多美岛和普林西比岛
#SU .su 苏联（前）
#SV .sv 萨尔瓦多
#SY .sy 叙利亚
#SZ .sz 斯威士兰
#TD .td 乍得
#TG .tg 多哥
#TH .th 泰国
#TJ .tj 塔吉克斯坦
#TK .tk 托克劳群岛
#TM .tm 土库曼斯坦
#TN .tn 突尼斯
#TO .to 汤加
#TP .tp 东帝汶岛
#TR .tr 土耳其
#TT .tt 特立尼达和多巴哥
#TW .tw 台湾
#TZ .tz 坦桑尼亚
#UA .ua 乌克兰
#UG .ug 乌干达
#UK .uk 英国
#US .us 美国
#UY .uy 乌拉圭
#UZ .uz 乌兹别克斯坦
#VA .va 梵蒂冈
#VE .ve 委内瑞拉
#VG .vg 维京岛（英）
#VI .vi 维京岛（美）
#VN .vn 越南
#WF .wf 瓦利斯群岛
#WS .ws 萨摩亚群岛
#YE .ye 也门
#YU .yu 南斯拉夫
#ZA .za 南非
#ZM .zm 赞比亚
#ZR .zr 扎伊尔
#ZW .zw 津巴布韦"""
