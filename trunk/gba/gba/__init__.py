#!/usr/bin/python
# -*- coding: utf-8 -*-

VERSION = None
__first_load = True
if VERSION is None and __first_load:
    __first_load = False
    def _getVersion():
        import subprocess
        import os
        words = __name__.split('.')
        if not words:
            return None
        root = words[0]
        try:
            m = __import__(root, {}, {}, ['__file__'])
        except:
            return None
        path = os.path.dirname(m.__file__)
        try:
            data = subprocess.Popen(['svnversion',  path],
                             stdout=subprocess.PIPE).communicate()[0]
            res = data.strip()
            return res
        except:
            return None
    VERSION = _getVersion()
    del _getVersion