#!/usr/bin/python
# -*- coding: utf-8 -*-

from xba.common.pamie.pam import PAMIE
from xba.common.file_utility import remove_dir_tree
import traceback
import os

def _clear_ie(): 
    """删除 IE cookies，临时文件: http://www.jaycn.com/"""
    username = os.environ['username']
    for path in [r'C:\Documents and Settings\%s\Cookies',
             r'C:\Documents and Settings\%s\Local Settings\Temporary Internet Files',
             r'C:\Documents and Settings\%s\Local Settings\History',
             r'C:\Documents and Settings\%s\Local Settings\Temp',]:
        try:
            remove_dir_tree(path % username)
        except:
            pass

def main():

    for i in range(500):
        reg(i)
    
def reg(i):
    
    import os
    #os.system("D:\\clear.bat")
    
    start = 2052
    
    ie = PAMIE("http://localhost:34591/xbaweb/login.aspx")
    
    ie.setTextBox("tbUserName", "sxwailyc-%s" % (start + i))
    ie.setTextBox("tbPassword", "123")
    import time
    #time.sleep(1000)
    ie.clickButton("btnLogin")
    
    ie.frameName = "Main.Center"
    
    ie._frameWait()
    
    if ie.buttonExists("btnNext"):
        ie.clickButton("btnNext")
    
    if not ie.textBoxExists("tbClubName"):
        print "tbClubName not exist"
    else:
        ie.setTextBox("tbClubName", "Club%s" % (start + i))
        ie.clickButton("btnRegClub")
        ie._wait()
    
    ie.getCheckBox("cb20").click()
    ie.getCheckBox("cb19").click()
    ie.getCheckBox("cb18").click()
    ie.getCheckBox("cb17").click()
    ie.getCheckBox("cb16").click()
    ie.getCheckBox("cb15").click()
    ie.getCheckBox("cb14").click()
    ie.getCheckBox("cb13").click()    
    
    ie.clickButton("btnOK")
    
    ie._wait()
    
    ie.clickButton("btnNext")
    
    ie._wait()
    
    ie.getCheckBox("cb8").click()
    ie.getCheckBox("cb7").click() 
    ie.getCheckBox("cb6").click() 
    ie.getCheckBox("cb5").click() 
    
    ie.clickButton("btnOK")
    
    ie._wait()
    
    ie.clickButton("btnNext")
    
    ie.navigate("http://localhost:34591/xbaweb/LogOut.aspx")

    ie._wait()
     
    ie.quit()


if __name__ == "__main__":
    #import os
    #os.system("D:\\clear.bat")
    main()