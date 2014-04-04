#!/bin/bash

rdate -s time.iastate.edu

#memcached -d -m 1024 -p 11212 -u root

# Replace these three settings.
PROJDIR="/data/apps/xba/xba"
PIDFILE="$PROJDIR/fastcgi.pid"

python $PROJDIR/web/manage.py runfcgi method=threaded maxrequests=1000 \
	maxspare=5 minspare=2 host=127.0.0.1 port=8090 pidfile=$PIDFILE

echo "python manage.py"
ps -eLf | grep python |grep manage.py |wc -l
ps -eLf | grep nginx
ps -eLf | grep memcached
