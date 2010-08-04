#!/bin/bash
export LD_LIBRARY_PATH=/usr/local/lib/mysql:$LD_LIBRARY_PATH
sudo sh stop.sh

memcached -d -m 1024 -p 11211 -u sxwailyc

# Replace these three settings.
PROJDIR="/data/apps/dtspider"
PIDFILE="$PROJDIR/fastcgi.pid"
SOCKET="$PROJDIR/fastcgi.sock"

python2.5 $PROJDIR/web/manage.py runfcgi method=prefork maxrequests=1000 \
	maxspare=50 minspare=25 host=127.0.0.1 port=8088 pidfile=$PIDFILE


sudo /usr/local/sbin/nginx -c $PROJDIR/nginx.conf

echo "python manage.py"
ps -eLf | grep python |grep manage |wc -l
ps -eLf | grep nginx
ps -eLf | grep memcached
