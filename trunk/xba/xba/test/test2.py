#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from xba.common import excel_importer

ABILITYS = ["Speed","Jump","Strength","Stamina","Shot","Point3","Dribble","Pass","Rebound","Steal","Block","Attack","Defense","Team"]
  
def main():
    
    infos = excel_importer.parse_excel("D:\\battle_app\\input\\npc.xls")
    for info in infos:
        ability_total = 0
        for ability in ABILITYS:
            ability_value = info[ability]
            ability_value = ability_value * 10 + random.randint(0, 10)
            info["%sMax" % ability] = ability_value
            info[ability] = ability_value
            ability_total += ability_value
             
        playerid =   info["PlayerID"]
        del info["PlayerID"]
        for k, v in info.items():
            try:
                info[k] = int(v)
            except:
                pass
        #del info["Name"]
        s = ",".join(["%s='%s'" % (k, v) for k, v in info.items()])
        sql = "UPDATE BTP_PLAYER5 SET %s, Ability=%i WHERE PlayerID=%i" % (s, ability_total / 14, playerid)
        print sql 
       
        
if __name__ == "__main__":
    main()