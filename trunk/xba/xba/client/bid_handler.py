#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import bid_manager, club_manager
from xba.common.decorators import ensure_success
from base import BaseClient
from xba.common.constants.market import MarketCategory

class BidHandler(BaseClient):
    
    
    CLIENT_NAME = "bid_handler"
    
    def __init__(self):
        super(BidHandler, self).__init__(BidHandler.CLIENT_NAME)
    
    def work(self):
        
        for category in (MarketCategory.STREET_FREE, MarketCategory.STREET_SELECTION, \
                         MarketCategory.PROFESSION_TRANSFER, MarketCategory.PROFESSION_SELECTION):
            self.log("start do bid handle for category:%s" % category)
            self.__work(category)   
        
        self.sleep(30) 
        
    def __work(self, category):
        
        #设定竞拍结果
        if category == MarketCategory.PROFESSION_TRANSFER:
            self.log("start to set auto bid!!")
            self.set_auto_bid()
        infos = self.get_end_bid(category)
        if not infos:
            self.log("not record time over for category:%s" % category)
        for info in infos:
            player_id = info['PlayerID']
            self.log("start to handler player with id:%s" % player_id)
            self.prepare_bid(player_id, category)
            
        infos = self.get_end_bid_for_end(category)
        if not infos:
            self.log("not record end for category:%s" % category)
        for info in infos:
            player_id = info['PlayerID']
            bidder_id = info["BidderID"]
            self.log("start to finish player with id:%s, bidder id:%s" % (player_id, bidder_id))
            number = 0
            if bidder_id > 0:
                club_id = self.get_club_by_user_id(bidder_id)
                if not club_id:
                    self.log("club with user id:%s not exist" % bidder_id)
                    continue
                
                number = self.get_club_player_number(club_id, category)
            self.finish_bid(player_id, number, category)
            
    @ensure_success
    def set_auto_bid(self):
        """自动出价"""
        return bid_manager.set_auto_bid()
        
    @ensure_success
    def get_end_bid(self, category):
        """获取截止时间到的拍卖"""
        return bid_manager.get_end_bid(category)
 
    @ensure_success
    def prepare_bid(self, player_id, category):
        """设定中标者"""
        return bid_manager.prepare_bid(player_id, category)
              
    @ensure_success
    def get_end_bid_for_end(self, category):
        """获取过了截止时间半个钟的拍卖"""
        return bid_manager.get_end_bid_for_end(category)
    
    @ensure_success
    def get_club_player_number(self, club_id, category):
        """分配一个球员号码"""
        if category in (MarketCategory.STREET_FREE, MarketCategory.STREET_SELECTION):
            type = 3
        else:
            type = 5
        return bid_manager.get_club_player_number(club_id, type)
    
    @ensure_success
    def finish_bid(self, player_id, number, category):
        """完成竞拍，球员归队"""
        return bid_manager.finish_bid(player_id, number, category)
    
    @ensure_success
    def get_club_by_user_id(self, bidder_id):
        """获取用户的俱乐部ID"""
        return club_manager.get_club_by_user_id(bidder_id)
    
if __name__ == "__main__":
    handler = BidHandler()
    handler.start()
