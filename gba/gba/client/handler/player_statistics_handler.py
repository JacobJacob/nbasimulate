#!/usr/bin/python
# -*- coding: utf-8 -*-

import random
import traceback

from gba.common.client.base import BaseClient
from gba.common import exception_mgr, logger
from gba.common.single_process import SingleProcess
from gba.entity import  LeagueMatchs, Matchs, LeagueTeams,  MatchStat, ProfessionPlayer, \
                        ProPlayerCareerStatTotal, ProPlayerSeasonStatTotal, ErrorMatch
from gba.common.constants.match import MatchStatus

class PlayerStatisticsHandler(BaseClient):
    '''职业球员生涯统计客户端'''
    
    def __init__(self):
        super(PlayerStatisticsHandler, self).__init__('PlayerStatisticsHandler')
        self._start_id = 0
        self._handle_total = 0

    def run(self):
        
        self.current_info = "start..."
        while True:
            league_matchs = self._get_league_match()
            if not league_matchs:
                self._start_id = 0
                self.current_info = 'now not tasks sleep 60s...[handle total: %s]' % self._handle_total
                self._sleep(30)
                continue
                
            self._start_id = league_matchs[-1].id
            self.current_info = "start handle"
            for league_match in league_matchs:
                self._handle_total += 1
                if self._handle_total % 10 == 0:
                    self.current_info = "handle total %s" % self._handle_total
                self.handle_match(league_match)
                
    def _get_league_match(self):
        while True:
            try:
                return LeagueMatchs.query(condition='id>%s and status=1' % self._start_id, limit=100, order='id asc')
            except KeyboardInterrupt:
                raise
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
            
    def get_point_from_str(self, point):
        '''从字符串的分数里提出两队的比分'''
        if not point:
            logger.log_to_db('出现比分是空值的现像')
            
        point = point[1:-1]
        split = point.split(':')
        home_point = int(split[0])
        guest_point = int(split[1])
        return home_point, guest_point
    
    def handle_match(self, league_match):
        while True:
            try:
                self._handle_match(league_match)
                break
            except KeyboardInterrupt:
                raise
            except:
                self.current_info = traceback.format_exc()
                exception_mgr.on_except()
            self._sleep()
    
    def _handle_match(self, league_match):
        '''处理比赛'''
        match = Matchs.load(id=league_match.match_id, status=MatchStatus.FINISH)
        if not match:
            return False
        
        #球队胜场统计
        match_team_home_id = league_match.match_team_home_id
        match_team_guest_id = league_match.match_team_guest_id
        
        match_team_home = LeagueTeams.load(id=match_team_home_id)
        match_team_guest = LeagueTeams.load(id=match_team_guest_id)
        
        win_team_id = 0 #赢球球队id
         
        home_point, guest_point = self.get_point_from_str(match.point)
        if home_point > guest_point:
            sub_point = home_point - guest_point
            match_team_home.win += 1
            match_team_home.net_points += sub_point
            match_team_guest.lose += 1
            match_team_guest.net_points -= sub_point
            win_team_id = match_team_home.team_id
        else:
            sub_point = guest_point - home_point
            match_team_home.lose += 1
            match_team_home.net_points -= sub_point
            match_team_guest.win += 1
            match_team_guest.net_points += sub_point
            win_team_id = match_team_guest.team_id
         
        #球队胜场统计end
        
        match_stats = MatchStat.query(condition='match_id=%s' % match.id)
        if not match_stats or len(match_stats) < 10:
            error_match = ErrorMatch()
            error_match.match_id = match.id
            error_match.type = match.type
            error_match.remark = '比赛无统计或统计条数小于10'
            while True:
                try:
                    error_match.persist()
                    break
                except:
                    exception_mgr.on_except()
                    self._sleep()
            logger.log_to_db('比赛无统计或统计条数小于10,比赛id[%s]' % match.id)
            
        for match_stat in match_stats:
            player = ProfessionPlayer.load(no=match_stat.player_no)
            #计算训练点
            if player.team_id == win_team_id:
                add_training_locations = random.randint(1000, 1500)
            else:
                add_training_locations = random.randint(800, 1300)
            player.training_locations += add_training_locations
            
            if not player:
                logger.log_to_db('球员找不到球员号码[%s]比赛id[%s]' % (match_stat.player_no, match.id))
                
            point_total = match_stat.point1_doom_times * 1 + match_stat.point2_doom_times * 2 + match_stat.point3_doom_times * 3
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
                
            player_season_total.assist_agv =  float(player_season_total.assist) / player_season_total.match_total
            player_season_total.block_agv = float(player_season_total.block) / player_season_total.match_total
            player_season_total.steals_agv = float(player_season_total.steals) / player_season_total.match_total
            player_season_total.point_agv = float(player_season_total.point_total) / player_season_total.match_total
            player_season_total.rebound_agv = float(player_season_total.rebound_total) / player_season_total.match_total
                
            league_match.status = 2 #状态2表示处理完了
            league_match.point = match.point
            ProPlayerCareerStatTotal.transaction()
            try:
                player_season_total.persist()
                player_caree_total.persist()
                player.persist()
                league_match.persist()
                match_team_home.persist()
                match_team_guest.persist()
                ProPlayerCareerStatTotal.commit()
            except:
                ProPlayerCareerStatTotal.rollback()
                raise
        
def main():  
    signle_process = SingleProcess('PlayerStatisticsHandler')
    signle_process.check()
    try:
        client = PlayerStatisticsHandler()
        client.main()
    except:
        exception_mgr.on_except()
        
if __name__ == '__main__':
    main()