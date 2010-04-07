#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

from gba.common.constants import AttributeMaps
from gba.entity import LeagueConfig, YouthPlayer, Message, ProfessionPlayer
from gba.client.betch import config
from gba.common import playerutil
from gba.client.betch.base import BaseBetchClient

class SeasonUpdate(BaseBetchClient):
    ''''''
    
    def __init__(self):
        super(SeasonUpdate, self).__init__()
    
    def _run(self):
        self._sys_update()

    def _sys_update(self):
        ''''''
        league_config = LeagueConfig.query(limit=1)[0]
        league_config.season += 1
        league_config.persist()

    
if __name__ == '__main__':
    client = SeasonUpdate()
    client.start()