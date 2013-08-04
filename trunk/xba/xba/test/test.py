#!/usr/bin/python
# -*- coding: utf-8 -*-

import random


from xba.common.sqlserver import connection
from xba.common import excel_importer

ABILITYS = ["Speed","Jump","Strength","Stamina","Shot","Point3","Dribble","Pass","Rebound","Steal","Block","Attack","Defense","Team"]
  
def main():
    i = 1
    infos = excel_importer.parse_excel("D:\\battle_app\\input\\npc.xls")
    id = 199047
    data = infos[0]
    del data["PlayerID"]
    for _ in range(5):
        
        info = {}
        for k, v in data.iteritems():
            info[k] = v
            
        ability_total = 0
        for ability in ABILITYS:
            ability_value = info[ability]
            #print ability, ability_value,
            ability_value = ability_value * 10 + random.randint(0, 10)
            if id != 199047:
                if ability in ("Attack","Defense","Team"):
                    ability_value = ability_value - random.randint(50, 100)
                else:
                    ability_value = ability_value - random.randint(0, 5)
            info["%sMax" % ability] = ability_value
            if ability in ("Attack","Defense","Team"):
                info[ability] = 350
                ability_total +=350
            else:
                info[ability] = 200
                ability_total += 200
             
        playerid = id
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
        id += 1
        
if __name__ == "__main__":
    main()