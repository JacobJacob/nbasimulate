#!/usr/bin/python
# -*- coding: utf-8 -*-

'''初始化各联赛数据'''

import random
from gba.entity import League, LeagueTeams, LeagueMatchs

DEGREE_COUNT = 12 #十二个等级


VIS_MAP = [
   [[1, 14], [2, 13], [3, 12], [4, 11], [5, 10], [6, 9], [7, 8]],
   [[1, 13], [14, 12], [2, 11], [3, 10], [4, 9], [5, 8], [6, 7]],
   [[1, 12], [13, 11], [14, 10], [2, 9], [3, 8], [4, 7], [5, 6]],
   [[1, 11], [12, 10], [13, 9], [14, 8], [2, 7], [3, 6], [4, 5]],
   [[1, 10], [11, 9], [12, 8], [13, 7], [14, 6], [2, 5], [3, 4]],
   [[1, 9], [10, 8], [11, 7], [12, 6], [13, 5], [14, 4], [2, 3]],
   [[1, 8], [9, 7], [10, 6], [11, 5], [12, 4], [13, 3], [14, 2]],
   [[1, 7], [8, 6], [9, 5], [10, 4], [11, 3], [12, 2], [13, 14]],
   [[1, 6], [7, 5], [8, 4], [9, 3], [10, 2], [11, 14], [12, 13]],
   [[1, 5], [6, 4], [7, 3], [8, 2], [9, 14], [10, 13], [11, 12]],
   [[1, 4], [5, 3], [6, 2], [7, 14], [8, 13], [9, 12], [10, 11]],
   [[1, 3], [4, 2], [5, 14], [6, 13], [7, 12], [8, 11], [9, 10]],
   [[1, 2], [3, 14], [4, 13], [5, 12], [6, 11], [7, 10], [8, 9]],
 ]

def main():
    
    for i in range(DEGREE_COUNT):
        degree = i + 1
        no_count = 2**(degree-1)
        for j in range(no_count):
            league = League()
            league.degree = degree
            league.no = j + 1
            #league.team_count = 0
            league.persist()
            
            league_id = league.id
            
            #先按14个坑填好球队
            for j in range(14):
                league_team = LeagueTeams()
                league_team.league_id = league_id
                league_team.status = 0
                league_team.seq = j + 1
                league_team.persist()
                
            dis_league(league_id)
            
                
def dis_league(league_id):
    '''分配赛程'''
    #26轮
    league_teams = LeagueTeams.query(condition="league_id='%s'" % league_id, order='seq asc')
    if len(league_teams) != 14:
        raise 'team count error %s' % len(league_teams)
    
    league_matchs = []  
    for i, mm in enumerate(VIS_MAP):
        for m in mm:
            league_match = LeagueMatchs()
            part_league_match = LeagueMatchs() #下半赛季
            if random.randint(1, 2) == 1:
                match_team_home_id = league_teams[m[0]-1].id
                match_team_guest_id = league_teams[m[1]-1].id
            else:
                match_team_home_id = league_teams[m[1]-1].id
                match_team_guest_id = league_teams[m[0]-1].id
                
            league_match.match_team_home_id = match_team_home_id
            league_match.match_team_guest_id = match_team_guest_id
                
            part_league_match.match_team_home_id = match_team_guest_id
            part_league_match.match_team_guest_id = match_team_home_id
            
            league_match.round = i + 1
            part_league_match.round = i + 1 + 13
            league_match.league_id = league_id
            part_league_match.league_id = league_id
            
            league_matchs.append(league_match)
            league_matchs.append(part_league_match)
            #league_match.persist()
            #part_league_match.persist()

    LeagueMatchs.inserts(league_matchs)
    
if __name__ == '__main__':
    main()
    #dis_league(1)