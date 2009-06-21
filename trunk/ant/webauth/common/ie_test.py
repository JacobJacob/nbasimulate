#!/usr/bin/python
# -*- coding: utf-8 -*-
"""ie test"""

from comtypes.client import CreateObject, ShowEvents

def main():
    ie = CreateObject("InternetExplorer.Application")
    print ie.Visible
    ie.Visible = True
    connection = ShowEvents(ie)
    print connection
    ie.navigate('http://baidu.com')
    import time
    time.sleep(100)
#    ie.quit()
    
if __name__ == '__main__':
    main()