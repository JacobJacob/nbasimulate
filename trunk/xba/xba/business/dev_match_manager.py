#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection

def get_round_dev_matchs(last_id, round):
    """获取游戏行"""
    cursor = connection.cursor()
    try:
        sql = "exec GetMatchTableByRound %s, %s" % (last_id, round)
        cursor.execute(sql)
        return cursor.fetchall()
    except Exception, e:
        a = e.message.decode("gbk")
        raise "error"
    finally:
        connection.close()


if __name__ == "__main__":
    print get_round_dev_matchs(0, 1)    
