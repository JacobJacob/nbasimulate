#!/usr/bin/python
# -*- coding: utf-8 -*-

def calcul_attendance(fare, level, fans_count, rank):
    '''根所票价,座位数,排名.球迷数.算出上座率'''
    default = 50
    default = default + (10-rank) * 5 #排名每上升一位上座率加5
    default = default + (60-fare) #票价每升一块上座 率减1
    
    default = default + 10 * (5 - (fans_count / (level * 1000))) #球迷数默认为座位数5倍,如果每大一倍,则相应的上座减加10
    
    default = float(default) / 100
    
    return default if default <= 1 else 1

