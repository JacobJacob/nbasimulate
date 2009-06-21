#!/usr/bin/python
# -*- coding: utf-8 -*-
"""unit test
"""

import unittest
import urlutil
from urlutil import *

class URLTest(unittest.TestCase):
    
    def test_standardize(self):
        urls = [
                ('http://www.kingsoft.com/a/b/c/index.htm', 
                 'http://www.kingsoft.com/a/b/c/index.htm',
                 'www.kingsoft.com',
                 'kingsoft.com',
                 'http://www.kingsoft.com/',
                 ),
                ('http://www.kingsoft.com.cn/a/b/c/index.htm?', 
                 'http://www.kingsoft.com.cn/a/b/c/index.htm',
                 'www.kingsoft.com.cn',
                 'kingsoft.com.cn',
                 'http://www.kingsoft.com.cn/',
                 ),
                ('http://27.168.29.5/a/b/c/index.htm?abc', 
                 'http://27.168.29.5/a/b/c/index.htm?abc',
                 '27.168.29.5',
                 '27.168.29.5',
                 'http://27.168.29.5/',
                 ),
                ('http://www.kingsoft.com/a/b/c/index.htM?a=aaa', 
                 'http://www.kingsoft.com/a/b/c/index.htm?a=aaa',
                 'www.kingsoft.com',
                 'kingsoft.com',
                 'http://www.kingsoft.com/',
                 ),
                 ('http://localhost/a/b/c/index.htM?a=aaa', 
                 'http://localhost/a/b/c/index.htm?a=aaa',
                 'localhost',
                 'localhost',
                 'http://localhost/',
                 ),
                ('www.kingsoft.com/a/b/c/index.htM?a=aaa', 
                 'http://www.kingsoft.com/a/b/c/index.htm?a=aaa',
                 'www.kingsoft.com',
                 'kingsoft.com',
                 'http://www.kingsoft.com/',
                 ),
                 ('http://mk2@www.kingsoft.com:8080/a/b/c/index.htM?a=aaa', 
                 'http://www.kingsoft.com:8080/a/b/c/index.htm?a=aaa',
                 'www.kingsoft.com',
                 'kingsoft.com',
                 'http://www.kingsoft.com/',
                 ),
                ('www.kingsoft.com', 
                 'http://www.kingsoft.com/',
                 'www.kingsoft.com',
                 'kingsoft.com',
                 'http://www.kingsoft.com/',
                 ),
                 
                ('/a/b/c/index.htm', None),
                ('abf://www.kingsoft.com/a/b/c/index.htM?a=aaa', None),
                ('abcde://www.kingsoft.com/a/b/c/index.htM?a=aaa', None),
                ('abcdef://www.kingsoft.com/a/b/c/index.htM?a=aaa', 
                 'http://www.kingsoft.com/a/b/c/index.htm?a=aaa',
                 'www.kingsoft.com',
                 'kingsoft.com',
                 'http://www.kingsoft.com/',
                 ),
                 
                 ('http://81715239.qzone.qq.com/aaa/sdfsdf/a.html', 
                 'http://81715239.qzone.qq.com/aaa/sdfsdf/a.html',
                 '81715239.qzone.qq.com',
                 'qq.com',
                 'http://81715239.qzone.qq.com/',
                 ),
                 ('http://81715239.qzone.qq.com:8088/aaa/sdfsdf/a.html', 
                 'http://81715239.qzone.qq.com:8088/aaa/sdfsdf/a.html',
                 '81715239.qzone.qq.com',
                 'qq.com',
                 'http://81715239.qzone.qq.com/',
                 ),
                 ('http://a1.a2.a3.a5.a6.81715239.qzone.qq.com/aaa/sdfsdf/a.html', 
                 'http://a1.a2.a3.a5.a6.81715239.qzone.qq.com/aaa/sdfsdf/a.html',
                 'a1.a2.a3.a5.a6.81715239.qzone.qq.com',
                 'qq.com',
                 'http://a1.a2.a3.a5.a6.81715239.qzone.qq.com/',
                 ),
                 ('google.com', 
                 'http://google.com/',
                 'google.com',
                 'google.com',
                 'http://google.com/',
                 ),
                 ('www.google.com', 
                 'http://www.google.com/',
                 'www.google.com',
                 'google.com',
                 'http://www.google.com/',
                 ),
                 ('www.google.com/', 
                 'http://www.google.com/',
                 'www.google.com',
                 'google.com',
                 'http://www.google.com/',
                 ),
                ]
        for u in urls:
            result = standardize(u[0])
#            print u
            if u[1]:
                self.assertEqual(result.geturl(), u[1])
                self.assertEqual(result.host, u[2])
                self.assertEqual(result.domain, u[3])
                self.assertEqual(result.host_url, u[4])
            else:
                self.assertEqual(result, u[1])
                
        self.assertTrue(is_ip('192.16.47.56'))
        
    def test_get_folder(self):
        url = 'http://81715239.qzone.qq.com:8088/aaa/sdfsdf/a.html'
        p1 = get_folder(url)
        self.assertEqual(p1, 'http://81715239.qzone.qq.com:8088/aaa/sdfsdf/')
        p2 = get_folder(p1)
        self.assertEqual(p2, 'http://81715239.qzone.qq.com:8088/aaa/')
        p3 = get_folder(p2)
        self.assertEqual(p3, 'http://81715239.qzone.qq.com:8088/')
        
        p4 = get_folder(p3)
        self.assertEqual(p4, 'http://81715239.qzone.qq.com:8088/')
        
        self.assertEqual(get_folder('http://81715239.qzone.qq.com:8088'), 'http://81715239.qzone.qq.com:8088/')
        
        self.assertEqual(get_folder('81715239.qzone.qq.com:8088'), 'http://81715239.qzone.qq.com:8088/')
        self.assertEqual(get_folder('81715239.qzone.qq.com:8088/aaa-aaa'), 'http://81715239.qzone.qq.com:8088/')
        self.assertEqual(get_folder('81715239.qzone.qq.com:8088/aaa/b'), 'http://81715239.qzone.qq.com:8088/aaa/')
    
    def test_bad_url(self):
        bads = ['http://\xe7\x88\xb1\xe4\xb8\xba\xe4\xbb\x80\xe4\xb9\x88\xe7\x97\x9b\xe8\x8b\xa6/',
                'http://wwwbaiducom/',
                'http://wwwbaidu.',
#                'http://www.baidu.',
#                'http://www.baidu./',
                'http://www,baidu,com/',
                'http://%6b%2e%74%72%71%6a%6a%75%2e%63%6e/',
                
                ]
        not_bads = [('http://www.3322.com./', 'http://www.3322.com/'),
                    ('http://wwwbaidu.com./', 'http://wwwbaidu.com/'),
                    ('http://www.baidu.com.', 'http://www.baidu.com/'),
                   ]
        for url in bads:
#            if standardize(url) is not None:
#                print url, urlutil._host_to_domain(standardize(url).host)
#                print standardize(url).geturl()
            self.assertTrue(standardize(url) is None)
            
        for url, want in not_bads:
            split_url = standardize(url)
            self.assertEqual(split_url.geturl(), want)
    
if __name__ == '__main__':
#    print urlutil._host_to_domain(standardize('http://www.baidu').host)
    unittest.main()