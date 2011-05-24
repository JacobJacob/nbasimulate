#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

class Position(ConstantBase):
    """球员位置"""
    POSITION_C = 1 #中锋
    POSITION_PF = 2 #大前锋
    POSITION_SF = 3 #小前锋
    POSITION_SG = 4 #得分后卫
    POSITION_PG = 5 #控球后卫

PositionMap = {
    Position.POSITION_C: u"中锋" , 
    Position.POSITION_PF: u"大前锋" ,          
    Position.POSITION_SF: u"小前锋" ,
    Position.POSITION_SG: u"得分后卫" ,
    Position.POSITION_PG: u"控球后卫" ,                 
}
