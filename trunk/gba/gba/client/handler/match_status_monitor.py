#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
import traceback

from gba.entity import Matchs, ChallengeHistory
from gba.common.constants import MatchShowStatus, MatchTypes
from gba.common import exception_mgr
from gba.common.single_process import SingleProcess
from gba.common import commonutil
from gba.common.client.base import BaseClient

class MatchStatusMonitor(BaseClient):
    '''比赛状态监控程序'''
    
    def __init__(self):
        super(MatchStatusMonitor, self).__init__('MatchStatusMonitor')
    
    def run(self):
        
        start_id = 0
        while True:
            matchs = self.get_match(start_id)
            if not matchs:
                start_id = 0
                self.current_info = "not matchs for handler now sleep 10s"
                return 10
                            
            start_id = matchs[-1].id
            for match in matchs:
                new_match, interval = commonutil.next_status(match)
                new_match.id = match.id
                new_match.persist()
                if new_match.show_status == MatchShowStatus.FINISH and match.type == MatchTypes.CHALLENGE:
                    challenge_history = ChallengeHistory.load(match_id=match.id)
                    if challenge_history:
                        challenge_history.finish = 1
                        challenge_history.point = match.point
                        challenge_history.persist()
                        
    def get_match(self, start_id):
        while True:
            try:
                return Matchs.query(condition='id>%s and show_status<%s and next_status_time<=now()' % (start_id, MatchShowStatus.FINISH), limit=100)
            except KeyboardInterrupt:
                raise
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
            
def main():
    signle_process = SingleProcess('MatchStatusMonitor')
    signle_process.check()
    try:
        monitor = MatchStatusMonitor()
        monitor.main()
    except:
        exception_mgr.on_except()
        
if __name__ == '__main__':
    main()