#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.web.render import render_to_response
from gba.business.user_roles import UserManager

class TeamInfoMiddleware(object):

    def __init__(self):
        pass

    def process_request(self, request):
        team = UserManager().get_team_info(request)
        if not team:
            render_to_response(request, 'not_login_error.html', {'error': '尚未登录'})
        print '=' * 100
        setattr(request, 'team', team)