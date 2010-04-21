#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.entity import Matchs
from gba.common.constants import MatchShowStatus, MatchStatus, MatchTypes
from gba.common.db.reserve_convertor import ReserveLiteral

def get_point_from_str(point):
    '''从字符串的分数里提出两队的比分'''
    if not point:
        return 0, 0
    point = point[1:-1]
    split = point.split(':')
    home_point = int(split[0])
    guest_point = int(split[1])
    return home_point, guest_point


def change_point_to_score_card(point):
    '''将比分显示成比分牌'''
    if not point:
        return {'a1': None, 'a2': 0, 'a3': 0, 'b1': 0, 'b2': 0, 'b3': None}
    point = point[1:-1]
    split = point.split(':')
    home_point = int(split[0])
    guest_point = int(split[1])
    a1 = home_point / 100
    home_point -= a1 * 100
    a2 = home_point / 10
    home_point -= a2 * 10
    a3 = home_point / 1 
    
    b1 = guest_point / 100
    guest_point -= b1 * 100
    b2 = guest_point / 10
    guest_point -= b2 * 10
    b3 = guest_point / 1
    
    if b1 == 0:
        b1 = b2
        b2 = b3
        b3 = 'a'
    
    if a1 == 0:
        a1 = 'a'
    
    data = {'a1': a1, 'a2': a2, 'a3': a3, 'b1': b1, 'b2': b2, 'b3': b3}
    return data

def next_status(match):
    '''将比赛切换到下一状态'''
    if match.type == MatchTypes.CHALLENGE:
        interval = 60
    else:
        interval = 12 * 60 / 5
    new_match = Matchs() #重新生成一个，以访有脏数据
    new_match.show_status = match.next_show_status
    show_status = match.show_status  #以当前显示的状态
    if show_status >= MatchShowStatus.READY and show_status < MatchShowStatus.FOURTH:
        new_match.next_show_status = show_status + 1
    elif show_status == MatchShowStatus.FOURTH:
        if match.overtime > 0:
            new_match.next_show_status = MatchShowStatus.OVERTIME_ONE
        else:
            new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.OVERTIME_ONE: #第一加时
        if match.overtime > 1:
            new_match.next_show_status = MatchShowStatus.OVERTIME_TWO
        else:
            new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.OVERTIME_TWO:#第二加时
        if match.overtime > 2:
            new_match.next_show_status = MatchShowStatus.OVERTIME_THREE
        else:
            new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.OVERTIME_THREE:#第三加时
        if match.overtime > 3:
            new_match.next_show_status = MatchShowStatus.OVERTIME_FOUR
        else:
            new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.OVERTIME_FOUR:#第四加时
        if match.overtime > 4:
            new_match.next_show_status = MatchShowStatus.OVERTIME_FIVE
        else:
            new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.OVERTIME_FIVE:#第五加时
        if match.overtime > 5:
            new_match.next_show_status = MatchShowStatus.OVERTIME_SIX
        else:
            new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.OVERTIME_SIX:
        new_match.next_show_status = MatchShowStatus.STATISTICS
    elif show_status == MatchShowStatus.STATISTICS:
        interval = 1 * 60 / 5
        if match.status == MatchStatus.FINISH:
            new_match.next_show_status = MatchShowStatus.FINISH
        else:
            new_match.next_show_status = show_status #如果真正的比赛还没打完则等待
    else:
        pass
            
    if MatchShowStatus.OVERTIME_ONE <= show_status and show_status <= MatchShowStatus.OVERTIME_SIX:
        if match.type == MatchTypes.CHALLENGE:
            interval = 30
        else:
            interval =  5 * 60 / 5
    new_match.next_status_time = ReserveLiteral('date_add(now(), interval %s second)' % interval)
                