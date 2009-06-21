#!/usr/bin/python
# -*- coding: utf-8 -*-
# Author: MK2[fengmk2@gmail.com]
"""unit test
"""

import unittest
from dboperator import *

class ResourceManagerTest(unittest.TestCase):
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.db = DBOperator.get_instance()
    
    def test_singleton(self):
        
        self.assertRaises(NotImplementedError, DBOperator)
        
        self.assertEqual(id(self.db), id(DBOperator.get_instance()))
    
    def test_get_tablename(self):
        self.assertEqual(self.db.get_current_table('test'), 'test_a')
        self.assertEqual(self.db.get_another_table('test'), 'test_b')
        self.db.shift_table('test')
        self.assertEqual(self.db.get_current_table('test'), 'test_b')
        self.assertEqual(self.db.get_another_table('test'), 'test_a')
        self.db.shift_table('test')

if __name__ == '__main__':
    unittest.main()