#!/usr/bin/python
# -*- coding: utf-8 -*-

'''年轻球员结算监控客户端'''

from gba.common import playerutil
from gba.common.single_process import SingleProcess
from gba.common import log_execption
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.entity import YouthFreePlayer, YouthFreeplayerAuctionLog, Team, PlayerAuctionLog

def main():
    
    start_id = 0
    #把球员置为成交,或者不成交
    while True:
        players = YouthFreePlayer.query(condition='id>%s and expired_time<=now()' % start_id, limit=10)
        if not players:
            break
        start_id = players[-1].id
        for player in players:
            if player.team_id: #已经成交过的，不处理
                continue
            handle_single_auction(player)
            
    #1.成交则把球员->买入经理->扣钱
    #2.从自由球员中删除
    start_id = 0
    while True:
        players = YouthFreePlayer.query(condition='id>%s and delete_time<=now()' % start_id, limit=10)
        if not players:
            break
        start_id = players[-1].id
        for player in players:
            handle_has_auction(player)
            
def handle_has_auction(player):
    '''处理单个成交竟拍'''
    
    team = Team.load(id=player.team_id)
    team.funds -= player.price
    youth_player = playerutil.copy_player(player)
    
    player_auction_log = PlayerAuctionLog()
    player_auction_log.player_no = player.no
    player_auction_log.content = u'交易成功 [球员:%s(%s)被经理%s以%s的价格购买]' % (player.name, player.no, team.username, player.price)
    player_auction_log.type = 1 #1代表年轻球员接易
    
    YouthFreePlayer.transaction()
    try:
        youth_player.persist()
        player.delete()
        team.persist()
        player_auction_log.persist()
        YouthFreePlayer.commit()
    except:
        log_execption()
        YouthFreePlayer.rollback()
    
def handle_single_auction(player):
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
        log_execption
        YouthFreeplayerAuctionLog.rollback()
    
if __name__ == '__main__':
    signle_process = SingleProcess('freeplayer_auction_handler')
    signle_process.check()
    try:
        main()
    except:
        log_execption()