#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime, timedelta

from xba.config import CUP_LADDER
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
        sql = sql.encode("gbk")
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()

def add_cup(category, end_reg_time):
    setid = get_max_setid() + 1
    info = {'setid': setid}
    if category in (21, 22):
        info['category'] = 2
    elif category in (23,):
        info['category'] = 1
        
    info['name'] = CupCategoryMap[category]
    info['introduction'] = CupCategoryDescMap[category]
    info['money_cost'] = CupCategoryMoneyCostMap[category]
    info['small_logo'] = CupCategorySmallLogoMap[category]
    info['big_logo'] = CupCategoryBigLogoMap[category]
    info['requirement_xml'] = CupCategoryRequirementMap[category]
    info['reward_xml'] = CupCategoryRewardMap[category]
    info['end_reg_time'] = end_reg_time
    info['match_time'] = datetime.strptime(end_reg_time, '%Y-%m-%d %H:%M') + timedelta(days=1)
    info['cup_ladder'] = CUP_LADDER
    info['ticket_category'] = CupCategoryTicketCategoryMap.get(category, 0)
    info['capacity'] = CupCategoryCapacityMap.get(category, 0)
    info['coin'] = 0
        
    for level, count in CupCategoryLevelCountMap[category].iteritems():
        for _ in range(count):
            info['levels'] = level
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
        
if __name__ == "__main__":
    add_cup(21, datetime.strftime(datetime.now() + timedelta(days=1), '%Y-%m-%d %H:%M'))
    