#!/usr/bin/python
# -*- coding: utf-8 -*-
"""安装测试
"""

from webauth.common.md5mgr import mkmd5fromstr

if __name__ == '__main__':
    print 'install test'
    print mkmd5fromstr('test')
    from init_system import init
    init()
    import django
    print django
    print 'install success'