#!/usr/bin/env python
# -*- encoding: utf8 -*-
import sys
import gzip
import operator
import subprocess
import optparse
import logging, traceback

from webauth.common.svnutils import svninfo


_prefixs = ['/home/kslab/deploy_coding/webobserver/',
            '/home/kslab/project/webobserver/',]

def truncLR(sa, sb, fname):
    if not sa or not sb: return sa, sb
    #length = len("Jan  3 05:52:54 192.168.51.176 11977:MainThread:ERROR:/home/kslab/")
    sa = sa[sa.find(fname):]
    sb = sb[sb.find(fname):]
    left = 0
    while left < len(sa) and left < len(sb) and sa[left] == sb[left]:
        left += 1
    sa = sa[left:][::-1]
    sb = sb[left:][::-1]
    left = 0
    while left < len(sa) and left < len(sb) and sa[left] == sb[left]:
        left += 1
    sa = sa[left:][::-1]
    sb = sb[left:][::-1]
    return sa, sb

def webobserver_log_parser(ifile):
    res = {}
    MySQLServerLost = 0
    KeyboardInterrupt = 0
    ResourceTemporarilyUnavailable = 0
    for line in ifile:
        if ':ERROR:' not in line and ':CRITICAL:' not in line:
            continue
        if 'KeyboardInterrupt' in line:
            KeyboardInterrupt += 1
            continue
        if ('MySQL server' in line and ('Lost' in line or "t connect" in line)) or \
           ('Lock wait timeout exceeded; try restarting transaction' in line) or \
           ('Connection timed out' in line) or \
           ('Connection reset by peer' in line):
            MySQLServerLost += 1
            continue
        if 'Resource temporarily unavailable' in line:
            ResourceTemporarilyUnavailable += 1
            continue
        words = line.split()
        data = words[4]
        fname, lineno = tuple(data.split(':')[3:5])
        # 236:scoreapp_jsmodule.MainBody:ERROR:c:\project\webobserver\client\analyzeapp\jsclient\js.py:98:jsOp:'Traceback
        if len(fname) < 5:
            fname = ":".join(data.split(':')[3:5])
            lineno = ":".join(data.split(':')[5:6])

        for x in _prefixs:
            if fname.startswith(x):
                fname = fname[len(x):]
                break

        key = (fname, lineno)
        if key not in res:
            res[key] = {'file': fname,
                        'line': lineno,
                        'count': 0,
                        'author': svninfo(fname),
                        'origin1': "assert can not be displayed",
                        'origin1_': None,
                        'origin2': None,}
        res[key]['count'] += 1
        def eval_line(line): 
            temp = eval('"""' + line.strip().replace('"', '\\"') + ' """')
            temp = temp.strip()
            if temp and temp.endswith("'"):
                temp = temp[:-1].strip()
            return temp
        if res[key]['count'] == 1:
            res[key]['origin1'] = "[at %d]: " % res[key]['count'] + eval_line(line)
            res[key]['origin1_'] = eval_line(line)
        else: # 最后一次为准
            origin1 = res[key]['origin1_']
            origin2 = eval_line(line)
            if origin2 and origin1 and origin2 != origin1:
                temp = truncLR(origin2, origin1, fname)[0]
                if temp:
                    temp = "***%s***" % temp
                    res[key]['origin2'] = "[at %d]: " % res[key]['count'] + temp
    res = res.values()
    res.sort(key=operator.itemgetter('count'), reverse=True)
    
    res2 = []
    
    count = 0
    for idx, x in enumerate(res):
        if x["file"].find("webobserver/") != -1 or x["file"].find("webobserver\\") != -1:
            res2.append('%s. ' % (count+1))
            res2.append('%(author)s, %(file)s:%(line)s, %(count)s times.\n' % x)
            count += 1
    res2.append('----------------- I - am - a - Clever - Partition - Line -----------------------------------------\n' % x)
    for idx, x in enumerate(res):
        if x["file"].find("webobserver/") == -1 and x["file"].find("webobserver\\") == -1:
            res2.append('%s. ' % (count+1))
            res2.append('%(author)s, %(file)s:%(line)s, %(count)s times.\n' % x)
            count += 1
        
    res2.append('''MySQL server Lost or Can not connect Times = %s
        Include(Lock wait timeout exceeded; try restarting transaction)
        Include(Connection timed out)
        Include(Connection reset by peer)\n''' % MySQLServerLost)
    res2.append('KeyboardInterrupt Times = %s \n' % KeyboardInterrupt)
    res2.append('Resource temporarily unavailable = %s \n' % ResourceTemporarilyUnavailable)
    res2.append('\n')

    count = 0
    for idx, x in enumerate(res):
        if x["file"].find("webobserver/") != -1 or x["file"].find("webobserver\\") != -1:
            res2.append('%s. ' % (count+1))
            res2.append('''%(author)s 
%(file)s:%(line)s --  %(count)s Times  
''' % x)
            x["--"] = "~~~~~~~~~~~~~~~~~ I ~ am ~ a ~ Insignificant ~ Partition ~ Line ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
            if x["origin2"] != None:
                res2.append('First Appear%(origin1)s\n%(--)s\nLast Different Piece%(origin2)s\n\n' % x)
            else:
                res2.append('First Appear%(origin1)s\n\n' % x)
            count += 1
    res2.append('***************** I * am * a * Hesitation * Partition * Line *****************************************\n\n' % x)
    for idx, x in enumerate(res):
        if x["file"].find("webobserver/") == -1 and x["file"].find("webobserver\\") == -1:
            res2.append('%s. ' % (count+1))
            res2.append('''%(author)s 
%(file)s:%(line)s --  %(count)s Times  
''' % x)
            x["--"] = "~~~~~~~~~~~~~~~~~ I ~ am ~ a ~ Insignificant ~ Partition ~ Line ~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~"
            if x["origin2"] != None:
                res2.append('First Appear%(origin1)s\n%(--)s\nLast Different Piece%(origin2)s\n\n' % x)
            else:
                res2.append('First Appear%(origin1)s\n\n' % x)
            count += 1

    if not res: 
        res2.append('Congratulations, no one error!!!')
    if res2:
        res2[0] = res2[0].lstrip()
        res2[-1] = res2[-1].rstrip()
    return res2

import re

LOG_RE = re.compile("""
    ^
    (\w+)\s # [0]:month
    (\d+)\s # [1]:day
    (\d+):(\d+):(\d+)\s # [2][3][4]hour:minute:second
    ([\d\.]+)\s # [5]client ip
    (\d+)\| # [6]process id
    ([\w\-]+)\| # [7]thread name
    (\w+)\| # [8]level
    ([^\|]+?)\| # [9]pathname
    (\d+)\| # [10]lineno
    ([\w\-]+)\| # [11]func name
    '([\s\S]*?)' # [12]message
    $ 
    """, re.IGNORECASE | re.VERBOSE)

def parse_line(line):
    m = LOG_RE.match(line)
    if m:
        return m.groups()
    return None

if __name__ == '__main__':
    line = """May 18 22:47:41 10.20.208.84 18985|MainThread|ERROR|e:\webauth/common/tests/log_test.py|26|test_log_exception|'Traceback (most recent call last):\n  File "/home/mk2/workspace/webauth/webauth/common/tests/log_test.py", line 24, in test_log_exception\n    1 / 0\nZeroDivisionError: integer division or modulo by zero\n'"""
    rs = parse_line(line)
    print svninfo(rs[9])