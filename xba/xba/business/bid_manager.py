#!/usr/bin/python
# -*- coding: utf-8 -*-

"""转会"""

import random
from datetime import datetime, timedelta

from xba.common.sqlserver import connection
from xba.common import logging
from xba.common import log_execption
from xba.common.constants.market import MarketCategory

def get_postfix(category):
    """获取后缀"""
    if category == MarketCategory.STREET_FREE:
        return "Free"
    elif category == MarketCategory.STREET_SELECTION:
        return "Close"
    elif category == MarketCategory.PROFESSION_TRANSFER:
        return "Open"
    elif category == MarketCategory.PROFESSION_SELECTION:
        return "Rookie"    
    else:
        logging.error("unknow category")  

def finish_bid(player_id, number, category):
    """转会完成"""
    cursor = connection.cursor()
    try:
        if category == MarketCategory.STREET_SELECTION:
            sql = "exec Bid_End%s %s, %s, ''" % (get_postfix(category), player_id, number)
        else:
            sql = "exec Bid_End%s %s, %s" % (get_postfix(category), player_id, number)
        cursor.execute(sql)
    except Exception, e:
        logging.error(e.message.decode("gbk"))
        raise "error"
    finally:
        cursor.close()

def prepare_bid(player_id, category):
    """转会准备完成"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_Pre%s %s" % (get_postfix(category), player_id)
        cursor.execute(sql)
    except:
        log_execption()
        raise "error"
    finally:
        cursor.close()
        
def get_end_bid_for_end(category):
    """得到已经截止转会(超过20分钟的)"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_GetEnd%s '%s'" % (get_postfix(category), datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise "error"
    finally:
        cursor.close()

def get_end_bid(category):
    """差不多了的"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_Get%s '%s'" % (get_postfix(category), datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        cursor.close()
        
def set_auto_bid():
    """设定自动出价"""
    cursor = connection.cursor()
    try:
        sql = "select * from BTP_BidAuto"
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            max_money = info["MaxMoney"]
            user_id = info["UserID"]
            player_id = info["PlayerID"]
            bid_auto_id = info["BidAutoID"]
            cursor.execute("select Category, BidPrice, EndBidTime from btp_player5 where PlayerID = %s" % player_id)
            player_info = cursor.fetchone()
            if not player_info:
                continue
            category = player_info["Category"]
            bid_price = player_info["BidPrice"]
            end_bid_time = player_info["EndBidTime"]
            if category != 2 and category != 4:
                print "warn:category error!!!:%s" % category
                #cursor.execute("delete from BTP_BidAuto where BidAutoID = %s" % bid_auto_id)
                continue
            bid_price = (bid_price / 100) * 102;
            if bid_price > max_money:
                cursor.execute("delete from BTP_BidAuto where BidAutoID = %s" % bid_auto_id)
            else:
                cursor.execute("SELECT TOP 1 UserID FROM BTP_Bidder WHERE PlayerID=%s AND Category=5 ORDER BY Price DESC" % player_id)
                bid_info = cursor.fetchone()
                #不能自己顶自己
                if bid_info and bid_info["UserID"] == user_id:
                    continue
                
                status = 0
                if category == 4:
                    status = 4
                elif category == 3:
                    status = 5
                elif category == 2:
                    status = 3
                else:
                    continue
                now = datetime.now().strftime("%Y-%m-%d %H:%M:%S")
                if end_bid_time < datetime.now() - timedelta(minutes=2):
                    cursor.execute("update btp_player5 set EndBidTime = dateadd(mm, 2, EndBidTime) where PlayerID = %s" % player_id)
                bid_sql = "EXEC SetDevisionTran %s, %s, %s, '%s', '127.0.0.1', %s" % (player_id, user_id, bid_price, now, status)
                cursor.execute(bid_sql) 
    except:
        log_execption()
        raise "error"
    finally:
        cursor.close()
        

def get_club_player_number(club_id, type):
    """get club player number"""
    cursor = connection.cursor()
    try:
        sql = "select number from btp_player%s where ClubID = %s " % (type, club_id)
        cursor.execute(sql)
        infos = cursor.fetchall()
        numbers = set()
        if infos:
            for info in infos:
                numbers.add(info['number'])

        while True:
            number = random.randint(0, 50)
            if number not in numbers:
                return number
    
    except:
        log_execption()
        raise "error"
    finally:
        cursor.close()
    
def get_random(max, age):
    cursor = connection.cursor()
    age_add = (26 - age ) * 2 + 6
    try:
        sql = "select %s + %s  + POWER(%s, 1.5)/%s*(2+4*dbo.GetRandom())" % (max, age_add, max, max)
        cursor.execute(sql)
        return cursor.fetchone()[0]
    finally:
        cursor.close()
if __name__ == "__main__":
    print "  7+\t8+\t9+\t10+\t11+\t12+\t13+"
    for i in range(20):
        max = 750 - i * 10
        print '当前属性:%s\t' % int(max / 10),
        add_7 = 0
        add_8 = 0
        add_9 = 0
        add_10 = 0
        add_11 = 0
        add_12 = 0
        add_13 = 0
        for j in range(1000):
            now = max * 11
            new = 0
            for k in range(11):
                add = int(get_random(max, 21))
                new += add
            
            add_ability = (new - now) / 14
            if add_ability >= 130:
                add_13 += 1
            elif add_ability >= 120:
                add_12 += 1
            elif add_ability >= 110:
                add_11 += 1
            elif add_ability >= 100:
                add_10 += 1
            elif add_ability >= 90:
                add_9 += 1
            elif add_ability >= 80:
                add_8 += 1
            else:
                add_7 += 1
                
        print "%s\t%s\t%s\t%s\t%s\t%s\t%s" % (add_7, add_8, add_9, add_10, add_11, add_12, add_13)    