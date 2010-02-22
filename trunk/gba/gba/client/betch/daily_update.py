#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

import traceback

from gba.entity import SysConfig, YouthPlayer, Message
from gba.client.betch import config

class DailyUpdate(object):
    '''每日更新批处理任务'''
    
    def __init__(self):
        pass
    
    def _run(self):
        self._sys_update()
        self._youth_player_update()
    
    def _sys_update(self):
        sys_config = SysConfig.load(value_key='season_round')
        if not sys_config:
            sys_config = SysConfig()
            sys_config.value_key = 'season_round'
            sys_config.value = 1
        else:
            sys_config.value = int(sys_config.value) + 1
            
        sys_config.persist()
            
    def _youth_player_update(self):
        
        start_id = 0
        while True:
            players = YouthPlayer.query(condition="id>%s" % start_id, limit=1000, order='id asc')
            if not players:
                break
            start_id = players[-1].id
            for player in players:
                self._handle_player(player)
                
    def _handle_player(self, player):
        if player.age > 21:
            return
        index = (21 - player.age) * 25
        ran_index = random.randint(1, 100)
        print ran_index, index
        if ran_index < index:
            attribute = config.random_attribute(player.position)
            base_attribute_value = getattr(player, '%s_max' % attribute)
            inc_value = random.uniform(1.5, 6)
            if inc_value + base_attribute_value > 99.9:
                inc_value = 99.9 - base_attribute_value
            setattr(player, '%s_max' % attribute, base_attribute_value + inc_value)
            player.persist()            
            
            message = Message()
            message.from_team_id = 0
            message.to_team_id = player.team_id
            message.content = "%s's %s inc %s" % (player.name, attribute, inc_value)
            message.persist()
            
    def start(self):
        try:
            self._run()
        except:
            print traceback.format_exc(3)
    
    
if __name__ == '__main__':
    client = DailyUpdate()
    client.start()