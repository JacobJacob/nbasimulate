#!/usr/bin/python
# -*- coding: utf-8 -*-

import pymssql
import traceback
from xba.business import dev_manager

VIS_MAP = [
    [[1, 8], [2, 9], [3, 10], [4, 11], [5, 12], [6, 13], [7, 14]],
    [[9, 1], [10, 8], [11, 2], [12, 3], [13, 4], [14, 5], [7, 6]],
    [[1, 10], [9, 11], [8, 12], [2, 13], [3, 14], [4, 7], [5, 6]],
    [[11, 1], [12, 10], [13, 9], [14, 8], [7, 2], [6, 3], [5, 4]],
    [[1, 12], [11, 13], [10, 14], [9, 7], [8, 6], [2, 5], [3, 4]],
    [[13, 1], [14, 12], [7, 11], [6, 10], [5, 9], [4, 8], [3, 2]],
    [[1, 14], [13, 7], [12, 6], [11, 5], [10, 4], [9, 3], [8, 2]],
    [[7, 1], [6, 14], [5, 13], [4, 12], [3, 11], [2, 10], [8, 9]],
    [[1, 6], [7, 5], [14, 4], [13, 3], [12, 2], [11, 8], [10, 9]],
    [[5, 1], [4, 6], [3, 7], [2, 14], [8, 13], [9, 12], [10, 11]],
    [[1, 4], [5, 3], [6, 2], [7, 8], [14, 9], [13, 10], [12, 11]],
    [[3, 1], [2, 4], [8, 5], [9, 6], [10, 7], [11, 14], [12, 13]],
    [[1, 2], [3, 8], [4, 9], [5, 10], [6, 11], [7, 12], [14, 13]],
    [[8, 1], [9, 2], [10, 3], [11, 4], [12, 5], [13, 6], [14, 7]],
    [[1, 9], [8, 10], [2, 11], [3, 12], [4, 13], [5, 14], [6, 7]],
    [[10, 1], [11, 9], [12, 8], [13, 2], [14, 3], [7, 4], [6, 5]],
    [[1, 11], [10, 12], [9, 13], [8, 14], [2, 7], [3, 6], [4, 5]],
    [[12, 1], [13, 11], [14, 10], [7, 9], [6, 8], [5, 2], [4, 3]],
    [[1, 13], [12, 14], [11, 7], [10, 6], [9, 5], [8, 4], [2, 3]],
    [[14, 1], [7, 13], [6, 12], [5, 11], [4, 10], [3, 9], [2, 8]],
    [[1, 7], [14, 6], [13, 5], [12, 4], [11, 3], [10, 2], [9, 8]],
    [[6, 1], [5, 7], [4, 14], [3, 13], [2, 12], [8, 11], [9, 10]],
    [[1, 5], [6, 4], [7, 3], [14, 2], [13, 8], [12, 9], [11, 10]],
    [[4, 1], [3, 5], [2, 6], [8, 7], [9, 14], [10, 13], [11, 12]],
    [[1, 3], [4, 2], [5, 8], [6, 9], [7, 10], [14, 11], [13, 12]],
    [[2, 1], [8, 3], [9, 4], [10, 5], [11, 6], [12, 7], [13, 14]],
]

class DevMatchHandler(object):
    
    def __init__(self):
        pass
    
    def run(self):
        self.dev_assign()
  
    def dev_assign(self):   
        info = self.get_full_dev()
        if not info:
            print "return"
            return
            
        dev_code = info["devcode"]
        self.do_dev_assign(dev_code)
        
    def do_dev_assign(self, dev_code):
        """联赛分配"""
        club_infos = dev_manager.get_dev_clubs(dev_code)
        club_map = {}
        for i, club_info in enumerate(club_infos):
            club_map[i+1] = club_info["ClubID"]
             
        conn = pymssql.connect(host='127.0.0.1', user='BTPAdmin', password='BTPAdmin123', database='NewBTP', as_dict=True)
        try:
            conn.autocommit(False)
            cursor = conn.cursor()
            round = 1
            for items in VIS_MAP:
                for item in items:
                    home = club_map.get(item[0])
                    away = club_map.get(item[1])
                    if home == 0 and away == 0:
                        continue
                    print "round:%s home:%s away:%s dev:%s" % (round, home, away, dev_code)
                    cursor.execute("EXEC AddDevMatch %s, %s, %s, %s", (round, home, away, dev_code))
                round += 1
             
            cursor.execute("update btp_dev set win=0 where devcode = %s", dev_code)   
            conn.commit()
        except:
            print traceback.format_exc().decode("gbk")
            conn.rollback()
        finally:
            conn.close()       
    
    def get_full_dev(self):
        """获取报名满14人的联赛"""
        while True:
            conn = pymssql.connect(host='127.0.0.1', user='BTPAdmin', password='BTPAdmin123', database='NewBTP', as_dict=True)
            try:
                cursor = conn.cursor()
                cursor.execute("select top 1 devcode from (select count(*) as total, devcode from btp_dev where win = -1 group by devcode)a where total = 14")
                infos = cursor.fetchone()
                return infos
            finally:
                conn.close()
        
if __name__ == '__main__':
    manager = DevMatchHandler()
    manager.run()
