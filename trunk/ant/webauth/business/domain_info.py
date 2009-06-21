#!/usr/bin/python
# -*- coding: utf-8 -*-

from webauth.common.db import connection
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.filelocker import FileLocker

_SELECT_DOMAINS_FOR_ALEXA = """select domain from domain_info where alexa_rank is null limit 1"""
_SELECT_DOMAINS_FOR_GOOGLE = """select domain from domain_info where google_pr is null limit 3"""
_RESET_ALEXA_DOMAIN_TASK = """update domain_info set alexa_rank = null where alexa_rank = -1 """
_RESET_GOOGLE_DOMAIN_TASK = """update domain_info set google_pr = null where google_pr = -1 """

def fetch_domain(task_type):
    """获取到要爬取域名列表
    @param: task_type 任务类别,分为ALEXA 和  GOOGLE
    @return: 列表类型，包括要爬取的域名 
    """
    cursor = connection.cursor()
    try:
        locker = FileLocker("fetch_domain")
        locker.lock()
        if task_type == 'ALEXA':
            sql = _SELECT_DOMAINS_FOR_ALEXA
        else:
            sql = _SELECT_DOMAINS_FOR_GOOGLE
        try:
            domains = cursor.fetchall(sql)
            if domains:
                domain_infos = domains.to_list()
                if task_type == 'ALEXA':
                    for domain_info in domain_infos:
                        domain_info['alexa_rank'] = -2
                else:
                    for domain_info in domain_infos:
                        domain_info['google_pr'] = -2
                update_domain(domain_infos)
                return domain_infos
            else:
                return None
        finally:
            locker.unlock()
    finally:
        cursor.close()

def update_domain(domain_info):
    """更新域名信息
    @param domain_info:单个字典或多个字典组成的列表,每个字典封装一个域名的信息 
    """
    if isinstance(domain_info, list):
        for domain in domain_info:
            if 'google_pr' in domain:
                domain['google_updated_time'] = ReserveLiteral("now()")
            if 'alexa_rank' in domain:
                domain['alexa_updated_time'] = ReserveLiteral("now()")
    else:
        if 'google_pr' in domain_info:
            domain_info['google_updated_time'] = ReserveLiteral("now()")
        if 'alexa_rank' in domain_info:
            domain_info['alexa_updated_time'] = ReserveLiteral("now()")
    cursor = connection.cursor()
    try:
        cursor.update(domain_info, 'domain_info', ['domain'])
    finally:
        cursor.close()

def insert_domain(domain_info):
    """插入域名信息
    @param domain_info:单个字典或多个字典组成的列表,每个字典封装一个域名的信息  
    """         
    cursor = connection.cursor()
    try:
        cursor.insert(domain_info, 'domain_info', False, ['domain_md5', 'domain'])
    finally:
        cursor.close()

def reset_domain_task(task_type):
    """重置失败的爬取任务
    @param task_type:任务类型,分为ALEX和GOOGLE 
    """
    if task_type == 'ALEXA':
        sql = _RESET_ALEXA_DOMAIN_TASK
    else:
        sql = _RESET_GOOGLE_DOMAIN_TASK
    cursor = connection.cursor()
    try:
        cursor.execute(sql)
    finally:
        cursor.close()