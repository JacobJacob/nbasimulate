#!/bin/bash

sudo sh stop.sh

memcached -d -m 1024 -p 11212 -u webauth

# Replace these three settings.
PROJDIR="/data/webauth"
PIDFILE="$PROJDIR/fastcgi.pid"
SOCKET="$PROJDIR/fastcgi.sock"

python $PROJDIR/web/manage.py runfcgi method=prefork maxrequests=1000 \
	maxspare=50 minspare=25 host=127.0.0.1 port=8088 pidfile=$PIDFILE


sudo /usr/local/nginx/sbin/nginx -c $PROJDIR/nginx.conf

echo "python manage.py"
ps -eLf | grep python |grep manage |wc -l
ps -eLf | grep nginx
ps -eLf | grep memcached