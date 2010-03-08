#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.constants import attriubtes

def calcul_otential(player):
    '''计算球员潜力'''
    print '=' * 100
    for attr in attriubtes:
        print attr
        attr_value = player.get(attr, 0)
        attr_max_value = player.get('%s_max' % attr, 0)
        attr_otential = attr_max_value - attr_value
        player['%s_oten' % attr] = attr_otential
        print player
