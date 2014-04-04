#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime, timedelta

from xba.common.sqlserver import connection
from xba.common.stringutil import ensure_gbk, ensure_utf8

        
def add_guess(type, money_type, namea, nameb, hot, end_time_interval=24, show_time_interval=0):
    """添加竞猜"""
    cursor = connection.cursor()
    try:
        end_time = datetime.now() + timedelta(hours=end_time_interval)
        show_time = datetime.now() + timedelta(hours=show_time_interval)
        sql = u"AddGuess '%s', %s, '%s', '%s', %s, '%s', '%s'" % (type, money_type, namea, nameb, hot, end_time.strftime("%Y-%m-%d %H:%M:%S"), show_time.strftime("%Y-%m-%d %H:%M:%S"))
        sql = ensure_utf8(sql)
        cursor.execute(sql)
        msg_sql = u"EXEC SendSystemMsg '[%s]%s VS %s 的竞猜开始啦, <a href=\"Guess.aspx?Type=Guess\">我要下注</a>'" % (u"资" if money_type == 0 else u"币", namea, nameb)
        msg_sql = ensure_utf8(msg_sql)
        cursor.execute(msg_sql)
    finally:
        cursor.close()
        
def get_guess_list(has_result, page, pagesize=20):
    """获取竞猜"""
    cursor = connection.cursor()
    try:
        total, infos = 0, None
        sql = "GetGuessTableByHasResult %s, 1, %s, %s" % (has_result, page, pagesize)
        cursor.execute(sql)
        total = cursor.fetchone()[0]
        sql = "GetGuessTableByHasResult %s, 0, %s, %s" % (has_result, page, pagesize)
        cursor.execute(sql)
        infos = cursor.fetchall()
        return total, infos
    finally:
        cursor.close()
        
def set_guess_result(id, result_type, result_text):
    """设置竞猜结果"""
    cursor = connection.cursor()
    try:
        sql = u"Update Btp_Guess Set HasResult=1, ResultType=%s, ResultText='%s' WHERE GuessID=%s" % (result_type, result_text, id)
        sql = ensure_gbk(sql)
        return cursor.execute(sql)
    finally:
        cursor.close()

def guess_begin(id):
    """平盘"""
    cursor = connection.cursor()
    try:
        sql = "GuessBegin %s" % id
        return cursor.execute(sql)
    finally:
        cursor.close()
            
if __name__ == "__main__":
    add_guess('NBA', '火箭', '湖人', 1)
    pass
