#!/usr/bin/python
# -*- coding: utf-8 -*-

'''年轻球员结算监控客户端'''

from datetime import datetime
import traceback

from gba.common import playerutil
from gba.common.single_process import SingleProcess
from gba.common import log_execption
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.entity import YouthFreePlayer, YouthFreeplayerAuctionLog, Team, PlayerAuctionLog, Message, UserInfo
from gba.common.client.base import BaseClient
from gba.common.constants import MessageType

class YouthFreePlayerAuctionHandler(BaseClient):
    
    def __init__(self):
        super(YouthFreePlayerAuctionHandler, self).__init__("YouthFreePlayerAuctionHandler")
    
    def run(self):
        
        start_id = 0
        #把球员置为成交,或者不成交
        while True:
            players = self.get_players(condition='id>%s and expired_time<=now()' % start_id, limit=10)
            if not players:
                break
            start_id = players[-1].id
            for player in players:
                if player.team_id or player.price == -1: #已经成交过的，不处理
                    continue
                self.handle_single_auction(player)
                
        #1.成交则把球员->买入经理->扣钱
        #2.从自由球员中删除
        start_id = 0
        while True:
            players = self.get_players(condition='id>%s and delete_time<=now()' % start_id, limit=10)
            if not players:
                break
            start_id = players[-1].id
            for player in players:
                self.handle_has_auction(player)
        
        self.current_info = "sleep 60%s"
        return 60
    
    def get_players(self, condition, limit=10):
        while True:
            try:
                return YouthFreePlayer.query(condition=condition, limit=10)
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
        
    def handle_has_auction(self, player):
        while True:
            try:
                self._handle_single_auction(player)
                break
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
       
    def _handle_has_auction(self, player):
        '''处理单个成交竟拍'''
        
        if player.team_id:
            team = Team.load(id=player.team_id)
            team.funds -= player.price
            youth_player = playerutil.copy_player(player)
        
            player_auction_log = PlayerAuctionLog()
            player_auction_log.player_no = player.no
            player_auction_log.content = u'交易成功 [球员:%s(%s)被经理%s以%s的价格购买]' % (player.name, player.no, team.username, player.price)
            player_auction_log.type = 1 #1代表年轻球员接易
        
        YouthFreePlayer.transaction()
        try:
            if player.team_id:
                youth_player.team_id = player.team_id
                youth_player.persist()
                team.persist()
                player_auction_log.persist()
                message = Message()
                message.type = MessageType.SYSTEM_MSG
                message.from_team_id = 0 #系统
                message.to_team_id = player.team_id
                message.is_new = 1
                user_info = UserInfo.load(username=team.username)
                nickname = user_info.nickname
                message.content = "[%s]" '%s经理,您以%s的价格购得球员%s,现已经到队' % (datetime.now(), nickname, player.price, player.name)
                message.persist()
            player.delete()
            YouthFreePlayer.commit()
        except:
            YouthFreePlayer.rollback()
            raise
        
    def handle_single_auction(self, player):
        while True:
            try:
                self._handle_single_auction(player)
                break
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
        
    def _handle_single_auction(self, player):
        '''处理单个竟拍'''
    
        auction_logs = YouthFreeplayerAuctionLog.query(condition='player_no="%s"' % player.no, order='price desc, id asc')
        if not auction_logs:#没人出价
            price = -1
            username = None
        else:
            price = auction_logs[0].price
            username = auction_logs[0].username
            
        YouthFreeplayerAuctionLog.transaction()
        try:
            if auction_logs:
                team = Team.load(username=username)
                player.team_id = team.id
                for auction_log in auction_logs:
                    auction_log.delete()
            player.price = price
            player.delete_time = ReserveLiteral('date_add(now(), interval 5 minute)')
            player.persist() 
            YouthFreeplayerAuctionLog.commit()
        except:
            YouthFreeplayerAuctionLog.rollback()
            raise
            
def main():
    signle_process = SingleProcess('youth_freeplayer_auction_handler')
    signle_process.check()
    try:
        client = YouthFreePlayerAuctionHandler()
        client.main()
    except:
        log_execption()
        
if __name__ == '__main__':
    main()