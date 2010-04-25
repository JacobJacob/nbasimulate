#!/usr/bin/python
# -*- coding: utf-8 -*-
"""SVN utils method"""

import os
import subprocess

def svn_update(path=None):
    """更新指定路径的代码，若path为空，则更新整个项目的代码。
    return (True/False, version string)
    """
    if path is None:
        path = os.path.dirname(os.path.realpath(__file__))
        path = os.path.dirname(path)
    if os.path.isdir(path):
        subprocess.check_call(['svn', 'cleanup', path]) # 先清理一下，保证缓存的svn服务器ip等信息得到更新
    
    update_cmd = ['svn', 'up', path]
    p = subprocess.Popen(update_cmd, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
    stdout, stderror = p.communicate()
    if p.returncode == 0:
        return True, stdout.split()[-1][:-1]
    else:
        return False, stderror
    
def svn_checkout(svnpath, localpath):
    """签出指定的svn到本地"""
    try:
        subprocess.check_call(['svn', 'cleanup', localpath])
    except:
        pass
    subprocess.check_call(['svn', 'co', svnpath, localpath, 
                           '--username', 'mercury_upgrader', 
                           '--password', 'mercury123',])
