#!/usr/bin/python
# -*- coding: utf-8 -*-

'''自由球员结算监控客户端'''

import traceback
from datetime import datetime

from gba.common import playerutil
from gba.common.single_process import SingleProcess
from gba.common import exception_mgr
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.entity import FreePlayer, Team, PlayerAuctionLog, AttentionPlayer, UserInfo, \
                       AllFinance, SeasonFinance, Message, LeagueConfig, ProfessionPlayer
from gba.common.client.base import BaseClient
from gba.common.constants import MessageType, FinanceSubType, FinanceType, MarketStatus

class FreePlayerAuctionHandler(BaseClient):
    
    def __init__(self):
        super(FreePlayerAuctionHandler, self).__init__("FreePlayerAuctionHandler")
        self._success_total = 0
        self._failure_total = 0
        self._total = 0
    
    def run(self):
        
        start_id = 0
        #把球员置为成交,或者不成交
        while True:
            players = self.get_plsyers(condition='id>%s and auction_status=0 and expired_time<=now()' % start_id, limit=10)
            if not players:
                break
            start_id = players[-1].id
            for player in players:
                if player.auction_status == 1 or player.auction_status == 2 : #已经成交过的，不处理
                    continue
                self.handle_single_auction(player)
                
        #1.成交则把球员->买入经理->扣钱
        #2.从自由球员中删除
        start_id = 0
        while True:
            players = self.get_plsyers(condition='id>%s and delete_time<=now()' % start_id, limit=10)
            if not players:
                break
            start_id = players[-1].id
            for player in players:
                self.handle_has_auction(player)
        
        self.current_info = "sleep 60s, [total:%s][success:%s][failure:%s]" % (self._total, self._success_total, self._failure_total)
        return 60
    
    def get_plsyers(self, condition, limit=10):
        while True:
            try:
                return FreePlayer.query(condition=condition, limit=10)
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
                 
    def handle_has_auction(self, player):
        while True:
            try:
                self._handle_has_auction(player)
                break
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
           
    def _handle_has_auction(self, player):
        '''处理单个成交竟拍'''
        
        if player.auction_status == MarketStatus.FINISH:
            team = Team.load(id=player.current_team_id)
            team.funds -= player.current_price
            profession_player = playerutil.copy_player(player, source='free_player', to='profession_player')
            profession_player.team_id = team.id
            i = 0
            while i < 100:
                i += 1
                if not ProfessionPlayer.load(team_id=team.id, player_no=i):
                    profession_player.player_no = i
                    break   
                
            player_auction_log = PlayerAuctionLog()
            player_auction_log.player_no = player.no
            player_auction_log.content = u'交易成功 [球员:%s(%s)被经理%s以%s的价格购买]' % (player.name, player.no, team.username, player.current_price)
            player_auction_log.type = 2 #1代表自由球员接易
            
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
            tinance.outlay = player.current_price
    
            #赛季收入概要
            all_tinance = AllFinance.load(team_id=team.id, season=league_config.season)
            if not all_tinance:
                all_tinance = AllFinance()
                all_tinance.team_id = team.id
                all_tinance.season = league_config.season
                all_tinance.income = 0
                all_tinance.outlay = 0
            all_tinance.outlay += player.current_price
        
        attentions = AttentionPlayer.query(condition='no="%s"' % player.no)
        
        FreePlayer.transaction()
        try:
            if player.auction_status == MarketStatus.FINISH:
                profession_player.persist()
                team.persist()
                player_auction_log.persist()
                
                message = Message()
                message.type = MessageType.SYSTEM_MSG
                message.from_team_id = 0 #系统
                message.to_team_id = player.current_team_id
                message.is_new = 1
                user_info = UserInfo.load(username=team.username)
                nickname = user_info.nickname
                message.title = '[%s] 球员%s已经到队' % (datetime.now(), player.name)
                message.content = '[%s] %s经理,您以%s的价格购得球员%s,现已经到队' % (datetime.now(), nickname, player.current_price, player.name)
                message.persist()
                all_tinance.persist()
                tinance.persist()
            
            #关注球员，删除
            for attention in attentions:
                attention.delete()
            
            player.delete()
            FreePlayer.commit()
        except:
            FreePlayer.rollback()
            exception_mgr.on_except()
            self.current_info = traceback.format_exc()
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
        self._total += 1  
        FreePlayer.transaction()
        try:
            if player.current_team_id:
                player.auction_status =  MarketStatus.FINISH #成交
                self._success_total += 1
            else:
                player.auction_status = MarketStatus.FINISH_FAILURE #流拍
                self._failure_total += 1
            player.delete_time = ReserveLiteral('date_add(now(), interval 5 minute)')
            player.persist() 
            FreePlayer.commit()
        except:
            FreePlayer.rollback()
            exception_mgr.on_except()
            self.current_info = traceback.format_exc()
            raise
    
def main():
    signle_process = SingleProcess('freeplayer_auction_handler')
    signle_process.check()
    try:
        client = FreePlayerAuctionHandler()
        client.main()
    except:
        exception_mgr.on_except()
        
if __name__ == '__main__':
    main()