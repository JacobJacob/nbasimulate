#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.client.betch.base import BaseBetchClient
from gba.common import log_execption
from gba.common.single_process import SingleProcess
from gba.common.constants import MatchTypes
from gba.entity import League, LeagueMatchs, Matchs, LeagueTeams, LeagueConfig

class DailyLeagueUpdate(BaseBetchClient):
    
    def __init__(self):
        super(DailyLeagueUpdate, self).__init__()
        self._start_id = 0
        config = LeagueConfig.load(id=1)
        if not config:
            raise 'config error'
        self._round = config.round
        self._total_created_match = 0
        
    def _run(self):
        
        self.append_log('start daily league update')
        self.load_status()
        if self.get_status('start_id'):
            self._start_id = self.get_status('start_id')
            
        while True:
            leagues = self._get_league()
            if not leagues:
                self.set_status('start_id', 0)
                self.save_status()
                break
            
            self._start_id = leagues[-1].id
            self.set_status('start_id', self._start_id)
            for league in leagues:
                league_matchs = self._get_league_matchs(league.id)
                Matchs.transaction()
                try:
                    for league_match in league_matchs:
                        if league_match.status > 0:
                            log_execption('出现比赛已经打完的情况!!')
                            continue
                        match_team_home_id = league_match.match_team_home_id
                        match_team_guest_id = league_match.match_team_guest_id
                        
                        home_team = LeagueTeams.load(id=match_team_home_id)
                        guest_team = LeagueTeams.load(id=match_team_guest_id)
                        
                        if not (home_team and guest_team):
                            log_execption('出现找不到球队的情况情况!!')
                            continue
                        
                        match = Matchs()
                        match.type = MatchTypes.LEAGUE
                        match.home_team_id = home_team.team_id
                        match.guest_team_id = guest_team.team_id
                        match.status = 1
                        match.persist()
                        
                        league_match.match_id = match.id
                        league_match.status = 1 #比赛进行中
                        league_match.persist()
                        self._total_created_match += 1
                    Matchs.commit()
                except:
                    log_execption()
                    Matchs.rollback()
            self.save_status()
            
        self.save_status()
        self.append_log('created %s matchs' % self._total_created_match)
                    
    def _get_league(self):
        return League.query(condition="id>%s and status<>0" % self._start_id, order="id asc", limit=100)  
        
    def _get_league_matchs(self, league_id):
        return LeagueMatchs.query(condition="league_id=%s and round=%s" % (league_id, self._round))
    
if __name__ == '__main__':  
    signle_process = SingleProcess('DailyLeagueUpdate')
    signle_process.check()
    try:
        client = DailyLeagueUpdate()
        client.start()
    except:
        log_execption()