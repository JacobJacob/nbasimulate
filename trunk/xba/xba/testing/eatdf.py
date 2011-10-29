#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.pamie.pam import PAMIE

def main():
    
    ie = PAMIE("http://www.113388.net/Main.aspx")

    ie.frameName = "Main.Center"
    
    ie._frameWait()
    
    if ie.buttonExists("btnNext"):
        ie.clickButton("btnNext")
    



if __name__ == "__main__":
    #import os
    #os.system("D:\\clear.bat")
    main()