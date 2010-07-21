#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
the default manager is http://127.0.0.1:8000/.

if you set the environment variable 'URLAUTH_MANAGER', the default 
manager will be set as the variable.
"""

import os

from dtspider.config import SERVICE_HOST

class _Manager(object):
    def __init__(self):
        self.default_manager = 'http://%s/' % SERVICE_HOST
        if 'URLAUTH_MANAGER' in os.environ:
            self.set_default_manager(os.environ['URLAUTH_MANAGER'])
        
    def set_default_manager(self, val):
        if val.startswith('http://') or val.startswith('https://'):
            pass
        else:
            val = 'http://' + val
            
        if not val.endswith('/'):
            val += '/'
            
        self.default_manager = val
        
    def get_default_manager(self):
        return self.default_manager
    
_manager = _Manager()
set_default_manager = _manager.set_default_manager
get_default_manager = _manager.get_default_manager
