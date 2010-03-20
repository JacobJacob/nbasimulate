#!/usr/bin/python
# -*- coding: utf-8 -*-

import random
import time

from gba.common.constants import attributes
from gba.common import md5mgr
from gba.entity import ProfessionPlayer
from gba.client.betch.config import AttributeConfig
from gba.common.attribute_factory import AvoirdupoisFactory, NameFactory, AgeFactory, StatureFactory, PictrueFactory

def calcul_otential(player):
    '''计算球员潜力'''
    if not player:
        return
    for attr in attributes:
        if isinstance(player, dict):
            attr_value = player.get(attr, 0)
            attr_max_value = player.get('%s_max' % attr, 0)
            attr_otential = attr_max_value - attr_value
            player['%s_oten' % attr] = attr_otential
        else:
            attr_value = getattr(player, attr)
            attr_max_value = getattr(player, '%s_max' % attr)
            attr_otential = attr_max_value - attr_value
            setattr(player, '%s_oten' % attr, attr_otential)
        
def calcul_ability(player):
    '''计算一个球员的综合'''
    ability = 0
    for attr in attributes:
        attr_value = getattr(player, attr)
        ability += attr_value
    player.ability = ability / len(attributes)
    
def create_profession_player(location):
    '''创建一个职业球员'''
    
    level = random.randint(1, 9)
    
    player = ProfessionPlayer()
    setattr(player, 'betch_no', '#' * 32)
    setattr(player, 'age', AgeFactory.create())
    setattr(player, 'stature', StatureFactory.create(location))
    setattr(player, 'avoirdupois', AvoirdupoisFactory.create(location))
                        
    for attribute in attributes:
        base = AttributeConfig.get_attribute_config(attribute, location, level)
        value = 30 * random.random() + base
        setattr(player, attribute, value * random.random())
        setattr(player, '%s_max' % attribute, value)
    setattr(player, 'position', location)
    setattr(player, 'position_base', location)
    setattr(player, 'player_no', random.randint(0, 30))
    setattr(player, 'no', md5mgr.mkmd5fromstr('%s_%s_%s' % (location, level, str(time.time()))))
    setattr(player, 'name', NameFactory.create_name())
    setattr(player, 'name_base', NameFactory.create_name())
    setattr(player, 'picture', PictrueFactory.create())
    setattr(player, 'power', 100) #体力100
    setattr(player, 'status', 1) #状态正常
    calcul_ability(player)
    
    return player
