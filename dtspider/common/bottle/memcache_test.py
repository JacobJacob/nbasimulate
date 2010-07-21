'''
Created on 2009-11-6

@author: jackyshi
'''
from dtspider.entity import InjectionUrl
from dtspider.common.bottle import cache

def test():
    injection_url = InjectionUrl.load(url_md5='b83ebd5f091ca496be2b263c5b5be3d4')
    injection_url = InjectionUrl.load(url_md5='b83ebd5f091ca496be2b263c5b5be3d4')
    
    print cache.get_stats()
    
    
if __name__ == '__main__':
    test()
