#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.stringutil import ensure_gbk
      
        
def update_stock_compay():
    """每日更新俱乐部市值排行榜"""
    cursor = connection.cursor()
    try:
        #把排名更新到昨天提名
        cursor.execute("UPDATE BTP_Company SET YestodaySort = Sort")
        cursor.execute("SELECT * FROM BTP_Account")
        infos = cursor.fetchall()
        for info in infos:
            user_id = info["UserID"]
            money = info["Money"]
            cursor.execute("SELECT * FROM BTP_Club WHERE UserID=%s AND Category = 5" % user_id)
            club_info = cursor.fetchone()
            if not club_info:
                continue
            club_id = club_info["ClubID"]   
            club_name = club_info["Name"]   
            cursor.execute("SELECT * FROM BTP_Player5 WHERE ClubID = %s" % club_id) 
            player_infos = cursor.fetchall()
            total = money
            for player_info in player_infos:
                player_id = player_info["PlayerID"]
                cursor.execute("GetSellMoneyPlayer5 %s, %s" % (user_id, player_id))
                price_info = cursor.fetchone()
                price = price_info["BidPrice"]
                total += price
            
            print "UserID:%s,Amount:%s" % (user_id, total)
            
            cursor.execute("SELECT * FROM BTP_Company WHERE UserID=%s" % user_id)
            stock_info = cursor.fetchone()
            #还没有记录
            if not stock_info:
                insert_sql = "INSERT INTO BTP_Company(UserID, Price, ClubName) VALUES(%s, %s, '%s')" % (user_id, price, club_name)
                cursor.execute(ensure_gbk(insert_sql))
            else:
                update_sql = "UPDATE BTP_Company SET Price=%s, ClubName='%s' WHERE UserID=%s" % (total, club_name, user_id)
                cursor.execute(ensure_gbk(update_sql))
                
        #更新排名
        update_stock_company_sort()
        
    finally:
        cursor.close()
        
def update_stock_company_sort():
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT * FROM BTP_Company ORDER BY PRICE DESC")
        infos = cursor.fetchall()
        for i, info in enumerate(infos):
            cursor.execute("UPDATE BTP_Company SET SORT=%s WHERE CompanyID=%s" % (i + 1, info["CompanyID"]))
    finally:
        cursor.close()
        
def set_team_ability():
    """更球队综合排名"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC SetTeamAbility")
    finally:
        cursor.close()
        
if __name__ == "__main__":
    update_stock_compay()
    #update_stock_company_sort()  
        