#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import player5_manager, account_manager
from xba.business import dev_match_manager
from xba.business import game_manager
from xba.business import finance_manager
from xba.business import dev_manager
from base import BaseClient
from xba.common.decorators import ensure_success
from xba.client.dev_match_handler import DevMatchHandler


class SeasonUpdateHandler(BaseClient):
    
    CLIENT_NAME = "season_update_handler"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        self.__dev_level_sum = -1
        self.__season = -1
    
    def work(self):
        self.log("season update start")
        
        self.assign_choose_car()
        
        #删除每天财政
        self.delete_turn_finance()
        
        #球员伤病恢复
        self.player5_holiday()
        
        #球员赛季数据清空
        self.clear_player5_season()
        
        #冠军奖励
        self.reward_dev()
         
        #联赛更新
        self.dev_update()
        
        #删除联赛留言
        self.delete_devmessage()
        
        #将球队往前面挤一挤
        self.reset_dev()
        
        #将高综合点的球队往前面补
        self.fill_not_full_dev()
        
        #初始化联赛
        self.assign_dev()
        
        #将球队往前面挤一挤
        self.reset_dev()
        
        #比赛安排
        self.dev_match_assign()
                
        #发放提拔卡
        self.assign_promotion_card()
        
        #更新结束,设置赛季开始
        self.season_update_finish()

        return "exist"
    
    def before_run(self):
        self.__dev_level_sum = self.get_total_level()
        game_info = self.get_game_info()
        self.__season = game_info["Season"]
        self.log("total dev level sum is %s" % self.__dev_level_sum)
        
    def season_update_finish(self):
        """赛季更新结束"""
        game_manager.set_to_next_days()
        game_manager.set_season()
    
    def get_total_level(self):
        """获取联赛等级数"""
        return game_manager.get_game_info()["DevLevelSum"]
    
    def delete_all_matches(self):
        """删除所有联赛比赛"""
        return dev_match_manager.delete_all_matches()
    
    def reset_dev(self):
        """将各级联赛的球队尽量集中在一起"""
        self.log("start reset dev")
        for level in range(1, self.__dev_level_sum + 1):
            self.log("start to reset level:%s" % level)
            self.reset_level_dev(level)
        
    def reset_level_dev(self, level):
        """将某一等级的俱乐部往前排"""
        club_infos = self.get_dev_table_by_level(level)
        
        for i, club_info in enumerate(club_infos):
            club_id = club_info["ClubID"]
            dev_code = club_info["DevCode"]
            if club_id == 0:
                #如果该俱乐部是空，则从最后面补一个上来
                index, new_club_info = self.get_last_club_info(dev_code, club_infos)
                if new_club_info and i < index:
                    #交换两支球队
                    self.exchange_two_dev(club_info, new_club_info)
                else:
                    #从后面拿不到一支有俱乐部的dev，则退出
                    return
                
    def delete_turn_finance(self):
        """删除每天财政"""
        finance_manager.delete_turn_finance(self.__season)
            
    def assign_dev(self):
        """初始化联赛"""
        self.log("start assign dev")
        return dev_manager.assign_dev()
    
    def dev_match_assign(self):
        """安排所有比赛"""
        #删除所有比赛
        self.delete_all_matches()
        self.log("start to dev match assign")
        for level in range(1, self.__dev_level_sum + 1):
            dev_count = self.get_dev_count(level)
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                self.do_dev_match_assign(dev_code)
                
    def do_dev_match_assign(self, dev_code):
        """为某个联赛安排比赛"""
        handler = DevMatchHandler()
        handler.do_dev_assign(dev_code)
    
    def exchange_two_dev(self, club_info_one, club_info_two):
        """交换两支球队"""
        return dev_manager.exchange_two_dev(club_info_one, club_info_two)
        
    
    def get_last_club_info(self, dev_code, club_infos):
        """获取最后一支球队"""
        i = len(club_infos)
        while i > 0:
            club_info = club_infos[i - 1]
            if club_info["ClubID"] > 0:
                if club_info["DevCode"] != dev_code:
                    return i, club_info
                else:
                    return 0, None
            i -= 1
        
        return 0, None
        
    def get_dev_table_by_level(self, level):
        """获取某一等级所有俱乐部"""
        return dev_manager.get_dev_table_by_level(level)
        

    def player5_holiday(self):
        """所有球员伤病恢复，体力恢复，心情恢复"""
        return player5_manager.player5_holiday()
    
    def clear_player5_season(self):
        """清理球员赛季数据"""
        return player5_manager.clear_player5_season()
    
    def reward_dev(self):
        """联赛冠军奖励"""
        for level in range(1, self.__dev_level_sum + 1):
            dev_count = self.get_dev_count(level)
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                dev_manager.reward_dev(dev_code, level, sort)
    
    def dev_update(self):
        """联赛更新"""
        for level in range(1, self.__dev_level_sum + 1):
            self.log("start to update dev levels %s" % level)
            
            dev_count = self.get_dev_count(level)
            self.log("level:%s, dev total:%s" % (level, dev_count))
            for sort in range(dev_count):
                dev_code = self.get_dev_code_by(level, sort)
                #self.log("start to handle dev[%s]" % dev_code)
                self.handle_dev(dev_code)
                
    def handle_dev(self, dev_code):
        """处理联赛"""
        club_infos = dev_manager.get_dev_clubs(dev_code)
        club_count = 0
        for club_info in club_infos:
            if club_info["ClubID"] > 0:
                club_count += 1
         
        self.log("dev %s has %s club's" % (dev_code, club_count))
        
        for i, club_info in enumerate(club_infos):
            if not dev_code:#一级联赛,只降不升
                if i in (10, 11, 12, 13):
                    self.log("club [%s] start to degrade" % club_info["ClubID"])
                    self.degrade_dev(club_info, i)
            elif len(dev_code) == self.__dev_level_sum - 1:#最后一级,只升不降
                if i in (0, 1):
                    self.log("club [%s] start to up grade" % club_info["ClubID"])
                    self.upgrade_dev(club_info)
            else:#又升又降
                if i in (10, 11, 12, 13):
                    self.log("club [%s] start to down grade" % club_info["ClubID"])
                    self.degrade_dev(club_info, i)
                if i in (0, 1):
                    self.log("club [%s] start to up grade" % club_info["ClubID"])
                    self.upgrade_dev(club_info)


    def upgrade_dev(self, dev_info):
        """联赛升级"""
        return dev_manager.upgrade_dev(dev_info)
    
    def degrade_dev(self, dev_info, sort):
        """联赛降级"""
        return dev_manager.degrade_dev(dev_info, sort)
            
    def get_dev_code_by(self, level, sort):
        """根据联赛等 级和序号获取dev code"""
        if level == 1:
            return ""
        
        base = ""
        while sort > 0:
            m = sort % 2
            sort = sort / 2
            base = "%s%s" % (m, base) 
            
        if len(base) < level - 1:
            base = "%s%s" % ("0" * (level - len(base) - 1), base)
        return base
    
    def get_dev_count(self, level):
        """根据联赛等级获取联赛数"""
        return 2 ** (level - 1)
    
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()
    
    @ensure_success
    def delete_devmessage(self):
        return dev_manager.delete_devmessage()
    
    @ensure_success
    def assign_choose_car(self):
        """发放选秀卡"""
        return account_manager.assign_devchoose_card_with_devsort()
    
    @ensure_success
    def assign_promotion_card(self):
        """发放选拔卡"""
        return account_manager.assign_promotion_card()
    
    def fill_not_full_dev(self):
        """把高级的缺人的联赛填满"""
        for level in range(1, self.__dev_level_sum):
            self.log("start to fill level:%s" % level)
            self.do_fill_one_not_full_dev(level)
  
    def do_fill_one_not_full_dev(self, level):
        """把高级的缺人的联赛填满"""
        dev_infos = dev_manager.get_dev_table_by_total(level)
        if not dev_infos:
            self.log("not dev infos, return")
            return
        for dev_info in dev_infos:
            devcode, total = dev_info["devcode"], dev_info["total"]
            if total != 14:
                infos = dev_manager.get_dev_clubs(devcode)
                for info in infos:
                    clubid = info["ClubID"]
                    if clubid <= 0:
                        self.log("has club id less 0")
                        #从下一级拿一级最高综合的球队
                        user_infos = account_manager.get_one_level_max_team(level+1)
                        self.log("get %s user infos" % len(user_infos))
                        for user_info in user_infos:
                            if user_info and user_info["UserID"]:
                                new_dev_info = dev_manager.get_dev_info_by_userid(user_info["UserID"])
                                self.log("start to change %s(%s), %s(%s)" % (info["DevCode"], info["DevID"], new_dev_info["DevCode"], new_dev_info["DevID"]))
                                self.exchange_two_dev(info, new_dev_info)
                                break
                                
def main():
    handler = SeasonUpdateHandler()
    handler.before_run()
    handler.fill_not_full_dev()
    #handler.reset_dev()
    handler.dev_match_assign()    

def run():
    handler = SeasonUpdateHandler()
    handler.start()
                      
if __name__ == "__main__":
    run()
    #main()
