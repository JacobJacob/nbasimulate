#!/usr/bin/python
# -*- coding: utf-8 -*-
"""win32 process killer"""

import os

import win32con
import win32api
import win32process 
import pywintypes


def kill_processname(process_names):
    is_list = hasattr(process_names, "__iter__")
    pids = win32process.EnumProcesses()
    for pid in pids:
        hProcess = None
        try:
            hProcess = win32api.OpenProcess(win32con.PROCESS_ALL_ACCESS, False, pid)
            hModules = win32process.EnumProcessModules(hProcess)
            for hModule in hModules:
                try:
                    process_path = win32process.GetModuleFileNameEx(hProcess, hModule)
                except pywintypes.error:
                    continue
                if process_path:
                    pname = os.path.basename(process_path)
                    pname = pname.lower()
                    if is_list:
                        if pname in process_names:
                            kill_pid(pid)
                            break
                    elif process_names == pname:
                        kill_pid(pid)
                        break
        except pywintypes.error:
            pass
        finally:
            if hProcess:
                win32api.CloseHandle(hProcess)
                
def kill_pid(pid):
    try:
        handle = win32api.OpenProcess(win32con.PROCESS_TERMINATE, False, pid)
        try:
            win32api.TerminateProcess(handle, -1)
        finally:
            win32api.CloseHandle(handle)
    except:
        pass