#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url第三方数据"""

from webauth.common.db import connection
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.md5mgr import mkmd5fromstr
from webauth.common import urlutil
from webauth.common.constants.url_task_const import UrlFromType

        
_SELECT_URL = """select * from third_data where host_md5 in (%s, %s)"""
def get_data(url):
    """获取url信息"""
    split = urlutil.standardize(url)
    if split is None:
        return None
    domain_md5, host_md5= mkmd5fromstr(split.domain), mkmd5fromstr(split.host)
    cursor = connection.cursor()
    try:
        return cursor.fetchall(_SELECT_URL, (domain_md5, host_md5))
    finally:
        cursor.close()
        
def get_total_count():
    """获取url总数"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone("select id from third_data order by id desc limit 1")[0]
    finally:
        cursor.close()