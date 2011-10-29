from xba.common import memcache


try:
    from xba.common import cache
except:    
    cache = memcache.Client([":11211"])