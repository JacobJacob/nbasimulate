#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from webauth.common.db import connection
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.md5mgr import mkmd5fromstr
from webauth.common import urlutil
from webauth.common.constants.url_task_const import UrlFromType


def add_hot_urls(urlinfos, source):
    """添加热门url
    将url保存到hot_url，并自动添加到url_source
    """
    if isinstance(urlinfos, dict):
        urlinfos = [urlinfos]
    for urlinfo in urlinfos:
        url_split = urlutil.standardize(urlinfo['url'])
        if not url_split:
            urlinfos.remove(urlinfo)
            continue
        urlinfo['url'] = url_split.geturl()
        urlinfo['url_md5'] = mkmd5fromstr(urlinfo['url'])
        urlinfo['from_type'] = source
        urlinfo['created_time'] = ReserveLiteral("now()")
    cursor = connection.cursor()
    try:
        cursor.insert(urlinfos, 'hot_url', True, 
                      ['url_md5', 'url', 'from_type', 'created_time'])
        # 添加到url_source
        add_url([urlinfo['url'] for urlinfo in urlinfos], source, cursor)
    finally:
        cursor.close()

def add_url(urls, source, cursor=None, change_source=False):
    """
    urls: 要添加的url列表
    source: 来源
    change_source: 是否需要更新来源，默认为false
    """
    if isinstance(urls, basestring):
        urls = [urls]
    items = []
    for url in urls:
        split_result = urlutil.standardize(url)
        if split_result is None:
            continue
        url = split_result.geturl()
        host = split_result.host
        domain = split_result.domain
        items.append({
            'url': url,
            'from_type': source,
            'url_md5': mkmd5fromstr(url),
            'host': host,
            'host_md5': mkmd5fromstr(host),
            'domain': domain,
            'domain_md5': mkmd5fromstr(domain),
            'created_time': ReserveLiteral("now()"),
        })
    
    if not cursor:
        insert_cursor = connection.cursor()
    else:
        insert_cursor = cursor
    try:
        columns = ['url_md5', 'url', 'created_time']
        if not change_source: # 指定是否更新来源
            columns.append('from_type')
        return insert_cursor.insert(items, 'url_source', True, columns) # 防止domain算法改变
    finally:
        if not cursor:
            insert_cursor.close()
        
_SELECT_URL = """select * from url_source where url_md5=%s"""
def get_url(url):
    """获取url信息"""
    url_md5 = mkmd5fromstr(url.strip())
    cursor = connection.cursor()
    try:
        url_info = cursor.fetchone(_SELECT_URL, url_md5)
        return url_info
    finally:
        cursor.close()

def import_webshield_url(datas):
    webshield_datas = []
    urls = set()
    for data in datas:        
        url = data.get("url")
        if url:
            webshield_data = {}
            webshield_data["url"] = url
            webshield_data["url_md5"] = data.get("url_md5")
            webshield_data["host"] = data.get("host")
            webshield_data["host_md5"] = data.get("host_md5")
            webshield_data["report_time"] = data.get("report_time")
            webshield_data["client_program"] = data.get("client_program")
            webshield_data["webshield_version"] = data.get("webshield_version")
            webshield_data["reason"] = data.get("reason")
            webshield_data["group_id"] = data.get("group_id")
            webshield_data["raw_data"] = data.get("raw_data")
            webshield_data["created_time"] = ReserveLiteral("now()")
            webshield_datas.append(webshield_data)
            if not url in urls:
                urls.add(webshield_data["url"])
    del datas
    
    cursor = connection.cursor()
    cursor.execute("start transaction;")
    try:
        cursor.insert(webshield_datas, "webshield_url")
        add_url(urls, UrlFromType.WEBSHIELD, cursor)
        cursor.execute("commit;")
    except:
        cursor.execute("rollback;")
        raise
    finally:
        cursor.close()
        
def get_total_count():
    """获取url总数"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone("select id from url_source order by id desc limit 1")[0]
    finally:
        cursor.close()