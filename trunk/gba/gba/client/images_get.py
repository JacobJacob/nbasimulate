#!/usr/bin/python
# -*- coding: utf-8 -*-
import urllib2

def main():
    
    save_dir = 'E:\workspace\gba\gba\web\media\images\%s'
    
    name = 'w_blank/blank_05.gif'
    
    url = 'http://s3.dlm.17uxi.cn/source/images/%s' % name

    request = urllib2.Request(url)
    opener = urllib2.build_opener()
    
    response = opener.open(request)
    
    f = open(save_dir % name, 'wb')
    try:
        data = response.read(1024)
        while data:
            print data
            f.write(data)
            data = response.read(1024)
    finally:
        f.close()
        
def main2():
    
    save_dir = 'E:\workspace\gba\gba\web\media\images\cup\%s.gif'
    
    
    for i in range(20):
        i += 1 
        url = "http://s3.dlm.17uxi.cn/source/images/bei_sai/%s.gif" % i
        print url
        #url = 'http://s3.dlm.17uxi.cn/source/images/%s' % name
        
        request = urllib2.Request(url)
        opener = urllib2.build_opener()
        
        response = opener.open(request)
        
        print save_dir % i
        
        f = open(save_dir % i, 'wb')
        try:
            data = response.read(1024)
            while data:
                #print data
                f.write(data)
                data = response.read(1024)
        finally:
            f.close()
                
if __name__ == '__main__':
    main2()