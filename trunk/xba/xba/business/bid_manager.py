#!/usr/bin/python
# -*- coding: utf-8 -*-

"""职业转会"""

import random
from datetime import datetime

from xba.common.sqlserver import connection
from xba.common import logging
from xba.business import club_manager


def finish_bid_open(player_id, number):
    """职业转会(明拍)完成"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_EndOpen %s, %s" % (player_id, number)
        cursor.execute(sql)
    except Exception, e:
        logging.error(e.message.decode("gbk"))
        raise "error"
    finally:
        connection.close()

def prepare_bid_open(player_id):
    """职业转会(明拍)准备完成"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_PreOpen %s" % player_id
        cursor.execute(sql)
    except Exception, e:
        logging.error(e.message.decode("gbk"))
        raise "error"
    finally:
        connection.close()
        
def get_end_bid_open_for_end():
    """得到已经截止职业转会(超过20分钟的)"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_GetEndOpen '%s'" % datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        cursor.execute(sql)
        return cursor.fetchall()
    except Exception, e:
        logging.error(e.message.decode("gbk"))
        raise "error"
    finally:
        connection.close()

def get_club_player5_number(club_id):
    """get club player 5 number"""
    cursor = connection.cursor()
    try:
        sql = "select number from btp_player5 where ClubID = %s " % club_id
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
    
    except Exception, e:
        logging.error(e.message.decode("gbk"))
        raise "error"
    finally:
        connection.close()
    
def get_end_bid_open():
    """差不多了的"""
    cursor = connection.cursor()
    try:
        sql = "exec Bid_GetOpen '%s'" % datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        cursor.execute(sql)
        return cursor.fetchall()
    except Exception, e:
        logging.error(e.message.decode("gbk"))
        raise "error"
    finally:
        connection.close()
    
if __name__ == "__main__":
    infos = get_end_bid_open()
    for info in infos:
        player_id = info['PlayerID']
        logging.info("start to handler player with id:%s" % player_id)
        prepare_bid_open(player_id)
        
    infos = get_end_bid_open_for_end()
    for info in infos:
        player_id = info['PlayerID']
        bidder_id = info["BidderID"]
        logging.info("start to finish player with id:%s, bidder id:%s" % (player_id, bidder_id))
        number = 0
        if bidder_id > 0:
            club_id = club_manager.get_club_by_user_id(bidder_id)
            if not club_id:
                logging.error("club with user id:%s not exist" % bidder_id)
                continue
             
            number = get_club_player5_number(club_id)
            
        finish_bid_open(player_id, number)
        