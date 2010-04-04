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

class MatchTypes(ConstantBase):
    FRIENDLY = 1  #
    TRAINING = 2  #
    YOUTH_TRAINING = 3 #青年训练赛
    YOUTH_FRIENDLY = 4
    
MatchTypeMaps = {
    MatchTypes.FRIENDLY: u'职业友谊 ',  
    MatchTypes.TRAINING: u'职业训练 ',
    MatchTypes.YOUTH_FRIENDLY: u'街头友谊 ',
    MatchTypes.YOUTH_TRAINING: u'街头训练 ',             
}