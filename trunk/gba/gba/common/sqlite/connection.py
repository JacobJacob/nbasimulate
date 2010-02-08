#-*- coding:utf-8 -*-
import os
import sqlite3

from cursor import Cursor

PROJECT_ROOT = os.path.dirname(os.path.dirname(os.path.dirname(os.path.realpath(__file__))))

class Connection():
    
    def __init__(self, data_file):
        """"""
        self._conn = sqlite3.connect('%s%s%s' % (PROJECT_ROOT, '/data/sqlite/', data_file))
        self._conn.row_factory = sqlite3.Row
        self._conn.isolation_level = None #set to auto commit
    
    def cursor(self):
        """"""
        return Cursor(self._conn.cursor(), self)
    
def main():
    connection = Connection('spiderproxy.s3db')
    cursor = connection.cursor()
    data = {'date': '2009-08-09', 'trans': u'卖', 'symbol': 'rhat', 'qty': 20, 'price': 23.4}
    
    
    for i in range(10000):
        cursor.insert(data, 'stocks')
    
#    sql = 'select trans, symbol from stocks'
#    
#    rows = cursor.fetchall(sql)
#    for row in rows:
#        print '%s,%s' % (row['trans'], row['symbol'])
#        
#    sql = 'delete from stocks where price=?'
#    
#    cursor.execute(sql, [23.4])
#    
#    sql = 'select trans, symbol from stocks'
#    
#    rows = cursor.fetchall(sql)
#    for row in rows:
#        print '%s,%s' % (row['trans'], row['symbol'])
        
if __name__ == '__main__':
    main()
    