#!/usr/bin/python
# -*- coding: utf-8 -*-

"""计划任务管理"""

from django.shortcuts import render_to_response

#from webauth.business.plantask.taskmanager import TaskManager
from webauth.business.user import login_required

#@login_required
def task_list(request):
#    mgr = TaskManager.get_instance()
#    data = {'cmds': mgr.TASKS}
    return render_to_response('plan_tasks.html')