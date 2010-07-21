#!/usr/bin/python
# -*- coding: utf-8 -*-
"""SVN utils method"""

import os
import subprocess


def svninfo(filepath, pre="dtspider/", 
            svnurl='http://svn.rdev.kingsoft.net/mercury/trunk/dtspider/'):
    filepath = filepath.replace("\\", "/")
    if filepath.find("dtspider/") != -1:
        filepath = filepath[filepath.rfind("dtspider/"):]
    url = '%s%s' % (svnurl, filepath)
    data = subprocess.Popen(['svn', 
                             '--username', 'mercury_upgrader', 
                             '--password', 'mercury123', 
                             'info',  url],
                             stdout=subprocess.PIPE).communicate()[0]
    author, version = None, None
    for line in data.splitlines():
        if line.startswith('Last Changed Author:'):
            author = line.split()[-1]
        elif line.startswith('Last Changed Rev:'):
            version = line.split()[-1]
    return author, version

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


#if __name__ == '__main__':
#    svn_checkout('http://svn.rdev.kingsoft.net/KXEngine/WebShield/trunk/Document', 'd:/eyurl')
#    p = os.path.realpath(__file__)
#    print p
#    print svninfo(p)
#    import dtspider
#    print dtspider.VERSION
#    
#    print svn_update()