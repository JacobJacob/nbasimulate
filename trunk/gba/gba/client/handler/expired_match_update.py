#!/usr/bin/python
# -*- coding: utf-8 -*-

'''超时比赛更新客户端'''

import traceback

from gba.common.client.base import BaseClient
from gba.entity import Matchs, MatchStat, MatchNodosityMain, MatchNodosityDetail, MatchNodosityTacticalDetail
from gba.common.db.reserve_convertor import ReserveLiteral

class ExpiredMatchUpdate(BaseClient):
    
    def __init__(self):
        super(ExpiredMatchUpdate, self).__init__(self.__class__.__name__)
        self._start_id = 0
        self._update_count = 0
        self._failure_count = 0
        
    def run(self):
    
        self.current_info = 'expire match update client start'
        expired_matchs = self.get_expired_match()
        if not expired_matchs:
            self._start_id = 0
            self.current_info = 'not expired matchs now, sleep, update total[%s] failure[%s]' % (self._update_count, self._failure_count)
            return 60
        
        self._start_id = expired_matchs[-1].id
        for expired_match in expired_matchs:
            self._update_count += 1
            if not self.handle(expired_match):
                self._failure_count += 1
        
        self.current_info = 'total update[%s]failure[%s]' % (self._update_count, self._failure_count)
    
    def handle(self, match):
        '''如果比赛出了异常。就要重赛
        如果已经开打，把所有信息删除，比赛状态改为1
        '''
        match_stats = MatchStat.query(condition='match_id="%s"' % match.id)
        match_nodosity_mains = MatchNodosityMain.query(condition='match_id="%s"' % match.id, order='seq asc')
        match_nodosity_details = MatchNodosityDetail.query(condition='match_id="%s"' % match.id)
        
        match_nodosity_tactical_details =[]
        if match_nodosity_mains:
            for match_nodosity_main in match_nodosity_mains:
                details = MatchNodosityTacticalDetail.query(condition='match_nodosity_main_id="%s"' % match_nodosity_main.id)
                match_nodosity_tactical_details += details
                
        match.status = 1
        match.expired_time = ReserveLiteral('date_add(now(), interval 60 minute)')
        
        Matchs.transaction()
        try:
            if match_stats:
                for match_stat in match_stats:
                    match_stat.delete()
            if match_nodosity_mains:
                for match_nodosity_main in match_nodosity_mains:
                    match_nodosity_main.delete()
            if match_nodosity_details:
                for match_nodosity_detail in match_nodosity_details:
                    match_nodosity_detail.delete()
            if match_nodosity_tactical_details:
                for match_nodosity_tactical_detail in match_nodosity_tactical_details:
                    match_nodosity_tactical_detail.delete()
                    
            match.persist()
            Matchs.commit()
        except:
            self.current_info = '%s' % traceback.format_exc()
            Matchs.rollback()
            return False
    
        return True
            
    def get_expired_match(self):
        while True:
            try:
                return Matchs.query(condition='status<>3 and expired_time<now() and id>%s' % self._start_id, order=' id asc ', limit=10)
            except KeyboardInterrupt:
                raise
            except:
                self.current_info = traceback.format_exc()
            self._sleep()
def main():
    spider = ExpiredMatchUpdate()
    spider.main()
     
if __name__ == '__main__':
    main()
            
