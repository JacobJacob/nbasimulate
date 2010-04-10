#!/usr/bin/python
# -*- coding: utf-8 -*-
"""server infomation
"""

import os
import re
import time
import socket

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

def get_ip():
    if os.name != 'nt':
        try:
            ip = os.popen("/sbin/ifconfig | grep 'inet addr' | awk '{print $2}'").read()
            ip = ip[ip.find(':')+1:ip.find('\n')]
        except:
            pass
    else:
        ip = socket.gethostbyname(socket.gethostname())
    return ip
