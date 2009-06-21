#!/usr/bin/python
# -*- coding: utf-8 -*-
"""black url相关操作
"""

from webauth.common.md5mgr import mkmd5fromstr
from webauth.common import urlutil
from webauth.common.db import connection
from webauth.common import json
from webauth.common.db.reserve_convertor import ReserveLiteral

    
_CHECK_URL = """
    SELECT url_md5, url_folder_md5, reason FROM black_url 
        WHERE url_md5 in (%s)
    UNION 
    SELECT url_md5, url_folder_md5, reason FROM black_url 
        WHERE url_folder_md5 IN(%s) limit 1"""

def check_url(url):
    """检测指定url的安全性
    若url不是规范的url，则直接返回(1, None, None)
    
    先使用url精确匹配，找到，则返回3；未找到，则
    按上3级的folder_md5进行匹配，若找到，则返回4；未找到，则
    按host_url和domain_url匹配, 若找到，则返回4；
    若都未找到，则返回1.
    
    level: 
    1 安全
    3 危险
    4 警告
    
    返回 (level, hit_url, reason)
    """
    url = url.lower()
    url_split = urlutil.standardize(url)
    level, hit_url, reason = 1, None, None
    # 若url不规则，则返回1, None
    if url_split is None:
        return level, hit_url, reason
    
    url_no_link = url_split.geturl()
    domain_url = url_split.domain_url
    domain_url_md5 = mkmd5fromstr(domain_url)
    host_url = url_split.host_url
    host_url_md5 = mkmd5fromstr(host_url)
    url_md5s = [mkmd5fromstr(url_no_link), 
                mkmd5fromstr(url_split.host), # 查host
                mkmd5fromstr(url_split.domain), # 查domain
                ]
    cursor = connection.cursor()
    try:
        parents = {domain_url_md5: domain_url, host_url_md5: host_url,}
        p = url_split
        for i in range(3):
            p = urlutil.get_folder(p)
            if p is None or p == host_url:
                break
            p_md5 = mkmd5fromstr(p)
            parents[p_md5] = p
        parent_md5s = ','.join("'%s'" % k for k in parents)
        rs = cursor.fetchall(_CHECK_URL % (','.join("'%s'" % k for k in url_md5s), 
                                           parent_md5s))
        if rs:
            for r in rs:
                if r[0] in url_md5s: # 如果精确找到，则修改结果
                    hit_url = url_no_link
                    level = 3 # 危险
                    reason = r[2]
                    break
                elif r[1] is not None and r[1] in parents:
                    hit_url = parents[r[1]]
                    reason = r[2]
                    level = 4 # 警告
    finally:
        cursor.close()
    if reason:
        reason = json.loads(reason)
    return level, hit_url, reason

def add_black_urls(urlinfos, cursor=None):
    """添加url到黑库
    确保来源是最原始的
    """
    if cursor is None:
        need_close = True
        cursor = connection.cursor()
    else:
        need_close = False
    try:
        cursor.insert(urlinfos, 'black_url', True, 
                      ['url_md5', 'url_created_time', 'created_time'])
    finally:
        if need_close:
            cursor.close()

_DELETE_BLACK_URLS = """delete from black_url where url_md5 in (%s)"""
def remove_black_urls(url_md5s, cursor=None):
    """从黑库移除指定url md5的数据, 返回成功删除的数目"""
    if not url_md5s:
        return
    if not isinstance(url_md5s, basestring):
        md5s = ','.join(["'%s'" % md5 for md5 in url_md5s])
    else:
        md5s = url_md5s
    if cursor is None:
        need_close = True
        cursor = connection.cursor()
    else:
        need_close = False
    try:
        return cursor.execute(_DELETE_BLACK_URLS % md5s)
    finally:
        if need_close:
            cursor.close()

def get_total_count():
    """获取黑url的总数"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone('show table status like "black_url"')['Rows']
    finally:
        cursor.close()

_SELECT_URL_BY_HOST = 'select * from black_url where host_md5=%s order by updated_time desc limit 10'
def get_urls_by_host(host):
    """根据host获取相关的url信息"""
    host_md5 = mkmd5fromstr(host)
    cursor = connection.cursor()
    try:
        return cursor.fetchall(_SELECT_URL_BY_HOST, host_md5)
    finally:
        cursor.close()

def _get_reason(url, ey_type):
    """
    http://trac.rdev.kingsoft.net/mercury/wiki/urlauth-url2.0
    
    ey_type: 1 网马, 2 木马, 3 钓鱼, 4 广告
    """
    if ey_type == 3:
        return [['16', url, '钓鱼网址']]
    elif ey_type == 4: 
        return [['11', url, '垃圾广告']]
    else: 
        return [['11', url, '恶意网址']]

def add_malicious_hosts(eyhost_infos, cursor=None):
    """添加恶意host
    直接添加到black_url
    """
    if isinstance(eyhost_infos, basestring):
        eyhost_infos = [eyhost_infos]
    black_hosts = []
    for host_info in eyhost_infos:
        host = host_info['host']
        host_md5 = mkmd5fromstr(host)
        url_split = urlutil.standardize(host)
        domain = url_split.domain
        domain_md5 = mkmd5fromstr(domain)
        url_folder = url_split.geturl()
        url_folder_md5 = mkmd5fromstr(url_folder)
        reason = _get_reason(url_folder, host_info['ey_type'])
        item = {
            'url': host,
            'url_md5': host_md5,
            'host': host,
            'host_md5': host_md5,
            'domain': domain,
            'domain_md5': domain_md5,
            'url_folder': url_folder,
            'url_folder_md5': url_folder_md5,
            'reason': json.dumps(reason),
            'from_type': host_info['from_type'],
            'check_type': host_info['check_type'],
            'reason_type': int(reason[0][0]),
            'created_time': ReserveLiteral("now()"),
            'url_created_time': ReserveLiteral("now()"), # 第一次更新
        }
        black_hosts.append(item)
    if cursor is None:
        need_close = True
        cursor = connection.cursor()
    else:
        need_close = False
    try:
        return cursor.insert(black_hosts, 'black_url', True, 
                             ['created_time', 'url_created_time', 'url_md5'])
    finally:
        if need_close:
            cursor.close()
            
def get_malicious_hosts():
    """获取调用网址源
    返回 {host: ey_type, ...}
    """
    cursor = connection.cursor()
    try:
        hosts = {}
        rs = cursor.fetchall("select url, domain_match, host_match, type from eyurl where domain_match > 0 or host_match > 0")
        for r in rs:
            if r[1] > 0 or r[2] > 0:
                hosts[r[0].encode('utf-8')] = r[3]
        return hosts
    finally:
        cursor.close()