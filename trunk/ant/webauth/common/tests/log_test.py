#!/usr/bin/python
# -*- coding: utf-8 -*-
"""test syslog-ng"""

import unittest

from webauth.common import init_log, log_execption

class SyslogTest(unittest.TestCase):
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        init_log()
    
    def test_log(self):
        import logging
        logging.warning('warning test')
        logging.debug('debug test')
        logging.error('error test')
        logging.info('info test')
        
    def test_log_exception(self):
        try:
            1 / 0
        except:
            log_execption()
        
if __name__ == '__main__':
    try:
        1 / 0
    except:
        log_execption()
        
    unittest.main()