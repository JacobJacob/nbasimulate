#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import cPickle as pickle

from datetime import datetime, timedelta

from xba.business import only_one_match_manager, game_manager, arrange_manager, union_field_manager, account_manager
from xba.common.decorators import ensure_success
from xba.common.constants.club import ClubCategory
from base import BaseClient
from xba.client.dev_match_handler import DevMatchHandler
from xba.client.devcup_handler import DevCupHandler
from xba.client.cup_handler import CupHandler

from xba.common.stringutil import ensure_utf8
from xba.config import PathSettings
from xba.model import PromotionHistory
from xba.common import file_utility
from xba.common import urlutil

class GameWorker(BaseClient):
    
    
    CLIENT_NAME = "game_worker"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        self.__not_login_users = None
        self.__expire_time = datetime.now()
        
    def work(self):
        
        #胜者配对
        self.log("start set only one match")
        self.set_only_one_match()     
        
        self.log("start to set not login user to match")
        self.set_not_login_user_to_match()
        
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
        
        #处理过期盟战
        self.log("start to update union filed game")
        self.day_update_union_field_game()
        
        #推广积分处理
        self.promotion()
        
        self.log("start sleep")
        self.sleep()
        
    @ensure_success
    def day_update_union_field_game(self):
        """处理过期盟战"""
        return union_field_manager.day_update_union_field_game()
    
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
        
    def promotion(self):
        """推广积分文件处理"""
        files = file_utility.get_files(PathSettings.PROMOTION_LOG_PATH)
        for file in files:
            path = os.path.join(PathSettings.PROMOTION_LOG_PATH, file)
            f = open(path, 'rb')
            try:
                user_id, ip, referrer = pickle.load(f)
            finally:
                f.close()
                
            os.remove(path)
            
            referrer = ensure_utf8(referrer)

            self.log("handle promotion log.file[%s], user_id[%s], ip[%s], referrer[%s]"  % (file, user_id, ip, referrer))
            
            history = PromotionHistory.load(user_id=user_id, ip=ip)
            if not history:
                history = PromotionHistory()
                history.user_id = user_id
                history.ip = ip
                history.count = 1
                if referrer:
                    promotion = 5
                    urlsplit = urlutil.standardize(referrer)
                    if urlsplit and urlsplit.is_host:
                        self.log("bad referrer:[%s]" % referrer )
                    else:   
                        account_manager.add_promotion(user_id, promotion)
                else:
                    self.log("skip,userid[%s]" % user_id) 

            else:
                history.count = history.count + 1
            
            history.persist()
        
    @ensure_success
    def set_online(self):
        return account_manager.set_online()
        
    def set_not_login_user_to_match(self):
        """胜者打比赛"""
        if datetime.now().hour < 10:
            return
        self.set_online()
        if not self.__not_login_users or self.__expire_time < datetime.now():
            self.__not_login_users = self.get_not_active_users()
            self.__expire_time = datetime.now() + timedelta(hours=1)
        self.__not_login_users.extend([15, 20, 46, 47, 75, 165, 158])
        for user_id in self.__not_login_users:
            only_one_reg_info = self.get_onlyone_match_row(user_id)
            if not only_one_reg_info:
                self.log("set user:%s to reg" % user_id)
                self.only_one_center_reg(user_id)
            else:
                status = only_one_reg_info["Status"]
                if status == 6:
                    self.log("set user:%s to out for lose" % user_id)
                    self.only_one_match_out(user_id)
                    self.log("set user:%s to reg" % user_id)
                    self.only_one_center_reg(user_id)
                elif status == 5:
                    win = only_one_reg_info["Win"]
                    if win == 9:
                        self.log("set user:%s to out for win" % user_id)
                        self.only_one_match_out(user_id)
                    else:
                        self.log("set user:%s to go on" % user_id)
                        self.only_one_match_goon(user_id)
                        
    @ensure_success      
    def get_not_active_users(self):
        return account_manager.get_not_active_users()
    
    @ensure_success      
    def get_onlyone_match_row(self, user_id):
        return only_one_match_manager.get_onlyone_match_row(user_id)
    
    @ensure_success      
    def only_one_center_reg(self, user_id):
        return only_one_match_manager.only_one_center_reg(user_id)
    
    @ensure_success      
    def only_one_match_goon(self, user_id):
        return only_one_match_manager.only_one_match_goon(user_id)
    
    @ensure_success      
    def only_one_match_out(self, user_id):
        return only_one_match_manager.only_one_match_out(user_id)
        
    def dev_assign(self):
        """联赛分配"""
        handler = DevMatchHandler()
        handler.run()

if __name__ == "__main__":
    worker = GameWorker()
    worker.start()
