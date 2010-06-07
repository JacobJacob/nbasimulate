#!/usr/bin/python
# -*- coding: utf-8 -*-

'''每轮更新'''

"""每轮更新客户端
"""



import time
from datetime import datetime

from gba.common.single_process import SingleProcess
from gba.common import exception_mgr
from gba.client.betch.daily_update import DailyUpdate
from gba.client.betch.daily_league_update import DailyLeagueUpdate
from gba.client.betch.daily_league_rank_update import DailyLeagueRankUpdate
from gba.entity import RoundUpdateLog, LeagueConfig, LeagueMatchs

class RoundUpdateClient(object):
    
    def __init__(self):
        self.__season = None
        self.__round = None
        
    def __before_run(self):
        '''开始更新之前始初化数据,如果返回True则执行后面的操作,反之则不'''
        config = LeagueConfig.load(id=1)
        self.__season = config.season
        self.__round = config.round
        if self.__round >= 27:
            return False
        return True
    
    def run(self):
        if self.__before_run():
            self.__player_update()
            self.__team_update()
            self.__challenge_update()
            self.__league_update()
            self.__before_finish()
            self.__finish()

    def __player_update(self):
        '''球员数据更新'''
        daily_update = DailyUpdate(self.__round)
        daily_update.start()
            
    def __team_update(self):
        '''球队数据更新'''
        pass
    
    def __challenge_update(self):
        '''胜者为王数据更新'''
        pass
        
    def __league_update(self):
        '''联赛更新'''
        daily_league_update = DailyLeagueUpdate(self.__season, self.__round)
        daily_league_update.start()
    
    def __before_finish(self):
        while True:
            match = LeagueMatchs.query(condition="round=%s and status=1 " % self.__round, limit=1)
            if not match:
                return
            time.sleep(60)
            print '.',
    
    def __finish(self):
        config = LeagueConfig()
        config.round = self.__round + 1
        config.persist()
      
def main():
    client = RoundUpdateClient()
    client.run()
           
def test():    
    for i in range(26):
        main()

if __name__ == '__main__':
    signle_process = SingleProcess('RoundUpdate')
    signle_process.check()
    try:
        test()
        #main()
    except:
        exception_mgr.on_except()
