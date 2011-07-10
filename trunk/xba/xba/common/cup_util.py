#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common import get_logging

logging = get_logging()

def get_round(club_count):
    """根据参赛球队数量获取比赛轮数"""
    round = 0
    start = 1
    while start < club_count:
        start = start << 1
        round += 1
    return round

def get_base_code(round, i):
    """获取base code"""
    base  = ""
    while i > 0:
        m = i % 2
        i = i / 2
        base = "%s%s" % (m, base) 
            
    if len(base) < round + 1:
        base = "%s%s" % ("0" * (round - len(base) + 1), base)
    return base

def create_cupLadder(capacity, club_ids):
    """安排赛程"""
    club_count = len(club_ids)
    print "start to create cup ladder"
    print "capacity %s, club count:%s" % (capacity, len(club_ids))
    
    round = get_round(club_count)
    print "round:%s" % round
    first_empty_club =  2 ** round - club_count
    print "first empty club:%s" % first_empty_club

    all_teams = []
    j = 0
    total_team = first_empty_club + club_count
    for i in range(total_team):
        if i % 2 == 0:
            all_teams.append(club_ids[j])
            j += 1
        else:
            if first_empty_club > 0:
                all_teams.append(-1)
                first_empty_club -= 1
            else:
                all_teams.append(club_ids[j])
                j += 1
                
    result_map = {}
                
    for i, team in enumerate(all_teams):
        if team != -1:
            code = get_base_code(round, i) 
        else:
            code = get_base_code(round, i)    
        
        #print code, team
        result_map[code]  = team
        
    return round, result_map
    
    
    
if __name__ == "__main__":
    club_ids = [i for i in range(128)]
    create_cupLadder(128, club_ids)
    club_ids = [i for i in range(32)]
    create_cupLadder(32, club_ids)
    club_ids = [i for i in range(15)]
    create_cupLadder(32, club_ids)
#    print get_round(15)
#    print get_round(16)
#    print get_round(17)    