#!/usr/bin/python
# -*- coding: utf-8 -*-
"""unit test
"""

import unittest
import url_source

class URLSourceTest(unittest.TestCase):
    
    def test_get_total_count(self):
        count = url_source.get_total_count()
        print count
        self.assertTrue(isinstance(count, (int, long)))

if __name__ == '__main__':
    unittest.main()