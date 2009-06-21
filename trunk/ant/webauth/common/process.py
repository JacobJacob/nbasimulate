#!/usr/bin/python
# -*- coding: utf-8 -*-
"""进程相关的辅助工具方法

目前只支持windows下的api
"""

import subprocess
import os

import win32process
import win32api
import win32con
import pywintypes


def get_pid(process_name):
    process_name = process_name.lower()
    pids = win32process.EnumProcesses()
    return_value = []
    for pid in pids:
        hProcess = None
        try:
            hProcess = win32api.OpenProcess(win32con.PROCESS_QUERY_INFORMATION | win32con.PROCESS_VM_READ, False, pid)
            hModules = win32process.EnumProcessModules(hProcess)
            process_path = None
            for hModule in hModules:
                try:
                    process_path = win32process.GetModuleFileNameEx(hProcess, hModule)
                except pywintypes.error:
                    continue
                if process_path:
                    pname = os.path.basename(process_path)
                    pname = pname.lower()
                    if process_name == pname:
                        return_value.append(pid)
                        break
        except pywintypes.error:
            pass
        finally:
            if hProcess:
                win32api.CloseHandle(hProcess)
    return return_value


#def get_pid(processname):
#    """根据进程名获取进程id，返回第一个匹配的进程id"""
#    cmd = 'tasklist /FI "IMAGENAME eq %s"' % processname
#    p = subprocess.Popen(cmd, stdout=subprocess.PIPE, stderr=subprocess.PIPE)
#    stdout = p.communicate()[0]
#    if not stdout:
#        return None
#    for line in stdout.split('\r\n'):
#        data = line.split()
#        if len(data) > 2:
#            if processname == data[0]:
#                return int(data[1])

def kill_process(pid_or_name):
    if isinstance(pid_or_name, basestring):
        pids = get_pid(pid_or_name)
        if not pids:
            return False
        pid_or_name = pids[0]
    cmd = 'ntsd -c q -p %s' % pid_or_name
    return subprocess.check_call(cmd)

def reboot():
    """机器重启"""
    kill_process('winlogon.exe')
#if __name__ == '__main__':
#    kill_process('notepad.exe')
#    reboot()