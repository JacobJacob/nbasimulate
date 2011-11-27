#!/usr/bin/python
# -*- coding: utf-8 -*-


import sys

from xba.config import CLIENT_EXE_PATH
from xba.business import game_manager, star_match_manager
from xba.common.decorators import ensure_success
from base import BaseClient

class StarMatchHandler(BaseClient):
    
    
    CLIENT_NAME = "star_match_handler"
    
    
    def __init__(self, cmd):
        super(StarMatchHandler, self).__init__(StarMatchHandler.CLIENT_NAME)
        self._season = -1
        self._days = -1
        self._cmd = cmd
        
    def before_run(self):
        """运行前的初始化"""
        self.log("before run")
        game_info = self.get_game_info()
        self._season = game_info["Season"]
        self._days = game_info["Days"]
        
    def work(self):
        
        if self._cmd == "RUN_MATCH":
            self.run_match()
        elif self._cmd == "FINISH_MATCH":
            self.finish_match()
        elif self._cmd == "INIT_PLAYER":
            self.init_star_player()
        elif self._cmd == "FINISH_VOTE":
            self.finish_star_player_vote()
        else:
            self.log("error cmd[%s]" % self._cmd)
   
        return "exist"
    
    def run_match(self):
        """进行比赛"""
        if self._days == 15:
            #休赛期早上8点执行比赛
            command = "%s %s %s" % (CLIENT_EXE_PATH, "star_match_handler", self._season)
            self.call_cmd(command)

    def finish_star_player_vote(self):
        """完成全明星投票"""
        #13轮晚上12点结束
        if self._days == 14:
            return star_match_manager.finish_star_player_vote()
    
    @ensure_success            
    def init_star_player(self):
        """初始化全明星球员"""
        if self._days == 12:
            """11轮中午12点开始投票"""
            star_match_manager.init_star_player()
            #发公告
            game_manager.add_system_message(u"本赛季全明星赛票选活动已经开始.")
            
    @ensure_success
    def finish_match(self):
        """完成比赛"""
        if self._days == 15:
            #早上10点发奖励
            return star_match_manager.finish_star_match(self._season)
        
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()

if __name__ == "__main__":
    args = sys.argv
    if len(args) == 2:
        worker = StarMatchHandler(args[1])
        worker.start()
    else:
        print "error parms"