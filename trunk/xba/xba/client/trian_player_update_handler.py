#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
import time

from subprocess import Popen, PIPE
from datetime import datetime

from xba.config import CLIENT_EXE_PATH, PathSettings, DEBUG
from xba.business import player5_manager
from base import BaseClient
from xba.common import single_process

class TrianPlayerUpdateHandler(BaseClient):
    
    CLIENT_NAME = "TrianPlayerUpdateHandler"
    
    def __init__(self):
        super(TrianPlayerUpdateHandler, self).__init__(TrianPlayerUpdateHandler.CLIENT_NAME)

    
    def work(self):
        self.log("start round update for season:%s, round:%s" % (self._season, self._turn))
        self.log("start trian player finish")
        self.trian_player_finish()
        return "exit"
        
    def trian_player_finish(self):
        """试训的球员离队"""
        trian_players = player5_manager.get_trialing_player()
        if trian_players:
            for trian_player in trian_players:
                command = "%s %s %s %s %s" % (CLIENT_EXE_PATH, "change_player_from_arrange5_handler", trian_player["PlayerID"], trian_player["ClubID"], 5)
                self.call_cmd(command)
                player5_manager.un_set_trial_player(trian_player["ClubID"], trian_player["PlayerID"])
    
    def call_cmd(self, cmd):
        """调用命令"""
        p = Popen(cmd, stdout=PIPE)
        while True:
            line = p.stdout.readline()
            if not line:
                break
            self.log(line.replace("\n", ""))
            
        if p.wait() == 0:
            self.log("call %s success" % cmd)
 
def main():
    s = single_process.SingleProcess("TrianPlayerUpdateHandler")
    s.check()
    lock_file = os.path.join(PathSettings.TRIAN_PLAYER_UPDATE_LOCK, datetime.now().strftime("%Y_%m_%d_H.lock"))
    if os.path.exists(lock_file) and not DEBUG:
        print "update had finish"
    else:
        f = open(lock_file, "wb")
        f.close()
        handler = TrianPlayerUpdateHandler()
        handler.start()
        
if __name__ == "__main__":
    while True:
        if datetime.now().hour % 2 == 0:
            main()
        print "sleep 60s"
        time.sleep(60)
