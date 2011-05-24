#!/usr/bin/python
# -*- coding: utf-8 -*-

import time

from xba.common import exception_mgr

def ensure_success(except_mgr=None):
    def ensure_success_decorator(input_function):
        """确保程序一成功执行装饰器
        @param input_function:被装饰函数 
        @return: 替换函数
        """
        def replace_function(*args, **kwargs):
            sleep_seconds = 0
            while True:
                try:
                    return input_function(*args, **kwargs)
                except KeyboardInterrupt:
                    raise
                except:
                    if except_mgr:
                        except_mgr()
                    else:
                        exception_mgr.on_except()
                if sleep_seconds < 600:
                    sleep_seconds += 10
                time.sleep(sleep_seconds)
        replace_function.func_name = input_function.func_name
        replace_function.__doc__ = input_function.__doc__
        return replace_function
    return ensure_success_decorator