#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.client.betch.base import BaseBetchClient
from gba.common import exception_mgr
from gba.business import user_operator
from gba.common.constants import MatchTypes, FinanceType, FinanceSubType
from gba.common import logger, teamutil
from gba.entity import League, LeagueMatchs, Matchs, LeagueTeams, SeasonFinance, TeamArena, Team, AllFinance

class DailyLeagueUpdate(BaseBetchClient):
    
    def __init__(self, season, round):
        super(DailyLeagueUpdate, self).__init__()
        self._start_id = 0
        self._season = season
        self._round = round
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
                        tinances = []
                        all_tinances = []
                        teams = []
                        if league_match.status > 0:
                            exception_mgr.on_except('出现比赛已经打完的情况!!')
                            continue
                        match_team_home_id = league_match.match_team_home_id
                        match_team_guest_id = league_match.match_team_guest_id
                        
                        home_league_team = LeagueTeams.load(id=match_team_home_id)
                        guest_league_team = LeagueTeams.load(id=match_team_guest_id)
                        
                        if not (home_league_team and guest_league_team):
                            exception_mgr.on_except('出现找不到球队的情况情况!!')
                            continue
                        
                        has_match = False
                        #这种当成是轮空
                        if home_league_team.team_id == -1 or guest_league_team.team_id == -1:
                            league_match.status = 3 #比赛轮空                        
                        else: 
                            has_match = True
                            match = Matchs()
                            match.type = MatchTypes.LEAGUE
                            match.home_team_id = home_league_team.team_id
                            match.guest_team_id = guest_league_team.team_id
                            match.status = 1
                            match.persist()
                            league_match.status = 1 #比赛进行中
                            
    
                        #不管打不打,球队都要给工资
                        if home_league_team.team_id != -1:
                            home_team = Team.load(id=home_league_team.team_id)
                            tinance = SeasonFinance()
                            tinance.sub_type = FinanceSubType.PLAYER_WAGE
                            tinance.team_id = home_league_team.team_id 
                            tinance.type = FinanceType.OUTLAY
                            tinance.income = 0
                            tinance.season = self._season
                            tinance.round = self._round
                            tinance.info = u'支付球员工资'
                            amount = user_operator.calcul_team_wave_total(home_league_team.team_id)
                            if amount == 0:
                                logger.log_to_db('球队总工资为0[球队id:%s]' % home_league_team.team_id)
                            tinance.outlay = amount
                            home_team.funds -= amount
                            tinances.append(tinance)
                            
                            #赛季开支概要
                            home_all_tinance = AllFinance.load(team_id=home_team.id, season=self._season)
                            if not home_all_tinance:
                                home_all_tinance = AllFinance()
                                home_all_tinance.team_id = home_team.id
                                home_all_tinance.season = self._season
                                home_all_tinance.income = 0
                                home_all_tinance.outlay = 0
                            
                            home_all_tinance.outlay += amount
                           
                            #如果比赛有打,则主队有一笔是门票收入
                            if has_match:
                                tickets_tinance = SeasonFinance()
                                tickets_tinance.sub_type = FinanceSubType.TICKETS
                                tickets_tinance.team_id = home_league_team.team_id 
                                tickets_tinance.type = FinanceType.INCOME
                                
                                team_arena = TeamArena.load(team_id=home_league_team.team_id)
                                if not team_arena:
                                    logger.log_to_db(u'球队%s球场信息不存在' % home_league_team.team_id)
                                    
                                fare = team_arena.fare
                                seat_count = team_arena.level * 1000 #座位数 
                                rank = home_league_team.rank if home_league_team.rank else 7
                                
                                attendance = teamutil.calcul_attendance(fare, team_arena.level, team_arena.fan_count, rank)
                                
                                amount = seat_count * attendance * fare
                                tickets_tinance.outlay = 0
                                tickets_tinance.income = amount
                                tickets_tinance.season = self._season
                                tickets_tinance.round = self._round
                                tickets_tinance.info = u'门票收入'
                                tinances.append(tickets_tinance)
                                home_team.funds += amount
                                home_all_tinance.income += amount
                                
                            teams.append(home_team)
                            all_tinances.append(home_all_tinance)
                            
                        if guest_league_team.team_id != -1:
                            guest_team = Team.load(id=guest_league_team.team_id)
                            tinance = SeasonFinance()
                            tinance.sub_type = FinanceSubType.PLAYER_WAGE
                            tinance.team_id = guest_league_team.team_id 
                            tinance.type = FinanceType.OUTLAY
                            amount = user_operator.calcul_team_wave_total(guest_league_team.team_id)
                            if amount == 0:
                                logger.log_to_db('球队总工资为0[球队id:%s]' % guest_league_team.team_id)
                            tinance.outlay = amount
                            tinance.income = 0
                            tinances.append(tinance)
                            guest_team.funds -= amount
                            tinance.season = self._season
                            tinance.round = self._round
                            tinance.info = u'支付球员工资'
                            
                            teams.append(guest_team)
                            
                            #赛季开支概要
                            guest_all_tinance = AllFinance.load(team_id=guest_team.id, season=self._season)
                            if not guest_all_tinance:
                                guest_all_tinance = AllFinance()
                                guest_all_tinance.team_id = guest_team.id
                                guest_all_tinance.season = self._season
                                guest_all_tinance.income = 0
                                guest_all_tinance.outlay = 0
                            
                            guest_all_tinance.outlay += amount
                            all_tinances.append(guest_all_tinance)
                        
                        if has_match:   
                            league_match.match_id = match.id
                        league_match.persist()
                        if teams:
                            Team.inserts(teams)
                        if tinances:
                            SeasonFinance.inserts(tinances)
                        if all_tinances:
                            AllFinance.inserts(all_tinances)
                            
                        self._total_created_match += 1
                    Matchs.commit()
                except:
                    exception_mgr.on_except()
                    Matchs.rollback()
            self.save_status()
            
        self.save_status()
        self.append_log('created %s matchs' % self._total_created_match)
                    
    def _get_league(self):
        return League.query(condition="id>%s and status<>0" % self._start_id, order="id asc", limit=100)  
        
    def _get_league_matchs(self, league_id):
        return LeagueMatchs.query(condition="league_id=%s and round=%s" % (league_id, self._round))