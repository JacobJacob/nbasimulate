#!/usr/bin/python
# -*- coding: utf-8 -*-


from xba.common.sqlserver import connection

def update_account_dev_code(user_id, dev_code):
    """更新俱乐部devcode"""
    cursor = connection.cursor()
    try:
        cursor.execute("")
        cursor.commit()
    finally:
        cursor.close()
        
        
if __name__ == "__main__":
    pass