#!/usr/bin/python
# -*- coding: utf-8 -*-
"""unit test
"""

import unittest
from urlmanager import *
from gateway.common_logic.const import MD5_DETAIL
from gateway.common_logic.md5mgr import mkmd5fromstr

class URLManagerTest(unittest.TestCase):
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.rm = URLManager.get_instance()
        self.urls = [
                ('http://www.kingsoft1-test.com/a/b/c/hit.htm#agdf', 'http://www.kingsoft1-test.com/a/b/c/hit.htm', 3, True),
                ('http://www.kingsoft1-test.com/a/b/c/hit.htm', 'http://www.kingsoft1-test.com/a/b/c/hit.htm', 3, True),
                ('http://www.kingsoft1-test.com/a/b/c/hit.htm#agdf', 'http://www.kingsoft1-test.com/a/b/c/hit.htm', 3, True),
                ('http://www.kingsoft1-test.com/a/b/c/index.htm', 'http://www.kingsoft1-test.com/a/b/c/', 4, True),
                ('http://www.kingsoft1-TEST.com/a/b/c/index.htm?q=bad&c=aaaa', 'http://www.kingsoft1-test.com/a/b/c/', 4, True),
                ('http://www.kingsoft1-test.com/a/b/c/index.htm', '', 1, False),
                ('http://www.kingsoft1-test.com/x/x/y/a/b/hit.htm', 'http://www.kingsoft1-test.com/x/x/y/a/b/hit.htm', 3, True),
                ('http://www.kingsoft1-test.com/x/x/y/a/b/a/1hit.htm', 'http://www.kingsoft1-test.com/x/x/y/a/b/', 4, True),
                ('http://www.kingsoft2-test.com/z/x/y/hit.htm', '', 1, True),
                ('http://www.kingsoft2-test.com', '', 1, True),
                ('http://www.kingsoft2-test.com/', '', 1, True),
                ('http://www.kingsoft3-test.com', 'http://www.kingsoft3-test.com/', 3, True),
                ('http://www.kingsoft3-test.com/a', 'http://www.kingsoft3-test.com/a', 3, True),
                ('http://www.kingsoft3-test.com/a/b/c/d/e/', 'http://www.kingsoft3-test.com/', 4, True),
                ('http://www.kingsoft3-test.com/a/b/c/d/', 'http://www.kingsoft3-test.com/', 4, True),
                ('http://www.kingsoft3-test.com/a/b/c/', 'http://www.kingsoft3-test.com/', 4, True),
                ('http://www.kingsoft3-test.com/a/b/c', 'http://www.kingsoft3-test.com/', 4, True),
                ('http://www.kingsoft3-test.com/a/', 'http://www.kingsoft3-test.com/', 4, True),
                ('http://www.kingsoft3-test.com/a', 'http://www.kingsoft3-test.com/a', 3, True),
                ('http://www.kingsoft3-test.com/z/x/y/hit.htm', 'http://www.kingsoft3-test.com/', 4, True),
                ('http://98.126.33.114/html/j/list_7.html', 'http://98.126.33.114/html/j/', 4, True),
                ('http://98.126.33.114/html/j/list_4.html', 'http://98.126.33.114/html/j/list_4.html', 3, False),
                ('http://98.126.33.114/html/j/list_5.html', 'http://98.126.33.114/html/j/list_5.html', 3, True),
                ('http://98.126.33.114/html/j/list_6.html', 'http://98.126.33.114/html/j/list_6.html', 3, True),
                ('http://98.126.33.114/html/j/list_8.html', 'http://98.126.33.114/html/j/', 4, True),
                ('http://98.126.33.114/html/j/a/b/list_8.html', 'http://98.126.33.114/html/j/', 4, True),
                ('http://98.126.33.114/html/j/a/b/c/list_8.html', '', 1, True),
                ('http://98.126.33.114/html/', '', 1, True),
                ('http://98.126.33.114/html/j', 'http://98.126.33.114/html/j', 3, True),
                ('http://98.126.33.114/html/j/', 'http://98.126.33.114/html/j/', 3, True),
                ('98.126.33.114/html/j/', 'http://98.126.33.114/html/j/', 3, True),
                ('http://www.google.cn/', '', 1, True),
                ('http://www.test1000000.com/', '', 1, True),
#                (u'http://m.1518.com/index.aspx?mainurl=http://m.1518.com/carcardtest.aspx?number=dg608&name=&sex=\u016e&birthyear=1980&birthmonth=07&birthday=20&birthhour=07&birthminute=53&blood=a&blood=car<',
#                 u'http://m.1518.com/index.aspx?mainurl=http://m.1518.com/carcardtest.aspx?number=dg608&name=&sex=\u016e&birthyear=1980&birthmonth=07&birthday=20&birthhour=07&birthminute=53&blood=a&blood=car<',
#                 3, False)
                ]
        for u in self.urls:
            if u[2] == 3:
                self.rm._save_black_url(u[1])
        
    
    def test_singleton(self):
        self.assertEqual(id(self.rm), id(URLManager.get_instance()))
    
    def test_get_parent_folder(self):
        url = 'http://www.kingsoft.com/a/b/c/index.htm'
        f1 = self.rm._get_folder(url)
        self.assertEqual(f1, 'http://www.kingsoft.com/a/b/c/')
        f2 = self.rm._get_folder(f1)
        self.assertEqual(f2, 'http://www.kingsoft.com/a/b/')
        f3 = self.rm._get_folder(f2)
        self.assertEqual(f3, 'http://www.kingsoft.com/a/')
        f4 = self.rm._get_folder(f3)
        self.assertEqual(f4, 'http://www.kingsoft.com/')
        f5 = self.rm._get_folder(f4)
        self.assertEqual(f5, None)
        
        self.assertEqual(self.rm._get_folder('http://www.kingsoft.com/a/b/c'), 
                         'http://www.kingsoft.com/a/b/')
        self.assertEqual(self.rm._get_folder('http://www.kingsoft.com/a/b/casdf.html?asdf=sdfwef'), 
                         'http://www.kingsoft.com/a/b/')
        
        self.assertEqual(self.rm._get_folder('ftp://g.cn/'), None)
        self.assertEqual(self.rm._get_folder('ftp://g.cn'), None)
        self.assertEqual(self.rm._get_folder('ftp://g.cn/a/'), 'ftp://g.cn/')
        self.assertEqual(self.rm._get_folder('ftp://g.cn/a'), 'ftp://g.cn/')
        
    def test_format_url(self):
        self.assertEqual(self.rm._format_url('http://www.kingsoft.com/a/b/casdf.html?asdf=sdfwef'), 
                         ('http://www.kingsoft.com/a/b/casdf.html?asdf=sdfwef', 'http://www.kingsoft.com/'))
        self.assertEqual(self.rm._format_url('http://www.kingsoft.com'), 
                         ('http://www.kingsoft.com/', 'http://www.kingsoft.com/'))
        self.assertEqual(self.rm._format_url('http://www.kingsoft.com/?test=abd要#aaaaaaa'), 
                         ('http://www.kingsoft.com/?test=abd要', 'http://www.kingsoft.com/'))
        self.assertEqual(self.rm._format_url('http://www.kingsoft.com:8080/'), 
                         ('http://www.kingsoft.com:8080/', 'http://www.kingsoft.com:8080/'))
        
    
    def test_get_url(self):
        # query, hit, type
        for url in self.urls:
#            print '-' * 10
#            print url
#            
            r = self.rm.get_url(url[0], url[-1])
#            print r
            self.assertEqual(r[MD5_DETAIL.QUERY_KEY], url[0].lower())
            self.assertEqual(r[MD5_DETAIL.HIT_KEY], url[1])
            self.assertEqual(r[MD5_DETAIL.TYPE], url[2])
            
    def _safeurl_test(self, url, hit_key, type):
        print url, type
        r = self.rm.get_url(url, False)
        self.assertEqual(r['query_key'], url)
        self.assertEqual(r['hit_key'], hit_key)
        self.assertEqual(r['type'], type)
        
    def test_batchs(self):
        from gateway.common_logic.db.connection_pool import ConnectionPool
        cursor = ConnectionPool.cursor()
        try:
            rs = cursor.fetchall('select url from black_url_a')
            for r in rs:
#                if 'http://98.126.33.114/html/j' not in r['url']:# != 'http://98.126.33.114/html/j/list_4.html':
#                    continue
                self._safeurl_test(r['url'], r['url'], 3)
#                break
        finally:
            cursor.close()

if __name__ == '__main__':
    unittest.main()