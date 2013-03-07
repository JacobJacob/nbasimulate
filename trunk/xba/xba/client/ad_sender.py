#!/usr/bin/python
# -*- coding: utf-8 -*-

import pymssql
import traceback
from xba.common.stringutil import ensure_gbk


def main():
    
    ips = ["222.73.173.178", "221.238.252.65"]
    for ip in ips:
        __main(ip)

def create():
    ips = ["222.73.173.178", "122.225.105.26", "221.238.252.65", "61.152.244.182", "xbam6.mygame.17173.com"]
    c = 0
    emails = {}
    for ip in ips:
        f = open("account_%s.txt" % ip, "rb")
        ff = open("accounts.txt", "ab")
        try:
            for line in f:
                line = line.replace("\n", "")
                line = line.lower()
                ds = line.split("\t")
                if len(ds) != 2:
                    continue
                name = ds[0]
                email = ds[1]
                if email.startswith("banzhuzhu"):
                    continue
                c += 1
                email_type = email[email.find('@') + 1:]
                print email_type
                count = emails.get(email_type, 0)
                count += 1
                emails[email_type] = count
                if email_type in ('ynet.com', '21cn.com', '263.net', 'sina.cn', 'sina.com.cn', 'qq.com', \
                                  '139.com', 'sina.com', 'eyou.com', 'vip.qq.com', 'msn.com' , \
                                  'yahoo.cn', 'hotmail.com', '56.com', 'elong.com', 'yeah.net',\
                                  '163.com', '126.com', 'yahoo.com', 'gmail.com', 'yahoo.com.cn' , \
                                  'live.cn', 'tom.com', 'sohu.com', 'sogou.com', 'foxmail.com'):
                    print line
                    ff.write("%s\n" % line)
        finally:
            f.close()
            ff.close()
    print c
    for email, count in emails.iteritems():
        if count > 50:
            print '\'%s\'' % email,',',
    
def send(nickname, cursor):

    content = u"<a href=\"http://www.113388.net\" target=\"_blank\">点击进入</a> xba老版篮球经理，期待你的加入." 
    print content
    #nickname = u"大头@"
    try:
        sql = u"EXEC AddMessage 1519698, '.莫雷.', '%s', '%s'" % (nickname, content)
        sql = ensure_gbk(sql)
        #print type(sql)
        cursor.execute(sql) 
        #info = cursor.fetchone()
        #print info
    except:
        print "error"

def __main(ip):

    conn = pymssql.connect(host=ip, user='BTPAdmin', password='btp20031118', database='NewBTP', as_dict=True)

    try:
        cursor = conn.cursor()
        send("", cursor)
        #cursor.execute("select top 10 * from btp_message where sendid = 1519698")
        #infos = cursor.fetchall()
        #for info in infos:
        #    print info
        #cursor.execute("select UserID, NickName from btp_online")
        cursor.execute("select UserID, NickName from btp_online")
        infos = cursor.fetchall()
        print len(infos)
        for info in infos:
            nickname = info["NickName"]
            try:
                nickname = nickname.decode("gbk")
            except:
                pass
            user_id = info["UserID"]
            if nickname == "大头@" or True:
                send(nickname, cursor)
        conn.commit()  
    except:
        #print traceback.format_exc().decode("gbk")
        conn.rollback()
        raise
    finally:
        conn.close()       

                
if __name__ == '__main__':
    main()
