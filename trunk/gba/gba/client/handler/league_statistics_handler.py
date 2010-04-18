#!/usr/bin/python
# -*- coding: utf-8 -*-

import time

from gba.client.betch.base import BaseBetchClient
from gba.common import log_execption, logger
from gba.common.constants import MatchTypes
from gba.common.single_process import SingleProcess
from gba.entity import League, LeagueMatchs, Matchs, LeagueTeams, \
                       LeagueConfig, MatchStat, ProfessionPlayer, ProPlayerCareerStatTotal, \
                       ProPlayerSeasonStatTotal
                       
from gba.common.constants.match import MatchStatus

class LeagueStatisticsHandler(BaseBetchClient):
    ''''''
    
    def __init__(self):
        super(LeagueStatisticsHandler, self).__init__()
        self._start_id = 0

    def _run(self):
        
        while True:
            league_matchs = self._get_league_match()
            if not league_matchs:
                self._start_id = 0
                self.append_log('now not tasks sleep 60s...')
                self._status = 'sleep'
                self.log()
                time.sleep(60)
                continue
                
            self._start_id = league_matchs[-1].id
            for league_match in league_matchs:
                self._handle_match(league_match)
                
    def _get_finish_league(self):
        return LeagueMatchs.query(condition='id>%s and status=1' % self._start_id, limit=100, order='id asc')
    
if __name__ == '__main__':  
    signle_process = SingleProcess('LeagueStatisticsHandler')
    signle_process.check()
    try:
        client = LeagueStatisticsHandler()
        client.start()
    except:
        log_execption()