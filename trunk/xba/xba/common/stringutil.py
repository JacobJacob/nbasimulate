#!/usr/bin/python
# -*- coding: utf-8 -*-

def ensure_utf8(s):
    if isinstance(s, unicode):
        s = s.encode("utf8")
    return s

def ensure_gbk(s):
    if isinstance(s, unicode):
        s = s.encode("gbk")
    return s