#!/usr/bin/python
# -*- coding: utf-8 -*-

from __future__ import with_statement

import os
import tempfile
import subprocess

from webauth.common.filelocker import FileLocker
from webauth.common.singleton import Singleton
from webauth.common import json
from webauth.common import log_execption
from webauth.config import PathSettings
from webauth.common import cache

class PLANTASK(object):
    """计划任务相关"""
    ID = 'id'
    MINUTE = 'minute'
    HOUR = 'hour'
    DAY = 'day'
    MONTH = 'month'
    WEEK = 'week'
    CMD = 'cmd'
    NAME = 'name'

class TaskManager(Singleton):
    """计划任务管理"""
    
    __locker = FileLocker('_plantasks_TaskManager')
    
    def _format(self, cmd):
        if '$project$' in cmd:
            cmd = cmd.replace('$project$', PathSettings.PROJECT_FOLDER)
        if '$working_folder$' in cmd:
            cmd = cmd.replace('$working_folder$', PathSettings.WORKING_FOLDER)
        return cmd
        
    @property
    def TASKS(self):
        """当前有效的可运行于计划任务的程序，在plantask.config下定义"""
        cache_key = 'plantasks_TaskManager_TASKS'
        tasks = cache.get(cache_key)
        if tasks is not None:
            return tasks
        tasks = {}
        filepath = os.path.join(os.path.dirname(os.path.abspath(__file__)), 
                               'plantask.config')
        with open(filepath, 'rb') as f:
            for line in f:
                line = line.strip()
                if not line or '\t|\t' not in line:
                    continue
                name, cmd = line.split('\t|\t')
                tasks[name] = self._format(cmd)
            cache.set(cache_key, tasks, 60)
            return tasks
    
    def add(self, data):
        """添加计划任务
        @return 结果，描述，taskid
        """
        with self.__locker:
            name, cmd = data[PLANTASK.NAME], data[PLANTASK.CMD]
            if name not in self.TASKS or cmd != self.TASKS[name]:
                return False, u'%s[%s] not allow!' % (name, cmd), 0
            tasks = self.get_tasks()
            if not tasks:
                taskid = 1
            else:
                taskid = tasks[-1][PLANTASK.ID] + 1
            data[PLANTASK.ID] = taskid
            tasks.append(data)
            r, msg = self._refresh(tasks)
            return r, msg, taskid
    
    def update(self, data):
        """更新计划任务"""
        with self.__locker:
            tasks = self.get_tasks()
            delete_task = None
            for task in tasks:
                if task[PLANTASK.ID] == data[PLANTASK.ID]:
                    tasks.insert(tasks.index(task), data)
                    tasks.remove(task)
                    delete_task = task
            r, msg = self._refresh(tasks, delete_task)
            return r, msg, data[PLANTASK.ID]
    
    def delete(self, taskid):
        """删除计划任务"""
        with self.__locker:
            tasks = self.get_tasks()
            for task in tasks:
                if task[PLANTASK.ID] == taskid:
                    tasks.remove(task)
            r, msg = self._refresh(tasks, task)
            return r, msg, task
    
    def get_tasks(self):
        """获取所有计划任务记录"""
        if not os.path.exists(PathSettings.PLANTASKS_FILE):
            return []
        with open(PathSettings.PLANTASKS_FILE, 'rb') as fp:
            try:
                tasks = json.load(fp)
            except:
                log_execption()
                tasks = []
            return tasks
    
    def sync(self):
        """同步计划任务"""
        with self.__locker:
            tasks = self.get_tasks()
            r, msg = self._refresh(tasks)
            return r, msg
    
    def get_current_tasks(self):
        """获取当前系统的计划任务"""
        cmd = 'crontab -l'
        p = subprocess.Popen(cmd, shell=True, close_fds=True,
                             stdout=subprocess.PIPE)
        outdata = p.communicate()[0]
        tasks = [t for t in outdata.strip().split('\n') if t]
        return tasks
    
    def delete_current_task(self, delete_task_cmd):
        """删除指定的计划任务"""
        with self.__locker:
            tasks = self.get_tasks()
            for r in tasks:
                new_task = '%s %s %s %s %s %s' % \
                (r[PLANTASK.MINUTE], r[PLANTASK.HOUR], r[PLANTASK.DAY],
                 r[PLANTASK.MONTH], r[PLANTASK.WEEK], r[PLANTASK.CMD],)
                if new_task.strip().lower() == delete_task_cmd:
                    return False, "当前执行任务在计划任务中，不能直接删除！"
            return self._refresh(tasks, delete_task_cmd)
    
    def _refresh(self, tasks, delete_task=None):
        """刷新计划任务
            把参数tasks去除掉当前计划任务中重复命令，刷新到当前计划任务，并保存到PLANTASKS_FILE文件中
        @param tasks: 计划任务集合
        @param delete_task:需要删除的计划任务
        @return:    
        """
        
        #生成一个不包含预定义命令行和delete_task命令行的剩余命令行列表old_left_task
        old_tasks = self.get_current_tasks()
        old_left_tasks = []
        if delete_task is not None:
            delete_task_cmd = isinstance(delete_task, basestring) and delete_task or delete_task[PLANTASK.CMD]
            #是计划任务命令行或者一个task描述字典
            delete_task_cmd = delete_task_cmd.strip().lower()
        else:
            delete_task_cmd = ''
            
        for old_t in old_tasks:
            need_left = True
            if delete_task_cmd in old_t.strip().lower():
                need_left = False
            else:
                for v in self.TASKS.values():
                    if v.strip().lower() in old_t.strip().lower(): 
                        #如果预定义列表中命令行包含在当前计划任务命令行中，先删除掉
                        need_left = False
                        break
                    
            if need_left:
                old_left_tasks.append(old_t)
        #根据tasks和delete_task，生成需要执行的计划任务命令行列表
        task_list = []
        for r in tasks:
            new_task = '%s %s %s %s %s %s' % \
                (r[PLANTASK.MINUTE], r[PLANTASK.HOUR], r[PLANTASK.DAY],
                 r[PLANTASK.MONTH], r[PLANTASK.WEEK], r[PLANTASK.CMD],)
            if new_task.strip().lower() != delete_task_cmd:
                task_list.append(new_task)
        
        #如果old_left_task中命令行不包含在task_list中，添加到task_list中
        for old_t in old_left_tasks:
            need_left = True
            for t in tasks:
                if t[PLANTASK.CMD].strip().lower() in old_t.strip().lower():
                    need_left = False
                    break
            if need_left:
                task_list.append(old_t)
        
        filename = tempfile.mkstemp()[1]
        with open(filename, 'wb') as f:
            f.write('\n'.join(task_list))
            f.write('\n')
        cmd = 'crontab %s' % filename
        try:
            p = subprocess.Popen(cmd, shell=True, close_fds=True,
                                 stdout=subprocess.PIPE,
                                 stderr=subprocess.PIPE)
            outdata, errdata = p.communicate()
            if p.returncode == 0:
                with open(PathSettings.PLANTASKS_FILE, 'wb') as fp:
                    json.dump(tasks, fp)
                return True, outdata
            else:
                return False, errdata
        finally:
            if os.path.exists(filename):
                try:
                    os.remove(filename)
                except:
                    pass