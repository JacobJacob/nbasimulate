#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.business.user_roles import UserManager

REDIRECT_FIELD_NAME = 'next'

class TeamInfoMiddleware(object):

    def __init__(self):
        pass

    def process_request(self, request):
        team = UserManager().get_team_info(request)
        #if not team:
        #    path = urlquote(request.get_full_path())
        #    return HttpResponseRedirect('%s?%s=%s' % (reverse('login-page'), REDIRECT_FIELD_NAME, path)) 
        setattr(request, 'team', team)