#!/usr/bin/python
# -*- coding: utf-8 -*-

import unittest
import spider

class Spider_test(unittest.TestCase):
    
    huajuntemplates = [
        {'same_level': ur"""
                <a\s[^<>]*            # <a title="...."
                href=[\'\"]            # href="
                (all_\d*.htm)        #url
                [\'\"]                #"
                [^<>]*>                # xxxx>
                [^<>]*                #a's innerHTML
                </a>                  #end of a
            """,
         'next_level': ur"""
                 <a[^<>]*?
                 href=[\'\"]
                 (../soft/\d*.htm)
                 [\'\"]
                 [^<>]*?>
                 [\S\s]*?
                 </a>
            """,
         'result_start': True,
        },
        {
         'downurl': ur"""
                 new\sArray\([\w\W\s]*?,'
                ((?:http|ftp)://[\w\W]*?\.(?:zip|exe|rar))
                '\)
            """,
            'info': ur"""
                <h1>(?P<name>[^<>]*?)</h1>
                (?:[\s\S]*?
                <td[^<>]*?><strong>软件大小：</strong>(?P<size>[^<>]*?)</td>)?
                (?:[\s\S]*?
                <td[^<>]*?><strong>软件类别：</strong>(?P<type>[^<>]*?)</td>)?
                (?:[\s\S]*?
                <td[^<>]*?><strong>软件授权：</strong><span[^<>]*?>(?P<copyright>[^<>]*?)</span></td>)?
                (?:[\s\S]*?
                <td[^<>]*?><strong>软件语言：</strong>(?P<language>[^<>]*?)</td>)?
                (?:[\s\S]*?
                <td[^<>]*?><strong>运行环境：</strong><span[^<>]*?>(?P<platform>[^<>]*?)</span></td>)?
                (?:[\s\S]*?
                <td[^<>]*?><strong>更新时间：</strong>(?P<last_updated>[^<>]*?)</td>)?
                (?:[\s\S]*?
                <td[^<>]*?><strong>开\s发\s商：</strong><a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>[^<>]*?</a></td>)?
                (?:[\s\S]*?
                <p>软件详细信息</p>
                [\s\S]*?
                <td[^<>]*?><p>
                (?P<description>[\s\S]*?)
                <script[^<>]*?></script>)?
            """
        },
    ]
    
    pconlinemplates = [
    {
     'next_level': ur"""
             <span\sclass=\"top_name\">
             <a[^<>]*?
             href=[\'\"]
             (http://dl\.pconline\.com\.cn/[^<>]*?.html)
             [\'\"]
             [^<>]*?>
             [\S\s]*?
             </a>
        """,
    },
    {
     'next_level': ur"""
            var\surl\s=\sdocument.URL;\s*?
            url\s=\surl.replace\(\"(.*?)\",\"(.*?)\"\)
        """,
     'next_level_callback': u"""
def next_level_callback(current_url, catch_result):
    return current_url.replace(catch_result[0], catch_result[1])
""",
        'info': ur"""
            <span[^<>]*?>软件名称</span><h1>(?P<name>[^<>]*?)</h1>
            [\s\S]*?
            <span[^<>]*?>官方网站</span><span[^<>]*?><a[^<>]*?href=['"](?P<officialweb>[^<>]*?)['"][^<>]*?>[^<>]*?</a></span>
            [\s\S]*?
            <span[^<>]*?>软件平台</span><span[^<>]*?>(?P<platform>[^<>]*?)</span>
            [\s\S]*?
            <span[^<>]*?>整理日期</span><span[^<>]*?>(?P<last_updated>[^<>]*?)</span>
            [\s\S]*?
            <span[^<>]*?>软件大小</span><span[^<>]*?>(?P<size>[^<>]*?)</span>
            [\s\S]*?
            <b>[^<>]*?软件简介</b>
            [\s\S]*?
            </script></span></div>\s*?
            <div[^<>]*?>
            (?P<description>[\s\S]*?)
            </div>
        """
    },
    {
     'downurl': ur"""
        <dd><a[^<>]*?
        href=\'
        (http://[^<>]*?/filedown\.jsp[^<>]*?)
        \'>[^<>]*?</a></dd>
    """,
    }
]
    
    crsky_templates = [
        {'same_level': ur"""
            <a[^<>]*?href=(\.\./new/0_\d*?\.html)[^<>]*?>\d*?</a>
        """,
         'next_level': ur"""
            <a[^<>]*?href="(../soft/\d*.html)">[^<>]*?</a>
        """,
        },
        {'next_level': ur"""
            <script[^<>]*?src="(/view_down\.asp?[^<>]*?SoftID=\d*[^<>]*?softname=[^<>]*?)"></script>
        """,
         'info': ur"""
             <title>(?P<name>[^<>]*?)\s-\s霏凡软件站</title>
             (?:[\s\S]*?
                <TD[^<>]*?>软件大小</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<size>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>软件类别</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<type>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>运行环境</TD>[\s]*
                <TD[^<>]*?>(?P<platform>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>授权方式</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<copyright>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>软件等级</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*<IMG[^<>]*?src="[^<>]*?images/(?P<rate>[^<>]*?)star.gif"[^<>]*?></TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>软件语言</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<language>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>整理时间</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<last_updated>[^<>]*?)</TD>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>相关连接</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*<a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>
             )?
             (?:[\s\S]*?
                <TD[^<>]*?>下载次数</TD>[\s]*
                <TD[^<>]*?>(?:&nbsp;)*(?P<downcount>[^<>]*?)</TD>
             )?
         """,
        },
        {'downurl': ur"""
            <a[^<>]*?href="(http://[^<>]*?\.crsky\.com/view_down.asp\?down_url=[^<>]*?)"[^<>]*?>[^<>]*?</a>
        """
        }
    ]
    
    freewarebox_templates = [
        {'same_level': ur"""
            <a[^<>]*?href="(browse_\w_freeware\.html)"[^<>]*?>\w*</a>
        """,
         'next_level': ur"""
            <p[^<>]*?><a[^<>]*?href="(free_\d*_[^<>]*?-download.html)"[^<>]*?>[^<>]*?</a>[\s\S]*?</p>
        """,
        },
        {'next_level': ur"""
            <p[^<>]*?><strong>Download\spage\s:\s</strong><a[^<>]*?href="([^<>]*?)"[^<>]*?>Download</a></p>
        """,
         'info': """
             <div[^<>]*?id="softview1">\s*?
             <h1><span[^<>]*?>(?P<name>[\s\S]*?)</span>[^<>]*?<span[^<>]*?>Freeware\sVersion\s:\s(?P<version>[^<>]*?)</span></h1>
             (?:[\s\S]*?
             <p[^<>]*?><strong>Author\s:\s</strong>(?P<company>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>OS\s:\s</strong>(?P<platform>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>[^<>]*?license\s:\s</strong>(?P<copyright>[^<>]*?)\s*(?:\([\s\S]*?\))?</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>Added\sdate\s:\s</strong>(?P<last_updated>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>Download\sSize\s:\s</strong>(?P<size>[^<>]*?)</p>)?
             (?:[\s\S]*?
             <p[^<>]*?><strong>Freeware\sdescription\s:\s</strong>(?P<description>[\s\S]*?)</p>)?
         """,
        },
        {'downurl': ur"""
            <a[^<>]*?href="([^<>]*?)"[^<>]*?>Download\shere</a>
        """
        }
    ]
    
    freewareweb_templates = [
        {'next_level': ur"""
        <a[^<>]*?href="([^<>]*?/cgi-bin/[^<>]*?\.cgi\?Category=[^<>]*?)"[^<>]*?>[^<>]*?</a>
        """,
        },
        {'same_level': ur"""
        <a[^<>]*?href="([^<>]*?/cgi-bin/[^<>]*?\.cgi\?Category=[^<>]*?nh=\d*)"[^<>]*?>[^<>]*?</a>
        """,
        'next_level': ur"""
            <a[^<>]*?href="([^<>]*?\.cgi\?ID=\d*)"><font[^<>]*?>Link
        """,
        },
        {'next_level': ur"""
            <B>Download\sNow:</B>[^<>]*?<A[^<>]*?HREF="([^<>]*?)">[^<>]*?</A>
        """,
         'info': """
             <TITLE>FreewareWeb\s*-\s*Freeware:\s*(?P<name>[^<>]*?)\s*-[^<>]*?</TITLE>
             (?:[\s\S]*?
             <B>Homepage[^<>]*?:[^<>]*?</B>\s*<a[^<>]*?href="(?P<officialweb>[^<>]*?)"[^<>]*?>[^<>]*?</a>)?
             (?:[\s\S]*?
             <B>Category[^<>]*?:[^<>]*?</B><A[^<>]*?>(?P<type>[^<>]*?)</a>)?
             (?:[\s\S]*?
             <B>Our\sRating:</B>[^<>]*?<IMG[^<>]*?SRC="/(?P<rate>[\d\.]*).gif"[^<>]*?>)?
             (?:[\s\S]*?
             <B>Description:</B>(?P<description>[\s\S]*?)<p[^<>]*?>[^<>]*?<IMG[^<>]*?SRC="/bluedown.gif"[^<>]*?>)?
             (?:[\s\S]*?
             <B>Download\sNow:</B>\s*<A[^<>]*?>[^<>]*?\[(?P<size>[^<>]*?)\][^<>]*?</A>)?
         """,
        },
        {'downurl': ur"""
            <A[^<>]*?HREF="([^<>]*?)">To\s*Start\s*your
        """
        }
    ]
    
    freewarearena_templates = [
        {'next_level': ur"""
        <a[^<>]*?href="(Downloads/c=\d*\.html)">[^<>]*?</a>
        """,
        },
        {'same_level': ur"""
             <a[^<>]*?href="(Downloads/c=\d*/page=\d*\.html)">\d*</a>
        """,
        'next_level': ur"""
            <a[^<>]*?href="(Downloads/details/id=\d*\.html)">[^<>]*?</a>
        """,
        'next_level_callback': u"""
def next_level_callback(current_url, catch_result):
    return 'http://freewarearena.com/html/' + catch_result
""",
        },
        {'next_level': ur"""
            <a[^<>]*?href="(Downloads/get=\d*.html)"[^<>]*?>Download\sNow</a>
        """,
        'next_level_callback': u"""
def next_level_callback(current_url, catch_result):
    return 'http://freewarearena.com/html/' + catch_result
""",
         'info': """
             <span[^<>]*?><strong[^<>]*?>(?P<name>[^<>]*?)</strong></span>[\s\S]*?<a[^<>]*?>Main\sPage</a>
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>Category</span></td>\s*<td[^<>]*?><a[^<>]*?>(?P<type>[^<>]*?)</a></td>
             )?
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>Author's\sName</span></td>\s*<td[^<>]*?>(?P<company>[\s\S]*?)</td>
             )?
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>Homepage</span></td>\s*<td[^<>]*?><a[^<>]*?>(?P<officialweb>[\s\S]*?)</a></td>
             )?
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>Version</span></td>\s*<td[^<>]*?>(?P<version>[^<>]*?)</td>
             )?
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>Compatibility</span></td>\s*<td[^<>]*?>(?P<platform>[^<>]*?)</td>
             )?
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>File\sSize</span></td>\s*<td[^<>]*?>(?P<size>[^<>]*?)</td>
             )?
             (?:[\s\S]*?
             <td[^<>]*?><span[^<>]*?>Last\sUpdated</span></td>\s*<td[^<>]*?><div[^<>]*?>(?P<last_updated>[^<>]*?)</div></td>
             )?
             (?:[\s\S]*?
             <span[^<>]*?><strong[^<>]*?>Description</strong></span><br[^<>]*?/>\s*<div[^<>]*?>(?P<description>[\s\S]*?)</div>
             )?
         """,
        },
        {'next_level': ur"""
            <a[^<>]*?href="(Downloads/get=\d*/mirror=\d*\.html)"[^<>]*?>[^<>]*?</a>
        """,
        'next_level_callback': u"""
def next_level_callback(current_url, catch_result):
    return 'http://freewarearena.com/html/' + catch_result
""",
        },
        {'downurl': ur"""
            get\s*it\s*<a[^<>]*?href="([^<>]*?)"[^<>]*?>here</a>
        """
        }
    ]
    
    def setUp(self):
        "Hook method for setting up the test fixture before exercising it."
        self.huajun_spider = spider.Spider('http://www.newhua.com/new/n0.htm', 
                                           self.huajuntemplates)
        self.pconline_spider = spider.Spider('http://dl.pconline.com.cn/soft_update.html', 
                                             self.pconlinemplates)
        self.crsky_spider = spider.Spider('http://www.crsky.com/new/0_1.html', 
                                          self.crsky_templates)
        self.freewarebox_spider = spider.Spider('http://www.freewarebox.com/browse_a_freeware.html', 
                                                self.freewarebox_templates)
        self.freewareweb_spider = spider.Spider('http://www.freewareweb.com/software.shtml',
                                                self.freewareweb_templates)
        self.freewarearena_spider = spider.Spider('http://freewarearena.com/html/Downloads.html',
                                                  self.freewarearena_templates)
        
        
#    def test_freewarebox_urls(self):
##        content, info = self.freewarebox_spider.get_webinfo(self.freewarebox_spider.start_url)
##        urls = self.freewarebox_spider.get_urls(content, 
##                        self.freewarebox_templates[0]['next_level']) 
##        for u in urls: print u
##        urls = self.freewarebox_spider.get_urls(content, 
##                        self.freewarebox_templates[0]['same_level'])     
##        print '-' * 50
##        for u in urls: print u
#        for level, url, parenturl, info, \
#                    content, downurls, softinfo in self.freewarebox_spider.walk():
#            print '-' * 50
#            print level, url, softinfo
#            print downurls 
#        content, info = self.freewarebox_spider.get_webinfo('http://www.freewarebox.com/download_5915_0-mass-mailing-freeware-paseo.html')
#        print self.freewarebox_spider.get_urls(content, 
#                        self.freewarebox_templates[2]['downurl']) 
        
#    def test_freewarebox_get_info(self):
#        urls = ['http://www.freewarebox.com/free_4235_absolute-smart-hextris-download.html',
#                'http://www.freewarebox.com/free_182_-the-prison-(interactive-desktop-mac)-download.html',
#                'http://www.freewarebox.com/free_5915_0-mass-mailing-freeware-paseo-download.html',]
#        for url in urls:
#            content = self.freewarebox_spider.get_webinfo(url)[0]
#            softinfo = self.freewarebox_spider.get_info(content, self.freewarebox_templates[1]['info'])
#            self.assertTrue(softinfo)
#            print u'\r\n------------------------------'
#            for key, val in softinfo.iteritems():
#                print u'%s: %s' % (key, val)
#            
#            print self.freewarebox_spider.get_urls(content, 
#                                                   self.freewarebox_templates[1]['next_level']) 
        
    def test_huajun(self):
        for level, url, parenturl, response, \
                    content, downurls, softinfo in self.huajun_spider.walk():
            print level, url
            if softinfo:
                print softinfo['name'], softinfo
            print downurls
    
#    def test_pconline_get_info(self):
#        urls = [
#                'http://dl.pconline.com.cn/download/51797.html',
##                'http://dl.pconline.com.cn/html_2/1/63/id=3632&pn=0.html',
##                'http://dl.pconline.com.cn/html_2/1/123/id=48451&pn=0.html',
#                ]
#        for url in urls:
#            content = self.pconline_spider.get_webinfo(url)[0]
#            softinfo = self.pconline_spider.get_info(content, self.pconlinemplates[1]['info'])
#            self.assertTrue(softinfo)
#            print u'\r\n------------------------------'
#            for key, val in softinfo.iteritems():
#                print u'%s: %s' % (key, val)
        
#    def test_pconline_get_downurls(self):
#        urls = [
#                'http://dl.pconline.com.cn/soft_update.html',
#                ]
#        for url in urls:
#            for level, url, parenturl, info, \
#                    content, downurls, softinfo in self.pconline_spider.walk(url):
#                print level, url, parenturl, len(downurls), softinfo

#    def test_brothersoft(self):
#        return
#        for level, url, parenturl, info, \
#                    content, downurls, softinfo in self.brothersoft_spider.walk():
#            print '-' * 60
#            if softinfo:
#                print level, url, len(downurls), softinfo['name'], softinfo['size']
#            else:
#                print level, url, downurls
##    
#    def test_brothersoft_info(self):
#        urls = [
#                'http://www.brothersoft.com/numerical-converter-183100.html',
#                'http://www.brothersoft.com/3d-ocean-sunset-for-mac-x-188402.html',
#                ]
#        for url in urls:
#            response = self.brothersoft_spider.request(url)
#            content = self.brothersoft_spider.get_content(response)
##            print self.brothersoft_templates[1]['info']
#            softinfo = self.brothersoft_spider.get_info(content, self.brothersoft_templates[1]['info'])
#            self.assertTrue(softinfo)
#            print u'\r\n------------------------------'
#            for key, val in softinfo.iteritems():
#                print u'%s: %s' % (key, val)
#    
##    def test_brothersoft_downurl(self):
##        urls = [
##                'http://www.brothersoft.com/numerical-converter-183100.html',
##                'http://www.brothersoft.com/shooting-star-9650.html',
##                ]
##        for url in urls:
##            content = self.brothersoft_spider.get_webinfo(url)[0]
###            print self.brothersoft_templates[1]['info']
##            softinfo = self.brothersoft_spider.get_info(content, self.brothersoft_templates[1]['info'])
##            self.assertTrue(softinfo)
##            print u'\r\n------------------------------'
##            for key, val in softinfo.iteritems():
##                print u'%s: %s' % (key, val)
#
#    def test_crsky(self):
#        return
#        for level, url, parenturl, response, \
#                content, downurls, softinfo in self.crsky_spider.walk():
#            print '-' * 50
#            print level, url, parenturl, len(downurls)
#            print softinfo
#            print downurls
#
#    def test_crsky_info(self):
#        return
#        urls = ['http://www.crsky.com/soft/13490.html',]
#        for url in urls:
#            content = self.crsky_spider.get_webinfo(url)[0]
#            softinfo = self.crsky_spider.get_info(content, self.crsky_templates[1]['info'])
#            self.assertTrue(softinfo)
#            print u'\r\n------------------------------'
#            for key, val in softinfo.iteritems():
#                print u'%s: %s' % (key, val)
#
#    def test_crsky_geturls(self):
#        return
#        content = self.crsky_spider.get_webinfo('http://www.crsky.com/soft/1187.html')[0]
#        print self.crsky_spider.get_urls(content, self.crsky_templates[1]['next_level'])
#    
#    def test_freewareweb_geturls(self):
#        return
##        content = self.freewareweb_spider.get_webinfo('http://www.freewareweb.com/software.shtml')[0]
##        urls = self.freewareweb_spider.get_urls(content, 
##                                               self.freewareweb_templates[0]['next_level'])
###        for u in urls:
###            print u
##        print len(urls)
##        
##        content = self.freewareweb_spider.get_webinfo('http://www.freewareweb.com/cgi-bin/archive.cgi?Category=Font+Tools&sb=0&so=descend&nh=2')[0]
##        urls = self.freewareweb_spider.get_urls(content, 
##                                               self.freewareweb_templates[1]['same_level'])
###        for u in urls:
###            print u
##        print len(urls)
##        urls = self.freewareweb_spider.get_urls(content, 
##                                               self.freewareweb_templates[1]['next_level'])
###        for u in urls:
###            print u
##        print len(urls)
#        
#        content, response = self.freewareweb_spider.get_webinfo('http://163.com')
#        for k in response.headers:
#            print k, response.headers[k]
#        urls = self.freewareweb_spider.get_urls(content, 
#                                               self.freewareweb_templates[3]['downurl'])
#        for u in urls:
#            print u
#        print len(urls)
#    
#    def test_freewareweb_info(self):
#        return
#        urls = ['http://www.freewareweb.com/cgi-bin/archive.cgi?ID=1507',
#                'http://www.freewareweb.com/cgi-bin/archive.cgi?ID=363']
#        for url in urls:
#            content = self.freewareweb_spider.get_webinfo(url)[0]
#            softinfo = self.freewareweb_spider.get_info(content, self.freewareweb_templates[2]['info'])
#            self.assertTrue(softinfo)
#            print u'\r\n------------------------------'
#            for key, val in softinfo.iteritems():
#                print u'%s: %s' % (key, val)
#            
#            print self.freewareweb_spider.get_urls(content, 
#                                               self.freewareweb_templates[2]['next_level'])
#            
#    def test_freewareweb(self):
#        return
#        for level, url, parenturl, response, \
#                content, downurls, softinfo in self.freewareweb_spider.walk():
#            print '-' * 50
#            print level, url, parenturl, len(downurls)
#            print softinfo
#            print downurls
#    
#    def test_freewarearena_info(self):
#        return
#        urls = ['http://freewarearena.com/html/Downloads/details/id=1304.html',
#                'http://freewarearena.com/html/Downloads/details/id=1347.html',
#                'http://freewarearena.com/html/Downloads/details/id=503.html']
#        for url in urls:
#            content = self.freewarearena_spider.get_webinfo(url)[0]
#            softinfo = self.freewarearena_spider.get_info(content, 
#                                                          self.freewarearena_templates[2]['info'])
#            self.assertTrue(softinfo)
#            print u'\r\n------------------------------'
#            for key, val in softinfo.iteritems():
#                print u'%s: %s' % (key, val)
#            
##            print self.freewarearena_spider.get_urls(content, 
##                                               self.freewarearena_templates[2]['next_level'])
#            
#    def test_freewarearena(self):
#        return
#        for level, url, parenturl, response, \
#                content, downurls, softinfo in self.freewarearena_spider.walk():
#            print '-' * 50
#            print level, url, parenturl, len(downurls)
#            print softinfo
#            print downurls
#    
#def add(a, b):
#    print '加法: add %d + %d = %d' % (b, a, a+b)
#    return a+b
#
#def test_parser():
#    src = u"""
#def add(a, b):
#    print '加法: add %d + %d = %d' % (a, b, a+b)
#    return a+b
#"""
#    exec src
#    print add
#    print add(1, 2)

if __name__ == '__main__':
    unittest.main()
