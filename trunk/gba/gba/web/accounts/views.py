#!/usr/bin/python
# -*- coding: utf-8 -*-

import random, cStringIO
import time, random

from PIL import Image, ImageDraw, ImageFont
from django.http import HttpResponse, HttpResponseRedirect
from django.core.urlresolvers import reverse

from gba.config import DOMAIN
from gba.web.render import render_to_response, json_response
from gba.common import md5mgr, mailutil, json
from gba.common.db import connection
from gba.common.captcha import get_captcha
from gba.entity import Captcha, UserInfo
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
        return render_to_response(request, 'accounts/register.html')
    
    else:
        captcha = request.POST.get('captcha')
        key_hash = request.COOKIES.get('CAPTCHA')
        email = request.POST.get('email')
        password = request.POST.get('password')
        nickname = request.POST.get('nickname')
        yyyy = request.POST.get('yyyy')
        mm = request.POST.get('mm')
        dd = request.POST.get('dd')
        city = request.POST.get('city')
        ddlgender = request.POST.get('ddlgender')
        
        success = False
        if captcha and key_hash:
            cursor = connection.cursor()
            try:
                rs = cursor.fetchone('select 1 from captcha where captcha_hash=%s and captcha=%s', (key_hash, captcha))
                if rs:
                    success = True
            finally:
                cursor.close()
                
        if success:
            user_info = UserInfo()
            user_info.username = email
            user_info.nickname = nickname
            user_info.password = md5mgr.mkmd5fromstr(md5mgr.mkmd5fromstr(password))
            user_info.active = 0
            user_info.persist()
            
            code = '%s%s%s' % (md5mgr.mkmd5fromstr(email), md5mgr.mkmd5fromstr('%s' % random.randint(0, 100)), md5mgr.mkmd5fromstr('%s' % time.time())) 
            
            active_link = "http://%s/accounts/active/?c=%s" % (DOMAIN, code)
            msg = u'请点击下面的链接激活:<br><a href="%s">%s</a>' % (active_link, active_link)
            try:
                mailutil.send_to(u'GBA篮球经理用激活邮件', msg, email)
            except:
                pass
        if success:
            return HttpResponse('<h1>register success.</h1>')
        else:
            return HttpResponse('<h1>register failure.</h1>')
    
#def captcha(request):
#    im = Image.new('RGBA', (90, 26), (50,50,50,50))
#    draw = ImageDraw.Draw(im)
#    rands = []
#    for i in range(4):
#        rands.append('%s' % random.randint(0, 9))
#    draw.text((2,0), rands[0], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18,24)), fill='red')
#    draw.text((24,0), rands[1], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18,24)), fill='yellow')
#    draw.text((43,0), rands[2], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18,24)), fill='blue')
#    draw.text((64,0), rands[3], font=ImageFont.truetype("tahomabd.TTF", random.randrange(18,24)), fill='white') 
#    del draw
#    buf = cStringIO.StringIO()
#    im.save(buf, 'gif')
#    response = HttpResponse(buf.getvalue(),'image/gif')
#    key = ''.join(rands)
#    key_hash = md5mgr.mkmd5fromstr(key)
#    captcha = Captcha()
#    captcha.captcha = key
#    captcha.captcha_hash = key_hash
#    captcha.persist()
#    response.set_cookie('CAPTCHA', key_hash)
#    return response

def captcha(request):
    '''获取生成的验证码'''
    r_dic = {'success' : True, 'img' : ''}
    ckey, img = get_captcha()
    
    r_dic['img'] = img
    response = HttpResponse(json.dumps(r_dic))
    response.set_cookie("CAPTCHA", ckey, max_age=60, path="/")
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