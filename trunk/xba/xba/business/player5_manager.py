#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.model import Player5
from datetime import datetime, timedelta
from xba.common import log_execption
import traceback

REG_CATEGORY_3 = [
[135, 55, 5],
[130, 55, 10],       
[125, 55, 10],
[120, 55, 80],
[115, 55, 100],
[110, 55, 120],       
]

REG_CATEGORY_4 = [
[130, 55, 5],
[125, 55, 40],       
[120, 55, 50],
[115, 55, 80],
[110, 55, 120],
[105, 55, 240],       
]

def betch_create_player(category=3):
    """刷选透球员"""
    if category == 3:
        for reg in REG_CATEGORY_3:
            create_player(reg[2], 3, 48, reg[1], reg[0])  
        update_player5_category3()
    elif category == 4:
        for reg in REG_CATEGORY_4:
            create_player(reg[2], 3, 20, reg[1], reg[0])  

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
        
def get_player5_pre_retire():
    """获取要退役的球员"""
    cursor = connection.cursor()
    try:
        sql = "exec Player5PreRetire"
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        connection.close()
        
def clear_train_point():
    """清空职业训练员"""
    cursor = connection.cursor()
    try:
        sql = "Update BTP_Player5 Set TrainPoint = 0"
        cursor.execute(sql)
    finally:
        cursor.close()
        
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
        
def point3_match_handle():
    """处理三分大赛"""
    cursor = connection.cursor()
    try:
        cursor.execute("select * from btp_point3match where status in (1, 2) and matchtime < getdate()")
        infos = cursor.fetchall()
        for info in infos:
            playerid = info["PlayerID"]
            status = 0 if info["Status"] == 1 else 3
            sql = "exec StartPoint3Match %s, %s" % (playerid, status)
            print sql
            cursor.execute(sql)
    except:
        print traceback.format_exc()
    finally:
        cursor.close()
        
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
        
def delete_player5(player_id):
    """删除年轻球员 """
    cursor = connection.cursor()
    try:
        sql = "exec DeletePlayer5 %s" % player_id
        cursor.execute(sql)
    finally:
        connection.close()
        
def clean_player5_pLink():
    """清除旧的查看潜力那些东西"""
    cursor = connection.cursor()
    try:
        sql = "delete from BTP_Player5PLink where category = 2 and CreateTime < dateadd(dd, -3, getdate())"
        cursor.execute(sql)
    finally:
        connection.close()
        
def player5_aging():
    """球员老化 """
    cursor = connection.cursor()
    try:
        sql = "exec Player5Aging"
        cursor.execute(sql)
    finally:
        connection.close()
        
def player_pre_retire():
    """球员退役 """
    cursor = connection.cursor()
    try:
        sql = "exec PlayerPreRetire"
        cursor.execute(sql)
    finally:
        connection.close()
        
def set_player5_hardness_leadship():
    """球员毅力，领导力更新"""
    cursor = connection.cursor()
    try:
        sql = "exec SetPlayer5HardnessLeadship"
        cursor.execute(sql)
    finally:
        connection.close()
        
def add_played_year():
    """增加球员球龄"""
    cursor = connection.cursor()
    try:
        sql = "exec AddPlayedYear"
        cursor.execute(sql)
    finally:
        connection.close() 
        
def update_player5_ability():
    """更新球员能力"""
    cursor = connection.cursor()
    try:
        sql = "exec UpdatePlayer5Ability"
        cursor.execute(sql)
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
                if ABILITY in ("Attack","Defense","Team"):
                    continue
                ability = info["%sMax" % ABILITY]
                if ability > 900:
                    ability = ability * random.randint(40, 99) / 100
                elif ability > 800:
                    ability = ability * random.randint(50, 99) / 100    
                elif ability > 700:
                    ability = ability * random.randint(60, 99) / 100    
                elif ability > 600:
                    ability = ability * random.randint(70, 99) / 100    
                else:
                    ability = ability * random.randint(80, 99) / 100
                
                sql = "UPDATE BTP_Player5 SET %s=%s WHERE PlayerID=%s" % (ABILITY, ability, info["PlayerID"])
                print sql
                cursor.execute(sql)
            sql = SQL % info["PlayerID"] 
            cursor.execute(sql)
            
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def update_player5_hight_yishi():
    return
    import random
    cursor = connection.cursor()
    try:
        sql = "select PlayerID, ClubID, team, defense, attack from btp_player5 where team + defense + attack > 1500 and endbidtime > '2011-07-25'"
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            print "-" * 20
            team, defense, attack, playerid, clubid = info["team"], info["defense"], info["attack"], info["PlayerID"], info["ClubID"] 
            now_team = 200 + random.randint(0, 100)
            now_defense = 200 + random.randint(0, 100)
            now_attack = 200 + random.randint(0, 100)
            if now_team > team:
                now_team = team
            if now_defense > defense:
                now_defense = defense
            if now_attack > attack:
                now_attack = attack
            sql = "UPDATE BTP_Player5 SET team=%s, defense=%s, attack=%s WHERE PlayerID=%s" % (now_team, now_defense, now_attack, playerid)
            print sql
            cursor.execute(sql)
            sql = SQL % info["PlayerID"] 
            print sql
            cursor.execute(sql)
            money = ((team + defense + attack) - (now_team + now_defense + now_attack)) / 3 * 10000
            sql = "select userid from btp_club where clubid=%s" % clubid
            print sql
            cursor.execute(sql)
            userid = cursor.fetchone()["userid"]
            sql = "update btp_account set money=money+%s where userid=%s" % (money, userid)
            print sql
            cursor.execute(sql)
            
    except Exception, e:
        raise
        print "error"
    finally:
        connection.close()
        
def view_player5_category3():
    cursor = connection.cursor()
    try:
        #cursor.execute("update btp_player3 set Blockmax = 670, Passmax = 654, Strengthmax=647, Jumpmax=669,Dribblemax=589,Stealmax=560 where name = 'ABC'")
        sql = "select * from btp_player5 "
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            id = info["PlayerID"]
           
            total_a, total_b, total_max_a, total_max_b, total_max_c = 0, 0, 0, 0, 0
            for ABILITY in ABILITYS:    
                ability_max = info["%sMax" % ABILITY]
                ability = info[ABILITY]
                #print ABILITY, ":", ability, ability_max
                if ABILITY not in ("Attack","Defense","Team"):
                    total_a += ability
                    total_max_a += ability_max
                    total_max_b += ability_max
                else:
                    total_max_b += ability
                
                total_b += ability
                total_max_c += ability_max
                
                #total1 += ability
                
                
            print "-" * 10
            print "id:", id, "Age:", info["Age"], info["Name"]
            print "当前算20意识", (total_a + 600) / 14,
            print "最大算20意识", (total_max_a + 600) / 14,
            print "当前包意识", total_b / 14,
            print "最大包当前意识 ", total_max_b / 14,
            print "最大包意识 ", total_max_c / 14
            
            #print total1 / 30
            a = random.randint(200, 300)
            b = random.randint(200, 300)
            c = random.randint(200, 300)
            #sql = "update btp_player5 set attack=%s, Defense=%s, team=%s where playerid = %s" % (a, b, c, id)
            #cursor.execute(sql)
            #print info["Age"], info["Ability"], (total2 + 600) / 14, total1 / 14, total2 / 11
            sql = SQL % id
            #print sql
            #cursor.execute(sql)
                        
            
    except Exception, e:
        a = e.message.decode("gbk")
        print a
    finally:
        connection.close()
        
def view_all_player():
    cursor = connection.cursor()
    try:
        sql = "select * from btp_player5 where category = 4"
        cursor.execute(sql)
        infos = cursor.fetchall()
        print len(infos)
        for info in infos:
            print info["PlayerID"]
            print info["Name"]

    finally:
        connection.close()

def clean_dirty_char():
    """"""
    cursor = connection.cursor()
    try:
        sql = u"""update btp_player5 set name = replace(name, '1', '1')"""
        print sql
        cursor.execute(sql)
        sql = u"update btp_club set name = replace(name, '', '  ')"
        cursor.execute(sql)
        sql = u"update btp_club set mainxml = replace(mainxml, '', '  ')"
        cursor.execute(sql)
    finally:
        connection.close()

if __name__ == "__main__":
    #view_player5_category3()
    #clean_dirty_char()
    point3_match_handle()