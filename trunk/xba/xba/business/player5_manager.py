#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Player5
from datetime import datetime, timedelta
from xba.common import log_execption

def create_player(count, category, hours, now_point, max_point):
    """创建球员"""
    end_bid_time = datetime.now() + timedelta(hours=hours)
    cursor = connection.cursor()
    try:
        sql = "exec CreatePlayer5 %s, %s, '%s', %s, %s" % (count, category, end_bid_time.strftime("%Y-%m-%d %H:%M:%S"), now_point, max_point)
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def get_trialing_player():
    """获取正在试训的球员"""
    cursor = connection.cursor()
    try:
        sql = "select * from btp_player5 where category=3 and clubid > 0"
        cursor.execute(sql)
        return cursor.fetchall()
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def un_set_trial_player(club_id, player_id):
    """试训球员离队"""
    cursor = connection.cursor()
    try:
        sql = "exec UnSetTrialPlayer %s, %s" % (club_id, player_id)
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def player_list(page, pagesize, category):
    """获取职业球员"""
    session = Session()
    total = session.query(Player5).filter(Player5.category==category).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(Player5).filter(Player5.category==category).order_by(Player5.clubid).offset(index).limit(pagesize).all()
    return total, infos

def recover_power5():
    """职业球员体力恢复，将当天训练点清空"""
    cursor = connection.cursor()
    try:
        sql = "exec RecoverPower5"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def clear_player5_stas():
    """将受伤球员的上一场数据统计在赛前初始化"""
    cursor = connection.cursor()
    try:
        sql = "exec ClearPlayer5Stas"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()  
        
def update_season_mvp_value():
    """更新球员的MVP值"""
    cursor = connection.cursor()
    try:
        sql = "exec UpdateSeasonMVPValue"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()
        
def reset_all_player_shirt():
    """重置球员的球衣销售量"""
    cursor = connection.cursor()
    try:
        sql = "exec ResetAllPlayerShirt"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def reset_all_player_pop():
    """重置球员受欢迎程度"""
    cursor = connection.cursor()
    try:
        sql = "exec ResetAllPlayerPop"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def add_player_age():
    """增加球员年龄"""
    cursor = connection.cursor()
    try:
        sql = "exec AddPlayerAge"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def recover_healthy5():
    """职业球员受伤恢复以及事件更新"""
    cursor = connection.cursor()
    try:
        sql = "exec RecoverHealthy5"
        cursor.execute(sql)
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
        
def player5_holiday():
    """赛季末时，恢复所有球员伤病，心情，体力"""
    cursor = connection.cursor()
    try:
        sql = "exec Player5Holiday"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close()    
        
def clear_player5_season():
    """清理球员赛季数据"""
    cursor = connection.cursor()
    try:
        sql = "exec ClearPlayer5Season"
        cursor.execute(sql)
    except:
        log_execption()
    finally:
        connection.close() 
    
SQL = "UPDATE BTP_Player5 SET Ability=(Speed+Jump+Strength+Stamina+Shot+Point3+Dribble+Pass+Rebound+Steal+Block+Attack+Defense+Team)/14 WHERE PlayerID=%s"    
        
ABILITYS = ["Speed","Jump","Strength","Stamina","Shot","Point3","Dribble","Pass","Rebound","Steal","Block","Attack","Defense","Team"]
        
def update_player5_category3():
    import random
    cursor = connection.cursor()
    try:
        sql = "select * from btp_player5 where category=3"
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            for ABILITY in ABILITYS:
                ability = info["%sMax" % ABILITY]
                ability = ability * random.randint(5, 9) / 10
                sql = "UPDATE BTP_Player5 SET %s=%s" % (ABILITY, ability)
                cursor.execute(sql)
            sql = SQL % info["PlayerID"] 
            cursor.execute(sql)
            
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
    
if __name__ == "__main__":
    update_player5_category3()

