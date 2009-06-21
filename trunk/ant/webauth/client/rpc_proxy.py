#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Service proxy define"""

from webauth.common.jsonrpclib import ServerProxy
from webauth.client import get_default_manager


__all__ = ("url_task_proxy",
           "client_service_proxy",
           "url_source_proxy",
           'client_service_proxy',
           )

web_service = get_default_manager()

url_task_proxy = ServerProxy("%sservices/url_task/" % web_service)

url_source_proxy = ServerProxy("%sservices/url/" % web_service)

client_service_proxy = ServerProxy("%sservices/client/" % web_service)

proxy_service_proxy = ServerProxy("%sservices/proxy/" % web_service)

domain_service_proxy = ServerProxy("%sservices/domain/" % web_service)