#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

class MarketCategory(ConstantBase):
    """市场类型"""
    STREET_FREE = 6
    STREET_SELECTION = 4
    PROFESSION_INTEAM = 1  #职业球员
    PROFESSION_TRANSFER = 2  #职业转会
    PROFESSION_SELECTION = 3 #职业选秀
    PROFESSION_ULTIMATE = 4  #极限球员
    

ProfessionMarketCategoryMap = {
    MarketCategory.PROFESSION_INTEAM: u"职业球员" , 
    MarketCategory.PROFESSION_TRANSFER: u"职业转会" ,          
    MarketCategory.PROFESSION_SELECTION: u"职业选秀" ,
    MarketCategory.PROFESSION_ULTIMATE: u"极限球员" ,                 
}