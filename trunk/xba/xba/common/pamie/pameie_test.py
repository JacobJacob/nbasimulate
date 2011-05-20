'''
Created on 2009-7-19

@author: Administrator
'''

from common.pamie.PAM30 import PAMIE

##ie = PAMIE(url='http://www.google.com')
#ie = PAMIE()
##ie.clickButton(name)
##links = ie.getLinks()
##ie.showAllTableText()
#ie_b = ie.findWindow('Google')
#ie.locationURL()
#print type(ie_b)
#ie._ie = ie_b
#ie.quit()


ie = PAMIE.attach('http://www.google.cn/')
ie.quit()