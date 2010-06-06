#!/usr/bin/python
# -*- coding: utf-8 -*-


import time, random
import datetime

from django.http import HttpResponse, HttpResponseRedirect
from django.core.urlresolvers import reverse

from gba.config import DOMAIN
from gba.web.render import render_to_response, json_response
from gba.common import md5mgr, mailutil, json
from gba.common.captcha import get_captcha, is_captcha
from gba.entity import UserInfo, EmailSendLog
from gba.business.user_roles import UserManager

SESSION_KEY = '_auth_user_id'

def login(request):
    "用户登陆"
    if request.method == 'GET':
        return render_to_response(request, 'accounts/login.html')
    else:
        username = request.POST.get('email')
        password = request.POST.get('passwd')
        result, session_id = UserManager().login(username, password)
        if result == -1:
            return render_to_response(request, 'accounts/login.html', {'error': u'用户名或密码错误'})
        response = HttpResponseRedirect(reverse('ucenter'))
        response.set_cookie(SESSION_KEY, session_id)
        return response
        
def logout(request):
    "This view is a basic 'hello world' example in HTML."
    return HttpResponse('<h1>Hello, world.</h1>')

def register(request):
    "This view is a basic 'hello world' example in HTML."
    if request.method == 'GET':
        user_name = UserManager().get_userinfo(request)
        if user_name:
            return HttpResponseRedirect('/accounts/ucenter/')
        return render_to_response(request, 'accounts/register.html')
    else:
        captcha = request.POST.get('captcha')
        email = request.POST.get('email')
        password = request.POST.get('password')
        nickname = request.POST.get('nickname')
        yyyy = request.POST.get('yyyy')
        mm = request.POST.get('mm')
        dd = request.POST.get('dd')
        city = request.POST.get('city')
        ddlgender = request.POST.get('ddlgender')
        
        captcha_key = request.COOKIES.get("CAPTCHA")
        b_captcha, m_captcha_key, fpath = is_captcha(captcha, captcha_key)

        success = True
        message = ""
        if b_captcha:
            user_info = UserInfo()
            user_info.username = email
            user_info.nickname = nickname
            user_info.password = md5mgr.mkmd5fromstr(md5mgr.mkmd5fromstr(password))
            user_info.active = 0
            user_info.city = city
            user_info.sex = ddlgender
            try:
                user_info.birthday  = datetime.datetime(int(yyyy), int(mm), int(dd))
            except:
                raise
            user_info.persist()
            
            code = '%s%s%s' % (md5mgr.mkmd5fromstr(email), md5mgr.mkmd5fromstr('%s' % random.randint(0, 100)), md5mgr.mkmd5fromstr('%s' % time.time())) 
            
            active_link = "http://%s/accounts/active/?c=%s" % (DOMAIN, code)
            msg = u'请点击下面的链接激活:<br><a href="%s">%s</a>' % (active_link, active_link)
            success = 1
            try:
                mailutil.send_to(u'GBA篮球经理用激活邮件', msg, email)
            except:
                success = 0
            email_send_log = EmailSendLog()
            email_send_log.username = email
            email_send_log.active_code = code
            email_send_log.success = 1 if success else 0
            email_send_log.persist()
        else:
            success = False
            message = "验证码错误"
        
        return render_to_response(request, 'accounts/register_result.html', {'success': success, 'message': message})
        
def captcha(request):
    '''获取生成的验证码'''
    r_dic = {'success' : True, 'img' : ''}
    ckey, img = get_captcha()
    
    r_dic['img'] = img
    response = HttpResponse(json.dumps(r_dic))
    response.set_cookie("CAPTCHA", ckey, max_age=300, path="/")
    return response  

def check_email(request):
    email = request.POST.get('email')
    user_info = UserInfo.load(username=email)
    if user_info:
        return json_response(False)
    return json_response(True)

def timeout(request):
    return render_to_response(request, 'accounts/timeout.html')

def ucenter(request):
    return render_to_response(request, 'accounts/ucenter.html')

def account_active(request):
    code = request.GET.get('c')
    success = u'帐户激活成功'
    error = None
    i = 0
    while i < 1:
        i += 1
        if not code:
            error = u'激活失败,请联系客服'
            break
            
        email_send_log = EmailSendLog.load(active_code=code)
        if not email_send_log:
            error = u'激活失败,请联系客服'
            break
        user_info = UserInfo.load(username=email_send_log.username)
        if user_info.active == 1:
            error = u'该帐号已经激活,无须重新激活'
            break
            
        user_info.active = 1
        user_info.persist()
            
    return render_to_response(request, 'accounts/active_result.html', {'success': success, 'error': error})
