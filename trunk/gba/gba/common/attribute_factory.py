#!/usr/bin/python
# -*- coding: utf-8 -*-

import random
from gba.entity import Names
from gba.common import cache

class NameFactory():
    
    @classmethod
    def create_name(cls):
        first_names = cache.get('first_names')
        if not first_names:
            first_names = Names.query(type=1)
            cache.set('first_names', first_names, 60*60)
        second_names = cache.get('second_names')
        if not second_names:
            second_names = Names.query(type=1)
            cache.set('second_names', second_names, 60*60)
    
        first_name = first_names[random.randint(0, len(first_names)-1)]
        second_name = second_names[random.randint(0, len(second_names)-1)]
        return '%s.%s' % (first_name.name, second_name.name)
        
class AgeFactory():
    
    @classmethod
    def create(cls):
        return random.randint(19, 30)
    
class StatureFactory():
    
    _BASE_VALUE = {'C': 195, 'PF': 190, 'SF': 185, 'SG': 180, 'PG': 160}
    
    @classmethod
    def create(cls, location):
        return random.randint(0, 30) + cls._BASE_VALUE[location.upper()]
        
class AvoirdupoisFactory():
    
    _BASE_VALUE = {'C': 90, 'PF': 85, 'SF': 70, 'SG': 60, 'PG': 55}
    
    @classmethod
    def create(cls, location):
        return random.randint(0, 50) + cls._BASE_VALUE[location.upper()]
    
class PictrueFactory():
    
    @classmethod
    def create(cls):
        return random.randint(1, 40)
    
if __name__ == '__main__':
    print NameFactory.create_name()
