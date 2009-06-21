#!/usr/bin/python
# -*- coding: utf-8 -*-
"""rpc api for all url check task"""

from webauth.common.jsonrpcserver import jsonrpc_function
from webauth.business import url_task


@jsonrpc_function
def fetch_task(request):
    client_ip = request.META["REMOTE_ADDR"]
    return url_task.fetch_task(client_ip)

@jsonrpc_function
def fetch_todo_url(request, begin_time, start_id, count):
    return url_task.fetch_todo_url(begin_time, start_id, count)

@jsonrpc_function
def insert_task(request, urls):
    return url_task.insert_task(urls)

@jsonrpc_function
def finish_task(request, tasks):
    client_ip = request.META["REMOTE_ADDR"]
    return url_task.finish_task(tasks, client_ip)

@jsonrpc_function
def update_exprired_task(request):
    return url_task.update_exprired_task()

@jsonrpc_function
def add_emergency_tasks(request, urls, source):
    """添加紧急检测任务"""
    return url_task.add_emergency_tasks(urls, source)