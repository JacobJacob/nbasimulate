#!/usr/bin/python
# -*- coding: utf-8 -*-

import random
import time
import traceback

from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common import md5mgr, json, playerutil
from gba.entity import PlayerBetchLog, YouthFreePlayer
from gba.client.betch.config import AttributeConfig
from gba.common.attribute_factory import AvoirdupoisFactory, NameFactory, AgeFactory, StatureFactory, PictrueFactory
from gba.common.constants import attributes, hide_attributes
      
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
                 
class YauthPlayerCreator(object):
    
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
                    setattr(player, 'age', AgeFactory.create(youth=True))
                    setattr(player, 'stature', StatureFactory.create(location, youth=True))
                    setattr(player, 'avoirdupois', AvoirdupoisFactory.create(location, youth=True))
                    setattr(player, 'expired_time', ReserveLiteral('date_add(now(), interval %s minute)' % random.randint(2160, 2880)))
                    setattr(player, 'picture', PictrueFactory.create())
                    name = NameFactory.create_name()
                    setattr(player, 'name', name)
                    setattr(player, 'name_base', name)
                    player.persist()
            if count >= getattr(Config, 'FREE_PLAERY_TOTAL_%s' % location):
                break
    
    def _create(self, location, level):
        '''create player'''
        player = YouthFreePlayer()
        for attribute in attributes:
            base = AttributeConfig.get_attribute_config(attribute, location, level)
            value = 30 * random.random() + base
            if attribute in hide_attributes:#隐藏属性不能太大
                atr_value = value * random.random()
                while atr_value > 20:
                    atr_value -= 20
                setattr(player, attribute, atr_value)
            else:
                setattr(player, attribute, value * random.random())
            setattr(player, attribute, value * random.random())
            setattr(player, '%s_max' % attribute, value)
        setattr(player, 'position', location)
        setattr(player, 'position_base', location)
        setattr(player, 'player_no', random.randint(0, 30))
        setattr(player, 'no', md5mgr.mkmd5fromstr('%s_%s_%s' % (location, level, str(time.time()))))
        playerutil.calcul_ability(player)
        return player

if __name__ == '__main__':
    creator = YauthPlayerCreator()
    creator.run()