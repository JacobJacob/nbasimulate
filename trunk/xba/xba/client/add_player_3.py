#!/usr/bin/python
# -*- coding: utf-8 -*-

import pymssql

def main():
    
    conn = pymssql.connect(host='127.0.0.1', user='BTPAdmin', password='BTPAdmin123', database='NewBTP', as_dict=True)
    cursor = conn.cursor()


if __name__ == "__main__":
    main()