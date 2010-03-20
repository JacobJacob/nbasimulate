#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from gba.web.render import render_to_response
from gba.common import playerutil
from gba.business import player_operator
from gba.business.user_roles import login_required


def index(request):
    """list"""
    datas = {}
    return render_to_response(request, 'admin/index.html', datas)

def action_desc(request):
    datas = {}
    return render_to_response(request, 'admin/action_desc.html', datas)
