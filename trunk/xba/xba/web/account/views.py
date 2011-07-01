#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.web.render import render_to_response
from xba.business import account_manager

def invitecode(request):
    """邀请码"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 20))
    status = int(request.REQUEST.get("status", 1))
    total, infos = account_manager.get_invite_code_list(page, pagesize, status)
    return render_to_response(request, "account/invitecode.html", locals())