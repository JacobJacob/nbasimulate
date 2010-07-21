#-*- coding:utf-8 -*-

import traceback
import re
from datetime import datetime

from dtspider.common.db import connection
from paitr import IE


class QQSpace(object):
    
    
   LINK_RE = re.compile('^http://user.qzone.qq.com/(?P<qq>\d+)/?$', re.VERBOSE)
    
   def __init__(self, url):
       self._ie = IE.attach(url)
       self._visted_cache = []
       self._visted_cache.append(url)
       self._visted_cache.append('http://user.qzone.qq.com/1181290794')
       self._current_work_ie = None
       self._current_level = 0
       self._max_level = 10
       
   def run(self):
       title = self._ie.title
       print title
       links = self._ie.links()
       for link in links:
           if self.is_space_link(link.href):
               #print '%s is a space link, handle it' % link.href
               if link.href in self._visted_cache:
                   #print '%s visited, skip it' % link.href
                   pass
               else:
                   self.handle_single_space(link)
           else:
               pass
               #print '%s, %s is not a space link, skip it' % (link.title, link.href)
   
   def close_forbidden_window(self):
       try:
           try_count = 0
           while True:
               ie = IE.attach(u'QQ空间--访问受限', 'title')
               if ie:
                   ie.close()
                   break
               else:
                   try_count += 1
                   if try_count > 3:
                       break
       except:
           print traceback.format_exc(3)
   
   def handle_single_space(self, link):
       '''handle single space'''
       qq = self.get_qq(link.href)
       if self.is_visited(qq):
           print u'%s 处理过了，跳过......'
           return
       if qq == '1181290794':
           print u'自己,跳过'
           return
       #self._visted_cache.append(link.href)
       print u"开始进入处理%s:%s, 当前深度:%s, 到目前共踩过的空间为：%s" % (link.title, link.href, self._current_level, len(self._visted_cache))
       link.click()
       url = link.href
       ie = IE.attach(url)
       if ie is None:
           print u'无法获取该窗口 :%s ......' % url
           print u'关闭禁止的窗口......'
           self.close_forbidden_window()
           self.save_log(qq)
           return False
       else:
           self.save_log(qq, ie.title)
           if self._current_level > self._max_level:
               print u'深度超过最大深度，返回'
               ie.close()
               self._current_level -= 1 
               return
           else:
               self._current_level += 1
               links = ie.links()
               print u'共获取%s个链接' % len(links)
               if self.click_recent_link(ie):
                   links = ie.links()
                   print u'点击最近访客成功!!!'
               else:
                   print u'点击最近访客失败,返回!!!'
                   ie.close()
                   return
               for link in links:
                   if not self.is_space_link(link.href):
                       #print u'%s 不是一个空间的链接，后退......' % link.href
                       #import time
                       #time.sleep(1)
                       continue
                   print u'%s 是一个空间的链接，处理......' % link.href
                   self.handle_single_space(link)
                   #print 'close space :%s' % url 
                   #self._current_work_ie.close()
                   #self.handle_single_space(link)
               #所有子的连接处理完后关掉自己
      
               ie.close() 
               
   def get_qq(self, url):
       '''get qq'''
       match = QQSpace.LINK_RE.search(url)
       if match:
           d = match.groupdict()
           return d['qq']
       return None
        
   def save_log(self, qq, title=None):
       log_info = {'qq': qq, 'title': title, 'visited_at': datetime.now(), 'created_at': datetime.now()}
       cursor = connection.cursor()
       try:
           cursor.insert(log_info, 'qqspace_log', True, ['created_at', ])
       except:
           print traceback.format_exc(3)
       finally:
           cursor.close()
           
           
   def is_visited(self, qq):
       cursor = connection.cursor()
       try:
           data = cursor.fetchone('select visited_at from qqspace_log where qq=%s and updated_at=now()', (qq, ))
           if data:
               return True
       except:
           print traceback.format_exc(3)
       finally:
           cursor.close()
       return False    
                         
   def click_recent_link(self, ie):
       try_count = 0
       while True:
           links = ie.links()
           for link in links:
               #print link.title
               if link.title and link.title == u'最近访客':
                   link.click()
                   ie._wait()
                   return True
           try_count += 1
           ie.refresh()
           import time
           time.sleep(4)
           ie._wait()
           if try_count > 3:
               break
       return False
       
   def is_space_link(self, url):
       '''check the link if is a space link'''
       try:
           match = QQSpace.LINK_RE.findall(url)
           if match:
               return True
       except:
           print traceback.format_exc(3)
       return False
   
   def start(self):
       try:
           self.run()
       except:
           print traceback.format_exc(3)
   
def main():
    client = QQSpace('http://1181290794.qzone.qq.com/')
    client.start()
    #client.close_forbidden_window()
    
def test():
    #ie = IE.attach('http://user.qzone.qq.com/840990085')
    #links = ie.links()
    #for link in links:
        #print link.href, link.title
         
    log_info = {'qq': 'ttt', 'title': 'title', 'visited_at': datetime.now(), 'created_at': datetime.now()}
    cursor = connection.cursor()
    try:
        cursor.insert(log_info, 'qqspace_log', True, ['created_at', ])
    except:
        print traceback.format_exc(3)
    finally:
        cursor.close()
       
if __name__ == '__main__':
    main()    
    #test()