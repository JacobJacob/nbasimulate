#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from gba.common.constants import StaffMap, StaffStatus
from gba.entity import TeamStaff
from gba.common.single_process import SingleProcess
from gba.client.betch.base import BaseBetchClient
from gba.common import exception_mgr
from gba.common.attribute_factory import NameFactory

class Config():
    
    LEVEL_COUNT = {
      1 : 100,              
      2 : 120,
      3 : 140,
      4 : 160,
      5 : 180,
      6 : 200,
      7 : 220,
      8 : 240,              
    }


class StaffCreator(BaseBetchClient):
    '''职员创建客户端'''
    
    def __init__(self):
        super(StaffCreator, self).__init__()
    
    def _run(self):
        
        self.append_log("start to create staff")
        staffs = []
        youth_type = [0, 1]
        for is_youth in youth_type:
            for type in StaffMap.keys():
                for k, v in Config.LEVEL_COUNT.items():
                    for i in range(v):
                        staff = self._create_staff(type, k, is_youth)
                        staffs.append(staff)
                        if len(staffs) > 100:
                            TeamStaff.inserts(staffs)
                            staffs = []
        if staffs:
            TeamStaff.inserts(staffs)
    
    def _create_staff(self, type, level, is_youth):
        
        staff = TeamStaff()
        staff.level = level
        staff.type = type
        staff.status = StaffStatus.NOT_IN_WORD
        staff.name = NameFactory.create_name()
        staff.age = random.randint(35, 60)
        staff.round = random.randint(3, 8)
        staff.wave = level * 1000 + random.randint(1, 1000)
        staff.hide_level = random.randint(1, 8)
        staff.is_youth = is_youth
        return staff
    
def main():
    signle_process = SingleProcess('StaffCreator')
    signle_process.check()
    try:
        client = StaffCreator()
        client.start()
    except:
        exception_mgr.on_except()
        
if __name__ == '__main__':
    main()