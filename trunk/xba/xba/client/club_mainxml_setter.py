#!/usr/bin/python
# -*- coding: utf-8 -*-

#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import game_manager, dev_match_manager, club_manager
from xba.common.decorators import ensure_success
from base import BaseClient

class ClubMainXMLSetter(BaseClient):
    
    
    CLIENT_NAME = "club_mainxml_stter"
    
    def __init__(self):
        super(self.__class__, self).__init__(self.__class__.CLIENT_NAME)
        self._turn = -1
        
    def work(self):
        self.update_next_match_info_to_mainxml()
        return "exist"
      
    def before_run(self):
        """运行前的初始化"""
        game_info = self.get_game_info()
        self._turn = game_info["Turn"]
          
    @ensure_success
    def get_game_info(self):
        return game_manager.get_game_info()
 
    @ensure_success
    def set_club_main_xml(self, club_id, info):
        """设置俱乐部的main xml"""
        self.log("start update club %s's main xml" % club_id)
        return club_manager.set_club_main_xml(club_id, info)
    
    @ensure_success
    def get_club_info(self, club_id):
        """获取俱乐部info"""
        return club_manager.get_club_by_id(club_id)

    @ensure_success
    def get_dev_match(self, next=False):
        """获取某轮的比赛列表"""
        turn = self._turn + 1 if next else self._turn
        return dev_match_manager.get_round_dev_matchs(0, turn)

    def update_next_match_info_to_mainxml(self):
        """将下一轮的对阵更新到club的main xml中"""
        if self._turn == 26:
            self.log("now is the last turn of the season, not need to handle next turn")
            return
        dev_match_infos = self.get_dev_match(True)
        for dev_match_info in dev_match_infos:
            club_home_id = dev_match_info["ClubHID"]
            club_away_id = dev_match_info["ClubAID"]
            if not (club_home_id and club_away_id):
                continue
            club_home_info = self.get_club_info(club_home_id)
            club_away_info = self.get_club_info(club_away_id)
            if not club_home_info or not club_away_info:
                print dev_match_info
                raise "error"

            club_home_name = club_home_info["Name"].strip() if club_home_info["Name"] else ""
            club_away_name = club_away_info["Name"].strip() if club_away_info["Name"] else ""
            club_home_logo = club_home_info["Logo"].strip() if club_home_info["Logo"] else ""
            club_away_logo = club_away_info["Logo"].strip() if club_away_info["Logo"] else ""
            next_match_info = {"NClubNameH": club_home_name, "NClubNameA": club_away_name,
                               "NClubLogoH": club_home_logo, "NClubLogoA": club_away_logo}
            self.set_club_main_xml(club_home_id, next_match_info)
            self.set_club_main_xml(club_away_id, next_match_info)

if __name__ == "__main__":
    worker = ClubMainXMLSetter()
    worker.start()
