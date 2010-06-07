#!/usr/bin/python
# -*- coding: utf-8 -*-

"""每天更新说明:
1. 年轻球员潜力增长，体力回复
2. 职业队球员意识增长，合同数减少,体力回复， 看下是不是生日，如果是生日则年龄增长
3. 球队职员合同数减少
4. 广告合同数剩余轮次减少

清空表

5. 业作训练中心表

"""

import random

from gba.common.constants import AttributeMaps
from gba.entity import YouthPlayer, Message, ProfessionPlayer
from gba.client.betch import config
from gba.client.betch.base import BaseBetchClient

class DailyUpdate(BaseBetchClient):
    '''每日更新批处理任务'''
    
    def __init__(self, round):
        super(DailyUpdate, self).__init__()
        self._round = round
        
    def _run(self):
        self._youth_player_update()
        self._prefission_player_update()
    
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
            ProfessionPlayer.inserts(players)
                
    def _handle_prefission_player(self, player):
        '''职业球员单个更新'''

        #年龄更新
        age = player.age
        birthday = player.birthday
        if birthday == self._round:
            player.age = age + 1
        
        #耐力更新
        power = player.power
        #球员每天能回的点数为 20 -30 点
        can_bakc_power = random.randint(20, 31)
        power += can_bakc_power
        if power > 100:
            power = 100
        player.power = power
        
        contract = player.contract
        contract -= 1
        player.contract = contract if contract > 0 else 0