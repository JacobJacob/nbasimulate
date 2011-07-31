#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from sandbox_client import SanboxBaseClient

class MatchSandBoxClient(SanboxBaseClient):
    
    def __init__(self):
        cmd_path = os.path.join(os.path.dirname(os.path.realpath(__file__)), 'match_client.py')
        super(MatchSandBoxClient, self).__init__("MatchSandBoxClient", cmd_path, 4)
        
if __name__ == "__main__":
    client = MatchSandBoxClient()
    client.start()