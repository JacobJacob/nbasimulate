#!/usr/bin/python
# -*- coding: utf-8 -*-

import random
import time
import copy

from gba.common import md5mgr
from gba.entity import ProfessionPlayer, YouthPlayer, FreePlayer
from gba.client.betch.config import AttributeConfig, YouthAttributeConfig
from gba.common.attribute_factory import AvoirdupoisFactory, NameFactory, AgeFactory, StatureFactory, PictrueFactory
from gba.common.constants import attributes, hide_attributes
from gba.common.config import import_attributes

def finish_training(training_center):
    '''一个球队完成训练'''
    team_id = training_center.team_id
    youth_players = YouthPlayer.query(condition='team_id="%s"' % team_id)
    for youth_player in youth_players:
        for attribute in attributes:
            if attribute not in hide_attributes:
                old_value = getattr(youth_player, attribute)
                max_value = getattr(youth_player, '%s_max' % attribute)
                if old_value + 0.2 > max_value:
                    new_value = max_value
                else:
                    new_value = old_value + 0.2
                setattr(youth_player, attribute, new_value)
                calcul_ability(youth_player)
                calcul_consume_power(youth_player)
                
    YouthPlayer.transaction()
    try:
        for youth_player in youth_players:
            youth_player.persist()
        training_center.status = 1
        training_center.persist()
        YouthPlayer.commit()
    except:
        YouthPlayer.rollback()
        raise
        
def calcul_consume_power(player):
    '''计算一个球员打一场比赛消耗的体力'''
    stamina = getattr(player, 'stamina')
    old_power = getattr(player, 'power')

    consume = 20
    consume += random.randint(0, 10) - stamina / 5
    
    new_power = old_power - consume
    if new_power < 0:
        new_power = 0
    
    setattr(player, 'power', new_power) 
            
def calcul_otential(player):
    '''计算球员潜力'''
    if not player:
        return
    for attr in attributes:
        if isinstance(player, dict):
            attr_value = player.get(attr, 0)
            attr_value = attr_value if attr_value else 0
            attr_max_value = player.get('%s_max' % attr, 0)
            attr_max_value = attr_max_value if attr_max_value else 0
            attr_otential = attr_max_value - attr_value
            player['%s_oten' % attr] = attr_otential if attr_otential > 0.1 else 0
        else:
            attr_value = getattr(player, attr)
            attr_value = attr_value if attr_value else 0
            attr_max_value = getattr(player, '%s_max' % attr)
            attr_max_value = attr_max_value if attr_max_value else 0
            attr_otential = attr_max_value - attr_value
            setattr(player, '%s_oten' % attr, attr_otential if attr_otential > 0.1 else 0)
            

def calcul_social_status(player):
    '''计算一个球员身价'''
    if isinstance(player, dict):
        ability = player['ability']
        position = player['position_base']
    else:
        ability = getattr(player, 'ability')
        position = getattr(player, 'position_base')
        
    times = 1
    if position == 'C':
        times = 1.5
    elif position == 'PF':
        times = 1.4
    elif position == 'SF':
        times = 1.3
    elif position == 'SG':
        times = 1.2
    else:
        position = 1.1
    
    twice_as_much = 15
     
    if ability <= 20:
        social_status = float(ability * twice_as_much) / 90 * 10000 + (times * 1000)
    elif ability <= 30:
        social_status = float(ability * twice_as_much) / 110 * 10000 + (times * 1000)
    elif ability <= 40:
        social_status = float(ability * twice_as_much) / 105 * 10000 + (times * 1000)
    elif ability <= 50:
        social_status = float(ability * twice_as_much) / 95 * 100000 + (times * 1000)
    elif ability <= 60:
        social_status = float(ability * twice_as_much) / 85 * 100000 + (times * 1000)
    elif ability <= 70:
        social_status = float(ability * twice_as_much) / 60 * 100000 + (times * 1000)
    elif ability <= 80:
        social_status = float(ability * twice_as_much) / 50 * 100000 + (times * 1000)
    elif ability <= 90:
        social_status = float(ability * twice_as_much) / 40 * 100000 + (times * 1000)
    else:
        social_status = float(ability * twice_as_much) / 30 * 100000 + (times * 1000)
        
    return int(social_status)
            
def calcul_wage(player, ran=True):
    '''计算工资'''
    if isinstance(player, dict):
        ability = player['ability']
        position = player['position_base']
    else:
        ability = getattr(player, 'ability')
        position = getattr(player, 'position_base')
        
    times = 1
    if position == 'C':
        times = 1.5
    elif position == 'PF':
        times = 1.4
    elif position == 'SF':
        times = 1.3
    elif position == 'SG':
        times = 1.2
    else:
        position = 1.1
    
    if ran:
        for i in range(3):
            if random.randint(1, 2) == 1:
                times += 0.1
            else:
                times -= 0.1
        
    if ability <= 20:
        wage = float(ability) / 90 * 10000 + (times * 1000)
    elif ability <= 30:
        wage = float(ability) / 110 * 10000 + (times * 1000)
    elif ability <= 40:
        wage = float(ability) / 105 * 10000 + (times * 1000)
    elif ability <= 50:
        wage = float(ability) / 95 * 10000 + (times * 1000)
    elif ability <= 60:
        wage = float(ability) / 85 * 10000 + (times * 1000)
    elif ability <= 70:
        wage = float(ability) / 60 * 10000 + (times * 1000)
    elif ability <= 80:
        wage = float(ability) / 50 * 10000 + (times * 1000)
    elif ability <= 90:
        wage = float(ability) / 40 * 10000 + (times * 1000)
    else:
        wage = float(ability) / 30 * 10000 + (times * 1000)
        
    wage = int(wage)
    if isinstance(player, dict):
        player['wage'] = wage
    else:
        setattr(player, 'wage', wage)
    return wage
    
def calcul_ability(player):
    '''计算一个球员的综合'''
    ability = 0
    for attr in attributes:
        attr_value = getattr(player, attr)
        if not attr_value:
            attr_value = 0
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
    setattr(player, 'no', md5mgr.mkmd5fromstr('%s_%s_%s_%s_%s' % (location, level, str(time.time()), player.player_no, random.random())))
    setattr(player, 'name', NameFactory.create_name())
    setattr(player, 'name_base', NameFactory.create_name())
    setattr(player, 'picture', PictrueFactory.create())
    setattr(player, 'power', 100) #体力100
    setattr(player, 'status', 1) #状态正常
    setattr(player, 'contract', 26) #合同26轮
    setattr(player, 'training', 'speed') #默认训练速度
    calcul_ability(player)
    calcul_wage(player)
    
    return player

def copy_player(player, source='youth_free_player', to='youth_player'):
    if source == 'youth_free_player' and to == 'youth_player':
        youth_free_player = copy.deepcopy(player)
        delattr(youth_free_player, 'id')
        delattr(youth_free_player, 'price')
        delattr(youth_free_player, 'bid_count')
        delattr(youth_free_player, 'expired_time')
        delattr(youth_free_player, 'delete_time')
        delattr(youth_free_player, 'created_time')
        delattr(youth_free_player, 'updated_time')
        youth_free_player.__class__ = YouthPlayer
        return youth_free_player
    elif source == 'free_player' and to == 'profession_player':
        profession_player = copy.deepcopy(player)
        delattr(profession_player, 'id')
        delattr(profession_player, 'bid_count')
        delattr(profession_player, 'expired_time')
        delattr(profession_player, 'delete_time')
        delattr(profession_player, 'created_time')
        delattr(profession_player, 'updated_time')
        delattr(profession_player, 'auction_status')
        delattr(profession_player, 'worth')
        delattr(profession_player, 'current_team_id')
        delattr(profession_player, 'current_price')
        profession_player.__class__ = ProfessionPlayer
        return profession_player
    elif source == 'draft_player' and to == 'profession_player':
        profession_player = copy.deepcopy(player)
        delattr(profession_player, 'id')
        profession_player.__class__ = ProfessionPlayer
        return profession_player
    elif source == 'youth_player' and to == 'profession_player':
        profession_player = copy.deepcopy(player)
        delattr(profession_player, 'id')
        profession_player.__class__ = ProfessionPlayer
        return profession_player
    elif source == 'profession_player' and to == 'free_player':
        free_player = copy.deepcopy(player)
        delattr(free_player, 'id')
        free_player.__class__ = FreePlayer
        return free_player
    elif source == 'init_profession_player' and to == 'profession_player':
        profession_player = copy.deepcopy(player)
        delattr(profession_player, 'id')
        profession_player.__class__ = ProfessionPlayer
        return profession_player
    elif source == 'init_youth_player' and to == 'youth_player':
        youth_player = copy.deepcopy(player)
        delattr(youth_player, 'id')
        youth_player.__class__ = YouthPlayer
        return youth_player
    
    return None

def create_youth_player(location):
    '''创建一个青年球员'''
    
    level = random.randint(1, 9)
    
    player = YouthPlayer()
    setattr(player, 'betch_no', '#' * 32)
    setattr(player, 'age', AgeFactory.create(youth=True))
    setattr(player, 'stature', StatureFactory.create(location, youth=True))
    setattr(player, 'avoirdupois', AvoirdupoisFactory.create(location, youth=True))
                        
    for attribute in attributes:
        base = YouthAttributeConfig.get_attribute_config(attribute, location, level)
        value = 30 * random.random() + base
        setattr(player, attribute, value * random.random())
        setattr(player, '%s_max' % attribute, value)
    setattr(player, 'position', location)
    setattr(player, 'position_base', location)
    setattr(player, 'player_no', random.randint(0, 30))
    setattr(player, 'no', md5mgr.mkmd5fromstr('%s_%s_%s_%s_%s' % (location, level, str(time.time()), player.player_no, random.random())))
    setattr(player, 'name', NameFactory.create_name())
    setattr(player, 'name_base', NameFactory.create_name())
    setattr(player, 'picture', PictrueFactory.create())
    setattr(player, 'power', 100) #体力100
    setattr(player, 'status', 1) #状态正常
    calcul_ability(player)

    return player

def youth_player_promoted(player):
    '''年轻球员提拔
    1. 属性加潜力
    2. 意识直接加能力
    '''
    profession_player = copy_player(player, source='youth_player', to='profession_player')
    profession_player.status = 1
    
    add_base = 4
    age = player.age
    for attribute in attributes:
        if attribute in import_attributes[player.position]:
            add_base += 4
        
        if age < 21:
            add_value = random.randint(0, 5)
        else:
            if age > 26:
                add_value = random.randint(0, 26-age)
            else:
                add_value = random.randint(0, 2)
        old_attr = getattr(profession_player, '%s_max' % attribute)
        if old_attr + add_base + add_value >= 100:
            new_attr = 99.9
        else:
            new_attr = old_attr + add_base + add_value
        
        setattr(profession_player, '%s_max' % attribute, new_attr)
        
    calcul_ability(profession_player)
    calcul_wage(profession_player)
    profession_player.contract = 26

    return profession_player
        
if __name__ == '__main__':

    player = ProfessionPlayer()
    player.ability = 70
    player.position = 'C'
    print calcul_wage(player, ran=False)