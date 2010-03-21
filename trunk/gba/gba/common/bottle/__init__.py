from gba.common import memcache


try:
    from gba.common import cache
except:    
    cache = memcache.Client([":11211"])