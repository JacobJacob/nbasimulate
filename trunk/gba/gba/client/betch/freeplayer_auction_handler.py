#!/usr/bin/python
# -*- coding: utf-8 -*-

'''自由球员结算监控客户端'''

from gba.common import playerutil
from gba.common.single_process import SingleProcess
from gba.common import log_execption
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.entity import FreePlayer, Team, PlayerAuctionLog

def main():
    
    start_id = 0
    #把球员置为成交,或者不成交
    while True:
        players = FreePlayer.query(condition='id>%s and expired_time<=now()' % start_id, limit=10)
        if not players:
            break
        start_id = players[-1].id
        for player in players:
            if player.auction_status == 1 or player.auction_status == 2 : #已经成交过的，不处理
                continue
            handle_single_auction(player)
            
    #1.成交则把球员->买入经理->扣钱
    #2.从自由球员中删除
    start_id = 0
    while True:
        players = FreePlayer.query(condition='id>%s and delete_time<=now()' % start_id, limit=10)
        if not players:
            break
        start_id = players[-1].id
        for player in players:
            handle_has_auction(player)
            
def handle_has_auction(player):
    '''处理单个成交竟拍'''
    
    if player.auction_status == 1:
        team = Team.load(id=player.current_team_id)
        team.funds -= player.current_price
        profession_player = playerutil.copy_player(player, source='free_player', to='profession_player')
    
        player_auction_log = PlayerAuctionLog()
        player_auction_log.player_no = player.no
        player_auction_log.content = u'交易成功 [球员:%s(%s)被经理%s以%s的价格购买]' % (player.name, player.no, team.username, player.current_price)
        player_auction_log.type = 2 #1代表自由球员接易
    
    FreePlayer.transaction()
    try:
        if player.auction_status == 1:
            profession_player.persist()
            team.persist()
            player_auction_log.persist()
        player.delete()
        FreePlayer.commit()
    except:
        log_execption()
        FreePlayer.rollback()
    
def handle_single_auction(player):
    '''处理单个竟拍'''        
    FreePlayer.transaction()
    try:
        if player.current_team_id:
            player.auction_status = 1 #成交
        else:
            player.auction_status = 2 #流拍
        player.delete_time = ReserveLiteral('date_add(now(), interval 5 minute)')
        player.persist() 
        FreePlayer.commit()
    except:
        log_execption
        FreePlayer.rollback()
    
if __name__ == '__main__':
    signle_process = SingleProcess('freeplayer_auction_handler')
    signle_process.check()
    try:
        main()
    except:
        log_execption()