#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from file_utility import ensure_dir_exists
from webauth.config import MinDiskSize


def get_diskinfo(pathinfo, format = "g"):
    """return available space and used space tuple with bytes"""
    formatdict = {"g" : 1024 * 1024 * 1024, 
                  "m" : 1024 * 1024,
                  "k" : 1024,
                  "b" : 1,}
    if os.name == "nt":
        info = _get_windows_diskinfo(pathinfo)
    else:
        info = _get_linux_diskinfo(pathinfo)
    return float(info[0])/formatdict[format], float(info[1])/formatdict[format]

def disk_available(path, min_size = None):
    ensure_dir_exists(path)
    if min_size is None:
        min_size = MinDiskSize
    size = get_diskinfo(path, "b")[0]
    return size > MinDiskSize

def _get_linux_diskinfo(pathinfo):
    dinfo = os.statvfs(pathinfo)
    return dinfo.f_bsize * dinfo.f_bavail, dinfo.f_bsize * (dinfo.f_blocks - dinfo.f_bavail)

def _get_windows_diskinfo(pathinfo):
    from win32api import GetDiskFreeSpaceEx
    dinfo = GetDiskFreeSpaceEx(pathinfo)
    return dinfo[0], dinfo[1] - dinfo[0]