#!/usr/bin/python
# -*- coding: utf-8 -*-
"""sandbox tool"""

import os
import threading
import time
import re
import win32api
import win32con
import win32process
import pywintypes


class Sandbox(object):
    _sandbox_path = r'C:\"Program Files\Sandboxie\Start.exe"'
    _box_names = ["s%s" % x for x in range(10)]
    _start_cmd = _sandbox_path + " /silent /nosbiectrl /box:%s python %s"
    _stop_cmd = _sandbox_path + " /silent /nosbiectrl /box:%s /terminate"
    _delete_cmd = _sandbox_path + " /silent /nosbiectrl /box:%s delete_sandbox_silent"
    _list_pid_cmd = _sandbox_path + " /silent /box:%s /listpids"
    _box_name_waiter = threading.Condition(threading.Lock())
    _cmd_path = None
    _running_threads = []
    _process_regex = re.compile(r"(\S+)\s+\d+\s+")
    _doing = True
    _restart = False
    _running_box = []

    @classmethod
    def run(cls, cmd_path = None):
        cls._cmd_path = cmd_path
        cls._run()
        while cls._restart:
            cls._restart = False
            cls._run()

    @classmethod
    def _run(cls):
        cls._threads_done()
        box_count = len(cls._box_names)
        cls._doing = True
        while box_count:
            box_count -= 1
            box_name = cls._get_boxname()
            cls._running_box.append(box_name)
            t = threading.Thread(target = cls.run1box, args = (box_name,))
            t.setDaemon(True)
            cls._running_threads.append(t)
            t.start()
        cls._threads_done()

    @classmethod
    def stop(cls):
        cls._doing = False
        cls._threads_done()

    @classmethod
    def restart(cls):
        cls._restart = True
        cls._doing = False

    @classmethod
    def run1box(cls, box_name = None):
        if not box_name:
            box_name = cls._get_boxname()
        #启动沙箱
        while cls._doing:
            cls._clear_sandbox(box_name)
            cmd_path = cls._get_cmd_path()
            cmd_path += " " + box_name
#            cmd_path = "d:\\runner.py"
            start_cmd = cls._start_cmd % (box_name, cmd_path)
            os.system(start_cmd)
            #等待沙箱结束
            cls._wait_complete(box_name)

    @classmethod
    def _get_boxname(cls):
        #取出空闲的沙箱
        cls._box_name_waiter.acquire()
        if not cls._box_names:
            cls._box_name_waiter.wait()
        box_name = cls._box_names.pop(0)
        cls._box_name_waiter.release()
        return box_name

    @classmethod
    def _wait_complete(cls, box_name):
        while cls._doing and cls._exists_python(box_name):
            time.sleep(1)
        cls._clear_sandbox(box_name)
        cls._box_name_waiter.acquire()
        if not box_name in cls._box_names:
            cls._box_names.append(box_name)
        cls._box_name_waiter.notify()
        cls._box_name_waiter.release()

    @classmethod
    def _clear_sandbox(cls, box_name):
        os.system(cls._stop_cmd % box_name)
        os.system(cls._delete_cmd % box_name)

    @classmethod
    def _get_cmd_path(cls):
        if not cls._cmd_path:
            code_path = os.path.realpath(__file__)
            code_folder = os.path.dirname(code_path)
            cls._cmd_path = os.path.join(code_folder, "sanbox_test.py")
        return cls._cmd_path

    @classmethod
    def _exists_python(cls, box_name):
        pids_cmd = cls._list_pid_cmd % box_name
        pid_process = os.popen(pids_cmd)
        pid_lines = pid_process.readlines()
        pid_process.close()
        
        for pid in pid_lines:
            pid = pid.strip()
            try:
                pid = int(pid)
            except:
                continue            
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
                        process_name = os.path.basename(process_path)
                        process_name = process_name.lower()
                        if process_name in ("python", "python.exe"):
                            return True
            except pywintypes.error:
                pass
            finally:
                if hProcess:
                    win32api.CloseHandle(hProcess)
        return False

    @classmethod
    def _threads_done(cls):
        while cls._running_threads:
            try:
                t = cls._running_threads.pop(0)
            except IndexError:
                break
            t.join()
        while cls._running_box:
            try:
                box_name = cls._running_box.pop(0)
            except IndexError:
                break
            cls._wait_complete(box_name)


def run(count=10, cmd_path = None):
#    assert 0 < count and count < 20, "(0, 20)" # 因为部署配置的时候只有20个
    Sandbox._box_names = ["s%s" % x for x in range(count)]
    Sandbox.run(cmd_path)

def stop():
    Sandbox.stop()

def restart():
    Sandbox.restart()

if __name__ == "__main__":
    run(2)
