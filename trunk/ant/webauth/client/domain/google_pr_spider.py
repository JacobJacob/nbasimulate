import urllib2
import re
import time
import sys 
from urllib import urlencode
from pprint import pprint
HOST = "toolbarqueries.google.com" 
def mix(a, b, c):
    M = lambda v: v % 0x100000000 # int32 modulo
    a, b, c = (M(a), M(b), M(c))
    a = M(a-b-c) ^ (c >> 13)
    b = M(b-c-a) ^ (a <<  8)
    c = M(c-a-b) ^ (b >> 13)
    a = M(a-b-c) ^ (c >> 12)
    b = M(b-c-a) ^ (a << 16)
    c = M(c-a-b) ^ (b >> 5)
    a = M(a-b-c) ^ (c >>  3)
    b = M(b-c-a) ^ (a << 10)
    c = M(c-a-b) ^ (b >> 15)
    return a, b, c

def checksum(iurl):
    C2I = lambda s: sum(c << 8*i for i, c in enumerate(s[:4]))
    a, b, c = 0x9e3779b9, 0x9e3779b9, 0xe6359a60
    lg  = len(iurl)    
    k = 0
    while k <= lg-12:
        a = a + C2I(iurl[k:k+4])
        b = b + C2I(iurl[k+4:k+8])
        c = c + C2I(iurl[k+8:k+12])
        a, b, c = mix(a, b, c)
        k += 12     
    
    a = a + C2I(iurl[k:k+4])
    b = b + C2I(iurl[k+4:k+8])
    c = c + (C2I(iurl[k+8:])<<8) + lg
    a, b, c = mix(a, b, c)
    return c

def GoogleHash(value):
    I2C = lambda i: [i & 0xff, i >> 8 & 0xff,  i >> 16 & 0xff, i >> 24 & 0xff]
    ch = checksum([ord(c) for c in value])
    ch = ((ch % 0x0d) & 7) | ((ch/7) << 2)
    return "6%s" % checksum(sum((I2C(ch-9*i) for i in range(20)), []))

def make_url(host, site_url):
    url = "info:" + site_url
    params = dict(client="navclient-auto", ch="%s" % GoogleHash(url),
                  ie="UTF-8", oe="UTF-8", features="Rank", q=url)
    return "http://%s/search?%s" % (host, urlencode(params))  # Where the fun begins 

def main(url):
    #if len(sys.argv) != 2:
    #    url = 'http://www.google.com/'
    #else:
    #    url = sys.argv[1]
    if not url.startswith('http://'):
        url = 'http://%s' % url
        # print make_url(HOST, url)
    req = urllib2.Request(make_url(HOST, url))
    try:
        f = urllib2.urlopen(req)
        response = f.readline()
    except Exception, err:
        print err
        # print err.read()
        #sys.exit(1)
    try:
        rank = re.match(r'^Rank_\d+:\d+:(\d+)', response.strip()).group(1)
    except AttributeError:
        print "This page is not ranked"
        rank = -1
    print "PagerRank: %d\tURL: %s" % (int(rank), url)

  
    
if __name__ == "__main__":
    urls = ['http://www.3-ya.com',
                'http://www.sina.com',
                'http://www.163.com',
                'http://www.yahoo.com',
                'http://www.hao123.com',
                'http://www.tom.com',
                'http://www.fm3721.com',
                'http://www.kingsoft.com',
                'http://www.jnu.edu.cn',
                'http://www.qq.com',
            ]
    for url in urls:
        main(url)
        import time
        #time.sleep(20)