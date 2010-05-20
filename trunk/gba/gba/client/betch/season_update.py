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
    '''赛季更新客户端'''
    
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

    def __league_update_before(self):
        '''在进行联赛更新之前，将一些状态置为初始状态'''
        while True:
            cursor = connection.cursor()
            try:
                cursor.execute('update league set update_finish=0')
                cursor.execute('update league set status=1 where team_id >0')
                cursor.execute('update league_teams set degrade=0, upgrade=0')
                break
            except:
                exception_mgr.on_except()
                self.sleep()

    def _league_update(self):
        '''联赛更新
        1.从高往低, 如果是一级联赛, 则只有降级
        2. 下面的联赛, 如果一上级联赛球队不足,则补 1/2 * 不足
        '''
        for degree in range(1, 12):
            for no in range(1, 2 ** (degree-1) + 1):
                self._single_league_update(degree, no)
                
    def _single_league_update(self, degree, no):
        '''单个联赛更新'''
        print 'start to handle degree:[%s] no[%s]' % (degree, no)
        if degree == 11:
            self.__single_league_update_11(degree, no)        
        else:
            self.__single_league_update_1_10(degree, no)
        
    def __single_league_update_11(self, degree, no):
        '''11级联赛的更新'''
        pass  
                
    def __single_league_update_1_10(self, degree, no):
        '''1级到10级联赛的更新'''
        league = self._get_league(degree, no)
        if league.team_count >=10:#
            #
            self.degrade(degree, no)
        else:
            next_league_a = self._get_league(degree+1, no*2-1)
            next_league_b = self._get_league(degree+1, no*2)
            #下级联赛也没人
            if next_league_a.team_count == 0 and next_league_b.team_count == 0:
                return
            else:
                #执行降更多球队降级的操作
                self.degrade_more(degree, no)   
        
        #更新当前联赛,下级联赛两支球队的球队数
        self.__update_league_team_count(degree, no)
        self.__update_league_team_count(degree+1, no*2-1)
        self.__update_league_team_count(degree+1, no*2)
        
    def degrade_more(self, degree, no):
        '''对质一个联赛执行降更多球队的操作'''
        #先获取所有空缺球队
        update_league_teams = []
        update_teams = []
        league = self._get_league(degree, no)
        empty_league_teams = self._get_empty_teams(league.id)
        index_a = 0
        index_b = 0
        next_league_a = self._get_league(degree+1, no*2-1)
        next_league_a_teams = self._get_teams(next_league_a.id)
        next_league_b = self._get_league(degree+1, no*2)
        next_league_b_teams = self._get_teams(next_league_b.id)
        for index, degrade_league_team in enumerate(empty_league_teams):
            if index % 2 == 0:
                next_league_team = next_league_a_teams[index_a]
                index_a += 1
            else:
                next_league_team = next_league_b_teams[index_b]
                index_b += 1
            index += 1
            if next_league_team.team_id == -1:#如果下面是空，上面也是空，就没必要上来
                continue
            else:
                upgrade_team = self._get_team(next_league_team.team_id)
                degrade_league_team.team_id, next_league_team.team_id = next_league_team.team_id, degrade_league_team.team_id
                degrade_league_team.degrade = 1
                next_league_team.upgrade = 1
                league.team_count += 1
                update_league_teams.append(degrade_league_team)
                update_league_teams.append(next_league_team)
                upgrade_team.profession_league_evel = degree
                upgrade_team.profession_league_class = no
                update_teams.append(upgrade_team)
        
        #更新
        self.update_all(update_league_teams, update_teams)
        
    def degrade(self, degree, no):
        '''对一个联赛实行普通降级操作'''
        update_league_teams = []
        update_teams = []
        league = self._get_league(degree, no)
        league_teams = self._get_teams(league.id)
        for index in range(10, 14):
            degrade_league_team = league_teams[index]
            degrade_team = None
            upgrade_team = None
            if degrade_league_team.team_id != -1:
                degrade_team = self._get_team(degrade_league_team.team_id)
            upgrade_league_team = self.get_upgrade_team(degree, no, index)
            if upgrade_league_team.team_id == -1:
                continue #如果都没队升上来,就不用降下去了
            else:
                upgrade_team = self._get_team(upgrade_league_team.team_id)
                #互换
                degrade_league_team.team_id, upgrade_league_team.team_id = upgrade_league_team.team_id, upgrade_league_team.team_id
                upgrade_league_team.upgrade = 1
                degrade_league_team.degrade = 1
                update_league_teams.append(degrade_league_team)
                update_league_teams.append(upgrade_league_team)
                if degrade_team:
                    degrade_team.profession_league_evel = upgrade_team.profession_league_evel
                    degrade_team.profession_league_class = upgrade_team.profession_league_class
                    update_teams.append(degrade_team)
                upgrade_team.profession_league_evel = degree
                upgrade_team.profession_league_class = no
                update_teams.append(upgrade_team)
            
            #降了一支空队，升了一支实队

        #更新   
        self.update_all(update_league_teams, update_teams)
            
    def __update_league_team_count(self, degree, no):
        '''更新联赛球队数'''
        while True:
            try:
                league = self._get_league(degree, no)
                league_team_count = LeagueTeams.count(condition='league_id=%s' % league.id)
                league.team_count = league_team_count
                league.persist()
            except:
                exception_mgr.on_except()
                self.sleep()
    
    def update_all(self, update_league_teams, update_teams):
        '''更新联赛，联赛球队， 实际球队'''
        while True:
            Team.transaction()
            try:
                if update_league_teams:
                    LeagueTeams.inserts(update_league_teams)
                if update_teams:
                    Team.inserts(update_teams)
                Team.commit()
                break
            except:
                Team.rollback()
                exception_mgr.on_except()
                self.sleep()
    
    def get_upgrade_team(self, degree, no, index):
        '''获取下一级联赛要升级的那支球队
        @param index: 10 -> 下级A队第二名; 11 -> 下级B队第二名;  12 -> 下级A队第一名; 13 -> 下级B第一名
        '''
        if index in (10, 12):
            next_league = self._get_league(degree+1, no*2-1)
            next_league_teams = self._get_teams(next_league.id)
            if index == 10:
                return next_league_teams[1]
            else:
                return next_league_teams[0]
        elif index in (11, 13):
            next_league = self._get_league(degree+1, no*2-1)
            next_league_teams = self._get_teams(next_league.id)
            if index == 11:
                return next_league_teams[1]
            else:
                return next_league_teams[0]
        else:
            raise 'error'
          
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
                return LeagueTeams.query(condition='league_id=%s' % league_id, order='status des, win_count desc, net_point desc')
            except:
                exception_mgr.on_except()
            self.sleep()
    
    def _get_empty_teams(self, league_id):
        '''获取一个联赛的所有空缺球队'''
        while True:
            try:
                return LeagueTeams.query(condition='league_id=%s and team_id=-1' % league_id)
            except:
                exception_mgr.on_except()
            self.sleep()
    
    def _get_next_level(self, degree, no):
        '''获取下一级的两支个联赛'''
        while True:
            try:
                return League.query(condition='where degree=%s (and no=%s or no=%s)' % (degree+1, no*2-1, no*2), order='no asc')
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
                league = League.load(degree=degree, no=no)
                if league:
                    return league
                else:
                    print degree, no
                    self.sleep()
            except:
                exception_mgr.on_except()
            self.sleep()
                
    
if __name__ == '__main__':
    client = SeasonUpdate()
    client.start()