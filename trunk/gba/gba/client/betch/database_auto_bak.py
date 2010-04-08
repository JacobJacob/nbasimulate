#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import traceback


def main():
    try:
        os.popen('mysqldump gba -u gba > mysql.sql')
    except:
        print traceback.format_exc()
    
if __name__ == '__main__':
    main()
