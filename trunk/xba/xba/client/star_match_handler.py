#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.config import CLIENT_EXE_PATH
from xba.business import game_manager, star_match_manager
from xba.common.decorators import ensure_success
from base import BaseClient

class StarMatchHandler(BaseClient):
    
    
    CLIENT_NAME = "star_match_handler"
    
    
    def __init__(self):
        super(StarMatchHandler, self).__init__(StarMatchHandler.CLIENT_NAME)
        self._season = -1
   
    def before_run(self):
        """运行前的初始化"""
        self.log("before run")
        game_info = self.get_game_info()
        self._season = game_info["Season"]
    
    def work(self):
        
        self.run_match()
        
        self.finish_match()
   
        return "exist"
    
    def run_match(self):
        """比赛"""
        command = "%s %s %s" % (CLIENT_EXE_PATH, "star_match_handler", self._season)
        while True:
            success = self.call_cmd(command)
            if success:
                return
            else:
                self.sleep(10)
            
    @ensure_success
    def finish_match(self):
        return star_match_manager.finish_star_match(self._season)
        
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()

if __name__ == "__main__":
    worker = StarMatchHandler()
    worker.start()
