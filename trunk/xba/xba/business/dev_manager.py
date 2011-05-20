#!/usr/bin/python
# -*- coding: utf-8 -*-

import pymssql

def get_dev_clubs(devcode):
    """根据devcode获取所有俱乐部id"""
    conn = pymssql.connect(host='127.0.0.1', user='BTPAdmin', password='BTPAdmin123', database='NewBTP', as_dict=True)
    try:
        cursor = conn.cursor()
        cursor.execute("select clubid from btp_dev where devcode = %s ", devcode)
        return cursor.fetchall()
    finally:
        conn.close()
        
        
if __name__ == "__main__":
    infos = get_dev_clubs("0000000")
    for info in infos:
        print info