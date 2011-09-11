#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
        
def delete_turn_finance(season):
    """删除每天财政"""
    cursor = connection.cursor()
    try:
        sql = "EXEC DeleteTurnFinance %s" % season
        cursor.execute(sql)
    finally:
        cursor.close()
        
def total_season_finance(season, fource=True):
    """统记赛纪财政总数"""
    cursor = connection.cursor()
    try:
        if fource:
            delete_sql = "delete from BTP_TFinance where season=%s and category=2" % season
            print delete_sql
            cursor.execute(delete_sql)
        cursor.execute("select UserID, sum(Income) as Income, sum(Outcome) Outcome" \
                        " from BTP_TFinance where season= %s and category=1 group by UserID" % season)
        infos = cursor.fetchall()
        for info in infos:
            income = info["Income"]
            outcome = info["Outcome"]
            user_id = info["UserID"]
            cursor.execute("EXEC AddTFinanceSeason %s, %s, %s, %s, 2" % (user_id, income, outcome, season))
    finally:
        cursor.close()
        
def payment_all():
    """收取税金所有人税金"""
    cursor = connection.cursor()
    try:
        cursor.execute("select UserID from btp_account")
        infos = cursor.fetchall()
        for info in infos:
            user_id = info["UserID"]
            sql = "EXEC Payment %s" % user_id
            cursor.execute(sql)
    finally:
        connection.close()
            
def payment(user_id):
    """收取税金"""
    cursor = connection.cursor()
    try:
        sql = "EXEC Payment %s" % user_id
        cursor.execute(sql)
    finally:
        cursor.close()
       
if __name__ == "__main__":
    total_season_finance(3)