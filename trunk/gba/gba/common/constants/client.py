#!/usr/bin/python
# -*- coding: utf-8 -*-
"""client const"""

from constant_base import ConstantBase

class ClientStatus(ConstantBase):
    """客户端状态"""
    ERROR = -1
    DEATH = -2
    SLEEP = 0
    ACTIVE = 1
    FINISH = 2
    BROKEN = 3
    PAUSE = 4
    PAUSE2 = 5
    
CLIENT_STATUS_NAMES = {
    ClientStatus.ERROR: 'Error',
    ClientStatus.DEATH: 'Death',
    ClientStatus.SLEEP: 'Sleep',
    ClientStatus.ACTIVE: 'Active',
    ClientStatus.FINISH: 'Finish',
    ClientStatus.BROKEN: 'Broken',
    ClientStatus.PAUSE: 'Pause',
    ClientStatus.PAUSE2: 'Pause',
}

class Command(ConstantBase):
    START = 'start'
    STOP = 'stop'
    SVNUP = 'svnup'
    QUIT = 'quit'
    ERROR = 'error'
    FINISH = 'finish' 
    PASS = 'pass'
    PAUSE = 'pause'
    CONTINUE = 'continue'
    REREG = 'register'
    REBOOT = 'reboot' # 机器重启

#状态转换图：http://trac.rdev.kingsoft.net/mercury/attachment/wiki/baseclient/abc.png
STATUS_MAP = {
    ClientStatus.SLEEP: {Command.START:  ClientStatus.ACTIVE,
                          Command.SVNUP:  ClientStatus.DEATH,
                          Command.REREG:  ClientStatus.DEATH,
                          Command.QUIT:   ClientStatus.DEATH,
                          Command.PAUSE:  ClientStatus.PAUSE2,
                          Command.REBOOT:  ClientStatus.DEATH,
                          },
                  
    ClientStatus.PAUSE2: {Command.CONTINUE: ClientStatus.SLEEP,
                          Command.REBOOT:  ClientStatus.DEATH,
                          },
                         
    ClientStatus.ACTIVE: {Command.STOP:   ClientStatus.BROKEN, 
                          Command.ERROR:  ClientStatus.ERROR,
                          Command.FINISH: ClientStatus.FINISH,
                          Command.PAUSE:  ClientStatus.PAUSE,
                          Command.SVNUP:  ClientStatus.DEATH, # 支持直接更新
                          Command.REBOOT:  ClientStatus.DEATH,
                          }, 
                         
    ClientStatus.PAUSE: {Command.CONTINUE: ClientStatus.ACTIVE,
                          Command.REBOOT:  ClientStatus.DEATH,
                          },
                         
    ClientStatus.BROKEN: {Command.PASS:   ClientStatus.SLEEP, # 如果发现当前状态里面有 CMD_PASS 指令，直接执行它
                          Command.REBOOT:  ClientStatus.DEATH,
                          }, 
                         
    ClientStatus.FINISH: {Command.PASS:   ClientStatus.SLEEP,
                          Command.REBOOT:  ClientStatus.DEATH,
                          },
                         
    ClientStatus.ERROR: {Command.QUIT:   ClientStatus.DEATH,
                          Command.SVNUP:  ClientStatus.DEATH,
                          Command.REREG:  ClientStatus.DEATH,
                          Command.REBOOT:  ClientStatus.DEATH,
                          }
}

class SmartClientCommand(ConstantBase):
    MACHINE_RESTART = 88 # 机器重启
    RESTART = 42
    SVNUP_RESTART = 43
    EXIT = 0

class ClientType(ConstantBase):
    DEMO = 'demo'
    URL_AUTH = 'url_auth' # url检测
    URL_AUTH_SANDBOX = 'url_auth_sandbox'
    WEBSHIELD_URL_IMPORTER = 'webshield_url_importer' #导入网盾数据
    HOT_URL_SPIDER = 'hot_url_spider'
    GATEWAY_DATA_SPIDER = 'gateway_data_spider' # 网关数据导入
    URL_SOURCE_STATISTIC = "url_source_statistic" #URL统计
    DOMAIN_IMPORTER = "domain_importer" #根据URL提取DOMAIN
    THIRDDATA_SYCN_CLIENT = 'thirddata_sycn_client' #旧库第三方数据迁入
    PROXY_SPIDER = 'proxy_spider' #代理爬取
    ALEXA_SPIDER = 'alexa_spider' #alexa 信息爬取
    GOOGLE_PR_SPIDER = 'google_pr_spider' #google pr值爬取
    HOST_SEARCH_SPIDER = 'host_search_spider' #根据hsot搜索
    DRAGON_UNION_URL_SPIDER = 'dragon_union_data_spider' #龙族联盟黑URL爬取
    
    BLACK_HOST_AUTO = 'black_host_auto_extractor' # 黑URL自动提取
    GOOGLE_BLACKURL_SPIDER = 'google_blackurl_spider' # google恶意库爬取
    GOOGLE_HOST_IMPORTER = 'google_host_importer' # google host导入
    GOOGLE_HOST_SEARCH_SPIDER = 'google_host_search_spider' # 根据google恶意库搜索
    
    KEYWORD_SEARCH_SPIDER = 'keyword_search_spider' # 根据人工关键字搜索
    SAFECENTER_HOST_SEARCH = 'safecenter_host_spider' #根据安全中心监控host搜索
    SAFECENTER_RECHECK_URL_IMPORTER = 'safecenter_recheck_url_importer' #定时导入安全中心需要重新检测的url
    SAFECENTER_HOST_STATUS_CHECKER = 'safecenter_host_status_checker' #定时查看host是否已经被检测
    AUTO_MONITOR_SITE_IMPORTER = 'auto_monitor_site_importer' #导入被挂马的站中PR值比较高的网站
    AUTO_MONITOR_SENDER = 'auto_monitor_sender' #网站挂马邮件通知客户端