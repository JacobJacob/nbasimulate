#!/usr/bin/python
# -*- coding: utf-8 -*-
"""公共custom tags and filter"""

import datetime

from django import template
from gba.common.constants import oten_color_map

register = template.Library()

@register.filter
def check_attr(attr_oten):
    for map_info in oten_color_map:
        if attr_oten >= map_info[0]:
            return map_info[1]
    return 0
    



