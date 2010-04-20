#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
from gba.entity import Matchs
from gba.common.constants import MatchShowStatus
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.common.constants.match import MatchStatus
from gba.common import exception_mgr
from gba.common.single_process import SingleProcess

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
            new_matchs = []
            for match in matchs:
                interval = 12 * 60 / 5
                new_match = Matchs() #重新生成一个，以访有脏数据
                new_match.show_status = match.next_show_status
                show_status = match.show_status  #以当前显示的状态
                if show_status >= MatchShowStatus.READY and show_status < MatchShowStatus.FOURTH:
                    new_match.next_show_status = match.next_show_status + 1
                elif show_status == MatchShowStatus.FOURTH:
                    if match.overtime > 0:
                        new_match.next_show_status = MatchShowStatus.OVERTIME_ONE
                    else:
                        new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.OVERTIME_ONE: #第一加时
                    if match.overtime > 1:
                        new_match.next_show_status = MatchShowStatus.OVERTIME_TWO
                    else:
                        new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.OVERTIME_TWO:#第二加时
                    if match.overtime > 2:
                        new_match.next_show_status = MatchShowStatus.OVERTIME_THREE
                    else:
                        new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.OVERTIME_THREE:#第三加时
                    if match.overtime > 3:
                        new_match.next_show_status = MatchShowStatus.OVERTIME_FOUR
                    else:
                        new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.OVERTIME_FOUR:#第四加时
                    if match.overtime > 4:
                        new_match.next_show_status = MatchShowStatus.OVERTIME_FIVE
                    else:
                        new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.OVERTIME_FIVE:#第五加时
                    if match.overtime > 5:
                        new_match.next_show_status = MatchShowStatus.OVERTIME_SIX
                    else:
                        new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.OVERTIME_SIX:
                    new_match.next_show_status = MatchShowStatus.STATISTICS
                elif show_status == MatchShowStatus.STATISTICS:
                    interval = 1 * 60 / 5
                    if match.status == MatchStatus.FINISH:
                        new_match.next_show_status = MatchShowStatus.FINISH
                    else:
                        new_match.next_show_status = show_status #如果真正的比赛还没打完则等待
                else:
                    pass
                        
                if MatchShowStatus.OVERTIME_ONE <= show_status and show_status <= MatchShowStatus.OVERTIME_SIX:
                    interval =  5 * 60 / 5
                print interval
                new_match.next_status_time = ReserveLiteral('date_add(now(), interval %s second)' % interval)
                
                new_match.id = match.id
                new_matchs.append(new_match)
                
            Matchs.inserts(new_matchs)
                
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