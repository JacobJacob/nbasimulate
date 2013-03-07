#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.client.base import BaseClient

class PingClient(BaseClient):
    
    CLIENT_NAME = "ping_clinet"
    
    def __init__(self, ip):
        super(PingClient, self).__init__(PingClient.CLIENT_NAME)
        self.__ip = ip
        
    def work(self):
        self.call_cmd("ping %s -t" % self.__ip)
        
    
if __name__ == "__main__":
    client = PingClient("210.14.68.194")
    client.start()