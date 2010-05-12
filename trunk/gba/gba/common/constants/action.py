#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from constant_base import ConstantBase

class ActionName(ConstantBase):
    """动作名称"""
    CATCH_SLAM_DUNK = 'CatchSlamDunk' #接球扣篮
    SHORT_SHOOT = 'ShortShoot' #中距离投篮
    LONG_SHOOT = 'LongShoot' #三分投篮
    
ActionNameMap = {
    ActionName.CATCH_SLAM_DUNK: u'接球扣篮',
    ActionName.SHORT_SHOOT: u'中距离投篮',
    ActionName.LONG_SHOOT: u'三分投篮',
}
