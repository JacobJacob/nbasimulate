#!/usr/bin/python
# -*- coding: utf-8 -*-

import random
from datetime import datetime

from xba.common.sqlserver import connection
from xba.common.orm import Session
from xba.common.constants.account import InviteCodeStatus
from xba.model import InviteCode

def update_account_dev_code(user_id, dev_code):
    """更新俱乐部devcode"""
    cursor = connection.cursor()
    try:
        cursor.execute("")
        cursor.commit()
    finally:
        cursor.close()
        
def get_invite_code_list(page, pagesize, status):
    """获取邀请码列表"""
    session = Session()
    total = session.query(InviteCode).filter(InviteCode.status==status).count()
    index = (page - 1) * pagesize
    infos = None
    if total > 0:
        infos = session.query(InviteCode).filter(InviteCode.status==status).order_by(InviteCode.id).offset(index).limit(pagesize).all()
    return total, infos

def add_invite_code(count=20, status=InviteCodeStatus.NEW):
    """增加邀请码"""
    session = Session()
    codes = []
    for _ in range(int(count)):
        invite_code = InviteCode()
        code = create_code()
        codes.append(code)
        invite_code.code = code
        invite_code.status = status
        invite_code.created_time = datetime.now()
        invite_code.updated_time = datetime.now()
        session.add(invite_code)
    session.commit()
    return codes
    
def assign_invite_code(code):
    """分配邀请码"""
    cursor = connection.cursor()
    try:
        cursor.execute("update BTP_InviteCode set Status = 2, UpdatedTime = getdate() where code = '%s'" % code)
    finally:
        cursor.close()
    
CODE = [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 
        'a', 'b', 'c', 'd', 'e', 'f' ,'g', 'h', 'i', 'j', 'k',
        'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z']
    
def create_code():
    """创建邀请码"""
    code = ""
    for _ in range(15):
        code = '%s%s' % (CODE[random.randint(0, 35)], code)
    return code

def assign_devchoose_card():
    """发放选秀卡"""
    cursor = connection.cursor()
    try:
        cursor.execute("select devcode from btp_dev where clubid > 0 group by devcode")
        infos = cursor.fetchall()
        for info in infos:
            user_id = info["UserID"]
            nickname = info["NickName"]
            print type(nickname)
            #nickname = nickname.decode("gbk")
            print type(nickname)
            cursor.execute("select 1 from btp_toollink where userid = %s and ToolID = 23" % user_id)
            info = cursor.fetchone()
            if info:
                print "has"
                continue
            print "assign"
            cursor.execute("EXEC ProvideChooseCard %s, %s, %s" % (user_id, 5, 20))
            sql = u"EXEC AddMessage '','秘书报告', '%s', '恭喜您 获得20轮选秀权,您可以在转会市场的职业选秀中选取你中意的一名球员 '" % nickname
            sql = sql.encode("gbk")
            cursor.execute(sql)
    finally:
        cursor.close()
        
def get_round(sort):
    """get round"""
    rand = random.randint(1, 30)
    value = 200 - sort * 15 + rand
    i = 1
    while i < 20:
        if value > 210:
            return i
        i += 1
        value += 10
    
    return i
            
def assign_devchoose_card_with_devsort():
    """发放选秀卡"""
    cursor = connection.cursor()
    try:
        cursor.execute("select devcode from btp_dev where clubid > 0 group by devcode")
        infos = cursor.fetchall()
        for info in infos:
            devcode = info["devcode"]
            sql = "select clubid from btp_dev where devcode = '%s' order by win asc, score asc" % devcode
            cursor.execute(sql)
            clubinfos = cursor.fetchall()
            for i, clubinfo in enumerate(clubinfos):
                club_id = clubinfo["clubid"]
                if club_id <= 0:
                    continue
                sql = "select userid from btp_club where clubid = %s" % club_id
                print sql
                cursor.execute(sql)
                user_id = cursor.fetchone()["userid"]
                cursor.execute("EXEC ProvideChooseCard %s, %s, %s" % (user_id, 5, get_round(i)))
    finally:
        cursor.close()
        
def assign_promotion_card():
    """发放提拔卡"""
    cursor = connection.cursor()
    try:
        cursor.execute("select UserID, NickName from btp_account")
        infos = cursor.fetchall()
        for info in infos:
            user_id = info["UserID"]
            cursor.execute("EXEC GiftTool %s, 27, 2, 1" % user_id)
    finally:
        cursor.close()
        
def delete_online_table():
    """清空在线表"""
    cursor = connection.cursor()
    try:
        cursor.execute("exec DeleteOnlineTable")
    finally:
        cursor.close()
        
def get_one_level_max_team(level):
    """获取某一等级最大综合的球员"""
    cursor = connection.cursor()
    try:
        sql = "select top 10 UserID from btp_account where len(DevCode)=%s order by TeamAbility Desc" % (level - 1)
        cursor.execute(sql)
        return cursor.fetchall()
    finally:
        cursor.close()
        
def delete_fri_match_msg():
    """清空在线聊天表"""
    cursor = connection.cursor()
    try:
        cursor.execute("delete from BTP_FriMatchMsg")
    finally:
        cursor.close()
        
def update_account():
    cursor = connection.cursor()
    try:
        sql = "select userid, sum(income)-sum(outcome) as money, 0 as test from BTP_DFinance d " \
                  "where category = 5 and (event = '球衣销售收入。' or  event = '主场球馆维护费用。' or event = '主场球馆维护费用。' or event = '主场饮料和食品销售收入。' ) group by userid"
        sql = sql.encode("gb2312")
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            userid, money = info["userid"], info["money"]
            cursor.execute("update btp_account set money=money+%s where userid=%s" % (money, userid))
    finally:
        cursor.close()
        
def update_team_ability():
    """发放提拔卡"""
    cursor = connection.cursor()
    try:
        cursor.execute("select UserID, NickName from btp_account")
        infos = cursor.fetchall()
        for info in infos:
            user_id = info["UserID"]
            cursor.execute("EXEC GiftTool %s, 27, 2, 1" % user_id)
    finally:
        cursor.close()
        
def remove_cupids_from_account(remove_cupid):
    """从用户的报名杯赛中去掉"""
    cursor = connection.cursor()
    try:
        cursor.execute("select UserID, CupIDS from btp_account")
        infos = cursor.fetchall()
        for info in infos:
            user_id = info["UserID"]
            cup_ids_str = info["CupIDS"]
            if not cup_ids_str:
                continue
            cup_ids = cup_ids_str.split("|")
            for cup_id in cup_ids:
                if remove_cupid == int(cup_id):
                    cup_ids.remove(cup_id)
            if not cup_ids:
                cup_ids = ["0"]
            new_club_id_str = "|".join(cup_ids)
            print cup_ids_str, new_club_id_str
            sql = "UPDATE BTP_Account SET CupIDs='%s' WHERE UserID=%s" % ("|".join(cup_ids), user_id)
            print sql
            #cursor.execute(sql)
    finally:
        cursor.close()
     
        
def get_not_active_users(interval_days=7):
    user_ids = []
    cursor = connection.cursor()
    try:
        cursor.execute("select top 15 UserID, ActiveTime from btp_account where ActiveTime < DATEADD(DAY,-%s,GETDATE()) order by TeamAbility desc" % interval_days)
        infos = cursor.fetchall()
        if infos:
            for info in infos:
                user_id = info["UserID"]
                user_ids.append(user_id)
    finally:
        cursor.close()
    return user_ids

def remove_finish_cup():
    cursor = connection.cursor()
    try:
        cursor.execute("select cupid from btp_cup where status=3")
        infos = cursor.fetchall()
        if infos:
            for info in infos:
                cupid = info["cupid"]
                print "remove", cupid
                remove_cupids_from_account(cupid)
    finally:
        cursor.close()

def append_not_finish_cup():
    cursor = connection.cursor()
    try:
        cursor.execute("select cupid from btp_cup where status <>3")
        infos = cursor.fetchall()
        if infos:
            for info in infos:
                cupid = info["cupid"]
                #print "append", cupid
                cursor.execute("select clubid from btp_cupreg where cupid=%s" % cupid)
                regs = cursor.fetchall()
                if not regs:
                    continue
                print len(regs)
                for reg in regs:
                    clubid = reg["clubid"]
                    print clubid
                    cursor.execute("select userid from btp_club where clubid=%s" % clubid)
                    club = cursor.fetchone()
                    userid = club["userid"]
                    print userid
                    cursor.execute("select cupids from btp_account where userid=%s" % userid)
                    account = cursor.fetchone()
                    cupids = account["cupids"]
                    print cupids
                    cup_ids = cupids.split("|")
                    cupid = str(cupid)
                    if cupid not in cup_ids:
                        print "not in"
                        cup_ids.append(cupid)
                        sql = "UPDATE BTP_Account SET CupIDs='%s' WHERE UserID=%s" % ("|".join(cup_ids), userid)
                        print sql
                        cursor.execute(sql)
                    else:
                        print "in"
                        
    finally:
        cursor.close()

     
if __name__ == "__main__":
    append_not_finish_cup()
    #remove_finish_cup()