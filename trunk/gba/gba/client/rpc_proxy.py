#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Service proxy define"""

from gba.common.jsonrpclib import ServerProxy
from gba.config import SERVICE_HOST

client_service_proxy = ServerProxy("http://%s/services/client/" % SERVICE_HOST)


if __name__ =='__main__':
    print client_service_proxy.report_status(39, 1, '')

