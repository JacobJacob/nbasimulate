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
        cursor.execute("select UserID from btp_account")
        infos = cursor.fetchall()
        for info in infos:
            print info
            cursor.execute("ProvideChooseCard %s, %s, %s" % (info["UserID"], 5, 20))
    finally:
        cursor.close()
        
def delete_online_table():
    """清空在线表"""
    cursor = connection.cursor()
    try:
        cursor.execute("exec DeleteOnlineTable")
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
            print userid, money
            cursor.execute("update btp_account set money=money+%s where userid=%s" % (money, userid))
    finally:
        cursor.close()
        
if __name__ == "__main__":
    update_account()