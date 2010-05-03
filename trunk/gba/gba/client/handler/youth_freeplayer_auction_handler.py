#!/usr/bin/python
# -*- coding: utf-8 -*-

'''年轻球员结算监控客户端'''

from datetime import datetime
import traceback

from gba.common import playerutil, exception_mgr
from gba.common.single_process import SingleProcess
from gba.common import log_execption
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.entity import YouthFreePlayer, AttentionPlayer, YouthFreeplayerAuctionLog, \
                       Team, PlayerAuctionLog, Message, UserInfo, LeagueConfig, SeasonFinance, \
                       AllFinance
from gba.common.client.base import BaseClient
from gba.common.constants import MessageType, MarketStatus, FinanceSubType, FinanceType

class YouthFreePlayerAuctionHandler(BaseClient):
    
    def __init__(self):
        super(YouthFreePlayerAuctionHandler, self).__init__("YouthFreePlayerAuctionHandler")
        self._success_total = 0
        self._failure_total = 0
        self._total = 0
        
    def run(self):
        
        start_id = 0
        #把球员置为成交,或者不成交
        while True:
            players = self.get_players(condition='id>%s and status=0 and expired_time<=now()' % start_id, limit=10)
            if not players:
                break
            start_id = players[-1].id
            for player in players:
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
        
        self.current_info = "sleep 60s, [total:%s][success:%s][failure:%s]" % (self._total, self._success_total, self._failure_total)
        return 60
    
    def get_players(self, condition, limit=10):
        while True:
            try:
                return YouthFreePlayer.query(condition=condition, limit=10, ordery='id asc')
            except:
                self.current_info = traceback.format_exc()
                exception_mgr.on_except()
            self._sleep()
        
    def handle_has_auction(self, player):
        while True:
            try:
                self._handle_has_auction(player)
                break
            except:
                self.current_info = traceback.format_exc()
                exception_mgr.on_except()
            self._sleep()
       
    def _handle_has_auction(self, player):
        '''处理单个成交竟拍'''
        
        auction_logs = YouthFreeplayerAuctionLog.query(condition='player_no="%s"' % player.no, order='price desc, id asc')
        attention_players = AttentionPlayer.query(condition='no="%s"' % player.no)
        
        if player.status == MarketStatus.FINISH:
            team = Team.load(username=auction_logs[0].username)
            price = auction_logs[0].price
            player.team_id = team.id
            team.funds -= price
            youth_player = playerutil.copy_player(player)
            
            player_auction_log = PlayerAuctionLog()
            player_auction_log.player_no = player.no
            player_auction_log.content = u'交易成功 [球员:%s(%s)被经理%s以%s的价格购买]' % (player.name, player.no, team.username, player.price)
            player_auction_log.type = 1 #1代表年轻球员接易
            
            league_config = LeagueConfig.load(id=1)
            #赛季支出明细
            tinance = SeasonFinance()
            tinance.sub_type = FinanceSubType.BID_PLAYER
            tinance.team_id = team.id
            tinance.type = FinanceType.OUTLAY
            tinance.season = league_config.season
            tinance.round = league_config.round
            tinance.info = u'购买街头球员%s' % player.name
            tinance.income = 0
            tinance.outlay = price
    
            #赛季收入概要
            all_tinance = AllFinance.load(team_id=team.id, season=league_config.season)
            if not all_tinance:
                all_tinance = AllFinance()
                all_tinance.team_id = team.id
                all_tinance.season = league_config.season
                all_tinance.income = 0
                all_tinance.outlay = 0
            all_tinance.outlay += price
        
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
                all_tinance.persist()
                tinance.persist()
            player.delete()
            if auction_logs:
                for auction_log in auction_logs:
                    auction_log.delete()
            if attention_players:
                for attention_player in attention_players:
                    attention_player.delete()
            
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
                exception_mgr.on_except()
            self._sleep()
        
    def _handle_single_auction(self, player):
        '''处理单个竟拍'''
    
        auction_logs = YouthFreeplayerAuctionLog.query(condition='player_no="%s"' % player.no, order='price desc, id asc')
        self._total += 1
        if not auction_logs:#没人出价
            status = MarketStatus.FINISH_FAILURE
            self._failure_total += 1
        else:
            status = MarketStatus.FINISH
            self._success_total += 1
            team = Team.load(username=auction_logs[0].username)
            player.team_id = team.id
            player.price = auction_logs[0].price
        
        YouthFreeplayerAuctionLog.transaction()
        try:
            player.status = status
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