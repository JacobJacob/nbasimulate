#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.entity import FreePlayer, YouthPlayer


if __name__ == '__main__':
    
    player = FreePlayer.load(id=797)
    
    player.__class__ = YouthPlayer
    player.team_id = 2
    delattr(player, 'id')
    player.persist()