#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from constant_base import ConstantBase

class UnionWarStatus(ConstantBase):
    """联盟战争状态"""
    SENDED = 1
    RECIVED = 2 
    DEFEN_SUCCESS = 3
    DEFEN_FAILURE = 4
    
UnionWarStatusMap = {
    UnionWarStatus.SENDED: u'尚未应战',
    UnionWarStatus.RECIVED: u'已经应战',
    UnionWarStatus.DEFEN_SUCCESS: u'防守成功',                     
    UnionWarStatus.DEFEN_FAILURE: u'防守失败',                 
}

class UnionPrestigeFromType(ConstantBase):
    """联盟威望来源"""
    ChallengeMatch = 1
    UnionWar = 2
    
UnionPrestigeFromTypeMap = {
    UnionPrestigeFromType.ChallengeMatch: u'胜者为王',
    UnionPrestigeFromType.UnionWar: u'联盟战争',                 
}