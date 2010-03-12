#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

attriubtes = ['dribble', 'backboard', 'blocked', 'bounce', 'shooting', 
              'speed', 'pass', 'trisection', 'stamina', 'steal', 'strength']


oten_color_map = [
   [25, 1],              
   [20, 2],
   [16, 3],
   [12.5, 4],                    
   [9.5, 5],
   [7, 6],
   [4.8, 7],
   [3.0, 8],
   [1.5, 9],
   [0.7, 10],
   [0, 11],             
]

class MatchStatus(ConstantBase):
    SEND = 0
    START = 1
    FINISH = 2
    CANCEL = 3
    
class MatchTypes(ConstantBase):
    FRIENDLY = 0