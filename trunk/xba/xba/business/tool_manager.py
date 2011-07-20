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