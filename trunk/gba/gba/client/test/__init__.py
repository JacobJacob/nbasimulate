#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.entity import YouthPlayer, ProfessionPlayer
from gba.common.constants import attributes
from gba.common import playerutil

def set_youth_to_full(no):
    '''将一个街球队员练满'''
    player = YouthPlayer.load(no=no)
    if not player:
        return False
    for attribute in attributes:
        attr_max = getattr(player, '%s_max' % attribute)
        setattr(player, attribute, attr_max)
    playerutil.calcul_ability(player)
    player.persist()
    return True

def set_pro_to_full(no):
    '''将一个职业队员练满'''
    player = ProfessionPlayer.load(no=no)
    if not player:
        return False
    for attribute in attributes:
        attr_max = getattr(player, '%s_max' % attribute)
        setattr(player, attribute, attr_max)
    playerutil.calcul_ability(player)
    playerutil.calcul_wage(player)
    player.persist()
    return True
    
if __name__ == '__main__':
    set_youth_to_full('f331e8817d02b7076da3e4c60ee19683')
    set_pro_to_full('18224bc558abd768d170938c745608ce')