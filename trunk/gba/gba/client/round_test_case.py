#!/usr/bin/python
# -*- coding: utf-8 -*-

from gba.client.betch import daily_update
from gba.client.betch import daily_league_update

def main():
    daily_update.main()
    daily_league_update.main()   
    
    
if __name__ == '__main__':
    main()