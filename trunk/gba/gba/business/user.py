#!/usr/bin/python
# -*- coding: utf-8 -*-
"""用户相关
"""

from __future__ import with_statement
from uuid import uuid4

from django.http import HttpResponseRedirect, HttpRequest, HttpResponseForbidden, HttpResponseServerError
from django.utils.http import urlquote
from django.core.urlresolvers import reverse

from webauth.business.dboperator import DBOperator
from webauth.common import md5mgr
from webauth.common import cache
from webauth.common.constants import User
from webauth.common import log_execption

SESSION_KEY = '_auth_user_id'
BACKEND_SESSION_KEY = '_auth_user_backend'
REDIRECT_FIELD_NAME = 'next'
SESSION_EXPIRE_TIME = 600

class UserManager(DBOperator):
    """用户管理: username, password, permission, session_id"""
    
    INSERT_USER = """INSERT INTO users(username, password, permission)
        VALUES(%s, %s, %s)"""
    USER_LOGIN = """SELECT username, password, permission, session_id
                        FROM users WHERE username=%s AND password=%s"""
    UPDATE_SESSION_ID = """UPDATE users SET session_id=%s 
                            WHERE username=%s"""
    SELECT_USER = """SELECT username, password, permission, session_id FROM users
        WHERE session_id=%s""" 
    SELECT_ALLOW_IPS = """SELECT ip FROM allow_ips"""
    INSERT_ALLOW_IP = """INSERT INTO allow_ips(ip) VALUES(%s)"""
    DELETE_ALLOW_IP = """DELETE FROM allow_ips WHERE ip=%s"""
    
    IP_CACHE_KEY = 'UserManager.allow_ips'
    
    @property
    def allow_ips(self):
        IPs = self.get_cache(self.IP_CACHE_KEY)
        if IPs is None:
            with self.cursor() as cursor:
                rs = cursor.fetchall(self.SELECT_ALLOW_IPS)
                IPs = [r[0] for r in rs]
                self.set_cache(self.IP_CACHE_KEY, IPs, 60)
        return IPs
    
    def add_allow_ip(self, ip):
        with self.cursor() as cursor:
            try:
                cursor.execute(self.INSERT_ALLOW_IP, (ip,))
                self.delete_cache(self.IP_CACHE_KEY)
                return True, None
            except Exception, e:
                return False, e
    
    def delete_allow_ip(self, ip):
        with self.cursor() as cursor:
            try:
                cursor.execute(self.DELETE_ALLOW_IP, (ip,))
                self.delete_cache(self.IP_CACHE_KEY)
                return True, None
            except Exception, e:
                return False, e
        
    def check_ip(self, ip):
        """校验ip是否在允许ip列表里面"""
        if ip.startswith("192.168.") \
            or ip.startswith("127.0") \
            or ip.startswith("10."):
            return True
        return ip in self.allow_ips
    
    def _login(self, username, password):
        """
        return
            0, session_id
            -1, fail message
        """
        password = md5mgr.mksha1fromstr(password)
        with self.cursor() as cursor:
            r =  cursor.fetchone(self.USER_LOGIN, (username, password))
            if r:
                session_id = md5mgr.mkmd5fromstr('%s%s%s' % \
                                                 (uuid4(), username, password))
                cursor.execute(self.UPDATE_SESSION_ID, 
                               (session_id, username))
                data = r.to_dict()
                self.set_cache(session_id, data, SESSION_EXPIRE_TIME)
                return 0, session_id
            else:
                return -1, 'Username or password is wrong.'
    
    def login(self, username, password, ip):
        """User login by name and password, ip
        
        return
            0, session_id
            -1, fail message
            -2, forbidden IP
        """
        if not self.check_ip(ip):
            return -2, 'forbidden IP: %s' % ip
        return self._login(username, password)
    
    def logout(self, request):
        """用户登出，从cache中删除相关session_id"""
        session_id = request.COOKIES.get(SESSION_KEY, None)
        if session_id is not None:
            self.delete_cache(session_id)
    
    def get_userinfo(self, request):
        """根据request中的session_id获取cache中的用户名
        若cache挂了，则从数据库中获取
        """
        session_id = request.COOKIES.get(SESSION_KEY, None)
        if session_id is not None:
            r = self.get_cache(session_id)
            if r is None and not cache.get_stats(): # 只有cache挂了才从数据库中读取，防止恶意刷后台页面
                # get from database
                with self.cursor() as cursor:
                    record = cursor.fetchone(self.SELECT_USER, (session_id,))
                if record:
                    r = record.to_dict()
            if r is not None:
                cache.set(session_id, r, SESSION_EXPIRE_TIME)
                return r
        return None

def login_required(f):
    """登录检测装饰器
    
    若request带参数password，则会校验密码是否正确
    """
    def new_f(*args, **kwargs):
        request = args[0]
        ip = request.META['REMOTE_ADDR']
        mgr = UserManager.get_instance()
        if not mgr.check_ip(ip):
            return HttpResponseForbidden('forbidden IP: %s' % ip)
        userinfo = mgr.get_userinfo(request)
        is_login = True
        if userinfo is None:
            is_login = False
        else:
            password = request.REQUEST.get(User.PASSWORD, None) # 有password的话，会重新校验密码
            if password is not None and \
                    md5mgr.mksha1fromstr(password) != userinfo[User.PASSWORD]:
                is_login = False
        if is_login:
            request.userinfo = userinfo
            try:
                return f(*args, **kwargs)
            except:
                log_execption()
                return HttpResponseServerError("Server Error (500)")
        else:
            path = urlquote(request.get_full_path())
            response = HttpResponseRedirect('%s?%s=%s' % \
                                            (reverse('login'), REDIRECT_FIELD_NAME, path))
#            response.delete_cookie(SESSION_KEY) # 清楚cookies中的session_id
            return response
    new_f.func_name = f.func_name
    new_f.__doc__ = f.__doc__
    return new_f

def check_ip(f):
    """IP检测装饰器"""
    def new_f(*args, **kwargs):
        request = args[0]
        ip = request.META['REMOTE_ADDR']
        mgr = UserManager.get_instance()
        if not mgr.check_ip(ip):
            return HttpResponseForbidden('forbidden IP: %s' % ip)
        try:
            return f(*args, **kwargs)
        except:
            log_execption()
            return HttpResponseServerError("Server Error (500)")
    new_f.func_name = f.func_name
    new_f.__doc__ = f.__doc__
    return new_f
