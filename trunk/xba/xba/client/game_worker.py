#!/usr/bin/python
# -*- coding: utf-8 -*-

#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import only_one_match_manager, game_manager, arrange_manager
from xba.common.decorators import ensure_success
from xba.common.constants.club import ClubCategory
from base import BaseClient
from xba.client.dev_match_handler import DevMatchHandler
from xba.client.devcup_handler import DevCupHandler
from xba.client.cup_handler import CupHandler

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
        
        #战术等级更新
        self.log("start to update arrange level")
        self.update_arrange_lvl()
        
        #自定义杯赛更新
        self.log("stat to handle devcup")
        self.devcup_handle()
        
        #街球杯赛更新
        self.log("stat to handle cup")
        self.cup_handle()
        
        self.log("start sleep")
        self.sleep()
        
    @ensure_success
    def set_only_one_match(self):
        """胜者为王挫合"""
        return only_one_match_manager.set_only_one_match()
    
    def update_arrange_lvl(self):
        """战术等级更新"""
        arrange_manager.update_arrange_lvl(ClubCategory.STREET)
        arrange_manager.update_arrange_lvl(ClubCategory.PROFESSION)
    
    @ensure_success
    def add_power_by_online(self):
        """在线体力恢复"""
        return game_manager.add_power_by_online()
    
    def devcup_handle(self):
        """自定义杯赛处理"""
        handle = DevCupHandler()
        handle.start()
        
    def cup_handle(self):
        """街球杯赛处理"""
        handle = CupHandler()
        handle.start()
    
    def dev_assign(self):
        """联赛分配"""
        handler = DevMatchHandler()
        handler.run()

if __name__ == "__main__":
    worker = GameWorker()
    worker.start()
