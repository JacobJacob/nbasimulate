#!/usr/bin/python
# -*- coding: utf-8 -*-

import traceback
import time
import os

from xba.business import dev_match_manager


def log(msg):
    print msg

class RoundUpdateHandler(object):
    

    MATCH_ENGINE_EXE_PATH = "D:\\develop\\xba_workspace\\src\\Client\\Client\\bin\\Debug\\Client.exe"
    
    def __init__(self):
        self._round = 1
    
    
    def run(self):
        self.dev_match_update()
    
    
    def dev_match_update(self):
        """比赛更新"""
        dev_match_infos = self.get_dev_match()
        for dev_match_info in dev_match_infos:
            match_id = dev_match_info["DevMatchID"]
            club_home_score = dev_match_info["ClubHScore"]
            club_away_score = dev_match_info["ClubAScore"]
            if club_home_score > 0 or club_away_score > 0:
                log("比赛已经完成")
                continue
            
            command = "%s %s %s" % (RoundUpdateHandler.MATCH_ENGINE_EXE_PATH, "dev_match_handler", match_id)
            log("开始执行命令:%s" % command)
            
            os.system(command)
            
    def dev_match_update_after(self):
        """比赛打完后的更新"""
        dev_match_infos = self.get_dev_match()
        for dev_match_info in dev_match_infos:
            match_id = dev_match_info["DevMatchID"]
            club_home_score = dev_match_info["ClubHScore"]
            club_away_score = dev_match_info["ClubAScore"]
            if club_home_score == 0 and club_away_score == 0:
                log("比赛还没完成")
                continue

    def get_dev_match(self):
        while True:
            try:
                return dev_match_manager.get_round_dev_matchs(0, self._round)
            except:
                print traceback.format_exc()
            time.sleep(10)    
            
            
            
if __name__ == "__main__":
    handler = RoundUpdateHandler()
    handler.run()
