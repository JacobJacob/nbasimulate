#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.business.user_roles import UserManager
from gba.web.render import render_to_response

class TeamInfoMiddleware(object):

    def __init__(self):
        pass

    def process_request(self, request):
        team = UserManager().get_team_info(request)
        if not team:
            return render_to_response(request, "message.html", {'error': u'登陆信息丢失,请重新登陆'}) 
        setattr(request, 'team', team)