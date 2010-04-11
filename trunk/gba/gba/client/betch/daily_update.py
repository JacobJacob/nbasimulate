#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from gba.common.constants import AttributeMaps
from gba.entity import LeagueConfig, YouthPlayer, Message, ProfessionPlayer
from gba.client.betch import config
from gba.common import playerutil
from gba.client.betch.base import BaseBetchClient

class DailyUpdate(BaseBetchClient):
    '''每日更新批处理任务'''
    
    def __init__(self):
        super(DailyUpdate, self).__init__()
    
    def _run(self):
        pass
        #self._sys_update()
        #self._youth_player_update()
        #self._prefission_player_update()
    
    def _youth_player_update(self):
        '''年轻球员更新'''
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
            message.title = u"%s 在刻苦训练中%s潜力加%s" % (player.name, AttributeMaps[attribute], inc_value)
            message.content = u"%s 在刻苦训练中%s潜力加%s" % (player.name, AttributeMaps[attribute], inc_value)
            message.persist()
            
    def _prefission_player_update(self):
        '''职业球员更新'''
        start_id = 0
        while True:
            players = ProfessionPlayer.query(condition="id>%s" % start_id, limit=1000, order='id asc')
            if not players:
                break
            start_id = players[-1].id
            for player in players:
                self._handle_prefission_player(player)
                
    def _handle_prefission_player(self, player):
        '''职业球员单个更新'''
        training = player.training
        if not training:
            training = 'speed'
        
        age = player.age
        
        #年龄加点因素
        age_inc = float((35 - age) / 5)
        #随机因素
        random_inc = random.randint(1, 7)
        #训练员因素
        trainers_inc = 0
    
        inc = age_inc + random_inc + trainers_inc
    
        old_attr = getattr(player, training)
        max_attr = getattr(player, '%s_max' % training)
        
        if old_attr + inc > max_attr:
            inc = max_attr - old_attr
            
        setattr(player, training, old_attr + inc)
        setattr(player, 'last_inc', inc)
        playerutil.calcul_ability(player)
        player.persist() 