#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from web.render import render_to_response
from gba.common.db import connection
from gba.common import json

def list(request):
    """list"""
    datas = {}
    return render_to_response(request, 'admin/list.html', datas)

def add(request):
    data = {}
    return render_to_response(request, 'admin/add.html', data)


def _save(request):
    return None

def edit(request):
    datas = {}
    return render_to_response(request, 'admin/add.html', datas)

   