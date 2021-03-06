#!/usr/bin/python
# -*- coding: utf-8 -*-

import time

from gba.common import exception_mgr

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
                exception_mgr.on_except()
                time.sleep(10)
                
    replace_function.func_name = input_function.func_name
    replace_function.__doc__ = input_function.__doc__
    return replace_function

@ensure_success
def test_method():
    raise 'test error'
    print 'hello world'
    
if __name__ == '__main__':
    test_method()