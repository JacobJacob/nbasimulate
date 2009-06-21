#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from webauth.business.client import ClientManager
from webauth.web.render import render_to_response


def list(request):
    clients = ClientManager.get_clients()
    categories = {}
    # 按类别分类
    for client in clients:
        client_type = client['type']
        if client_type not in categories:
            categories[client_type] = []
        categories[client_type].append(client)
    return render_to_response(request, 
                              'monitor/clients.html',
                              {'categories': categories})