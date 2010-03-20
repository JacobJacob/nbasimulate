#!/usr/bin/python
# -*- coding: utf-8 -*-

'''初始化各联赛数据'''

from gba.entity import League

DEGREE_COUNT = 12 #十二个等级

def main():
    
    for i in range(DEGREE_COUNT):
        degree = i + 1
        no_count = 2**(degree-1)
        for j in range(no_count):
            league = League()
            league.degree = degree
            league.no = j + 1
            league.team_count = 0
            league.persist()
    
if __name__ == '__main__':
    main()