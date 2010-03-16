#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

class NameFactory():
    
    @classmethod
    def create_name(cls):
        return 'player name'
    
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
