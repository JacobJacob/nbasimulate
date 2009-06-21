#!/usr/bin/python
# -*- coding: utf-8 -*-
"""default veiws"""

from webauth.web.render import render_to_response


def index(request):
    response = render_to_response(request, 'index.html')
    return response