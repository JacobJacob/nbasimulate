#!/usr/bin/python
# -*- coding: utf-8 -*-

import logging
import os

import win32api
import win32con

from webauth.client.sandbox_client import SanboxBaseClient
from webauth.common.constants import ClientType
from webauth import wangdun_proxy

def _set_windows_registry(path, name, newvalue):
    try:
        key = win32api.RegOpenKeyEx(win32con.HKEY_CURRENT_USER, path, 0, win32con.KEY_ALL_ACCESS)
        _oldvalue, _type = win32api.RegQueryValueEx(key, name)
#        if _type not in (1, 4):
#            print path, name, newvalue, _type
        win32api.RegSetValueEx(key, name, 0, _type, newvalue)
        win32api.RegCloseKey(key)
        return True
    except Exception, e:
        if (e[0] == 2): # 系统找不到指定的文件。
            print u"系统找不到指定的文件。"
            try:
                try:
                    key = win32api.RegOpenKeyEx(win32con.HKEY_CURRENT_USER, path, 0, win32con.KEY_ALL_ACCESS)
                except Exception, ex:
                    if (ex[0] == 2): # 系统找不到指定的文件。
                        key = win32api.RegOpenKeyEx(win32con.HKEY_CURRENT_USER, "\\".join(path.split("\\")[:-1]), 0, win32con.KEY_ALL_ACCESS)
                        win32api.RegCreateKey(key, path.split("\\")[-1])
                        win32api.RegCloseKey(key)
                        key = win32api.RegOpenKeyEx(win32con.HKEY_CURRENT_USER, path, 0, win32con.KEY_ALL_ACCESS)
                    else:
                        raise 
                win32api.RegCreateKey(key, name)
                if isinstance(newvalue, int):
                    _type = 4 # DWORD win32con.REG_DWORD)
                    win32api.RegSetValueEx(key, name, 0, _type, newvalue)
                if isinstance(newvalue, basestring):
                    _type = 1 # win32con.REG_SZ) win32con.REG_BINARY??????
                    win32api.RegSetValueEx(key, name, 0, _type, newvalue)
                win32api.RegCloseKey(key)
            except:
                pass
        print e, path, name, newvalue
    return False

def set_windows_registry(path, name, newvalue):
    try:
        _set_windows_registry(path, name, newvalue)
    except Exception, e:
        print e, path, name, newvalue
    return False

def set_ie_registry():
    """设置IE注册表，处理各类已知弹出窗口"""
    for _i in range(3): # 多试几次，如果首次失败会创建路径，第二次再尝试
        set_windows_registry(r"Software\Microsoft\Windows\CurrentVersion\Internet Settings\Zones\3", 
                       "1601", 0) # 以后不再弹出, baidu.com 搜索的时候出现
        set_windows_registry(r"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                       "WarnOnZoneCrossing", 0) # 以后都不再弹出, gmail.com 的时候出现
        set_windows_registry(r"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                       "PrivDiscUiShown", 1) # 以后都不再显示，隐私策略
        set_windows_registry(r"Software\Microsoft\Internet Explorer\Main",
                       "Start Page", "about:blank") # 默认主页
        set_windows_registry(r"Software\Microsoft\Internet Explorer\Main",
                       "First Home Page", "about:blank") # 默认主页
        set_windows_registry(r"Software\Microsoft\Internet Explorer\New Windows",
                       "PopupMgr", 0) # 关闭弹出窗口阻止程序, IE7
        set_windows_registry(r"Software\Microsoft\Internet Explorer\New Windows",
                       "PopupMgr", "no") # 关闭弹出窗口阻止程序, IE6
        set_windows_registry(r"Software\Microsoft\Internet Explorer\Main",
                       "Display Inline Images", "no") # 关闭图片显示
    
        set_windows_registry(r"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                       "WarnonBadCertRecving", 0) # 猜的，未验证
        set_windows_registry(r"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                       "WarnOnPost", "no") # 猜的，未验证
        set_windows_registry(r"Software\Microsoft\Windows\CurrentVersion\Internet Settings",
                       "WarnOnPostRedirect", 0) # 猜的，未验证 
    
        set_windows_registry(r"Software\Microsoft\Internet Explorer\InformationBar",
                       "FirstTime", 0) 
        set_windows_registry(r"Software\Microsoft\Internet Explorer\Main",
                       "Use FormSuggest", "no") 
    
        # IE7, 仿冒网站筛选
        set_windows_registry(r"Software\Microsoft\Internet Explorer\PhishingFilter",
                       "Enabled", 1) 
        set_windows_registry(r"Software\Microsoft\Internet Explorer\PhishingFilter",
                       "ShownVerifyBalloon", 3) 

class SandBoxAuthClient(SanboxBaseClient):
    """验证客户端沙箱容器"""
    
    def __init__(self):
        cmd_path = os.path.join(os.path.dirname(os.path.realpath(__file__)), 'authclient.py')
        super(SandBoxAuthClient, self).__init__(ClientType.URL_AUTH_SANDBOX, cmd_path, 6)
        
    def before_run(self):
        set_ie_registry()
        logging.info('web shield proxy setup!')
        wangdun_proxy.setup()
        
        return super(SandBoxAuthClient, self).before_run()
        
        
def main():
    client = SandBoxAuthClient()
    client.main()

if __name__ == '__main__':
    main()