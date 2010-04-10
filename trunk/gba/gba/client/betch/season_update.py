#!/usr/bin/python
# -*- coding: utf-8 -*-

'''赛季更新'''

import random

from gba.common.constants import AttributeMaps
from gba.entity import LeagueConfig, YouthPlayer, Message, ProfessionPlayer
from gba.client.betch import config
from gba.common import playerutil
from gba.client.betch.base import BaseBetchClient
from gba.common.db import connection


TRUNCATE_TABLES = ['matchs', 'match_nodosity_detail', 'match_nodosity_main', 'match_nodosity_tactical_detail',
                   'match_not_in_player', 'match_stat', 'message']

class SeasonUpdate(BaseBetchClient):
    ''''''
    
    def __init__(self):
        super(SeasonUpdate, self).__init__()
    
    def _run(self):
        self._sys_update()
        self.truncate_tables()
    
    def truncate_tables(self):
        
        for table in TRUNCATE_TABLES:
            cursor = connection.cursor()
            try:
                cursor.execute('truncate %s;' % table)
            finally:
                cursor.close()
                
        cursor = connection.cursor()
        try:
            cursor.execute('update league_matchs set status=0, point=null, match_id=null;')
        finally:
            cursor.close()
            
    def _sys_update(self):
        '''赛季加1'''
        league_config = LeagueConfig.query(limit=1)[0]
        league_config.season += 1
        league_config.round = 0
        league_config.persist()

    
if __name__ == '__main__':
    client = SeasonUpdate()
    client.start()