#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.client.betch.base import BaseBetchClient
from gba.entity import League, LeagueTeams

'''每天比赛后的排名统计, 很简单, select 排序，再更新'''

class DailyLeagueRankUpdate(BaseBetchClient):
    
    def __init__(self):
        super(DailyLeagueRankUpdate, self).__init__()
        self._start_id = 0
        
    def _run(self):
        
        self.append_log('start daily league rank update')
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
                league_teams = self._get_league_teams(league.id)
                for i, league_team in enumerate(league_teams):
                    league_team.rank = i + 1
                LeagueTeams.inserts(league_teams)
                
            self.save_status()
            
        self.save_status()
        self.append_log('finish update league rank')
                    
    def _get_league(self):
        return League.query(condition="id>%s and status<>0" % self._start_id, order="id asc", limit=100)  
        
    def _get_league_teams(self, league_id):
        return LeagueTeams.query(condition="league_id=%s" % (league_id,), order="win desc, net_points desc, team_id desc ")
