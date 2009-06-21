#!/usr/bin/python
# -*- coding: utf-8 -*-
"""根据lighttpd的mod_access模块生成的日志, 做访问统计
http://redmine.lighttpd.net/projects/lighttpd/wiki/Docs:ModAccessLog

accesslog.format
  the format of the logfile

  ====== ================================
  Option Description
  ====== ================================
  %%     a percent sign
  %h     name or address of remote-host
  %l     ident name (not supported)
  %u     authenticated user
  %t     timestamp for the request-start
  %r     request-line 
  %s     status code 
  %b     bytes sent for the body
  %i     HTTP-header field
  %a     remote address
  %A     local address
  %B     same as %b
  %C     cookie field (not supported)
  %D     time used in ms (not supported)
  %e     environment (not supported)
  %f     physical filename
  %H     request protocol (HTTP/1.0, ...)
  %m     request method (GET, POST, ...)
  %n     (not supported)
  %o     `response header`_
  %p     server port
  %P     (not supported)
  %q     query string
  %T     time used in seconds
  %U     request URL
  %v     server-name
  %V     HTTP request host name
  %X     connection status
  %I     bytes incomming
  %O     bytes outgoing
  ====== ================================

accesslog.format = "%h %V %u %t \"%r\" %>s %b \"%{Referer}i\" \"%{User-Agent}i\" %T"
accesslog.filename = "|/usr/sbin/cronolog /data/gateway_working/lighttpd/logs/%Y/%m/%d.log"

nginx:
'$remote_addr $host $remote_user [$time_local] "$request" $status $bytes_sent "$http_referer" "$http_user_agent" cgi request_time'
"""

try:
    import django
except:
    from gateway import init_gateway
    init_gateway.init()
    del init_gateway
    
import os
import shutil
os.environ["DJANGO_SETTINGS_MODULE"] = "gateway.settings" 
from datetime import date, timedelta
import time
import re
from operator import itemgetter

from django.template import loader

from gateway.config import PathSettings
from gateway.common_logic.single_process import SingleProcess
from gateway.config import DataSync
from gateway.common_logic import file_utility


FORMAT_RE = re.compile(r"""
        ^[\w\.]+\s                                          # remote_host
        (?P<host_name>[\w\.]+)\s
        [\w\-]+\s
        \[(?:\d+/\w+/\d+:(?P<hour>\d+):\d+:\d+\s\+\d+)\]\s
        \"(?P<method>\w+)\s
        (?P<url>.+?)\sHTTP/
        (?P<protocol>[\w\.]+)\"\s
        (?P<status>\d+)\s
        \d+\s                                                 # size
        \".+?\"\s                                             # referer
        \"(?P<user_agent>.+?)\"\s
        (?P<process>\w+)\s
        (?P<request_time>.+?)\s
        .+$
    """, re.VERBOSE)
KEY_RE = re.compile(r'key=(\d+)')
URL_RE = re.compile(r'(/[\w\-]+)(/[\w\-]+)?.*')

def total(logpath):
    rs = {}
    log = open(logpath, 'rb')
    try:
        for l in log:
            m = FORMAT_RE.match(l)
            if m:
                d = m.groupdict()
                for k, v in d.iteritems():
                    if k == 'hour':
                        continue
                    elif k == 'protocol':
                        v = 'HTTP %s' % v
                    elif k == 'status':
                        v = 'Status %s' % v
                    elif k == 'method':
                        v = 'Method %s' % v
                    elif k == 'method':
                        v = 'UserAgent %s' % v
                    elif k == 'host_name':
                        v = 'HOST %s' % v
                    elif k == 'process':
                        v = 'Process %s' % v
                    elif k == 'request_time':
                        v = float(v)
                        if v > 1:
                            v = 'Use %0.0f' % v
                        else:
                            v = 'Use %0.1f' % v
                    elif k == 'url':
                        url_m = URL_RE.match(v)
                        if url_m:
                            main, sub = url_m.groups()
                            if main == '/receive':
                                v = '%s%s' % (main, sub)
                            else:
                                v = '%s' % (main, )
                        else:
                            v = v[:10]
                    r = rs.get(v, {})
                    if not r:
                        for i in range(24):
                            r['%02d' % i] = 0
                        rs[v] = r
                    r[d['hour']] += 1
    finally:
        log.close()
    return rs

def _cal(logpath, total_path, logdate):
    print 'start caculate %s' % logpath
    rs = total(logpath)
    keys = ['Name', 'Total']
    for i in range(24):
        keys.append('%02d' % i)
    values = []
    for k, r in rs.iteritems():
        v = [k, 0]
        for i in range(24):
            c = r['%02d' % i]
            v[1] += c
            v.append(c)
        if v[1] > 50:
            values.append(v)
    data = {'keys': keys, 
            'values': sorted(values, key=itemgetter(0)), 
            'logdate': logdate}
    f = open(total_path, 'wb')
    try:
        f.write(loader.render_to_string('server/accesslog.html', data).encode('utf-8'))
    finally:
        f.close()

def calculate(today=None):
    if today is None:
        today = date.today()
        yesterday = today - timedelta(days=1)
    logpath = os.path.join(PathSettings.WEB_LOGS, 
                           yesterday.strftime('%Y/%m/access_%d.log'))
    total_path = logpath + '.total'
    
    if os.path.exists(logpath):
        _cal(logpath, total_path, yesterday)
        data_folder = os.path.join(DataSync.EXPORTLOGPATH, "exportdata", 
                                   today.strftime("%Y-%m-%d"))
        file_utility.ensure_dir_exists(data_folder)
        to_path = os.path.join(data_folder, 
                               yesterday.strftime('access_log.%Y.%m.%d.done'))
        shutil.move(logpath, to_path)
        file_utility.chmod_777(to_path)
        
def get_total(logdate=None):
    if logdate is None:
        logdate = date.today()
    total_path = os.path.join(PathSettings.WEB_LOGS, 
                              logdate.strftime('%Y/%m/access_%d.log.total'))
    if not os.path.exists(total_path):
        return None
    return total_path

if __name__ == '__main__':
    s = SingleProcess(os.path.realpath(__file__))
    s.check()
    print 'access log start'
    calculate()
    print 'access log finish'