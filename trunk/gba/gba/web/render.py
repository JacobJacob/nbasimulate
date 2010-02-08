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