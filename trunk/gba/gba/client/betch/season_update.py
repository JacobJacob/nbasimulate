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

MIN_DEGREE = 12 #最小一级，则只升升无降


class SeasonUpdate(BaseBetchClient):
    ''''''
    
    def __init__(self):
        super(SeasonUpdate, self).__init__()
    
    def _run(self):
        success = True
        try:
            self.truncate_tables()
            self.update_tables()
            self._league_update()
        except:
            exception_mgr.on_except()
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
        #for degree in [11, 10 ,9, 8, 7 , 6, 5, 4, 3, 2, 1]:
        for degree in range(1, 12):
            for no in range(1, 2 ** (degree-1) + 1):
                self._single_league_update(degree, no)
                
    def _single_league_update(self, degree, no):
        '''单个联赛更新'''
        print 'start to handle degree:[%s] no[%s]' % (degree, no)
        update_league_teams = []
        update_teams = []
        update_leagues = []
        #11级联赛,只升不降
        if degree == 11:
            league = self._get_league(degree, no)
            league_teams = self._get_teams(league.id)
            pre_degree = degree - 1
            pre_no = (no + 1 ) // 2
            pre_league = self._get_league(pre_degree, pre_no)
            pre_league_teams = self._get_teams(pre_league.id)
            team_count = league.team_count
            if team_count == 0:
                return
            pre_team_count = pre_league.team_count
            #上一级联赛没人
            if pre_team_count == 0:
                if no % 2 == 1:
                    base = 0
                else:
                    base = 7
                for i in range(7):
                    league_team_a = league_teams[i]
                    league_team_b = pre_league_teams[base+1]
                    if league_team_a.team_id != -1:
                        team_a = self._get_team(league_team_a.team_id)
                        team_a.profession_league_evel = league_team_b.degree
                        team_a.profession_league_class = league_team_b.no
                        
                        league_team_b.team_id = league_team_a.id
                        pre_league.team_count += 1
                        league_team_a.team_id = -1
                        league.team_count -= 1
                        
                        update_league_teams.append(league_team_a)
                        update_league_teams.append(league_team_b)
                        update_teams.append(team_a)
                update_leagues.append(league)
                update_leagues.append(pre_league)
            elif pre_team_count < 14:
                pre_empty_count = 14 - pre_team_count #上一级空缺球队
                if no % 2 == 1:
                    up_team_count = pre_empty_count // 2
                else:
                    up_team_count = pre_empty_count - (pre_empty_count // 2)
 
                for i in range(up_team_count):
                    league_team_a = league_teams[i]
                    league_team_b = self._get_free_league_team(pre_league_teams)
                    if league_team_a.team_id != -1:
                        team_a = self._get_team(league_team_a.team_id)
                        team_a.profession_league_evel = league_team_b.degree
                        team_a.profession_league_class = league_team_b.no
                        
                        league_team_b.team_id = league_team_a.id
                        pre_league.team_count += 1
                        league_team_a.team_id = -1
                        league.team_count -= 1
                        
                        update_league_teams.append(league_team_a)
                        update_league_teams.append(league_team_b)
                        update_teams.append(team_a)
                        
                    if up_team_count < 2: #如果空的球队只有一个,不足两个名额,则上一级联赛需要降极
                        for i in range(2 - up_team_count):
                            league_team_a = league_teams[i]
                            league_team_b = self._get_demote_team(pre_league_teams)
                            
                            if league_team_a.team_id != -1:
                                team_a = self._get_team(league_team_a.team_id)
                                team_b = self._get_team(league_team_b.team_id)
                                
                                #升级
                                team_a.profession_league_evel = league_team_b.degree
                                team_a.profession_league_class = league_team_b.no
                                #降级
                                team_b.profession_league_evel = degree
                                team_b.profession_league_class = no
                        
                                league_team_b.team_id = team_a.id
                                league_team_a.team_id = team_b.id
                            
                            update_league_teams.append(league_team_a)
                            update_league_teams.append(league_team_b)
                            update_teams.append(team_a)
                            update_teams.append(team_b)
                update_leagues.append(league)
                update_leagues.append(pre_league)
            else:
                pass           
        elif degree == 1:
            pass
        else:
            pass
        
        #保存
        while True:
            Team.transaction()
            try:
                if update_league_teams:
                    LeagueTeams.inserts(update_league_teams)
                if update_teams:
                    Team.inserts(update_teams)
                if update_leagues:
                    League.inserts(update_leagues)
                Team.commit()
                break
            except:
                Team.rollback()
                exception_mgr.on_except()
            self.sleep()
            
    
    def __single_league_update_2_10(self, degree, no):
        '''2级到10级联赛的更新'''
        update_league_teams = []
        update_teams = []
        update_leagues = []
        league = self._get_league(degree, no)
        league_teams = self._get_teams(league.id)
        pre_degree = degree - 1
        pre_no = (no + 1 ) // 2
        pre_league = self._get_league(pre_degree, pre_no)
        pre_league_teams = self._get_teams(pre_league.id)
        team_count = league.team_count
        if team_count == 0:
            return
        pre_team_count = pre_league.team_count
        #上一级联赛没人
        if pre_team_count == 0:
            if no % 2 == 1:
                base = 0
            else:
                base = 7
            for i in range(7):
                league_team_a = league_teams[i]
                league_team_b = pre_league_teams[base+1]
                if league_team_a.team_id != -1:
                    team_a = self._get_team(league_team_a.team_id)
                    team_a.profession_league_evel = league_team_b.degree
                    team_a.profession_league_class = league_team_b.no
                   
                    league_team_b.team_id = league_team_a.id
                    pre_league.team_count += 1
                    league_team_a.team_id = -1
                    league.team_count -= 1
                   
                    update_league_teams.append(league_team_a)
                    update_league_teams.append(league_team_b)
                    update_teams.append(team_a)
            update_leagues.append(league)
            update_leagues.append(pre_league)
  
            update_leagues.append(league)
            update_leagues.append(pre_league)
        
        #执行降级操作
        for j in rrange(10, 14):
            league_team_a = league_teams[i]
            league_team_b = pre_league_teams[base+1]
    
    def _get_demote_team(self, pre_league_teams):
        '''从一个联赛中获取所有的降级球队'''
        i = 10
        while i < len(pre_league_teams):
            if pre_league_teams[i].upgrade == 0 and pre_league_teams[i].team_id != -1:
                return pre_league_teams[i]
            else:
                i += 1
    
    def _get_free_league_team(self, pre_league_teams):
        '''从一个联赛的所有队里获取一支空的球队'''
        i = 0
        while i < len(pre_league_teams):
            if pre_league_teams[i].team_id == -1:
                return pre_league_teams[i]
            else:
                i += 1

            
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
        
    def _get_league(self, degree, no):
        '''获取联赛
        '''
        while True:
            try:
                return League.load(degree=degree, no=no)
            except:
                exception_mgr.on_except()
            self.sleep()
                
    
if __name__ == '__main__':
    client = SeasonUpdate()
    client.start()