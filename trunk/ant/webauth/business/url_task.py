#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url check task"""

from webauth.common.db import connection
from webauth.common import json
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.constants import UrlTaskStatus, UrlFromType, Priority
from webauth.common.filelocker import FileLocker
from webauth.common import urlutil
from webauth.common.md5mgr import mkmd5fromstr
from webauth.business import url_result


#_SELECT_TASKS = """select id, url, url_md5, priority, from_type, url_created_time from url_check_task 
#    where status=%s order by priority desc, id limit 20
#"""
_SELECT_TASKS = """select url, url_md5, priority, from_type, url_created_time from url_check_task 
    where status=%s limit 20
"""
_UPDATE_TASKS = """update url_check_task set status=%%s, last_ip=%%s, 
        expired_time=now()+interval 30 minute, fetch_count=fetch_count+1
    where url_md5 in (%s)
"""
def fetch_task(client_ip=None):
    cursor = connection.cursor()
    try:
        locker = FileLocker("fetch_url_task")
        locker.lock()
        try:
            tasks = cursor.fetchall(_SELECT_TASKS, (UrlTaskStatus.TODO,))
            if tasks:
                ts, url_md5s = {}, []
                for task in tasks.to_list():
                    if task["url_md5"] not in ts:
                        ts[task["url_md5"]] = task
                        url_md5s.append('"%s"' % task["url_md5"])
                update_sql = _UPDATE_TASKS % ','.join(url_md5s)
                cursor.execute(update_sql, (UrlTaskStatus.DOING, client_ip))
                return ts.values()
            else:
                return None
        finally:
            locker.unlock()
    finally:
        cursor.close()

_SELECT_NEW_URLS = """select id, url, url_md5, from_type, created_time as url_created_time
    from url_source where id>%s and created_time>=%s limit %s
"""
_SELECT_RECHECK_TASKS = """select id, url, url_md5, url_created_time from url_check_result 
    where updated_time <= date_sub(now(), interval 12 hour) limit %s"""
def fetch_todo_url(begin_time, start_id, count):
    """获取新增url，及需要重新检测的url"""
    cursor = connection.cursor()
    try:
        tasks = []
        # 获取新url
        rs = cursor.fetchall(_SELECT_NEW_URLS, (start_id, begin_time, count))
        if rs:
            tasks = rs.to_list()
        # 获取重新检测的url
        recheck_rs = cursor.fetchall(_SELECT_RECHECK_TASKS, (count,))
        for r in recheck_rs:
            tasks.append({
                'id': r['id'],
                'url': r['url'],
                'url_md5': r['url_md5'],
                'from_type': UrlFromType.RECHECK,
                'url_created_time': r['url_created_time'],
            })
        return tasks
    finally:
        cursor.close()

_UPDATE_RECHECK_TASKS = 'update url_check_result set updated_time=now() where url_md5 in (%s)'
def insert_task(url_tasks):
    updated_md5s = []
    need_tasks = []
    for i in url_tasks:
        if i['from_type'] == UrlFromType.RECHECK: # 需要更新时间
            updated_md5s.append('"%s"' % i['url_md5'])
        if 'no_need' in i: # 钓鱼的不需要添加
            continue
#        if i.get('is_fishing', False): # 钓鱼的不需要添加
#            continue
        
        if 'id' in i: # 删除id，避免出问题
            del i['id']
        i["created_time"] = ReserveLiteral("now()")
        i['status'] = UrlTaskStatus.TODO
        need_tasks.append(i)
    cursor = connection.cursor()
    try:
        if updated_md5s: # 更新结果表的时间，避免重复获取
            cursor.execute(_UPDATE_RECHECK_TASKS % ','.join(updated_md5s))
        if need_tasks:
            return cursor.insert(need_tasks, "url_check_task", True, 
                                 ['url', 'url_md5', 'created_time'])
    finally:
        cursor.close()
        
def add_emergency_tasks(urls, source):
    """添加紧急任务，优先级将被设置成CRITICAL"""
    if isinstance(urls, basestring):
        urls = [urls]
    tasks = []
    if source is None:
        source = UrlFromType.WEB_SUBMIT
    for url in urls:
        url_split = urlutil.standardize(url)
        if url_split is None:
            continue
        url = url_split.geturl()
        url_md5 = mkmd5fromstr(url)
        tasks.append({'url': url, 
                      'url_md5': url_md5,
                      'from_type': source,
                      'priority': Priority.CRITICAL,
                      })
    insert_task(tasks)
    return len(tasks)

def update_exprired_task():
    """重置超时的任务，当超时时间小于当前时间时，重置"""
    sql = """update url_check_task set status=%s, expired_time=null 
        where expired_time<=now() and status=%s limit 1000"""
    cursor = connection.cursor()
    try:
        return cursor.execute(sql, (UrlTaskStatus.TODO, UrlTaskStatus.DOING))
    finally:
        cursor.close()

__FINISH_TASK_SQL = """
    delete from url_check_task where url_md5 in (%s)
"""
def finish_task(task_results, client_ip):
    """任务完成"""
    # 先添加结果
    recheck_tasks = url_result.add_results(task_results)
    # 再结束任务
    cursor = connection.cursor()
    try:
        # 1 移动任务到备份表
        _backup_task(task_results, client_ip, cursor)
        # 2 删除任务
        url_md5s = ['"%s"' % task["url_md5"] for task in task_results]
        delete_sql = __FINISH_TASK_SQL % ','.join(url_md5s)
        task_count = cursor.execute(delete_sql)
    finally:
        cursor.close()
    # 添加重新检测任务
    if recheck_tasks:
        insert_task(recheck_tasks)
    return task_count
    
def _backup_task(tasks, client_ip, cursor):
    """保存检测历史"""
    items = []
    for t in tasks:
        url = t['url']
        url_md5 = t['url_md5']
        if t['reason']:
            reason_type = int(t['reason'][0][0])
        else:
            reason_type = None
        item = {
            'url': url,
            'url_md5': url_md5,
            'real_url': t['real_url'],
            'title': t['title'],
            'description': t['description'],
            'usetime': t['usetime'],
            'status_code': t['status_code'],
            'visit_success': t['visit_success'],
            'is_timeout': t['is_timeout'],
            'reason': json.dumps(t['reason']),
            'priority': t['priority'],
            'from_type': t['from_type'],
            'created_time': ReserveLiteral("now()"),
            'client_ip': client_ip,
            'url_created_time': t.get('url_created_time', ReserveLiteral("now()")),
            'reason_type': reason_type,
        }
        items.append(item)
    cursor.insert(items, 'url_check_history', True)


_SELECT_HISTORY = """select * from url_check_history where url_md5=%s"""
def get_check_history(url):
    """获取检测历史"""
    split = urlutil.standardize(url)
    if split is None:
        return None
    url_md5 = mkmd5fromstr(split.geturl())
    cursor = connection.cursor()
    try:
        return cursor.fetchall(_SELECT_HISTORY, url_md5)
    finally:
        cursor.close()
    
def get_total_count():
    """获取url检测任务的总数"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone('show table status like "url_check_task"')['Rows']
    finally:
        cursor.close()
        
def get_history_total_count():
    """获取已完成的url检测任务总数"""
    cursor = connection.cursor()
    try:
        return cursor.fetchone('show table status like "url_check_history"')['Rows']
    finally:
        cursor.close()