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
    LEAGUE = 5 #职业联赛
    CHALLENGE = 6 #胜者为王
    
MatchTypeMaps = {
    MatchTypes.FRIENDLY: u'职业友谊 ',  
    MatchTypes.TRAINING: u'职业训练 ',
    MatchTypes.YOUTH_FRIENDLY: u'街头友谊 ',
    MatchTypes.YOUTH_TRAINING: u'街头训练 ',
    MatchTypes.LEAGUE: u'职业联赛',             
}

class FinanceType(ConstantBase):
    INCOME = 1 #收入
    OUTLAY = 2 #支出
    
class FinanceSubType(ConstantBase):
    TICKETS = 11 #球票收入
    AD = 12 #广告收入
    SELL_PLAYER = 13 #出售球员
    PLAYER_WAGE = 21 #球员工资支出
    STAFF_WAGE = 22 #职员工资支出
    ARENA_BUILD = 23 #球场建设支出
    BID_PLAYER = 24 #购买球员
    
#比赛显示的状态
class MatchShowStatus(ConstantBase):
    READY = 1 #赛前准备中
    FIRST = 2 #第一节
    SECOND = 3 #第二节
    THIRD = 4 #第三节
    FOURTH = 5 #第四节
    OVERTIME_ONE = 6 #第一加时
    OVERTIME_TWO = 7 #第二加时
    OVERTIME_THREE = 8 #第三加时
    OVERTIME_FOUR = 9 #第四加时
    OVERTIME_FIVE = 10 #第五加时
    OVERTIME_SIX = 11 #第六加时
    STATISTICS = 12 #统计中
    FINISH = 13 #完了
    CANCEL = 14 #比赛取消
    WAITING = 15 #等待中