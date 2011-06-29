#!/usr/bin/python
# -*- coding: utf-8 -*-

#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import only_one_match_manager, game_manager
from xba.common.decorators import ensure_success
from base import BaseClient

class GameWorker(BaseClient):
    
    
    CLIENT_NAME = "game_worker"
    
    def __init__(self):
        super(GameWorker, self).__init__(GameWorker.CLIENT_NAME)
        
    def work(self):
        
        #胜者配对
        self.log("start set only one match")
        self.set_only_one_match()     
        
        #在线体力恢复
        self.log("start add power by online")
        self.add_power_by_online()
        
        self.log("start sleep")
        self.sleep()
        
    @ensure_success()
    def set_only_one_match(self):
        """胜者为王挫合"""
        return only_one_match_manager.set_only_one_match()
    
    @ensure_success()
    def add_power_by_online(self):
        """在线体力恢复"""
        return game_manager.add_power_by_online()

if __name__ == "__main__":
    worker = GameWorker()
    worker.start()
