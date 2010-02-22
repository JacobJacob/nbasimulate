#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback

from gba.entity import SysConfig, YouthPlayer, Message
from gba.client.betch import config

class DailyUpdate(object):
    '''每日更新批处理任务'''
    
    def __init__(self):
        pass
    
    
    
    def _run(self):
        self._sys_update()
    
    
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
            if not player:
                break
            start_id = players[-1].id
            for player in player:
                self._handle_player(player)
                
    def _handle_player(self, player):
        
           
    def start(self):
        try:
            self._run()
        except:
            print traceback.format_exc(3)
    
    
if __name__ == '__main__':
    client = DailyUpdate()
    client.start()