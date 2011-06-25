#!/usr/bin/python
# -*- coding: utf-8 -*-
"""server infomation
"""

import os
import re
import time
import subprocess
import socket

from xba.config import PathSettings
from xba.common.file_utility import ensure_dir_exists

    
def netstat_info():
    cmd = ["netstat -nta|awk '/^tcp/{++S[$NF]} END {for(a in S) print a,S[a]}'"]
    p = subprocess.Popen(cmd, shell=True, close_fds=True,
                         stdout=subprocess.PIPE)
    outdata = p.communicate()[0]
    return outdata.strip().split('\n')

def linux_cpu_usage():
    info1 = _linux_cpu_info()
    time.sleep(1)
    info2 = _linux_cpu_info()
    usage = []
    for i in range(len(info1)):
        cpu, used1, total1 = info1[i]
        used2, total2 = info2[i][1:]
        percent = (used2 - used1) * 100.0 / (total2 - total1)
        usage.append((cpu, percent))
    return usage

def _linux_cpu_info():
    cmd = "cat /proc/stat"
    p = os.popen(cmd)
    lines = p.readlines()
    p.close()
    r = re.compile(r"\s+")
    cpu_list = []
    for line in lines:
        if line.startswith("cpu"):
            cpu_list.append(r.split(line))
    info_list = []
    for cpu in cpu_list:
        total = 0
        used = 0
        for i in range(1, len(cpu)):
            if cpu[i]:
                total += int(cpu[i])
                if i < 4:
                    used += int(cpu[i])
        info_list.append((cpu[0], used, total))
    return info_list

def linux_mem_usage(unit = "m"):
    format = {"k": 1024.0,
              "m": 1024 * 1024.0,
              "g": 1024 * 1024 * 1024.0}
    unit = format[unit]
    cmd = "cat /proc/meminfo"
    p = os.popen(cmd)
    lines = p.readlines()
    p.close()
    total = None
    free = None
    is_total = False
    is_free = False
    for line in lines:
        if free and total:
            break
        line = line.lower()
        if not total and line.startswith("memtotal:"):
            is_total = True
        elif not free and line.startswith("memfree:"):
            is_free = True
        if is_total or is_free:
            info = line.split(" ")
            size = info[-2].strip()
            if is_total:
                total = int(size) * 1024
            else:
                free = int(size) * 1024
            is_total = is_free = False
    return free / unit, total / unit

def disk_usage():
    ensure_dir_exists(PathSettings.WORKING_FOLDER)
    free, used = get_diskinfo(PathSettings.WORKING_FOLDER, "g")
    return free, used + free

def get_real_ip():
    """获取真实的外网ip"""
    try:
        if os.name != 'nt':
            p = subprocess.Popen(["/sbin/ifconfig | grep 'inet addr' | awk '{print $2}'"], 
                             stdout=subprocess.PIPE, shell=True)
            out = p.communicate()[0]
            ip = out[out.find(':')+1:out.find('\n')]
            if not ip: # 测试环境如果是中文
                p = subprocess.Popen(["/sbin/ifconfig | grep 'inet' | awk '{print $2}'"], 
                             stdout=subprocess.PIPE, shell=True)
                out = p.communicate()[0]
                ip = out[out.find(':')+1:out.find('\n')]
            return ip
    except:
        pass
    return socket.gethostbyname(socket.gethostname())


if __name__ == '__main__':
    print get_real_ip()