#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common import get_logging

logging = get_logging()

def get_round(club_count):
    """根据参赛球队数量获取比赛轮数"""
    round = 0
    while club_count > 1:
        club_count = club_count >> 1
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
    first_empty_club = club_count - 2 ** round
    need_append_zero = capacity - len(club_ids)
    print "first empty club:%s" % first_empty_club

    all_teams = []
    j = 0
    for i in range(capacity):
        if i % 2 == 0:
            all_teams.append(club_ids[j])
            j += 1
        else:
            if need_append_zero > 0:
                all_teams.append(-1)
                need_append_zero -= 1
            else:
                all_teams.append(club_ids[j])
                j += 1
                
    
                
    for i, team in enumerate(all_teams):
        if team != -1:
            print "team", i, get_base_code(round, i) 
        else:
            print "pass", i, get_base_code(round, i)    
            pass
        
    return 
    
    
    
if __name__ == "__main__":
    club_ids = [i for i in range(128)]
    create_cupLadder(32, club_ids)
    club_ids = [i for i in range(32)]
    create_cupLadder(32, club_ids)
    club_ids = [i for i in range(17)]
    create_cupLadder(32, club_ids)
    