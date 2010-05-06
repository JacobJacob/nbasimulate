#!/usr/bin/python
# -*- coding: utf-8 -*-

import random, cStringIO

#from PIL import Image, ImageDraw, ImageFont
from django.http import HttpResponse, HttpResponseRedirect
from django.core.urlresolvers import reverse

from gba.web.render import render_to_response
from gba.common import md5mgr
from gba.common.db import connection
#from gba.business import accounts
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
        return render_to_response(request, 'account/register.xhtml')
    
    else:
        captcha = request.POST.get('captcha')
        key_hash = request.COOKIES.get('CAPTCHA')
        
        success = False
        if captcha and key_hash:
            cursor = connection.cursor()
            try:
                rs = cursor.fetchone('select 1 from captcha where captcha_md5=%s and captcha=%s', (key_hash, captcha))
                if rs:
                    success = True
            finally:
                cursor.close()
        
        if success:
            return HttpResponse('<h1>register success.</h1>')
        else:
            return HttpResponse('<h1>register failure.</h1>')
    
def captcha(request):
    pass
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
#    #accounts.save_captcha(key, key_hash)
#    response.set_cookie('CAPTCHA', key_hash)
#    return response

def timeout(request):
    return render_to_response(request, 'accounts/timeout.html')

def ucenter(request):
    return render_to_response(request, 'accounts/ucenter.html')