#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
用户权限相关操作
"""

import time
import random
from uuid import uuid4



from xba.common import md5mgr
from xba.common import cache
from xba.model import User

SESSION_KEY = '_auth_user_id'
BACKEND_SESSION_KEY = '_auth_user_backend'
REDIRECT_FIELD_NAME = 'next'
SESSION_EXPIRE_TIME = 60 * 60 * 24

def login(username, password):
    """"""
    LOGIN_ERROR = "您输入的用户名或者密码不正确，请重新登录。"
    if not username or not password:
        return -1, LOGIN_ERROR
    
    password = md5mgr.mkmd5fromstr(md5mgr.mkmd5fromstr(password))

    user = User.load(username=username)
    if not user or user.password!= password:
        return -1, LOGIN_ERROR
    
    session_id = md5mgr.mkmd5fromstr('%s%s%s%s' % (uuid4(), username, random.randint(1, 10000), time.time()))
    user.session_id = session_id
    user.persist()

    return 0, session_id
    
def logout(self, request):
    """用户登出，从cache中删除相关session_id"""
    session_id = request.COOKIES.get(SESSION_KEY, None)
    if session_id is not None:
        self.delete_cache(session_id)
    
def get_userinfo(request):
    """根据request中的session_id获取cache中的用户名. 若cache挂了，则从数据库中获取
    """
    session_id = request.COOKIES.get(SESSION_KEY, None)
    if session_id is not None:
        username = None
        if username is None and not cache.get_stats(): # 只有cache挂了才从数据库中读取，防止恶意刷后台页面
            user = User.load(session_id=session_id)
            if user:
                username = user.username
        if username is not None:
            return username
    return None

if __name__ == '__main__':
    pass
