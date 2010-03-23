#!/bin/bash

sudo killall nginx
killall memcached

# Replace these three settings.
PROJDIR="/data/apps/gba/gba"
PIDFILE="$PROJDIR/fastcgi.pid"
SOCKET="$PROJDIR/fastcgi.sock"

cd $PROJDIR
if [ -f $PIDFILE ]; then
    kill -9 `cat -- $PIDFILE`
    rm -f -- $PIDFILE
fi


echo "python manage.py"
ps -eLf | grep python |grep manage |wc -l
ps -eLf | grep nginx
ps -eLf | grep memcached