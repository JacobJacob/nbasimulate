#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

attributes = ['dribble', 'backboard', 'blocked', 'bounce', 'shooting', 
              'speed', 'pass', 'trisection', 'stamina', 'steal', 'strength',
              'defencons', 'offencons', 'buildupcons', 'leadcons', 'backbone']

hide_attributes = ['defencons', 'offencons', 'buildupcons', 'leadcons', 'backbone',]

AttributeMaps = {
    attributes[0]: u'运球',
    attributes[1]: u'篮板',
    attributes[2]: u'封盖',
    attributes[3]: u'弹跳',
    attributes[4]: u'投篮',
    attributes[5]: u'速度',
    attributes[6]: u'传球',
    attributes[7]: u'耐力',
    attributes[8]: u'三分',
    attributes[9]: u'抢断',
    attributes[10]: u'强壮',
    attributes[11]: u'防守意识',
    attributes[12]: u'进攻意识',
    attributes[13]: u'团队意识',
    attributes[14]: u'领导力',
    attributes[15]: u'毅力',                                   
}


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

class Position(ConstantBase):
    PG = 'PG'
    SG = 'SG'
    SF = 'SF'
    PF = 'PF'
    C = 'C'
    
PositioneMap = {
    Position.PG: u'组织后卫',
    Position.SG: u'得分后卫',
    Position.SF: u'小前锋',
    Position.PF: u'大前锋',
    Position.C: u'中锋',                                        
}
    
class MatchTypes(ConstantBase):
    FRIENDLY = 1
    TRAINING = 2
 
class TacticalGroupType(ConstantBase):
    PROFESSION = 1
    CUP = 2
    OTHERS = 3
    
TacticalGroupTypeMap = {
    TacticalGroupType.PROFESSION: u'职业联赛',
    TacticalGroupType.CUP: u'杯赛',
    TacticalGroupType.OTHERS: u'其它比赛',                                            
}
   
class TacticalSectionType(ConstantBase):
    '''战术节数'''
    FIRST_SECTION = 1 #第一节
    SECOND_SECTION = 2
    THIRD_SECTION = 3
    FOURTH_SECTION = 4
    OVERTIME_ONE = 5 #加时一
    OVERTIME_TWO = 6
    FRONT_15 = 7 #领先15分
    BEHIND_15 = 8 #落后15分
    
TacticalSectionTypeMap = {
   TacticalSectionType.FIRST_SECTION: u'第一节',
   TacticalSectionType.SECOND_SECTION: u'第二节',
   TacticalSectionType.THIRD_SECTION: u'第三节',
   TacticalSectionType.FOURTH_SECTION: u'第四节',
   TacticalSectionType.OVERTIME_ONE: u'第一加时',
   TacticalSectionType.OVERTIME_TWO: u'第二加时',
   TacticalSectionType.FRONT_15: u'领先15分',
   TacticalSectionType.BEHIND_15: u'落后15分',                                           
}

class OffensiveTacticalType(ConstantBase):
    '''进攻战术'''
    STRONG_INSIDE = 1
    CENTER_COORDINATE = 2
    OUTSIDE_SHOT = 3
    FAST_ATTACK = 4
    OVERALL_CO_ORDINATION = 5
    COVER_SCREENS_FOR = 6

OffensiveTacticalTypeMap = {
    OffensiveTacticalType.STRONG_INSIDE: u'强打内线',
    OffensiveTacticalType.CENTER_COORDINATE: u'中锋策应',       
    OffensiveTacticalType.OUTSIDE_SHOT: u'外线投篮',       
    OffensiveTacticalType.FAST_ATTACK: u'快速进攻',       
    OffensiveTacticalType.OVERALL_CO_ORDINATION: u'整体配合',       
    OffensiveTacticalType.COVER_SCREENS_FOR: u'掩护挡拆',                           
}
    
class DefendTacticalType(ConstantBase):
    '''防守战术'''
    TWO_THREE_DEFENSE = 1
    THREE_TWO_DEFENSE = 2
    TWO_ONE_TWO_DEFENSE = 3
    MAN_MARKING_DEFENSE = 4
    MAN_MARKING_INSIDE = 5
    MAN_MARKING_OUTSIDE = 6
    
DefendTacticalTypeMap = {
    DefendTacticalType.TWO_THREE_DEFENSE: u'2-3联防',
    DefendTacticalType.THREE_TWO_DEFENSE: u'3-2联防',       
    DefendTacticalType.TWO_ONE_TWO_DEFENSE: u'2-1-2联防',       
    DefendTacticalType.MAN_MARKING_DEFENSE: u'盯人防守',       
    DefendTacticalType.MAN_MARKING_INSIDE: u'盯人内线',       
    DefendTacticalType.MAN_MARKING_OUTSIDE: u'盯人外线',                           
}

class MessageType(ConstantBase):
    SYSTEM_MSG = 1 #系统消息
    PLAYER_MSG = 2 #经理消息
    AUCTION_MSG = 3 #购买球员消息