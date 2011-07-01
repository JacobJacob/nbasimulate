#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

class InviteCodeStatus(ConstantBase):
    NEW = 1
    ASSIGNED = 2
    USED = 3
    
InviteCodeStatusMap = {
   InviteCodeStatus.NEW: u'新建',
   InviteCodeStatus.ASSIGNED: u'已分配',
   InviteCodeStatus.USED: u'已使用',               
}
