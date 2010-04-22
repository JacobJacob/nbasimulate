#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.common.db import connection
from gba.common import exception_mgr

def main():
    cursor = connection.cursor()
    try:
        cursor.execute('update training_center set status=1 where finish_time<=now() and status=0')
    except:
        exception_mgr.on_except()
    finally:
        cursor.close()
        
        
    
        
if __name__ == '__main__':
    main()
        