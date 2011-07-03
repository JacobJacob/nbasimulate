#!/usr/bin/python
# -*- coding: utf-8 -*-

from django.http import HttpResponse

from xba.common.constants.account import InviteCodeStatus
from xba.web.render import render_to_response
from xba.business import account_manager

def invitecode(request):
    """邀请码"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    status = int(request.REQUEST.get("status", 1))
    total, infos = account_manager.get_invite_code_list(page, pagesize, status)
    return render_to_response(request, "account/invitecode.html", locals())

def download_invite_code(request):
    count = int(request.REQUEST.get("count", 20))
    codes = account_manager.add_invite_code(count, status=InviteCodeStatus.ASSIGNED)
    data = "\n".join(codes)
    response = HttpResponse(data, mimetype='text/plain')   
    response['Content-Disposition'] = 'attachment; filename=invite.txt'
    return response 