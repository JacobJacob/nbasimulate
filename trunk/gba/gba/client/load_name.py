#!/usr/bin/python
# -*- coding: utf-8 -*-

import re
from gba.entity import Names

def main():
    
    f = open('e://name.txt')
    try:
        for line in f:
            matchs = re.findall(ur'([^\s]*?)-', line, re.VERBOSE)
            for m in matchs:
                if not m:
                    continue
                name = Names() 
                name.name = m.decode('gb2312')
                name.type = 1
                name.persist()
                
            matchs = re.findall(ur'-([^,]*)', line, re.VERBOSE)
            for m in matchs:
                if not m:
                    continue
                name = Names()
                print m.decode('gb2312')
                name.name = m.decode('gb2312')
                name.type = 2
                name.persist()
    finally:
        f.close()
    
    
if __name__ == '__main__':
    main()