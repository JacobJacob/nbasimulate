#!/usr/bin/python
# -*- coding: utf-8 -*-
"""http render shortcuts"""

from django.shortcuts import render_to_response as _render_to_response
from django.template import RequestContext
from django.http import HttpResponse

from gba.common import json


def render_to_response(request, template_name, dictionary=None, context_instance=None, mimetype=None):
    if context_instance is None:
        context_instance = RequestContext(request)
    return _render_to_response(template_name, dictionary, context_instance=context_instance, mimetype=mimetype)

def json_response(data):
    return HttpResponse(json.dumps(data))


def admin_render(self, request, cls, template, filters=[]):
    '''admin render'''
    page = int(request.GET.get('page', 1))
    pagesize = int(request.GET.get('pagesize', 15))
    
    where = "1=1"
    for filter in filters:
        filter_value = request.GET.get(filter)
        if filter_value:
            where += ' and %s="%s"' % (filter, filter_value)
    
    infos, total = cls.paging(page, pagesize)
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / pagesize + 1
    
    datas = {'infos': infos, 'totalpage': totalpage, 'page': page, 'nextpage': page + 1, 'prevpage': page - 1}
        
    return render_to_response(request, template, datas)