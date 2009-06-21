#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""
from copy import deepcopy

from webauth.common.db import connection
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.md5mgr import mkmd5fromstr
from webauth.common import urlutil, log_execption
from webauth.common import json
from webauth.common.constants import UrlFromType
from webauth.common.mailutil import send_to
from webauth.business import black_url
from webauth.business import url_source
from webauth.business.priority_logic import url_type2priority
from webauth.business import white_host


_DELETE_RESULTS = """delete from url_check_result where url_md5 in (%s)"""
_SELECT_URL_INFO = """select url_md5, from_type from url_source where url_md5 in (%s)"""
def add_results(results):
    """添加扫描结果
    若url扫描结果为黑
    1. 来源是重检测的，则直接入黑库
    2. 来源不是重检测的，则添加新任务，设置来源为重检测
    若url扫描结果为白
    1. 来源是重检测的，只从黑库里面删除该url
    2. 来源不是重检测的，从结果表和黑表中都删除该url
    
    
    返回需要重新检查的任务
    """
    results_items, url_infos, black_urls, recheck_tasks = [], [], [], []
    remove_from_black_md5s, remove_from_result_md5s = [], []
    new_urls = set() # 若是危险url，则切割获取新的url
    white_hosts = white_host.get_white_hosts()
    for r in results:
        url = r['url']
        split_result = urlutil.standardize(url)
        if split_result is None:
            continue
        url = split_result.geturl()
        url_md5 = mkmd5fromstr(url)
        host = split_result.host
        host_md5 = mkmd5fromstr(host)
        domain = split_result.domain
        domain_md5 = mkmd5fromstr(domain)
        url_folder = split_result.folder
        # 若url_folder等于host url并且url不是根目录，则设置url_folder为空
        if url_folder is None or (not split_result.is_host and url_folder == split_result.host_url):
            url_folder_md5 = None
        else:
            url_folder_md5 = mkmd5fromstr(url_folder)
        
        # 更新url source信息
        url_infos.append({
            'url': url,
            'title': r.get('title', None),
            'description': r.get('description', None),
            'from_type': r['from_type'],
            'url_md5': url_md5,
            'host': host,
            'host_md5': host_md5,
            'domain': domain,
            'domain_md5': domain_md5,
            'created_time': ReserveLiteral("now()"),
        })
        
        item = {'url': url,
                'url_md5': url_md5,
                'host': host,
                'host_md5': host_md5,
                'domain': domain,
                'domain_md5': domain_md5,
                'url_folder': url_folder,
                'url_folder_md5': url_folder_md5,
                'created_time': ReserveLiteral("now()"),
                'reason': json.dumps(r['reason']),
                'url_created_time': r.get('url_created_time', ReserveLiteral("now()")),
                'from_type': r['from_type'],
                'reason_type': 0,
            }
        
        if r['reason'] is not None: # 有reason，则表示是危险url
            # 过滤白名单，在白名单host的话，folder必须设置为None
            if host in white_hosts or domain in white_hosts:
                if split_result.is_host: # 误报，发邮件通知
                    try:
                        msg = '%s: %r' % (url, item)
                        send_to('严重, 误报白名单host!', msg)
                    except:
                        log_execption()
                    continue
                item['url_folder'] = None
                item['url_folder_md5'] = None
            item['reason_type'] = int(r['reason'][0][0]) # 第一个原因的代号
            results_items.append(item)
            if r['from_type'] == UrlFromType.RECHECK:
                # 来源是重检测，则直接入黑库
                black_item = deepcopy(item)
                black_urls.append(black_item)
            else: 
                # 其他来源，则入结果表，并添加一条重检测任务
                recheck_task = {'url': url, 
                                'url_md5': url_md5,
                                'from_type': UrlFromType.RECHECK,
                                'priority': url_type2priority(UrlFromType.RECHECK),
                                }
                recheck_tasks.append(recheck_task)
            
                # 切割，获取host和domain url，及上3级folder
                host_url = split_result.host_url
                new_urls.add(host_url)
                new_urls.add(split_result.domain_url)
                p = split_result
                for i in range(3):
                    p = urlutil.get_folder(p)
                    if p is None or p == host_url:
                        break
                    new_urls.add(p)
                    
                # 若realurl不等于当前url，则添加
                real_url = r['real_url']
                if real_url:
                    real_url_split = urlutil.standardize(real_url)
                    if real_url_split is not None:
                        real_url = real_url_split.geturl()
                        if real_url != url:
                            new_urls.add(real_url)
        else:
            remove_from_black_md5s.append(url_md5)
            # 安全并且不是重检测的，则需要从结果删除
            if r['from_type'] != UrlFromType.RECHECK:
                remove_from_result_md5s.append("'%s'" % url_md5)
            else:
                # recheck的话，需要修改结果表，使它变白
                results_items.append(item)
    cursor = connection.cursor()
    try:
        if black_urls:
            # 获取url的原始来源
            rs = cursor.fetchall(_SELECT_URL_INFO % \
                                ','.join(['"%s"' % urlinfo['url_md5'] for urlinfo in black_urls]))
            url_source_infos = {}
            for r in rs:
                url_source_infos[r[0].encode('utf-8')] = r[1]
            for info in black_urls:
                from_type = url_source_infos.get(info['url_md5'], None)
                if from_type is not None:
                    info['from_type'] = from_type
            black_url.add_black_urls(black_urls, cursor)
        if remove_from_black_md5s:
            black_url.remove_black_urls(remove_from_black_md5s, cursor)
        if new_urls:
            url_source.add_url(new_urls, UrlFromType.SPLIT_URL, cursor=cursor)
        if results_items:
            cursor.insert(results_items, 'url_check_result', True, 
                          ['url_md5', 'url_created_time', 'from_type', 'created_time'])
        cursor.insert(url_infos, 'url_source', True, 
                      ['url_md5', 'url', 'from_type', 'created_time']) # 更新url额外信息
        if remove_from_result_md5s:
            cursor.execute(_DELETE_RESULTS % ','.join(remove_from_result_md5s))
    finally:
        cursor.close()
        
    return recheck_tasks

def get_total_count():
    """获取url检测任务结果的总数"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone('show table status like "url_check_result"')['Rows']
    finally:
        cursor.close()