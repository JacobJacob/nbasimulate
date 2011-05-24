#!/usr/bin/python
# -*- coding: utf-8 -*-

from constant_base import ConstantBase

class MarketCategory(ConstantBase):
    """市场类型"""
    STREET_INTEAM = 1 #街球球员
    STREET_FREE = 6 #街球自由
    STREET_SELECTION = 4  #职业选秀(街球)
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

StreeMarketCategoryMap = {
    MarketCategory.STREET_INTEAM: u"街球球员" , 
    MarketCategory.STREET_FREE: u"街球自由" ,          
    MarketCategory.STREET_SELECTION: u"街球选秀" ,                 
}