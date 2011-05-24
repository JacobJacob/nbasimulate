#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import bid_manager, club_manager
from xba.common.decorators import ensure_success
from base import BaseClient

class BidHandler(BaseClient):
    
    
    CLIENT_NAME = "bid_handler"
    
    def __init__(self):
        super(BidHandler, self).__init__(BidHandler.CLIENT_NAME)
        
        
    def work(self):
        
        #设定竞拍结果
        infos = self.get_end_bid_open()
        for info in infos:
            player_id = info['PlayerID']
            self.log("start to handler player with id:%s" % player_id)
            self.prepare_bid_open(player_id)
            
        infos = self.get_end_bid_open_for_end()
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
                
                number = self.get_club_player5_number(club_id)
            self.finish_bid_open(player_id, number)
                
        self.sleep()
        
    @ensure_success()
    def get_end_bid_open(self):
        """获取截止时间到的拍卖"""
        return bid_manager.get_end_bid_open()
 
    @ensure_success()
    def prepare_bid_open(self, player_id):
        """设定中标者"""
        return bid_manager.prepare_bid_open(player_id)
              
    @ensure_success()
    def get_end_bid_open_for_end(self):
        """获取过了截止时间半个钟的拍卖"""
        return bid_manager.get_end_bid_open_for_end()
    
    @ensure_success()
    def get_club_player5_number(self, club_id):
        """分配一个球员号码"""
        return bid_manager.get_club_player5_number(club_id)
    
    @ensure_success()
    def finish_bid_open(self, player_id, number):
        """完成竞拍，球员归队"""
        return bid_manager.finish_bid_open(player_id, number)
    
    @ensure_success()
    def get_club_by_user_id(self, bidder_id):
        """获取用户的俱乐部ID"""
        return club_manager.get_club_by_user_id(bidder_id)
    
    
if __name__ == "__main__":
    handler = BidHandler()
    handler.start()
