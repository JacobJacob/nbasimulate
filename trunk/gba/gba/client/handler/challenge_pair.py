#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
from gba.entity import ChallengePool, ChallengeHistory, Matchs
from gba.common.constants import MatchStatus , MatchTypes, MatchShowStatus
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common import exception_mgr
from gba.common.single_process import SingleProcess

class ChallengePair(object):
    '''胜者为王配对客户端'''
    
    def run(self):
        
        while True:
            challenge_pools = self.get_challenge_pool()
            if not challenge_pools:
                time.sleep(5)
            size = len(challenge_pools)
            paired = []
            for i in range(size):
                challenge_pool = challenge_pools[i]
                for j in range(size):
                    if i == j:
                        continue
                    if j in paired:
                        continue
                    if self.check_can_pair(challenge_pool.team_id, challenge_pools[j].team_id):#可以配对
                        self.pair(challenge_pool, challenge_pools[j])
                        paired.append(i)
                        paired.append(j)
                        
    def pair(self, challenge_pool_home, challenge_pool_guest):
        """配对"""
        challenge_pool_home.status = 2 #比赛中
        challenge_pool_guest.status = 2
        
        match = Matchs()
        match.home_team_id = challenge_pool_home.team_id
        match.guest_team_id = challenge_pool_guest.team_id
        match.status = MatchStatus.ACCP
        match.show_status = MatchShowStatus.READY
        match.next_show_status = MatchShowStatus.FIRST
        match.next_status_time = ReserveLiteral('date_add(now(), interval 60 second)')
        match.is_youth = 0
        match.type = MatchTypes.CHALLENGE
        
        challenge_history = ChallengeHistory()
        challenge_history.home_team_id = challenge_pool_home.team_id
        challenge_history.guest_team_id = challenge_pool_guest.team_id
        
        Matchs.transaction()
        try:
            match.persist()
            challenge_pool_home.match_id = match.id
            challenge_pool_guest.match_id = match.id
            challenge_pool_home.persist()
            challenge_pool_guest.persist()
            challenge_history.persist()
            Matchs.commit()
        except:
            Matchs.rollback()
            exception_mgr.on_except()
            raise
            
    def get_challenge_pool(self):
        '''从池中获取球队'''
        return ChallengePool.query(condition='status="1"', order="ability asc")

    def check_can_pair(self, home_team_id, guest_team_id):
        '''判断下能不能配对,二个小时以上没打过则可以配对'''
        condition = '(home_team_id="%s" and guest_team_id="%s") or (home_team_id="%s" and guest_team_id="%s") \
                     and date_add(created_time, interval 120 minute)<now()' % (home_team_id, guest_team_id, guest_team_id, home_team_id)
        if ChallengeHistory.query(condition=condition):
            return False
        return True
    
if __name__ == '__main__':
    signle_process = SingleProcess('ChallengePair')
    signle_process.check()
    try:
        client = ChallengePair()
        client.run()
    except:
        exception_mgr.on_except()