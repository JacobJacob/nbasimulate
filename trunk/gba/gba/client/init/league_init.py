#!/usr/bin/python
# -*- coding: utf-8 -*-

'''初始化各联赛数据'''

from gba.entity import League, LeagueTeams, LeagueMatchs

DEGREE_COUNT = 12 #十二个等级

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
    
    round_info = {}
    for i in range(13):
        round_info[i+1] =  []
    
    team_count = 14
    round_count = 13
    
    for i in range(team_count):
        league_team = league_teams[i]
        for j in range(round_count):
            if league_team.id in round_info[j+1]:
                continue
            success = False
            for k in range(team_count):
                vist_league_team = league_teams[k]
                #取出的球队这轮有比赛了,或者取到自己
                if vist_league_team.id in round_info[j+1] or vist_league_team.id == league_team.id:
                    continue
                else:
                    success = True
                    break
            
            if not success:
                break
            
            league_match = LeagueMatchs()
            part_league_match = LeagueMatchs() #下半赛季
            if j % 2 == 0:
                league_match.match_team_home_id = league_team.id
                league_match.match_team_guest_id = vist_league_team.id
                
                part_league_match.match_team_home_id = vist_league_team.id
                part_league_match.match_team_guest_id = league_team.id
            else:
                league_match.match_team_home_id = vist_league_team.id
                league_match.match_team_guest_id = league_team.id
                
                part_league_match.match_team_home_id = league_team.id
                part_league_match.match_team_guest_id = vist_league_team.id
            
            #这两个队这轮都比过了
            round_info[j+1].append(league_team.id)
            round_info[j+1].append(vist_league_team.id)
            league_match.round = j + 1
            part_league_match.round = j + 1 + 13
            league_match.league_id = league_id
            part_league_match.league_id = league_id
            
            league_match.persist()
            part_league_match.persist()
       
if __name__ == '__main__':
    main()
    #dis_league(1)