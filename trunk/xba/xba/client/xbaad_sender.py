#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
import os
import random

from xba.common.mailutil import send_to

def send(nickname, email):
    subject = "%s经理你好, 还记得当初一起战斗的兄弟吗？" % nickname
    content = "%s经理你好, 还记得当初一起打盟战的兄弟吗？无兄弟不篮球，快点复出吧，无道篮球等待您的加入 <a target=\"_blank\" href=\"http://www.113388.net\">点击复出</a>" % nickname 
    print subject
    print content
    send_to(subject, content, email)
    
if __name__ == "__main__":
    historys = set()
    if os.path.exists("accounts_history.txt"):
        fff = open("accounts_history.txt", "rb")
        try:
            for line in fff:
                ds = line.replace("\r", "").replace("\n", "").split("\t")
                historys.add(ds[0])
        finally:
            fff.close()
    f = open("accounts.txt", "rb")
    ff = open("accounts_history.txt", "ab")
    try:
        for line in f:
            ds = line.replace("\r", "").replace("\n", "").split("\t")
            if len(ds) != 2:
                print len(ds)
                continue
            nickname, email = ds[0].strip(), ds[1].strip()
            try:
                nickname = nickname.decode("utf8")
            except:
                nickname = nickname.decode("utf8", "replace")
            #print nickname, email
            if email.find("@xba.com.cn") != -1:
                print email
            if not email or email.find("@") == -1:
                continue
            if email == "114476513@qq.com" or True:
                if email in historys:
                    print "skip"
                    continue
                print "start to send to %s" % email
                try:
                    #send(nickname, email)
                    ff.write("%s\t1\n" % email)
                except Exception, e:
                    print e
                    ff.write("%s\t0\n" % email)
                time.sleep(random.randint(30, 60))
            
    finally:
        f.close()
        ff.close()