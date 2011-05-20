#!/usr/bin/python
# -*- coding: utf-8 -*-

if __name__ == "__main__":
    
    m = {292172: 1, 292170: 2, 292168: 3, 292166: 4, 
         292164: 5, 292162: 6, 291860: 7, 111118: 8,
         193042: 9, 291550: 10, 291552: 11, 291854: 12,
         291856: 13, 291858: 14}
    
    f = open("D:\\btp\\new\\ToadTabFile_2011-05-05T23_42_38.txt", "rb")
    try:
        old_round = 0 
        for line in f:
            ds = line.split("\t")
            round = int(ds[2])
            home_id = int(ds[3])
            away_id = int(ds[4])
            if round != old_round:
                if round != 1:
                    print ""
                #print round, 
                old_round = round
            #print "[%s[%s], %s[%s]], " % (m.get(home_id), home_id, m.get(away_id), away_id),
            print "[%s, %s], " % (m.get(home_id), m.get(away_id)), 
    finally:
        f.close()