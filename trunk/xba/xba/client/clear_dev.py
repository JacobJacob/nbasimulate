#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.sqlserver import connection

def main():
    cursor = connection.cursor()
    try:
        cursor.execute("SELECT * FROM (SELECT COUNT(1) AS TOTAL, DEVCODE, LEVELS FROM BTP_DEV GROUP BY DEVCODE, LEVELS)TEMP_TABLE  WHERE TOTAL > 14 ")
        infos = cursor.fetchall()
        for info in infos:
            print info["DEVCODE"]
            cursor.execute("select * from btp_dev where devcode='%s'" % info["DEVCODE"])
            dev_infos = cursor.fetchall()
            print len(dev_infos)
            i = 0
            for dev_info in dev_infos:
                i += 1
                if i > 14:
                    cursor.execute("delete from btp_dev where devid='%s'" % dev_info["DevID"])
    finally:
        cursor.close()       

if __name__ == "__main__":
    main()