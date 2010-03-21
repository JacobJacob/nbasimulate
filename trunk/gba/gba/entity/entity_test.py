#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.entity import Team
from gba.common import cache

def main():
    
    team = Team.load(username='amy')
    print team
    team = Team.load(username='amy')
    
    cache.set('test', 'jackyshi')
    print cache.get('test')
    
    
if __name__ == '__main__':
    main()