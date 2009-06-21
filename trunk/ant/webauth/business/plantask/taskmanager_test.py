#!/usr/bin/python
# -*- coding: utf-8 -*-
"""unit test
"""

import unittest
from taskmanager import TaskManager

class TaskManagerTest(unittest.TestCase):
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.mgr = TaskManager.get_instance()
    
    def test_refresh(self):
        self.mgr.sync()
        
    def test_format(self):
        cmd = 'python $project$/client/test.py'
        print self.mgr._format(cmd)
        cmd = 'python $working_folder$/client/datasynchronization/export_data_mysqldump.py'
        print self.mgr._format(cmd)
        print self.mgr.TASKS

if __name__ == '__main__':
    unittest.main()