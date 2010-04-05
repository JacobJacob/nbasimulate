#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from gba.common.single_process import SingleProcess
from gba.common import log_execption
from gba.common.constants import AttributeMaps
from gba.entity import LeagueConfig, YouthPlayer, Message, ProfessionPlayer
from gba.client.betch import config
from gba.common import playerutil
from gba.client.betch.base import BaseBetchClient

class LeagueDisClient(BaseBetchClient):
    '''赛程分配任务'''
    
    def __init__(self):
        super(LeagueDisClient, self).__init__()
    
    def _run(self):
        self.log('start to dis league')
        
        
    def _dis(self, league_id):
        pass
    
if __name__ == '__main__':
    signle_process = SingleProcess('league dis client')
    signle_process.check()
    try:
        client = LeagueDisClient()
        client.start()
    except:
        log_execption()