#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.business import only_one_match_manager
from xba.common.decorators import ensure_success
from base import BaseClient

class OnlyOneMatchHandler(BaseClient):
    
    
    CLIENT_NAME = "only_one_match_handler"
    
    def __init__(self):
        super(OnlyOneMatchHandler, self).__init__(OnlyOneMatchHandler.CLIENT_NAME)
        
        
    def work(self):
        
        self.set_only_one_match()        
        self.sleep()
        
    @ensure_success()
    def set_only_one_match(self):
        """胜者为王挫合"""
        return only_one_match_manager.set_only_one_match()

if __name__ == "__main__":
    handler = OnlyOneMatchHandler()
    handler.start()
