#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.client.betch.base import BaseBetchClient
from gba.entity import Team, ProfessionPlayer

class TeamAgvAbilityHandler(BaseBetchClient):
    '''每日更新球队平均综合'''
    
    def __init__(self):
        super(TeamAgvAbilityHandler, self).__init__()
        
    def _run(self):
        
        start_id = 0
        #选求平均综合
        while True:
            teams = self.get_teams(start_id)
            if not teams:
                break
            start_id = teams[-1].id
            for team in teams:
                self.update_team(team)
                
            Team.inserts(teams)
        
        i = 1
        #再计算排名
        teams = self.get_teams(order="agv_ability desc ")

        for team in teams:
            team.agv_ability_rank = i
            i += 1
            
        Team.inserts(teams)
        
    def update_team(self, team):
        
        pro_players = ProfessionPlayer.query(condition='team_id="%s"' % team.id)
        total = 0
        count = 0
        for pro_player in pro_players:
            total += pro_player.ability
            count += 1
            
        agv_ability = float(total)/count
        team.agv_ability = agv_ability
        return team
        
    def get_teams(self, start_id=None, order=None):
        if order:
            if start_id:
                return Team.query(condition='id>%s' % start_id, limit=100, order=order)
            else:
                return Team.query(order=order)
        else:
            if start_id:
                return Team.query(condition='id>%s' % start_id, limit=100)
            else:
                return Team.query()
            
if __name__ == '__main__':
    client = TeamAgvAbilityHandler()
    client.start()