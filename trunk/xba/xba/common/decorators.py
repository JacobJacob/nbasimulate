#!/usr/bin/python
# -*- coding: utf-8 -*-

import time

from django.http import HttpResponseRedirect
#from django.core.urlresolvers import reverse

from xba.common import exception_mgr, log_execption
from xba.business import user_roles

def ensure_success(input_function):
    """确保程序一成功执行装饰器
    @param input_function:被装饰函数 
    @return: 替换函数
    """
    def replace_function(*args, **kwargs):
        while True:
            try:
                return input_function(*args, **kwargs)
            except:
                log_execption()
                exception_mgr.on_except()
                time.sleep(10)
                
    replace_function.func_name = input_function.func_name
    replace_function.__doc__ = input_function.__doc__
    return replace_function


def login_required(input_function):
    """登录检测装饰器，若request带参数password，则会校验密码是否正确
    @param input_function:被装饰函数 
    @return: 替换函数
    """
    def replace_function(*args, **kwargs):
        request = args[0]
        user = user_roles.get_userinfo(request)
        if user:
            request.user = user
            try:
                return input_function(*args, **kwargs)
            except:                
                full_path = request.get_full_path()
                exception_mgr.on_except('request url: %r' % full_path, 1)
                log_execption('request url: %r' % full_path, 1)
                raise
        else:
            response = HttpResponseRedirect("/admin/login")
            return response
    replace_function.func_name = input_function.func_name
    replace_function.__doc__ = input_function.__doc__
    return replace_function

def rpc_login_required(input_function):
    """登录检测装饰器，若request带参数password，则会校验密码是否正确
    @param input_function:被装饰函数 
    @return: 替换函数
    """
    def replace_function(*args, **kwargs):
        request = args[0]
        user = user_roles.get_userinfo(request)
        if user:
            request.user = user
            try:
                return input_function(*args, **kwargs)
            except:                
                full_path = request.get_full_path()
                exception_mgr.on_except('request url: %r' % full_path, 1)
                log_execption('request url: %r' % full_path, 1)
                raise
        else:
            response = HttpResponseRedirect(reverse('admin-login'))
            return response
    replace_function.func_name = input_function.func_name
    replace_function.__doc__ = input_function.__doc__
    return replace_function