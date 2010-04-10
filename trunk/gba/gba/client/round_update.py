#!/usr/bin/python
# -*- coding: utf-8 -*-

'''每轮更新'''

import time
from datetime import datetime

from gba.common.single_process import SingleProcess
from gba.common import exception_mgr
from gba.client.betch.daily_update import DailyUpdate
from gba.client.betch.daily_league_update import DailyLeagueUpdate
from gba.entity import RoundUpdateLog, LeagueConfig, LeagueMatchs

def main():
    
    config = LeagueConfig.load(id=1)
    
    log = RoundUpdateLog()
    log.season = config.season
    log.round = config.round
    log.start_time = datetime.now()
    log.persist()
    
    daily_update = DailyUpdate()
    #daily_update.start()
    daily_league_update = DailyLeagueUpdate(log.round)
    daily_league_update.start()
    
    print 'update'
    while True:
        match = LeagueMatchs.query(condition="round=%s and status<>2" % config.round, limit=1)
        if not match:
            break
        time.sleep(60)
        print '.',
       
    log.end_time = datetime.now() 
    log.log = 'finish'
    log.persist()
    
if __name__ == '__main__':
    signle_process = SingleProcess('RoundUpdate')
    signle_process.check()
    try:
        main()
    except:
        exception_mgr.on_except()
