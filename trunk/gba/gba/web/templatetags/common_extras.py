#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

import datetime

from django import template
from entity.module import module_admin_list

register = template.Library()

@register.filter
def display_row(object):
    object_admin = module_admin_list[object.__class__.__name__]
    display_columns = object_admin.DISPLAY_COLUMNS
    html = '<tr>'
    for display_column in display_columns:
        html += '<td>%s</td>' % getattr(object, display_column)
    html += '</tr>'
    return html

@register.filter
def display_header(module):
    object_admin = module_admin_list[module]
    display_columns = object_admin.DISPLAY_COLUMNS
    html = '<thead><tr>'
    for display_column in display_columns:
        html += '<th>%s</th>' % display_column
    html += '</tr></thead>'
    return html

@register.filter
def display_form(module):
    object_admin = module_admin_list[module.__class__.__name__]
    add_columns = object_admin.ADD_COLUMNS
    html = '<form action="%s" method="post">'
    html += '<table>'
    for add_column in add_columns:
        html += '<tr><td>%s</td>' % add_column
        html += '<td><input name="%s"></td></tr>' % add_column 
    html += '</table></form>'
    return html
    

