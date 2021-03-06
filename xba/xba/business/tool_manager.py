#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection
from xba.common import log_execption

def assign_tool_id_by_category_ticket(userid, category, ticket_category):
    """根据tool类型，ticket,分配tool"""
    cursor = connection.cursor()
    try:
        sql = "SELECT ToolID FROM BTP_Tool WHERE Category=%s AND TicketCategory=%s" % (category, ticket_category)
        cursor.execute(sql)
        info = cursor.fetchone()
        if not info:
            print "not this tool"
            return
        
        tool_id = info["ToolID"]
        
        sql = "INSERT INTO BTP_ToolLink (ToolID,UserID,Amount,ExpireTime) VALUES (%s, %s, 1, DATEADD(Day,27,GETDATE()))" 
        sql = sql % (tool_id, userid)
        cursor.execute(sql)
        
    except:
        log_execption()
    finally:
        connection.close()
        
        
def assign_tool(user_id, tool_id, amount, type=1):
    """发放道具"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC GiftTool %s, %s, %s, %s" % (user_id, tool_id, amount, type))
    finally:
        cursor.close()
        
def delete_expired_tool():
    """删除过期道具"""
    cursor = connection.cursor()
    try:
        cursor.execute("EXEC DeleteExpiredTool")
    finally:
        cursor.close()
        
def get_back_card():
    cursor = connection.cursor()
    try:
        cursor.execute("select * from btp_toollink where toolid = 27")
        infos = cursor.fetchall()
        for info in infos:
            userid = info["UserID"]
            amount = info["Amount"]
            toollinkid = info["ToolLinkID"]
            if amount > 2:
                assign_tool(userid, 36, amount - 2)
                sql = "update btp_toollink set amount = 2 where toollinkid = %s" % toollinkid
                print sql
                cursor.execute(sql)
    finally:
        cursor.close()
        
def assign_df():
    cursor = connection.cursor()
    try:
        cursor.execute("select * from btp_account where activetime > dateadd(hh, -24, getdate())")
        infos = cursor.fetchall()
        for info in infos:
            userid = info["UserID"]
            nickname = info["NickName"]
            assign_tool(userid, 36, 10)
            sql = u"EXEC AddMessage '','秘书报告', '%s', '恭喜您 获得10个臭豆腐 '" % nickname
            sql = sql.encode("gbk")
            cursor.execute(sql)
    finally:
        cursor.close()
        
def clear_xba_box():
    """清除臭豆腐"""
    cursor = connection.cursor()
    try:
        cursor.execute("DELETE FROM BTP_ToolLink WHERE ToolID = 36")
    finally:
        cursor.close()
        
if __name__ == "__main__":
    #clear_xba_box()
    pass