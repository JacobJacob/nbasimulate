#!/usr/bin/python
# -*- coding: utf-8 -*-

'''赛季更新'''

from gba.entity import LeagueConfig, League, Team, LeagueTeams
from gba.common import exception_mgr
from gba.client.betch.base import BaseBetchClient
from gba.common.db import connection


TRUNCATE_TABLES = ['matchs', 'match_nodosity_detail', 'match_nodosity_main', 'match_nodosity_tactical_detail',
                   'match_not_in_player', 'match_stat', 'message', 'season_finance', 'pro_player_season_stat_total',
                   'team_ticket_history']

UPDATE_SQLS = ['update league_matchs set status=0, point=null, match_id=null;',
               'update league_teams set win = 0, lose=0, net_points=0 ,status=0, rank=null;',
               ]

class SeasonUpdate(BaseBetchClient):
    ''''''
    
    def __init__(self):
        super(SeasonUpdate, self).__init__()
    
    def _run(self):
        success = True
        try:
            self.truncate_tables()
            self.update_tables()
        except:
            success = False
        if success:
            self._sys_update()
    
    def truncate_tables(self):
        '''赛季更新要清空的表'''
        for table in TRUNCATE_TABLES:
            cursor = connection.cursor()
            try:
                cursor.execute('truncate %s;' % table)
            finally:
                cursor.close()
    
    def update_tables(self):
        '''赛季更新一些表要重设值'''
        for sql in UPDATE_SQLS:
            cursor = connection.cursor()
            try:
                cursor.execute(sql)
            finally:
                cursor.close() 
               
    def _sys_update(self):
        '''赛季加1'''
        league_config = LeagueConfig.query(limit=1)[0]
        league_config.season += 1
        league_config.round = 1
        league_config.persist()

    def _league_update(self):
        '''联赛更新
        1.从高往低, 如果是一级联赛, 则只有降级
        2. 下面的联赛, 如果一上级联赛球队不足,则补 1/2 * 不足
        '''
        start_id = 0
        while True:
            leagues = self._get_league(start_id)
            if not leagues:
                return
            
            league = leagues[0]
            start_id = league.id
            league_teams = self._get_teams(league.id)
            
            next_leagues = self._get_next_level(league.degree, league.no)
            next_league_a = next_leagues[0]
            next_league_b = next_leagues[1]
            
            next_league_a_teams = self._get_teams(next_league_a.id)
            next_league_b_teams = self._get_teams(next_league_b.id)
            
            update_teams = []
            #13 14 名和下一级a的第一第二名换, 11 12 名和下一级b的第一第二名换
            team_a, team_b = self._change_team(league_teams[13], next_league_a_teams[0])
            update_teams.append(team_a)
            update_teams.append(team_b)
            
            team_a, team_b = self._change_team(league_teams[12], next_league_a_teams[1])
            update_teams.append(team_a)
            update_teams.append(team_b)
            
            team_a, team_b = self._change_team(league_teams[11], next_league_b_teams[0])
            update_teams.append(team_a)
            update_teams.append(team_b)
            
            team_a, team_b = self._change_team(league_teams[10], next_league_b_teams[1])
            update_teams.append(team_a)
            update_teams.append(team_b)
            
            next_league_a_teams[0].demote = 1
            next_league_a_teams[1].demote = 1
            next_league_b_teams[0].demote = 1
            next_league_b_teams[1].demote = 1
            
            update_league_teams = []
            update_league_teams.append(league_teams[13])
            update_league_teams.append(league_teams[12])
            update_league_teams.append(league_teams[11])
            update_league_teams.append(league_teams[10])
            update_league_teams.append(next_league_a_teams[0])
            update_league_teams.append(next_league_a_teams[1])
            update_league_teams.append(next_league_b_teams[0])
            update_league_teams.append(next_league_b_teams[1])
     
    def _get_team(self, team_id):
        '''获取球队'''
        while True:
            try:
                return Team.load(id=team_id)
            except:
                exception_mgr.on_except()
            self.sleep()
           
    def _change_team(self, league_team_a, league_team_b):
        '''两个球队交换位置'''

        team_a = self._get_team(league_team_a.team_id)
        team_b = self._get_team(league_team_b.team_id)
        team_a.profession_league_evel = league_team_b.degree
        team_a.profession_league_class = league_team_b.no
        team_b.profession_league_evel = league_team_a.degree
        team_b.profession_league_class = league_team_a.no
        
        temp_id = league_team_a.team_id
        league_team_a.team_id = league_team_b.team_id
        league_team_b.team_id = temp_id
        
        return team_a, team_b
            
    def _get_teams(self, league_id):
        '''获取一个联赛的所有球队'''
        while True:
            try:
                return LeagueTeams.query(condition='league_id=%s' % league_id, order='rank asc')
            except:
                exception_mgr.on_except()
            self.sleep()
    
    def _get_next_level(self, degree, no):
        '''获取下一级的两支球队'''
        while True:
            try:
                return League.query(condition='where degree=%s (and no=%s or no=%s)' % (degree-1, no*2-1, no*2), order='no asc')
            except:
                exception_mgr.on_except()
            self.sleep()
    
    def _get_ona_league_remain_team(self, degree, no):
        '''获取上一级联赛剩余球队'''
        
    
    def _update_league(self, league):
        pass
        
    def _get_league(self, start_id):
        '''获取联赛
        '''
        while True:
            try:
                return League.query(condition='where id>%s and status=2', order='degree asc, no asc', limit=1)
            except:
                exception_mgr.on_except()
            self.sleep()
                
    
if __name__ == '__main__':
    client = SeasonUpdate()
    client.start()