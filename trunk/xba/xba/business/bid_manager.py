#!/usr/bin/python
# -*- coding: utf-8 -*-

"""转会"""

import random
from datetime import datetime

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
        connection.close()

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
        connection.close()
        
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
        connection.close()

def get_end_bid(category):
    """差不多了的"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_Get%s '%s'" % (get_postfix(category), datetime.now().strftime("%Y-%m-%d %H:%M:%S"))
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise "error"
    finally:
        connection.close()

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
        connection.close()
    
if __name__ == "__main__":
    pass
        