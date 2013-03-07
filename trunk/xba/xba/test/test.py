#!/usr/bin/python
# -*- coding: utf-8 -*-

import random


from xba.common.sqlserver import connection
from xba.common import excel_importer

ABILITYS = ["Speed","Jump","Strength","Stamina","Shot","Point3","Dribble","Pass","Rebound","Steal","Block","Attack","Defense","Team"]
  
def main():
    i = 1
    infos = excel_importer.parse_excel("D:\\battle_app\\input\\npc1.xls")
    
    for data in infos:    
        info = {}
        for k, v in data.iteritems():
            info[k] = v
            
        ability_total = 0
        for ability in ABILITYS:
            ability_value = info[ability]
            #print ability, ability_value,
            ability_value = ability_value * 10 + random.randint(0, 10)
            if ability in ("Attack","Defense","Team"):
                ability_value = ability_value - random.randint(50, 100)
            else:
                ability_value = ability_value - random.randint(0, 5)
            info["%sMax" % ability] = ability_value
            info[ability] = ability_value
            ability_total += ability_value
             
        playerid = info["PlayerID"]
        del info["PlayerID"]
        i += 1
        for k, v in info.items():
            try:
                info[k] = int(v)
            except:
                pass
        #del info["Name"]
        s = ",".join(["%s='%s'" % (k, v) for k, v in info.items()])
        sql = u"UPDATE BTP_PLAYER5 SET %s, Ability=%i WHERE PlayerID=%i" % (s, ability_total / 14, playerid)
        print sql 
        
        if isinstance(sql, unicode):
            sql = sql.encode("gbk")
        
        
        cursor = connection.cursor()
        try:
            cursor.execute(sql)
        finally:
            cursor.close()
        
if __name__ == "__main__":
    main()