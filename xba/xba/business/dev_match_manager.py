#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection
from xba.common import log_execption

def get_round_dev_matchs(last_id, round):
    """获取游戏行"""
    cursor = connection.cursor()
    try:
        sql = "exec GetMatchTableByRound %s, %s" % (last_id, round)
        print sql
        cursor.execute(sql)
        return cursor.fetchall()
    except Exception, e:
        a = e.message.decode("gbk")
        raise "error"
    finally:
        connection.close()

def delete_all_matches():
    """删除所有联赛比赛"""
    cursor = connection.cursor()
    try:
        sql = "exec DeleteAllMatches "
        cursor.execute(sql)
    except:
        log_execption
        raise
    finally:
        connection.close()

if __name__ == "__main__":
    print get_round_dev_matchs(0, 1)    
