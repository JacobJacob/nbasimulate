#!/usr/bin/python
# -*- coding: utf-8 -*-
"""base client test"""

import time

from webauth.client.base import BaseClient

class DemoClient(BaseClient):
    
    def __init__(self):
        super(DemoClient, self).__init__('demo')
        self.debug = False
        
#    def before_run(self):
#        return 'start deal 1-1000'
    
    def run(self):
#        raise Exception('test')
        time.sleep(2)
        print self.params
        return 5

def main():
    demo = DemoClient()
    demo.main()
    
if __name__ == '__main__':
    demo = DemoClient()
    demo.params = 'k1:v1,k2:2,1111,'
    print demo.get_value_from_params('k1', str, 'sss')
    print demo.get_value_from_params('k2', int, 111)
    print demo.get_value_from_params('k3', int, 10)
    demo.main()