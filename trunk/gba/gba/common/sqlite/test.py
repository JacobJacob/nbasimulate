#-*- coding:utf-8 -*-
import os
import sqlite3

PROJECT_ROOT = os.path.dirname(os.path.dirname(os.path.dirname(os.path.realpath(__file__))))

data_file = os.path.join(PROJECT_ROOT, 'data/sqlite/spiderproxy.s3db')
print data_file
conn = sqlite3.connect(data_file)
conn.row_factory = sqlite3.Row


c = conn.cursor()

# Create table
#c.execute('''create table stocks
#(date text, trans text, symbol text,
# qty real, price real)''')

c.execute("""select key_word, title from email""")

rows = c.fetchall()

for row in rows:
    print row['title']

