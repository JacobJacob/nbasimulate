#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import random
import time

from xml.dom.minidom import parse, parseString
from xml.dom.minidom import Element

from gba.config import PathSettings
from gba.common import md5mgr
from gba.entity import FreePlayer

class AttributeConfig(object):
    '''格式:{'shoot': {'grade-c': [xx, xx xx, xx, xx, xx, xx, xx, xx]}}'''
    
    _LOADED = False
    _DATA = {}
    
    
    @classmethod
    def get_attribute_config(cls, attribute, location, level=1):
        if level > 9 or level < 1:
            return None
        index = level - 1
        location = 'grade-%s' % location.lower() 
        if not cls._LOADED:
            cls._load()
        if attribute in cls._DATA:
            if location in cls._DATA[attribute]:
                return int(cls._DATA[attribute][location][index])
        return None

    @classmethod
    def _load(cls):
        config_dom = parse(os.path.join(PathSettings.PROJECT_FOLDER, 'config', 'attribute-config.xml'))
        root = config_dom._get_firstChild()
        for node in root.childNodes:
            if not isinstance(node, Element):
                continue
            name = node.getAttribute('name')
            child_map = {}
            for child_node in node.childNodes:
                if not isinstance(child_node, Element):
                    continue
                child_name = child_node.tagName
                text_node = child_node._get_firstChild()
                attributes = text_node.data.split(';')
                child_map[child_name] = attributes
            cls._DATA[name] = child_map
                
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
                    
class PlayerCreator(object):
    
    _attributes = ['dribble', 'backboard', 'blocked', 'bounce', 
                   'shooting', 'speed', 'pass', 'trisection',
                   'stamina', 'steal', 'strength']
    
    def __init__(self):
        self._betch_no = '%s' % int(time.time())
        
    def run(self):
        '''run'''
        for location in ['C', 'PF', 'SF', 'SG', 'PG']:
            self._run(location)
                
    def _run(self, location):
        count = 0 
        while True:
            for i, percent in enumerate(getattr(Config, 'FREE_PLAYER_PERCENT_%s' % location)):
                level = i + 1
                total = int(getattr(Config, 'FREE_PLAERY_TOTAL_%s' % location) * percent / getattr(Config, 'FREE_PLAYER_TOTAL_PERCENT_%s' % location))
                for i in range(total):
                    count += 1
                    player = self._create(location, level)
                    setattr(player, 'betch_no', self._betch_no)
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
    #print AttributeConfig.get_attribute_config('shooting', 'c', 1)
    
    