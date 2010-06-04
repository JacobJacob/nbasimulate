#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
用户权限相关操作
"""

from __future__ import with_statement

import time
import random
from uuid import uuid4

from django.http import HttpResponseRedirect, HttpResponseServerError
from django.utils.http import urlquote
from django.core.urlresolvers import reverse

from gba.business.dboperator import DBOperator
from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common import md5mgr
from gba.common import cache
from gba.common import log_execption
from gba.common import exception_mgr
from gba.entity import Team

SESSION_KEY = '_auth_user_id'
BACKEND_SESSION_KEY = '_auth_user_backend'
REDIRECT_FIELD_NAME = 'next'
SESSION_EXPIRE_TIME = 60 * 60 * 24

class UserManager(DBOperator):
    """用户管理: username, session_id"""
    
    #---------------------权限部分sql---------------------------------#
    
    CHECK_USER = """SELECT id, username, nickname,session_id FROM user_info where username = %s"""
    CHECK_USER_PASSWORD = """SELECT id, username, session_id FROM user_info where username = %s AND password = %s"""

    SELECT_USERS = """SELECT id, username FROM user_info"""
    SELECT_USER = """SELECT id, username, session_id, nickname FROM user_info WHERE session_id = %s"""
    SELECT_SESSION = """SELECT id, username, session_id FROM session WHERE session_id = %s"""
    ADD_USER = """INSERT user_info (username, password, login_times, create_time) 
                    VALUES (%s, '', 0, now())"""
    ADD_USER_LOGIN = """INSERT user_info (username, password, session_id, login_times, last_login_time, create_time) 
                    VALUES (%s, '', %s, 1, now(), now())"""
    DELETE_USER = """DELETE FROM user_info WHERE id = %s"""
    UPDATE_LOGIN_INFO = """UPDATE user_info SET session_id = %s, last_login_time = now(), password=%s, 
                              login_times = login_times + 1 WHERE username = %s"""
    
    #---------------------权限部分sql---------------------------------#
    SELECT_PRIVILEGE = """SELECT user_id, page_id, privilege from user_privilege_info
                        WHERE user_id = %s and page_id = %s"""
    SELECT_PRIVILEGES = """SELECT page_id, privilege from user_privilege_info
                        WHERE user_id = %s"""
    UPDATE_PRIVILEGE = """UPDATE user_privilege_info SET privilege = %s WHERE
                        user_id = %s and page_id = %s"""
    ADD_PRIVILEGE = """INSERT user_privilege_info (user_id, page_id, privilege, create_time)
                        VALUES(%s, %s, %s, now())"""
    DELETE_PRIVILEGE_BY_USER = """DELETE FROM user_privilege_info WHERE user_id = %s"""
    DELETE_PRIVILEGE_BY_PAGE = """DELETE FROM user_privilege_info WHERE page_id = %s"""
    
    #---------------------页面部分sql---------------------------------#
    CHECK_PAGE = """SELECT id FROM page_info where page_name = %s"""
    SELECT_PAGE = """SELECT page_name, page_descript from page_info where id = %s"""
    SELECT_PAGES = """SELECT id, page_name, page_descript FROM page_info ORDER BY page_name"""
    ADD_PAGE = """INSERT page_info(page_name,page_descript,create_time) 
                    VALUES (%s,%s,now())"""
    MODIFY_PAGE = """UPDATE page_info SET page_name = %s, page_descript = %s  WHERE id = %s"""
    DELETE_PAGE = """DELETE FROM page_info WHERE id = %s"""
    
    UPDATE_ACTIVE_TIME = """UPDATE session set active_time=now() WHERE username = %s"""
    
    def login(self, username, password):
        """
         根据用户名和密码判断是否是合法用户
        @param username: 登录账号
        @param password: 登录密码
        @return:  0, session_id or -1, fail message
        """
        LOGIN_ERROR = "您输入的用户名或者密码不正确，请重新登录。"
        if not username or not password:
            return -1, LOGIN_ERROR
        
        user_info = None
        password = md5mgr.mkmd5fromstr(md5mgr.mkmd5fromstr(password))
        if not user_info:
            with self.cursor() as cursor:                
                r =  cursor.fetchone(self.CHECK_USER_PASSWORD, (username, password,))
                if not r:
                    return -1, LOGIN_ERROR
        
        
        session_id = md5mgr.mkmd5fromstr('%s%s%s%s' % (uuid4(), username, random.randint(1, 10000), time.time()))
        with self.cursor() as cursor:
            literal_now = ReserveLiteral('now()')
            info = {'username': username, 'session_id': session_id, 'active_time': literal_now, 'created_time': literal_now}
            try:
                cursor.insert(info, 'session', True, ['created_time'])
            except Exception, e:
                return -1, e

            self.set_cache(session_id, username, SESSION_EXPIRE_TIME)
            return 0, session_id
    
    def check_is_authentication(self, user_name, password=''):
        session_id = md5mgr.mkmd5fromstr('%s%s%s%s' % (uuid4(), user_name, random.randint(1, 10000), time.time()))
        with self.cursor() as cursor:
            r =  cursor.fetchone(self.CHECK_USER, (user_name,))
            if not r:
                return -1, "您不是from gba的授权用户，请联系相关人员授权。"
            try:
                cursor.execute(self.UPDATE_LOGIN_INFO,(session_id, password, user_name))
            except Exception, e:
                return -1, e
            data = r.to_dict()
            self.set_cache(session_id, data, SESSION_EXPIRE_TIME)
            return 0, session_id
        
    def logout(self, request):
        """用户登出，从cache中删除相关session_id"""
        session_id = request.COOKIES.get(SESSION_KEY, None)
        if session_id is not None:
            self.delete_cache(session_id)
    
    def get_userinfo(self, request):
        """根据request中的session_id获取cache中的用户名. 若cache挂了，则从数据库中获取
        """
        session_id = request.COOKIES.get(SESSION_KEY, None)
        if session_id is not None:
            username = self.get_cache(session_id)
            if username is None and not cache.get_stats(): # 只有cache挂了才从数据库中读取，防止恶意刷后台页面
                with self.cursor() as cursor:
                    record = cursor.fetchone(self.SELECT_SESSION, (session_id, ))
                if record:
                    r = record.to_dict()
                    username = r['username']
            if username is not None:
                cache.set(session_id, username, SESSION_EXPIRE_TIME)
                with self.cursor() as cursor:
                    cursor.execute(self.UPDATE_ACTIVE_TIME, (username, ))
                return username
        return None
    
    def get_team_info(self, request):
        '''获取球队信息'''
        username = self.get_userinfo(request)
        if username:
            team = Team.load(username=username)
            return team
    
    def add_user(self, username):
        """判断用户是否在ksso中存在，如果存在，就增加到当前用户信息表
        @param username: 用户名
        @return bool, user_id, message
        """
#        if not Check_user_exist(username):
#            return False, -1, "您输入的用户名不存在。"
        
        with self.cursor() as cursor:
            r = cursor.fetchone(self.CHECK_USER, (username,))
            try:
                if r:
                    return True, r["id"], ""
                else:
                    cursor.execute(self.ADD_USER,(username,))
            except Exception, e:
                return False, -1 , e
           
            r = cursor.fetchone(self.CHECK_USER, (username,))
            return True, r["id"], "" 
    
    def delete_user(self, user_id):
        """删除原有用户
        @param user_id:用户ID号码
        @return bool,message 删除成功与否 
        """
        with self.cursor() as cursor:
            try:
                cursor.execute(self.DELETE_PRIVILEGE_BY_USER, (str(user_id),))
                cursor.execute(self.DELETE_USER, (str(user_id),))
                return True, None
            except Exception, e:
                return False, e
    
    def check_user_exist(self, username):
        """检查用户是否为授权用户
        @param username: 用户登录账号
        @return bool 
        """
        with self.cursor() as cursor:                
            r =  cursor.fetchone(self.CHECK_USER, (username,))
            if not r:
                return False
            else:
                return True
                
    def query_users(self):
        """获取当前所有用户登录名
        @return: DataRowCollection[(),()]
        """
        with self.cursor() as cursor:
            return cursor.fetchall(self.SELECT_USERS)
    
    def register_user(self, username, password, nickname):
        
        password = md5mgr.mkmd5fromstr(md5mgr.mkmd5fromstr(password))
        session_id = md5mgr.mkmd5fromstr('%s%s%s' % (uuid4(), username, password))
        
        user_info = {'username': username, 'password': password, 'nickname': nickname}
        
        cursor = connection.cursor()
        try:
            cursor.insert(user_info, 'user_info')
        except:
            log_execption()
            return False, None
        finally:
            cursor.close()
           
        return True, session_id
            
            
    
    def get_page_info(self, page_id=0):
        """根据PAGE_ID，返回页面的描述信息
        @param page_id：页面编号
        @return:{"page_name":"","page_descript":""} or None
        """
        with self.cursor() as cursor:
            record = cursor.fetchone(self.SELECT_PAGE, 
                                     (str(page_id),))
            if not record:
                return None   
                   
            return record.to_dict()
    
    def query_pages(self):
        """获取当前所有页面
        @return: DataRowCollection[(),()]
        """
        with self.cursor() as cursor:
            return cursor.fetchall(self.SELECT_PAGES) 
        
    def add_page(self,page_name,page_descript=None):
        """增加新页面
        @param page_name:页面名称
        @param page_descript:页面功能描述
        @return:
        """
        if page_name == None or not isinstance(page_name,basestring):
            return False, "页面名称格式非法。"
        
        if not page_descript:
            page_descript = ""
        
        with self.cursor() as cursor:
            record = cursor.fetchone(self.CHECK_PAGE, 
                                     (page_name,))
            if record:
                return False, "页面名称已经存在。"
            
            try:
                cursor.execute(self.ADD_PAGE, (page_name, page_descript, ))
                return True, None
            except Exception, e:
                return False, e
            
    def modify_page(self, page_id, page_name, page_descript):
        """修改页面信息
        @param page_id:页面ID号码，int
        @param page_name: 页面名称
        @param page_descript: 页面描述
        @return: bool,message 修改成功与否 
        """   
        pages = self.query_pages().to_list();
        for page in pages:
            if page["page_name"] == page_name and page["id"] != page_id:
                return False, "页面名称已经存在。"
        
        with self.cursor() as cursor:
            try:
                cursor.execute(self.MODIFY_PAGE, (page_name, page_descript, str(page_id)))
                return True, None
            except Exception, e:
                return False, e   
         
    def delete_page(self, page_id):
        """删除原有页面
        @param user_id:用户ID号码
        @return: bool,message 删除成功与否 
        """
        with self.cursor() as cursor:
            try:
                cursor.execute(self.DELETE_PRIVILEGE_BY_PAGE, (str(page_id),))
                cursor.execute(self.DELETE_PAGE, (str(page_id),))
                return True, None
            except Exception, e:
                return False, e

    def get_privilege(self, request, page_id=0):
        """根据当前登录用户信息和功能编号，获取对该功能的权限
        @param request: 用户请求
        @param page_id: 页面编号
        @return: {"user_id":int, "page_id":int, "privilege":int}
        """
        privilege = None
        
        user_info = self.get_userinfo(request)
        if user_info is None:
            return privilege
        
        with self.cursor() as cursor:
            record = cursor.fetchone(self.SELECT_PRIVILEGE, 
                                     (str(user_info["id"]), str(page_id),))
            if not record:
                return privilege
            
            return record.to_dict()
        
    def get_privileges(self, request, user_id=None):
        """根据用户ID，获取该用户对所有页面的的权限
        @param request: 用户请求
        @param user_id: 用户ID号码
        @return: [(page_id,privilege),()]
        """
        with self.cursor() as cursor:
            privilege = cursor.fetchall(self.SELECT_PRIVILEGES, 
                                     (str(user_id),))
        return privilege
    
    def modify_privileges(self, user_id, privilege_info={}):
        """修改用户权限
        @param user_id:用户编号 
        @param privilege_info: 权限信息{"page_id":int,"privilege",int}
        @return bool,message 更新成功与否 
        """
        with self.cursor() as cursor:
            try:
                cursor.execute(self.DELETE_PRIVILEGE_BY_USER, (str(user_id),))
                #先删除该用户原有权限
                for k,v in privilege_info:
                    cursor.execute(self.ADD_PRIVILEGE, (str(user_id),str(k),str(v)))
                    
                return True, None
            except Exception, e:
                return False, e

    def modify_privilege(self, user_id, page_id, privilege):
        """修改用户权限
        @param user_id:用户编号 
        @param page_id:页面编号
        @param privilege:权限 
        @return bool,message 更新成功与否 
        """
        with self.cursor() as cursor:
            record = cursor.fetchone(self.SELECT_PRIVILEGE, 
                                     (str(user_id),str(page_id)))
            try:
                if record:
                    cursor.execute(self.UPDATE_PRIVILEGE, \
                                   (str(privilege), str(user_id), str(page_id)))
                else:
                    cursor.execute(self.ADD_PRIVILEGE, \
                                   (str(user_id), str(page_id), str(privilege)))
                return True, None
            except Exception, e:
                return False, e
            
def privilege_required(page_id=0, operator_id=0):
    """装饰函数，检查当前用户是否对指定功能有查询权限
    @param page_id:页面编号 
    @param operator_id:操作编号，从低到高，按位-> 1查新，2更新 
    """
    def decorator_funtion(input_function): #定义一个新函数，接收被装饰的函数
        def replace_function(*args, **kwargs): #被替换执行的函数，主要工作就在这里完成
            request = args[0]#这里要求参数传进来，第一个必须是request对象
            user_manager = UserManager()
            
            user_privilege = 0 #默认没有权限
            
            privilege = user_manager.get_privilege(request, page_id)
            if privilege:
                user_privilege = privilege["privilege"]
            
            if operator_id & 1 == 1 and (user_privilege & 1) == 0: ##没有查询权限
                return HttpResponseRedirect('%s%s/%s/' % \
                                            (reverse('privilege_error'), str(page_id), "1"))
            if operator_id & 2 == 2 and (user_privilege & 2) == 0:
                return HttpResponseRedirect('%s%s/%s/' % \
                                            (reverse('privilege_error'), str(page_id), "2"))
                
            try:
                return input_function(*args, **kwargs)
            except:
                full_path = request.get_full_path()
                exception_mgr.on_except('request url: %r' % full_path, 1)
                log_execption('request url: %r' % full_path, 1)
                return HttpResponseServerError("Server Error (500)")
            
        replace_function.func_name = input_function.func_name
        replace_function.__doc__ = input_function.__doc__
        return replace_function
    return decorator_funtion

def login_required(input_function):
    """登录检测装饰器，若request带参数password，则会校验密码是否正确
    @param input_function:被装饰函数 
    @return: 替换函数
    """
    def replace_function(*args, **kwargs):
        request = args[0]

        user_manager = UserManager()
        userinfo = user_manager.get_userinfo(request)
        if userinfo:
            request.userinfo = userinfo
            try:
                return input_function(*args, **kwargs)
            except:                
                full_path = request.get_full_path()
                exception_mgr.on_except('request url: %r' % full_path, 1)
                log_execption('request url: %r' % full_path, 1)
                raise
        else:
            response = HttpResponseRedirect(reverse('timeout'))
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

        user_manager = UserManager()
        userinfo = user_manager.get_userinfo(request)
        if userinfo:
            request.userinfo = userinfo
            try:
                return input_function(*args, **kwargs)
            except:                
                full_path = request.get_full_path()
                exception_mgr.on_except('request url: %r' % full_path, 1)
                log_execption('request url: %r' % full_path, 1)
                raise
        else:
            response = HttpResponseRedirect(reverse('timeout'))
            return response
    replace_function.func_name = input_function.func_name
    replace_function.__doc__ = input_function.__doc__
    return replace_function

if __name__ == '__main__':
    pass
