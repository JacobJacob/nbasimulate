#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Singleton class unit test
"""

import unittest
from uuid import uuid4

from singleton import Singleton


class A(Singleton):
    def __init__(self):
        self.name = 'Child A: %s' % uuid4()
        print self.name

class B(Singleton):
    def __init__(self):
        self.name = 'Child B --> A: %s' % uuid4()
    
class C(object):
    def __init__(self):
        self.name = 'C: %s' % uuid4()
    
class E(A):
    pass

class D(A):
    pass

class SingletonTest(unittest.TestCase):
    
    def test_instance(self):
        a = A.get_instance()
        self.assertEqual(id(a), id(A.get_instance()))
        self.assertEqual(id(A.get_instance()), id(A.get_instance()))
        self.assertEqual(a.name, A.get_instance().name)
        
        b = B.get_instance()
        self.assertEqual(id(b), id(B.get_instance()))
        self.assertEqual(b.name, B.get_instance().name)
        
        c = C()
        self.assertNotEqual(id(c), id(C()))
        
        s = Singleton.get_instance()
        self.assertEqual(id(s), id(Singleton.get_instance()))
        self.assertEqual(id(Singleton.get_instance()), id(Singleton.get_instance()))
        
        self.assertNotEqual(id(a), id(s))
        self.assertNotEqual(id(b), id(s))
        self.assertNotEqual(id(a), id(b))
        
        self.assertEqual(id(D.get_instance()), id(E.get_instance()))
        
if __name__ == '__main__':
    unittest.main()