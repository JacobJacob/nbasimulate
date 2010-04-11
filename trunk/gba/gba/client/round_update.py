#!/usr/bin/python
# -*- coding: utf-8 -*-

'''每轮更新'''

import time
from datetime import datetime

from gba.common.single_process import SingleProcess
from gba.common import exception_mgr
from gba.client.betch.daily_update import DailyUpdate
from gba.client.betch.daily_league_update import DailyLeagueUpdate
from gba.client.betch.daily_league_rank_update import DailyLeagueRankUpdate
from gba.entity import RoundUpdateLog, LeagueConfig, LeagueMatchs

def main():
    
    config = LeagueConfig.load(id=1)
    
    log = RoundUpdateLog()
    log.season = config.season
    log.round = config.round
    if log.round >= 27:
        return
    log.start_time = datetime.now()
    log.persist()
    
    daily_league_update = DailyLeagueUpdate(config.season, config.round)
    daily_league_update.start()
    
    while True:
        match = LeagueMatchs.query(condition="round=%s and status=1 " % config.round, limit=1)
        if not match:
            break
        time.sleep(60)
        print '.',
       
    log.end_time = datetime.now() 
    log.log = 'finish'
    log.persist()
    
    config.round += 1
    config.persist()
    
    daily_league_rank_update = DailyLeagueRankUpdate()
    daily_league_rank_update.start()
    
    daily_update = DailyUpdate()
    daily_update.start()
    
def test():
    
    for i in range(17):
        main()

if __name__ == '__main__':
    signle_process = SingleProcess('RoundUpdate')
    signle_process.check()
    try:
        test()
        #main()
    except:
        exception_mgr.on_except()
