#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from datetime import datetime, timedelta

from xba.config import DOMAIN
from xba.model import Cup
from xba.common.orm import Session
from xba.common.sqlserver import connection
from xba.common import log_execption
from xba.common.constants.cup import CupCategoryMap, CupCategoryCapacityMap, CupCategoryLevelCountMap
from xba.common.constants.cup import CupCategoryBigLogoMap, CupCategoryDescMap, CupCategorySmallLogoMap
from xba.common.constants.cup import CupCategoryTicketCategoryMap, CupCategoryRewardMap, CupCategoryMoneyCostMap
from xba.common.constants.cup import CupCategoryRequirementMap

def get_max_setid():
    """获取当前最大的setid"""
    cursor = connection.cursor()
    try:
        sql = "select max(setid) as setid from btp_cup"
        cursor.execute(sql)
        info = cursor.fetchone()
        if info and info.get('setid'):
            return info["setid"]
        return 0
    except:
        log_execption()
    finally:
        connection.close()

def insert_cup(info):
    """插入杯赛"""
    params = [info['setid'], info['category'], info['levels'], info.get('union_id', 0), info['name'], info['introduction']]
    params.extend([info.get('money_cost', 0), info['small_logo'], info['big_logo'], info['requirement_xml'], info['reward_xml']])
    params.extend([info.get('round', 0), info['capacity'], info['end_reg_time'], info['match_time'], info['coin']]) 
    params.extend([info['cup_ladder'], info['ticket_category']])
    cursor = connection.cursor()
    try:
        sql = "exec AddCup %s" % ",".join(["'%s'" % param for param in params])
        sql = sql.decode("utf8").encode("gb2312")
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()

def add_cup(category, end_reg_time):
    setid = get_max_setid() + 1
    info = {'setid': setid}
    if category in (21, 22):
        info['category'] = 2
    elif category in (23,):
        info['category'] = 1
    elif category in (24,):
        info['category'] = 8
        
    info['name'] = CupCategoryMap[category]
    info['introduction'] = CupCategoryDescMap[category] % (end_reg_time, end_reg_time)
    info['money_cost'] = CupCategoryMoneyCostMap[category]
    info['small_logo'] = CupCategorySmallLogoMap[category]
    info['big_logo'] = CupCategoryBigLogoMap[category]
    info['reward_xml'] = CupCategoryRewardMap[category]
    info['end_reg_time'] = end_reg_time
    info['match_time'] = datetime.strptime(end_reg_time, '%Y-%m-%d %H:%M') + timedelta(days=1)
    info['cup_ladder'] = "%sCupLadder/%s/" % (DOMAIN, datetime.now().strftime("%Y%m"))
    info['ticket_category'] = CupCategoryTicketCategoryMap.get(category, 0)
    info['capacity'] = CupCategoryCapacityMap.get(category, 0)
    info['coin'] = 0
        
    for level, count in CupCategoryLevelCountMap[category].iteritems():
        for _ in range(count):
            info['levels'] = level
            info['requirement_xml'] = CupCategoryRequirementMap[category] % level
            insert_cup(info)

def get_cup_list(page, pagesize, category):
    """获取街球杯赛"""
    session = Session()
    total = session.query(Cup).filter(Cup.category==category).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Cup).filter(Cup.category==category).order_by(Cup.cupid).offset(index).limit(pagesize).all()
    return total, infos

def get_cup_table_toarrage():
    """得到可以安排赛程的杯赛"""
    cursor = connection.cursor()
    try:
        sql = "exec GetCupTableToArrage"
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_alive_reg_table_by_cupid(cupid):
    """得到某个杯赛存活球队列表"""
    cursor = connection.cursor()
    try:
        sql = "exec GetAliveRegTableByCupID %s" % cupid
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_code_by_regid(regid, code):
    """设置杯赛报名code"""
    cursor = connection.cursor()
    try:
        sql = "exec SetCodeByRegID %s, '%s'" % (regid, code)
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_round_by_cupid(cupid, round):
    """设置杯赛轮数"""
    cursor = connection.cursor()
    try:
        sql = "exec SetRoundByCupID %s, %s" % (cupid, round)
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_status_by_cupid(cupid, status):
    """设置杯赛状态"""
    cursor = connection.cursor()
    try:
        sql = "exec SetStatusByCupID %s, %s" % (cupid, status)
        print sql
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_reg_by_cupid_end_round(cupid, round):
    """得到某轮存活球队"""
    cursor = connection.cursor()
    try:
        sql = "exec GetRegByCupIDEndRound %s, %s" % (cupid, round)
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_reg_table_by_cupid_deadround(cupid, round):
    """得到某轮挂的球队"""
    cursor = connection.cursor()
    try:
        sql = "exec GetRegTableByCupIDDeadRound %s, %s" % (cupid, round)
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def reward_cup_by_clubid(clubid, cupid, round, money, score):
    """杯赛奖励"""
    cursor = connection.cursor()
    try:
        sql = "exec RewardCupByClubID %s, %s, %s, %s, %s" % (clubid, cupid, round, money, score)
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_cup_champion(cupid, user_id, club_name):
    """设置自定义杯赛冠军"""
    cursor = connection.cursor()
    try:
        sql = "exec SetChampion %s, %s, '%s'" % (cupid, user_id, club_name.encode("gb2312"))
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close() 
        
def get_cup_match_by_gaincode_cupid(cupid, gain_code):
    """根据GainCode和杯赛ID获取比赛"""
    cursor = connection.cursor()
    try:
        sql = "SELECT * FROM BTP_CupMatch WHERE GainCode = '%s' AND CupID = %s" % (gain_code, cupid)
        cursor.execute(sql)
        return cursor.fetchone()
    except:
        log_execption()
        raise
    finally:
        connection.close() 
        
def get_run_cuptable():
    """得到可以可以打下一轮的杯赛"""
    cursor = connection.cursor()
    try:
        sql = "SELECT *,  convert(text,RewardXML) as RewardXML FROM BTP_Cup WHERE Status=1 AND MatchTime<'%s' ORDER BY MatchTime ASC " % datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def day_add_cup():
    """每日刷新创建杯赛"""
    for _ in range(2):
        for category in (21, 22, 23, 24):
            hours = random.randint(30, 33)
            add_cup(category, datetime.strftime(datetime.now() + timedelta(hours=hours), '%Y-%m-%d %H:%M'))

if __name__ == "__main__":
    #add_cup(23, datetime.strftime(datetime.now() + timedelta(hours=32), '%Y-%m-%d %H:%M'))
    day_add_cup()
    