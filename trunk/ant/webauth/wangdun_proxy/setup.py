#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

def __cmd(cmd):
    print cmd
    os.system(cmd)

def uninstall(path):
    __cmd('regsvr32 /s /u "%s"' % path)
    
def _change_setting():
    """修改配置，不弹对话框"""
    ini_path = r"C:\Documents and Settings\All Users\Application Data\Kingsoft\kws\kws.ini"
    if not os.path.exists(ini_path):
        return
    ini_infos = []
    f_read = open(ini_path, 'rb')
    try:
        for line in f_read:
            if not line.startswith('needblockdlg'):
                ini_infos.append(line)
    finally:
        f_read.close()
    ini_infos.append('needblockdlg=0\r\n')
    f = open(ini_path, 'wb')
    try:
        f.write(''.join(ini_infos))
    finally:
        f.close()

def setup():
    _change_setting()
    old_path = r"C:\Program Files\Kingsoft\WangDunProxy\WangDunProxy.dll"
    uninstall(old_path)
    kks = os.path.dirname(os.path.realpath(__file__))
    kks += r"\WangDunProxy\Release\WangDunProxy.dll"
    new_path = os.path.abspath(kks)
    __cmd('regsvr32 /s "%s"' % new_path)
    

if __name__ == "__main__":
    setup()