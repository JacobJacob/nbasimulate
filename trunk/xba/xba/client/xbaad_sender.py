#!/usr/bin/python
# -*- coding: utf-8 -*-

import time
import os

from xba.common.mailutil import send_to

def send(nickname, email):
    subject = "一款公平无道具的篮球经理期待您的加入"
    content = "尊敬的玩家，你好!曾经官方的会员区让我们体验了一把公平竞赛的乐趣," \
              "但随着官方的跳票，让所有期待公平游戏的玩家伤透了心，所以，在当初 一些会员区老玩家的" \
               "支持下，完全无道具的xba篮球经理开服了, 在这邀请所有喜欢公平竞赛的玩家们加入: http://www.113388.net"
    send_to(subject, content, email)
    
if __name__ == "__main__":
    historys = set()
    if os.path.exists("history.txt"):
        fff = open("history.txt", "rb")
        try:
            for line in fff:
                historys.add(line.replace("\r", "").replace("\n", ""))
        finally:
            fff.close()
    f = open("email4.txt", "rb")
    ff = open("history.txt", "ab")
    try:
        for line in f:
            ds = line.replace("\r", "").replace("\n", "").split("\t")
            if len(ds) != 2:
                print len(ds)
                continue
            nickname, email = ds[0][1:-1].strip(), ds[1][1:-1].strip()
            try:
                nickname = nickname.decode("gbk")
            except:
                nickname = nickname.decode("gbk", "replace")
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
                    send(nickname, email)
                    ff.write("%s\n" % email)
                except:
                    print "error"
                time.sleep(30)
            
    finally:
        f.close()
        ff.close()