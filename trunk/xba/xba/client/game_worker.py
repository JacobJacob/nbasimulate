#!/usr/bin/python
# -*- coding: utf-8 -*-

#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import only_one_match_manager, game_manager
from xba.common.decorators import ensure_success
from base import BaseClient
from xba.client.dev_match_handler import DevMatchHandler

class GameWorker(BaseClient):
    
    
    CLIENT_NAME = "game_worker"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        
    def work(self):
        
        #胜者配对
        self.log("start set only one match")
        self.set_only_one_match()     
        
        #在线体力恢复
        self.log("start add power by online")
        self.add_power_by_online()
        
        #联赛分配
        self.log("start to assign dev for new registe club")
        self.dev_assign()
        
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
    
    def dev_assign(self):
        """联赛分配"""
        handler = DevMatchHandler()
        handler.run()

if __name__ == "__main__":
    worker = GameWorker()
    worker.start()
