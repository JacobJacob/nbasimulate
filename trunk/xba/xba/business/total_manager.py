#!/usr/bin/python
# -*- coding: utf-8 -*-

from datetime import datetime, timedelta

from xba.common.sqlserver import connection
from xba.common import log_execption
from xba.common.mailutil import send_to

def total():
    """统计每天的情况"""
    now = datetime.now()
    start = now - timedelta(days=1)
    end = now
    start = start.strftime("%Y-%m-%d 00:00:00")
    end = end.strftime("%Y-%m-%d 00:00:00")
    cursor = connection.cursor()
    try:
        
        total_msg = ""
        
        sql = "select count(*) as total from btp_account where createtime >= '%s' and createtime <= '%s'" % (start, end)
        cursor.execute(sql)
        info = cursor.fetchone()
        day_register_count = info["total"]
        msg = "昨天新注册%s人" % day_register_count
        total_msg += "%s\n" % msg
        
        sql = "select count(*) as total from btp_account where activetime >= '%s' and createtime <= '%s'" % (start, end)
        cursor.execute(sql)
        info = cursor.fetchone()
        active_user_count = info["total"]
        msg = "昨天活跃人数:%s" % active_user_count
        total_msg += "%s\n" % msg
        
        sql = "select count(*) as total from btp_player5plink where type=3 and createtime >= '%s' and createtime <= '%s'" % (start, end)
        cursor.execute(sql)
        info = cursor.fetchone()
        use_show_privite_count = info["total"]
        msg = "昨天使用街球潜力道具%s次" % use_show_privite_count
        total_msg += "%s\n" % msg
        
        sql = "select toolid, amountinstock from btp_tool where toolid in (27, 28, 33)"
        cursor.execute(sql)
        infos = cursor.fetchall()
        for info in infos:
            toolid = info["toolid"]
            amountinstock = info["amountinstock"]
            if toolid == 27:
                msg = "到目前为止总共卖出选拔卡%s张" % (1000 - amountinstock)
            elif toolid == 28:
                msg = "到目前为止总共卖出会员卡%s张" % (1000 - amountinstock)
            elif toolid == 33:
                msg = "到目前为止总共卖出双倍训练卡%s张" % (10000 - amountinstock)
            
            total_msg += "%s\n" % msg
            
        title = "无道篮球经理每日统计[%s--%s]" % (start, end)
        print total_msg
        send_to(title, total_msg)
            
    except:
        log_execption()
    finally:
        cursor.close()
        
if __name__ == "__main__":
    total()