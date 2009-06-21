#coding=utf-8

import random

GOOGLE_PR_DATACENTER_IPS = [
"64.233.161.100",       # 0
"64.233.161.101",       # 1
"64.233.183.91",        # 3
"64.233.185.19",        # 4
"64.233.189.44",        # 5
"66.102.1.103",         # 6
"66.102.9.115",         # 7
"66.249.81.101",        # 8
"66.249.89.83",         # 9
"66.249.91.99",         # 10
"66.249.93.190",        # 11
"72.14.205.113",        # 13
"72.14.255.107",        # 14
"64.233.187.107",       # 17
"216.239.59.103",       # 19
"216.239.59.104",       # 22
"64.233.187.99",        # 23
"216.239.59.107",       # 31
"216.239.59.147",       # 34
"216.239.59.99",        # 40
"216.239.51.100",       # 42
"64.233.161.104",       # 55
"66.102.9.104",         # 74
"64.233.183.91",        # 79
"64.233.183.93",        # 82
"66.102.9.107",         # 83
"64.233.183.99",        # 85
"66.102.9.147",         # 86
"64.233.187.104",       # 88
"66.102.9.99",          # 89
"toolbarqueries.google.com" 
]
GOOGLE_PR_DATACENTER_IPS = GOOGLE_PR_DATACENTER_IPS[-1:]
pow2 = [pow(2, i) for i in range(33)]

def _long_move(fNum, nMove):
    """ nMove >= 0 means 'fNum << nMove', 
        nMove <= 0 means 'fNum >> nMove' 
    """
    if nMove >= 0:
        nMove = nMove % 32
    else:
        nMove = -(abs(nMove) % 32)
    if nMove == 0 or fNum < 0 or fNum > 0xffffffff:
        return fNum
    if nMove > 0:
        fNum %= pow2[32 - nMove]
        fNum *= pow2[nMove]
    else:
        fNum /= pow2[-nMove]
#        fNum = float(int(fNum))
    return fNum
    
def _long_over_flow(lNum):
    return lNum & 0xffffffff
    
googleMagic = long(0xE6359A60)
    
def calc_checksum(szUrl):
    ch = _google_ch('info:' + szUrl);
    ch = _long_over_flow(((ch / 7) << 2) | (((ch % 13)) & 7))
    prbuf = [ch]
    for i in range(19):
        prbuf.append(prbuf[-1] - 9)
    ch = _google_ch_core(c32_to_8bit(prbuf), 80, googleMagic)
    return '6%d' % long(ch)
    
def _google_ch(szUrl):
    return _google_chs_tring(szUrl, 0)

def _google_chs_tring(szUrl, length):
    lsUrl = [long(ord(c)) for c in szUrl]
    return _google_ch_core(lsUrl, length, googleMagic)

def _google_ch_core(lsUrl, length, init):
    if length == 0:
        length = len(lsUrl)
    a = long(0x9E3779B9)
    b = long(0x9E3779B9)
    c = init
    k = 0
    lengthTemp = length
    while lengthTemp >= 12:
        a += lsUrl[k + 0] + _long_move(lsUrl[k + 1], 8) + \
            _long_move(lsUrl[k + 2], 16) + _long_move(lsUrl[k + 3], 24) 
        b += lsUrl[k + 4] + _long_move(lsUrl[k + 5], 8) + \
            _long_move(lsUrl[k + 6], 16) + _long_move(lsUrl[k + 7], 24) 
        c += lsUrl[k + 8] + _long_move(lsUrl[k + 9], 8) + \
            _long_move(lsUrl[k + 10], 16) + _long_move(lsUrl[k + 11], 24) 
            
        (a, b, c) = mix(_long_over_flow(a), _long_over_flow(b), _long_over_flow(c))
        k += 12
        lengthTemp -= 12
        
    c += length
    
    if lengthTemp >= 11:
        c += _long_move(lsUrl[k + 10], 24)
    if lengthTemp >= 10:
        c += _long_move(lsUrl[k + 9], 16)
    if lengthTemp >= 9:
        c += _long_move(lsUrl[k + 8], 8)
    if lengthTemp >= 8:
        b += _long_move(lsUrl[k + 7], 24)
    if lengthTemp >= 7:
        b += _long_move(lsUrl[k + 6], 16)
    if lengthTemp >= 6:
        b += _long_move(lsUrl[k + 5], 8)
    if lengthTemp >= 5:
        b += _long_move(lsUrl[k + 4], 0)
    if lengthTemp >= 4:
        a += _long_move(lsUrl[k + 3], 24)
    if lengthTemp >= 3:
        a += _long_move(lsUrl[k + 2], 16)
    if lengthTemp >= 2:
        a += _long_move(lsUrl[k + 1], 8)
    if lengthTemp >= 1:
        a += _long_move(lsUrl[k + 0], 0)
    
    (a, b, c) = mix(_long_over_flow(a), _long_over_flow(b), _long_over_flow(c))
    return _long_over_flow(c)
                    
def mix(a, b, c):
    a -= b
    a -= c
    a = _long_over_flow(a)
    a ^= _long_move(c, -13)
    b -= c
    b -= a
    b = _long_over_flow(b)
    b ^= _long_move(a, 8)
    c -= a
    c -= b
    c = _long_over_flow(c)
    c ^= _long_move(b, -13)
    
    a -= b
    a -= c
    a = _long_over_flow(a)
    a ^= _long_move(c, -12)
    b -= c
    b -= a
    b = _long_over_flow(b)
    b ^= _long_move(a, 16)
    c -= a
    c -= b
    c = _long_over_flow(c)
    c ^= _long_move(b, -5)
    
    a -= b
    a -= c
    a = _long_over_flow(a)
    a ^= _long_move(c, -3)
    b -= c
    b -= a
    b = _long_over_flow(b)
    b ^= _long_move(a, 10)
    c -= a
    c -= b
    c = _long_over_flow(c)
    c ^= _long_move(b, -15)
    
    return (a, b, c)
    
def c32_to_8bit(lsCh32):
    lsCh8 = []
    for f in lsCh32:
        for i in range(4):
            lsCh8.append(f % 256)
            f = _long_move(f, -8)
    return lsCh8
            
def get_random_google_ip():
    index = random.randint(0,len(GOOGLE_PR_DATACENTER_IPS)-1)
    return GOOGLE_PR_DATACENTER_IPS[index]

def make_url(url):
    url_ch = calc_checksum(url)
    google_ip = get_random_google_ip()
    return "http://%s/search?client=navclient-auto&ch=%s&ie=UTF-8&oe=UTF-8&features=Rank&q=info:%s" % (google_ip, url_ch, url)

if __name__ == '__main__':
    print make_url('sina.com')

