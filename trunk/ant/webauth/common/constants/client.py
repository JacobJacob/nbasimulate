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
    PROXY_SPIDER = 'proxy_spider'
    ALEXA_SPIDER = 'alexa_spider'
    GOOGLE_PR_SPIDER = 'google_pr_spider'
    