#!/usr/bin/python
# -*- coding: utf-8 -*-

import random

def get_sub_list(plist, sub_size):
    
    size = len(plist)
    if size <= sub_size:
        return plist
    
    sub_list = []
    for _ in range(sub_size):
        sub_list.append(None)
   
    odd = size
    index = 0
    while index < sub_size:
        randindex = random.randint(0, odd-1)
        value = plist[randindex]
        plist.remove(value)
        sub_list[index] = value
        index += 1
        odd -= 1
        
    return sub_list


if __name__ == "__main__":
    plist = [4, 6, 9, 10, 113, 2, 7]
    print get_sub_list(plist, 5)
