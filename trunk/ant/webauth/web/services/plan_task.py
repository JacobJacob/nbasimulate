#!/usr/bin/python
# -*- coding: utf-8 -*-
"""plan task 查询等相关操作"""

from webauth.common.jsonrpcserver import jsonrpc_function
from webauth.business.plantask.taskmanager import PLANTASK
from webauth.business.plantask.taskmanager import TaskManager
#from webauth.business.user import login_required
#from webauth.business.actionlog import log_action

DEFAULT_RESULT = False
DEFAULT_MESSAGE = "服务器运行错误，请与网站管理员联系。"
DEFAULT_TASK_ID = 0

#@login_required
@jsonrpc_function
def add_task(request, task):
    """添加计划任务
    @param task: 需要增加的任务，dict类型
    @return: 列表[结果，{参数}]
    task = {PLANTASK.MINUTE: request.REQUEST[PLANTASK.MINUTE],
            PLANTASK.HOUR: request.REQUEST[PLANTASK.HOUR],
            PLANTASK.DAY: request.REQUEST[PLANTASK.DAY],
            PLANTASK.MONTH: request.REQUEST[PLANTASK.MONTH],
            PLANTASK.WEEK: request.REQUEST[PLANTASK.WEEK],
            PLANTASK.CMD: request.REQUEST[PLANTASK.CMD],
            PLANTASK.NAME: request.REQUEST[PLANTASK.NAME],
            }
    """
    result, message, task_id = DEFAULT_RESULT, DEFAULT_MESSAGE, DEFAULT_TASK_ID 
#   默认操作失败
    try:
        result, message, task_id = TaskManager.get_instance().add(task)
        action = 'add plan task: %s, success: %s, message: %s' % \
            (task, result, message)
#      log_action(action, request)
    finally:
        return [result, {'id': task_id, 'message': message}]

#@login_required
@jsonrpc_function
def update_task(request, task):
    """更新计划任务
    @param task:计划任务字典，如下所示 
    task = {PLANTASK.MINUTE: request.REQUEST[PLANTASK.MINUTE],
            PLANTASK.HOUR: request.REQUEST[PLANTASK.HOUR],
            PLANTASK.DAY: request.REQUEST[PLANTASK.DAY],
            PLANTASK.MONTH: request.REQUEST[PLANTASK.MONTH],
            PLANTASK.WEEK: request.REQUEST[PLANTASK.WEEK],
            PLANTASK.CMD: request.REQUEST[PLANTASK.CMD],
            PLANTASK.NAME: request.REQUEST[PLANTASK.NAME],
            PLANTASK.ID: taskid
            }
    @return: 列表，格式如[result, {'id': task_id, 'message': msg}]
"""
    result, msg, task_id = DEFAULT_RESULT, DEFAULT_MESSAGE, DEFAULT_TASK_ID #默认操作失败
    try:
        result, msg, task_id = TaskManager.get_instance().update(task)
        action = 'update plan task: %s, success: %s, message: %s' % \
            (task, result, msg)
#    log_action(action, request)
    finally:
        return [result, {'id': task_id, 'message': msg}]

#@login_required
@jsonrpc_function
def delete_task(request, task_id):
    """删除计划任务
    @param task_id:计划任务ID
    @return:  return [result, {'id': task_id, 'message': msg}]
    """
    result, msg, task_id = DEFAULT_RESULT, DEFAULT_MESSAGE, int(task_id) #默认操作失败
    try: 
        result, msg, task = TaskManager.get_instance().delete(task_id)
        action = 'delete plan task: %s, success: %s, message: %s' % \
            (task, result, msg)
#    log_action(action, request)
    finally:
        return [result, {'id': task_id, 'message': msg}]

#@login_required
@jsonrpc_function
def get_task(request, task_id):
    """获取计划任务中task_id的计划任务
    @param task_id:计划任务ID
    @return: 字典，计划任务描述
    """
    task_info = None
    try:
        rs = TaskManager.get_instance().get_tasks()
        for r in rs:
            if r[PLANTASK.ID] == task_id:
                task_info = {PLANTASK.ID: r[PLANTASK.ID],
                        PLANTASK.MINUTE: r[PLANTASK.MINUTE],
                        PLANTASK.HOUR: r[PLANTASK.HOUR],
                        PLANTASK.DAY: r[PLANTASK.DAY],
                        PLANTASK.MONTH: r[PLANTASK.MONTH],
                        PLANTASK.WEEK: r[PLANTASK.WEEK],
                        PLANTASK.CMD: r[PLANTASK.CMD],
                        PLANTASK.NAME: r[PLANTASK.NAME],
                        }
                break;
    finally:
        return task_info;
#@login_required
@jsonrpc_function
def sync_tasks(request):
    """同步计划任务
    @return: 列表，[result, {'message': msg}]
    """
    result, msg = DEFAULT_RESULT, DEFAULT_MESSAGE
    try:
        result, msg = TaskManager.get_instance().sync()
        action = 'sync plan tasks, success: %s, message: %s' % (result, msg)
 #       log_action(action, request)
    finally:
        return [result, {'message': msg}]

#@login_required
@jsonrpc_function
def get_tasks(request):
    """获取当前所有任务
    @return: 字典， {'tasks': [{task_info},{task_info},...], 'current_tasks': ["task","task",...]}
    """
    
    tasks = []
    current_tasks = []
    try:
        rs = TaskManager.get_instance().get_tasks()
        for r in rs:
            tasks.append({PLANTASK.ID: r[PLANTASK.ID],
            PLANTASK.MINUTE: r[PLANTASK.MINUTE],
            PLANTASK.HOUR: r[PLANTASK.HOUR],
            PLANTASK.DAY: r[PLANTASK.DAY],
            PLANTASK.MONTH: r[PLANTASK.MONTH],
            PLANTASK.WEEK: r[PLANTASK.WEEK],
            PLANTASK.CMD: r[PLANTASK.CMD],
            PLANTASK.NAME: r[PLANTASK.NAME],
                     })
        current_tasks = TaskManager.get_instance().get_current_tasks()
    finally:
        return {'tasks': tasks, 'current_tasks': current_tasks}
 
#@login_required
@jsonrpc_function
def delete_current_task(request, task):
    """删除当前执行的计划任务
    @param task: 当前执行的计划任务命令行
    @return: 列表， [result, {'message': msg}]
    """
    result, msg = DEFAULT_RESULT, DEFAULT_MESSAGE
    try:
        result, msg = TaskManager.get_instance().delete_current_task(task)
        action = 'delete current plan task: %s, success: %s, message: %s' % \
            (task, result, msg)
#    log_action(action, request)
        pass
    finally:
        return [result, {'message': msg}]

#@login_required
@jsonrpc_function
def get_defined_task(request):
    """获取预定义的命令
    @return: 字典 {'cmds':{"指令名","指令内容"}}
    """
    
    define_task = []
    try:
        mgr = TaskManager.get_instance()
        define_task = mgr.TASKS
    finally:
        return {'cmds': define_task}