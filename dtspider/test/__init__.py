from dtspider.common import cache

cache.set('test','hello world')

print cache.get('test')

print cache.get_stats()
