#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.web.render import render_to_response
from gba.business.user_roles import UserManager

class TeamInfoMiddleware(object):

    def __init__(self):
        pass

    def process_request(self, request):
        user_info = UserManager().get_userinfo(request)
        if not user_info:
            render_to_response(request, 'accounts/timeout.html', {'error': '尚未登录'})
        team = UserManager().get_team_info(request)
        if not team:
            render_to_response(request, 'accounts/timeout.html', {'error': ''})
        setattr(request, 'team', team)