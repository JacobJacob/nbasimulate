#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.entity import ErrorLog

def log_to_db(log, type='error'):
    error_log = ErrorLog()
    error_log.type = type
    error_log.log = log
    error_log.persist()