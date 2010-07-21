'''
Created on 2009-9-21

@author: jackyshi
'''
import win32api
import win32con

def set_reg_value(key, sub_key, item, value):
    reg_key = win32api.RegOpenKey(key, sub_key, 0, win32con.KEY_ALL_ACCESS)
    win32api.RegSetValueEx(reg_key, item, 0, win32con.REG_SZ, value)
    win32api.RegCloseKey(reg_key) 
   
def get_reg_value(key, sub_key, item): 
    reg_key = win32api.RegOpenKey(key, sub_key, 0, win32con.KEY_ALL_ACCESS)
    reg_value = win32api.RegQueryValueEx(reg_key, item)
    return reg_value
    
def test():
    set_reg_value(win32con.HKEY_CURRENT_USER, 'Software\Microsoft\Windows\CurrentVersion\Internet Settings', 'ProxyServer', '20.35.68.57:80')
    set_reg_value(win32con.HKEY_CURRENT_USER, 'Software\Microsoft\Windows\CurrentVersion\Internet Settings', 'ProxyEnable', '1')
    print get_reg_value(win32con.HKEY_CURRENT_USER, 'Software\Microsoft\Windows\CurrentVersion\Internet Settings', 'ProxyEnable')
    print get_reg_value(win32con.HKEY_LOCAL_MACHINE, 'SOFTWARE\\Microsoft\\Internet Explorer', 'Version')
    
    
if __name__ == '__main__':
    test()
