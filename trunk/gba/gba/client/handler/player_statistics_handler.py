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

class PlayerStatisticsHandler(BaseBetchClient):
    '''职业球员生涯统计客户端'''
    
    def __init__(self):
        super(PlayerStatisticsHandler, self).__init__()
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
                
    def _get_league_match(self):
        return LeagueMatchs.query(condition='id>%s and status=1' % self._start_id, limit=100, order='id asc')
    
    
    def _handle_match(self, league_match):
        '''处理比赛'''
        match = Matchs.load(id=league_match.match_id, status=MatchStatus.FINISH)
        if not match:
            return False
        
        match_stats = MatchStat.query(condition='match_id=%s' % match.id)
        if not match_stats or len(match_stats) < 10:
            logger.log_to_db('比赛无统计或统计条数小于10,比赛id[%s]' % match.id)
            
        for match_stat in match_stats:
            player = ProfessionPlayer.load(no=match_stat.player_no)
            if not player:
                logger.log_to_db('球员找不到球员号码[%s]比赛id[%s]' % (match_stat.player_no, match.id))
                
            point_total = match_stat.point1_shoot_times * 1 + match_stat.point2_shoot_times * 2 + match_stat.point3_shoot_times * 3
            rebound_total =  match_stat.offensive_rebound + match_stat.defensive_rebound
            player_caree_total = ProPlayerCareerStatTotal.load(player_no=player.no)
            if not player_caree_total:
                player_caree_total = ProPlayerCareerStatTotal()
                player_caree_total.player_no = match_stat.player_no
                player_caree_total.point2_shoot_times = match_stat.point2_shoot_times
                player_caree_total.point2_doom_times = match_stat.point2_doom_times
                player_caree_total.point3_shoot_times = match_stat.point3_shoot_times
                player_caree_total.point3_doom_times = match_stat.point3_doom_times
                player_caree_total.point1_shoot_times = match_stat.point1_shoot_times
                player_caree_total.point1_doom_times = match_stat.point1_doom_times
                player_caree_total.offensive_rebound = match_stat.offensive_rebound
                player_caree_total.defensive_rebound = match_stat.defensive_rebound
                player_caree_total.foul = match_stat.foul
                player_caree_total.lapsus = match_stat.lapsus
                player_caree_total.assist = match_stat.assist
                player_caree_total.block = match_stat.block
                player_caree_total.steals = match_stat.steals
                player_caree_total.point_total = point_total
                player_caree_total.rebound_total = rebound_total
                player_caree_total.match_total = 1
                player_caree_total.main_total = 1 if match_stat.is_main else 0 #是否主力
            else:
                player_caree_total.point2_shoot_times += match_stat.point2_shoot_times
                player_caree_total.point2_doom_times += match_stat.point2_doom_times
                player_caree_total.point3_shoot_times += match_stat.point3_shoot_times
                player_caree_total.point3_doom_times += match_stat.point3_doom_times
                player_caree_total.point1_shoot_times += match_stat.point1_shoot_times
                player_caree_total.point1_doom_times += match_stat.point1_doom_times
                player_caree_total.offensive_rebound += match_stat.offensive_rebound
                player_caree_total.defensive_rebound += match_stat.defensive_rebound
                player_caree_total.foul += match_stat.foul
                player_caree_total.lapsus += match_stat.lapsus
                player_caree_total.assist += match_stat.assist
                player_caree_total.block += match_stat.block
                player_caree_total.steals += match_stat.steals
                player_caree_total.match_total += 1
                player_caree_total.point_total += point_total
                player_caree_total.rebound_total += rebound_total
                player_caree_total.main_total += 1 if match_stat.is_main else 0 #是否主力
                
            player_season_total = ProPlayerSeasonStatTotal.load(player_no=player.no)
            
            if not player_season_total:
                player_season_total = ProPlayerSeasonStatTotal()
                player_season_total.player_no = match_stat.player_no
                player_season_total.point2_shoot_times = match_stat.point2_shoot_times
                player_season_total.point2_doom_times = match_stat.point2_doom_times
                player_season_total.point3_shoot_times = match_stat.point3_shoot_times
                player_season_total.point3_doom_times = match_stat.point3_doom_times
                player_season_total.point1_shoot_times = match_stat.point1_shoot_times
                player_season_total.point1_doom_times = match_stat.point1_doom_times
                player_season_total.offensive_rebound = match_stat.offensive_rebound
                player_season_total.defensive_rebound = match_stat.defensive_rebound
                player_season_total.foul = match_stat.foul
                player_season_total.lapsus = match_stat.lapsus
                player_season_total.assist = match_stat.assist
                player_season_total.block = match_stat.block
                player_season_total.steals = match_stat.steals
                player_season_total.match_total = 1
                player_season_total.point_total = point_total
                player_season_total.rebound_total = rebound_total
                player_season_total.main_total = 1 if match_stat.is_main else 0 #是否主力
            else:
                player_season_total.point2_shoot_times += match_stat.point2_shoot_times
                player_season_total.point2_doom_times += match_stat.point2_doom_times
                player_season_total.point3_shoot_times += match_stat.point3_shoot_times
                player_season_total.point3_doom_times += match_stat.point3_doom_times
                player_season_total.point1_shoot_times += match_stat.point1_shoot_times
                player_season_total.point1_doom_times += match_stat.point1_doom_times
                player_season_total.offensive_rebound += match_stat.offensive_rebound
                player_season_total.defensive_rebound += match_stat.defensive_rebound
                player_season_total.foul += match_stat.foul
                player_season_total.lapsus += match_stat.lapsus
                player_season_total.assist += match_stat.assist
                player_season_total.block += match_stat.block
                player_season_total.steals += match_stat.steals
                player_season_total.point_total += point_total
                player_season_total.rebound_total += rebound_total
                player_season_total.match_total += 1
                player_season_total.main_total += 1 if match_stat.is_main else 0 #是否主力
                
            player_season_total.assist_agv =  player_season_total.assist / player_season_total.match_total
            player_season_total.block_agv = player_season_total.block / player_season_total.match_total
            player_season_total.steals_agv = player_season_total.steals / player_season_total.match_total
            player_season_total.point_agv = player_season_total.point_total / player_season_total.match_total
            player_season_total.rebount_agv = player_season_total.rebound_total / player_season_total.match_total
                
            league_match.status = 2 #状态2表示处理完了
            league_match.point = match.point
            ProPlayerCareerStatTotal.transaction()
            try:
                player_season_total.persist()
                player_caree_total.persist()
                league_match.persist()
                ProPlayerCareerStatTotal.commit()
            except:
                log_execption()
                ProPlayerCareerStatTotal.rollback()  
        
if __name__ == '__main__':  
    signle_process = SingleProcess('PlayerStatisticsHandler')
    signle_process.check()
    try:
        client = PlayerStatisticsHandler()
        client.start()
    except:
        log_execption()