#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
Also see: django.core.context_processors
A set of request processors that return dictionaries to be merged into a
template context. Each function takes the request object as its only parameter
and returns a dictionary to add to the context.

These are referenced from the setting TEMPLATE_CONTEXT_PROCESSORS and used by
RequestContext.
"""

def data_total(request):
    return {'data_total': {}}