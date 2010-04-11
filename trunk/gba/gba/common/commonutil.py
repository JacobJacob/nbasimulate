#!/usr/bin/python
# -*- coding: utf-8 -*-


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