#!/usr/bin/python
# -*- coding: utf-8 -*-

import urllib2


def read(url):
    opener = urllib2.build_opener()
    request = urllib2.Request(url)  
    return opener.open(request).read()

    
def main():

    f = open("url.txt", "rb")
    for line in f:
        print line,
        try:
            s = read(line)
        except:
            continue
        if s.find("113388.net") != -1:
            print "success"
        else:
            print "failure"

if __name__ == "__main__":
    main()  