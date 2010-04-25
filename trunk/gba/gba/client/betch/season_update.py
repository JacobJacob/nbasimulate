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
                   'match_not_in_player', 'match_stat', 'message', 'season_finance', 'pro_player_season_stat_total',
                   'team_ticket_history']

UPDATE_SQLS = ['update league_matchs set status=0, point=null, match_id=null;',
               'update league_teams set win = 0, lose=0, net_points=0 ,status=0, rank=null;',
               ]

class SeasonUpdate(BaseBetchClient):
    ''''''
    
    def __init__(self):
        super(SeasonUpdate, self).__init__()
    
    def _run(self):
        success = True
        try:
            self.truncate_tables()
            self.update_tables()
        except:
            success = False
        if success:
            self._sys_update()
    
    def truncate_tables(self):
        '''赛季更新要清空的表'''
        for table in TRUNCATE_TABLES:
            cursor = connection.cursor()
            try:
                cursor.execute('truncate %s;' % table)
            finally:
                cursor.close()
    
    def update_tables(self):
        '''赛季更新一些表要重设值'''
        for sql in UPDATE_SQLS:
            cursor = connection.cursor()
            try:
                cursor.execute(sql)
            finally:
                cursor.close() 
               
    def _sys_update(self):
        '''赛季加1'''
        league_config = LeagueConfig.query(limit=1)[0]
        league_config.season += 1
        league_config.round = 1
        league_config.persist()

    
if __name__ == '__main__':
    client = SeasonUpdate()
    client.start()