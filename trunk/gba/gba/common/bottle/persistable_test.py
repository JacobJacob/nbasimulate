#!/usr/bin/python
# -*- coding: utf-8 -*-


from gba.common.bottle.persistable import Persistable

class Test(Persistable):
    CACHE_KEY = 'test:id'
    
    
if __name__ == '__main__':
    
    test = Test.load(id=1)
    test.b = 1
    test.value = 'cccccc'
    test.persist()