#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.entity import ProfessionPlayer
from gba.common import playerutil


if __name__ == '__main__':

    start_id = 0
    while True:
        print start_id, '---->', start_id + 100
        players = ProfessionPlayer.query(condition="id>%s" % start_id, order="id asc ", limit=100)
        if not players:
            break
        
        start_id = players[-1].id
        
        for player in players:
            playerutil.calcul_wage(player)
            player.persist()