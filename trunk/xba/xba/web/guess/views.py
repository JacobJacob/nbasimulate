#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.web.render import render_to_response

from xba.common.decorators import login_required
from xba.business import guess_manager

@login_required
def list(request):
    """竞猜列表"""
    page = int(request.REQUEST.get("page", 1))
    pagesize = int(request.REQUEST.get("pagesize", 10))
    has_result = int(request.REQUEST.get("has_result", 1))

    total, infos = guess_manager.get_guess_list(has_result, page, pagesize)
    
    return render_to_response(request, "guess/list.html", locals())


