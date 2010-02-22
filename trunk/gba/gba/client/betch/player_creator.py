#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import random
import time
import traceback

from gba.common import md5mgr, json
from gba.entity import FreePlayer, PlayerBetchLog
from gba.client.betch.config import AttributeConfig
  
class Config(object):
    
    FREE_PLAERY_TOTAL_C = 10
    FREE_PLAYER_PERCENT_C = [1, 2, 5, 10, 20, 25, 30, 40, 50]
    FREE_PLAYER_TOTAL_PERCENT_C = 0
    for r in FREE_PLAYER_PERCENT_C:
        FREE_PLAYER_TOTAL_PERCENT_C += r
        
    FREE_PLAERY_TOTAL_PF = 60
    FREE_PLAYER_PERCENT_PF = [1, 2, 5, 10, 20, 25, 30, 40, 50]
    FREE_PLAYER_TOTAL_PERCENT_PF = 0
    for r in FREE_PLAYER_PERCENT_PF:
        FREE_PLAYER_TOTAL_PERCENT_PF += r
        
    FREE_PLAERY_TOTAL_SF = 70
    FREE_PLAYER_PERCENT_SF = [1, 2, 5, 10, 20, 25, 30, 40, 50]
    FREE_PLAYER_TOTAL_PERCENT_SF = 0
    for r in FREE_PLAYER_PERCENT_SF:
        FREE_PLAYER_TOTAL_PERCENT_SF += r
        
    FREE_PLAERY_TOTAL_SG = 80
    FREE_PLAYER_PERCENT_SG = [1, 2, 5, 10, 20, 25, 30, 40, 50]
    FREE_PLAYER_TOTAL_PERCENT_SG = 0
    for r in FREE_PLAYER_PERCENT_SG:
        FREE_PLAYER_TOTAL_PERCENT_SG += r
        
    FREE_PLAERY_TOTAL_PG = 100
    FREE_PLAYER_PERCENT_PG = [1, 2, 5, 10, 20, 25, 30, 40, 50]
    FREE_PLAYER_TOTAL_PERCENT_PG = 0
    for r in FREE_PLAYER_PERCENT_PG:
        FREE_PLAYER_TOTAL_PERCENT_PG += r
        
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
                    
class PlayerCreator(object):
    
    _attributes = ['dribble', 'backboard', 'blocked', 'bounce', 
                   'shooting', 'speed', 'pass', 'trisection',
                   'stamina', 'steal', 'strength']
    
    def __init__(self):
        self._betch_no = '%s' % int(time.time())
        self._info = {}
        self._created_total = 0
        
    def run(self):
        '''run'''
        betch_log = PlayerBetchLog()
        betch_log.betch_no = self._betch_no
        try:
            for location in ['C', 'PF', 'SF', 'SG', 'PG']:
                self._run(location)
        except:
            betch_log.is_success = 0
            self._info['error_msg'] = traceback.format_exc(3)
            print self._info['error_msg']
        else:
            betch_log.is_success = 1
        
        self._info['created_total'] = self._created_total   
        betch_log.info = json.dumps(self._info)
        betch_log.persist()
                
    def _run(self, location):
        count = 0 
        while True:
            for i, percent in enumerate(getattr(Config, 'FREE_PLAYER_PERCENT_%s' % location)):
                level = i + 1
                total = int(getattr(Config, 'FREE_PLAERY_TOTAL_%s' % location) * percent / getattr(Config, 'FREE_PLAYER_TOTAL_PERCENT_%s' % location))
                for i in range(total):
                    count += 1
                    self._created_total += 1
                    player = self._create(location, level)
                    setattr(player, 'betch_no', self._betch_no)
                    setattr(player, 'age', AgeFactory.create())
                    setattr(player, 'stature', StatureFactory.create(location))
                    setattr(player, 'avoirdupois', AvoirdupoisFactory.create(location))
                    player.persist()
            if count >= getattr(Config, 'FREE_PLAERY_TOTAL_%s' % location):
                break
    
    def _create(self, location, level):
        '''create player'''
        player = FreePlayer()
        for attribute in self._attributes:
            base = AttributeConfig.get_attribute_config(attribute, location, level)
            value = 30 * random.random() + base
            setattr(player, attribute, value * random.random())
            setattr(player, '%s_max' % attribute, value)
        setattr(player, 'position', location)
        setattr(player, 'position_base', location)
        setattr(player, 'player_no', random.randint(0, 50))
        setattr(player, 'no', md5mgr.mkmd5fromstr('%s_%s_%s' % (location, level, str(time.time()))))
        setattr(player, 'name', NameFactory.create_name())
        setattr(player, 'name_base', NameFactory.create_name())
        return player

if __name__ == '__main__':
    creator = PlayerCreator()
    creator.run()
    #AvoirdupoisFactory.create('C')
    #print AttributeConfig.get_attribute_config('shooting', 'c', 1)
    
    