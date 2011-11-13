#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime

from xba.common import log_execption
from xba.common.sqlserver import connection

from xba.common.reward import Reward

def get_devcup_table_toarrage():
    """得到可以安排赛程的杯赛"""
    cursor = connection.cursor()
    try:
        sql = "exec GetDevCupTableToArrage"
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_reg_by_devcupid_end_round(devcupid, round):
    """得到某轮存活球队"""
    cursor = connection.cursor()
    try:
        sql = "exec GetRegByDevCupIDEndRound %s, %s" % (devcupid, round)
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_place_by_round(total_round, end_round):
    """根据轮次获取名次1: 第一 2: 第二 3: 第三，第四, 4:第五-第八
    @param total_round: 总的轮数
    @param end_round: 被淘汰的轮数  
    """
    if end_round == 100:
        return 1
    diff_round = total_round - end_round
    return diff_round + 1
        
def reward_devcup_by_clubid(devcupid):
    """自定义杯赛奖励"""
    cursor = connection.cursor()
    try:
        cursor.execute("select *, convert(text,RewardXML) as RewardXML, Round from btp_devcup where devcupid = %s " % devcupid)
        devcup_info = cursor.fetchone()
        reward_xml = devcup_info["RewardXML"]
        total_round = devcup_info["Round"]
        
        reward = Reward(reward_xml, round_key="place")
        rounds = [i for i in range(total_round + 1)]
        rounds.append(100)
        for round in rounds:
            sql = "SELECT * FROM BTP_DevCupReg WHERE DevCupID=%s AND DeadRound=%s ORDER BY BaseCode DESC" % (devcupid, round)
            cursor.execute(sql)
            alive_clubs = cursor.fetchall()
            if not alive_clubs:
                continue
            
            place = get_place_by_round(total_round, round)
            reward_info = reward.get_reward(place)
            print "reward info for place:%s[%s]" % (place, reward_info)
            for alive_club in alive_clubs:
                club_id = alive_club["ClubID"]
                wealth = reward_info.get("wealth")
                
                if wealth > 0 or round == 100:
                    sql = "exec RewardDevCupByClubID %s, %s, %s, %s" % (club_id, devcupid, round, wealth.wealth)
                    print sql
                    cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()  
        
def set_devcup_champion(devcupid, user_id, club_name):
    """设置自定义杯赛冠军"""
    cursor = connection.cursor()
    try:
        sql = "exec SetDevCupChampion %s, %s, '%s'" % (devcupid, user_id, club_name.encode("gbk"))
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()     
    
def set_devclub_dead_round(devcupid, club_id, round):
    """设置报名表的被淘汰轮次"""
    cursor = connection.cursor()
    try:
        sql = "exec SetDevClubDeadRound %s, %s, %s" % (devcupid, club_id, round)
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()        
        
def get_run_devcuptable():
    """得到可以可以打下一轮的杯赛"""
    cursor = connection.cursor()
    try:
        sql = "exec GetRunDevCupTable '%s'" % datetime.now().strftime("%Y-%m-%d %H:%M:%S")
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_alive_reg_table_by_devcupid(devcupid):
    """得到某个杯赛存活球队列表"""
    cursor = connection.cursor()
    try:
        sql = "exec GetAliveRegTableByDevCupID %s" % devcupid
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_round_by_devcupid(devcupid, round):
    """设置杯赛轮数"""
    cursor = connection.cursor()
    try:
        sql = "exec SetRoundByDevCupID %s, %s" % (devcupid, round)
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def set_status_by_devcupid(devcupid, status):
    """设置杯赛状态"""
    cursor = connection.cursor()
    try:
        sql = "exec SetStatusByDevCupID %s, %s" % (devcupid, status)
        print sql
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()
        
def get_devcup_match_by_gaincode_devcupid(devcupid, gain_code):
    """根据GainCode和杯赛ID获取比赛"""
    cursor = connection.cursor()
    try:
        sql = "SELECT * FROM BTP_DevCupMatch WHERE GainCode = '%s' AND DevCupID = %s" % (gain_code, devcupid)
        cursor.execute(sql)
        return cursor.fetchone()
    except:
        log_execption()
        raise
    finally:
        connection.close() 
    
def set_code_by_devregid(regid, code):
    """设置杯赛报名code"""
    cursor = connection.cursor()
    try:
        sql = "exec SetCodeByDevRegID %s, '%s'" % (regid, code)
        print sql
        cursor.execute(sql)
    except:
        log_execption()
        raise
    finally:
        connection.close()

if __name__ == "__main__":
    a = u"榮光の團 "
    reward_devcup_by_clubid(103)