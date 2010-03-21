#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from constant_base import ConstantBase

class ActionType(ConstantBase):
    PASS = 1;
    SHOUT = 2;
    THROUGH = 3;
    REBOUND = 4;
    SERVICE = 5;
    SCRIMMAGE = 6;
    FOUL = 7;
    
class MatchStatus(ConstantBase):
    SEND = 0
    ACCP = 1
    START = 2
    FINISH = 3
    CANCEL = 4
    

MatchStatusMap = {
    MatchStatus.SEND: u'等待',      
    MatchStatus.ACCP: u'己接受',
    MatchStatus.START: u'进行中',
    MatchStatus.FINISH: u'完成',
    MatchStatus.CANCEL: u'取消',              
}