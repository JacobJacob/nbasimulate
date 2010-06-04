#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.http import HttpResponseRedirect

from gba.web.render import render_to_response
from gba.business.user_roles import UserManager
from gba.config import DEBUG

NOT_CHECK_PATHS = ['/accounts/login/', '/accounts/register/', '/accounts/captcha/', '/accounts/check_email/']


class TeamInfoMiddleware(object):

    def __init__(self):
        pass

    def process_request(self, request):
        if DEBUG:
            if request.path.startswith('/site_media'):
                return
        user_info = UserManager().get_userinfo(request)
        if not user_info:
            if request.path not in NOT_CHECK_PATHS:
                return render_to_response(request, 'accounts/timeout.html', {'error': '尚未登录'})
            else:
                return
        team = UserManager().get_team_info(request)
        if not team:
            if request.path in ['/team/register/', '/accounts/ucenter/', '/accounts/login/']:
                return
            else:
                return HttpResponseRedirect('/team/register/')
        setattr(request, 'team', team)