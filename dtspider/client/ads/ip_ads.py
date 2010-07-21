
import traceback
import logging
import win32con
import os

from dtspider.common.spider import Spider
from dtspider.business import http_proxy
from dtspider.common import reg_util
from paitr import IE


def main():
    proxy_infos = http_proxy.get_all_proxy()
    for proxy_info in proxy_infos:
        try:
            set_proxy(proxy_info['proxy'])
            ie = IE()
            ie.show()
            ie.navigate('http://www.china201.com')
            ie.close()
            clear_sys_trash()
        except:
            print traceback.format_exc()
        else:
            print 'success'
        
def set_proxy(proxy):
    reg_util.set_reg_value(win32con.HKEY_CURRENT_USER, 'Software\Microsoft\Windows\CurrentVersion\Internet Settings', 'ProxyServer', proxy)
    reg_util.set_reg_value(win32con.HKEY_CURRENT_USER, 'Software\Microsoft\Windows\CurrentVersion\Internet Settings', 'ProxyEnable', '1')
   
def clear_sys_trash():
    os.system("clear_sys_trash.bat")
 
if __name__ == "__main__":
    main()    