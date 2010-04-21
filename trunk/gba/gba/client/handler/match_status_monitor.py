#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
from gba.entity import Matchs
from gba.common.constants import MatchShowStatus
from gba.common import exception_mgr
from gba.common.single_process import SingleProcess
from gba.common import commonutil

class MatchStatusMonitor(object):
    '''比赛状态监控程序'''
    
    def __init__(self):
        pass
    
    def run(self):
        
        start_id = 0
        while True:
            matchs = self.get_match(start_id)
            if not matchs:
                start_id = 0
                time.sleep(2)
                continue
            
            start_id = matchs[-1].id
            for match in matchs:
                new_match, interval = commonutil.next_status(match)
                new_match.id = match.id
                new_match.persist()
                
    def get_match(self, start_id):
        return Matchs.query(condition='id>%s and show_status<%s and next_status_time<=now()' % (start_id, MatchShowStatus.FINISH), limit=100)
    
if __name__ == '__main__':
    signle_process = SingleProcess('MatchStatusMonitor')
    signle_process.check()
    try:
        monitor = MatchStatusMonitor()
        monitor.run()
    except:
        exception_mgr.on_except()