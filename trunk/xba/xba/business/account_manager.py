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

def add_invitec_code(count):
    """增加邀请码"""
    session = Session()
    for _ in range(int(count)):
        invite_code = InviteCode()
        invite_code.code = create_code()
        invite_code.status = InviteCodeStatus.NEW
        invite_code.created_time = datetime.now()
        invite_code.updated_time = datetime.now()
        session.add(invite_code)
    session.commit()
    
    
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
        
if __name__ == "__main__":
    print create_code()