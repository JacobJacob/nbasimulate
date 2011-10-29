#!/usr/bin/python
# -*- coding: utf-8 -*-

import pymssql
import traceback



def main():
    
    ips = ["222.73.173.178", "122.225.105.26", "221.238.252.65", "61.152.244.182", "xbam6.mygame.17173.com"]
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
    
def __main(ip):

    conn = pymssql.connect(host=ip, user='BTPAdmin', password='btp20031118', database='NewBTP', as_dict=True)
    f = open("account_%s.txt" % ip, "wb")
    try:
        cursor = conn.cursor()
        cursor.execute("select NickName, Email from btp_account")
        infos = cursor.fetchall()
        for info in infos:
            nickname = info["NickName"]
            try:
                nickname = nickname.decode("gbk")
            except:
                pass
            email = info["Email"]
            print nickname, email
            f.write("%s\t%s\n" % (nickname, email))
    except:
        print traceback.format_exc().decode("gbk")
        conn.rollback()
    finally:
        f.close()
        conn.close()       

                
if __name__ == '__main__':
    create()
