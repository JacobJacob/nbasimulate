#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Service proxy define"""

from dtspider.common.jsonrpclib import ServerProxy
from dtspider.client import get_default_manager


__all__ = ("client_service_proxy",
           'proxy_service_proxy',
           )

web_service = get_default_manager()


client_service_proxy = ServerProxy("%sservices/client/" % web_service)

proxy_service_proxy = ServerProxy("%sservices/proxy/" % web_service)



if __name__ == '__main__':
    print proxy_service_proxy.add_proxy({'proxy': '192.168.1.1', 'timeout':20})
