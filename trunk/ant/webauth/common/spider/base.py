#!/usr/bin/python
# -*- coding: utf-8 -*-
"""A web spider can just define a template, and it will give what you want.
"""

import time
from random import randint
import urllib2
import urlparse
import re


class Spider(object):
    """Base templates web spider.
    
    You only setting the template, and it will give you what you want.
    """
    
    DEFAULT_CHARSETS = ('utf-8', 'gb2312', 'gbk')
    # charset pattern
    CHARSET_RE = re.compile(ur'(?:<meta\s[^<>]*?\s)?charset=([\w\d\-]*)(?:[^<>]*?>)?', 
                            re.I)
    REMOVE_HTMLTAG_RE = re.compile(r'(<.*?>)', re.I)
    
    DEFAULT_TIMEOUT = 10 #默认超时时间为10秒
    
    def __init__(self, url, templates, referer=None, remove_html=True,
                 user_agent=None, http_headers=None, timeout=DEFAULT_TIMEOUT,
                 pass_level=None):
        """Init a spider
        
        Set referer if the url has, None default;
        Also can set the request timeout, default is 60 seconds;
        user_agent it will be use for builder the http request headers, None default.
        http_headers is a dict, if it set, spider will use it.
        
        pass_level: 忽略异常的等级, 如果pass_level为None，则忽略所有异常
        """
        self.start_url = url
        self.templates = templates
        self.maxlevel = len(templates)
        self.referer = referer
        self.timeout = timeout
        self.user_agent = user_agent
        self.http_headers = http_headers
        self.remove_html = remove_html
        self.pass_level = pass_level
        
        self.init_spider()
        
    def init_spider(self):
        """Init spider, like setting timeout and other things"""
        self._old_timeout = urllib2.socket.getdefaulttimeout()
        urllib2.socket.setdefaulttimeout(self.timeout)
        self.opener = urllib2.build_opener()
        self._caches = set()
        self._same_levels = set()
        self.index = 0
    
    def __del__(self):
        """Release things and set back the socket timeout to default value. """
        urllib2.socket.setdefaulttimeout(self._old_timeout)
    
    def _walk(self, url=None, parent_url=None, level=1):
        if url is None:
            url = self.start_url
        else:
            assert(isinstance(url, basestring))
        if parent_url is not None:
            assert(isinstance(parent_url, basestring))
#        url = self.urlencode(url)
        if url in self._caches:
            return
        self._current_url = url
        self._caches.add(url)
        template = self.templates[level-1]
        downurls, softinfo = [], {}
        try:
            response = self.request(url, parent_url)
            content = self.get_content(response)
        except Exception, e:
            print e, url
            if self.pass_level is not None and level not in self.pass_level:
                raise
        else: 
            if 'downurl' in template:
                if 'downurl_callback' in template:
                    exec template['downurl_callback']
                    callback = downurl_callback
                else:
                    callback = None
                downurls = set(self.get_urls(content, template['downurl'], callback))
                if 'downurl_finish_callback' in template:
                    exec template['downurl_finish_callback']
                    downurls = downurl_finish_callback(self._current_url, downurls)
                if not downurls:
                    print '###### %s: no download urls ######' % url
            if 'info' in template:
                softinfo = self.get_info(content, template['info'])
                if not softinfo:
                    print 'no info', url
            self.index += 1
            yield level, url, parent_url, response, content, downurls, softinfo
            downurls, softinfo = [], {}
            if 'next_level' in template:
                if 'next_level_callback' in template:
                    exec template['next_level_callback']
                    callback = next_level_callback
                else:
                    callback = None
                if hasattr(self, 'get_next_level_urls'):
                    next_level_urls = self.get_next_level_urls(content, template['next_level'], callback)
                else:
                    next_level_urls = self.get_urls(content, template['next_level'], 
                                                    callback)
                if hasattr(self, 'next_level_finish_callback'):
                    next_level_urls = self.next_level_finish_callback(self._current_url, next_level_urls)
                elif 'next_level_finish_callback' in template:
                    if not callable(template['next_level_finish_callback']):
                        exec template['next_level_finish_callback']
                        template['next_level_finish_callback'] = next_level_finish_callback
                    next_level_urls = template['next_level_finish_callback'](self._current_url, 
                                                                             next_level_urls)
                next_level = level + 1
                if next_level <= self.maxlevel:
                    for u in next_level_urls:
                        for r in self._walk(u, url, next_level):
                            yield r
            if 'same_level' in template:
                if 'same_level_callback' in template:
                    exec template['same_level_callback']
                    callback = same_level_callback
                else:
                    callback = None
                self._current_url = url # 修复当前url被改变的bug
                same_level_urls = self.get_urls(content, template['same_level'], callback)
                
                if 'same_level_finish_callback' in template:
                    if not callable(template['same_level_finish_callback']):
                        exec template['same_level_finish_callback']
                        template['same_level_finish_callback'] = same_level_finish_callback
                    same_level_urls = template['same_level_finish_callback'](self._current_url, 
                                                                             same_level_urls)
                for u in same_level_urls:
                    self._same_levels.add((u, url, level))
                    
    def _sleep(self):
        time.sleep(randint(5, 10))
    
    def walk(self, url=None, parent_url=None, level=1):
        """Like os.walk(), this will return iters.
        
        Return level, url, parent_url, grab_info, grab_urls, content, reponse
        
        content is a unicode, spider will try to decode with the http page charset,
        if it no charset, spider use 'utf-8', 'gb2312', 'gbk' by default.
        """
        for r in self._walk(url, parent_url, level):
            yield r
            self._sleep()
        while True:
            try:
                u, url, level = self._same_levels.pop() # 防止递归调用栈空间耗尽
                for r in self._walk(u, url, level):
                    yield r
                    self._sleep()
            except KeyError:
                print 'walk end!!!!!!!!!!!!!!'
                break
        
    def request(self, url, referer=None):
        """请求url，返回响应的response"""
        self._current_url = url
        req = urllib2.Request(url)
        req.add_header('User-Agent', 
                       # 'Mozilla/5.0 (Windows; U; Windows NT 5.1; en-US; rv:1.9.0.6) Gecko/2009011913 Firefox/3.0.6')
                       'Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; MAXTHON 2.0)')
        if referer is not None:
            req.add_header('Referer', referer)
        try:
            return self.opener.open(req)
        except urllib2.URLError, e:
            print 'error url: %s' % url
            if hasattr(e, 'reason'):
                print 'We failed to reach a server.'
                print 'Reason: ', e.reason
                raise
            elif hasattr(e, 'code'): # 2级跳转
                if e.code == 302:
                    info = e.info()
                    print '302: restart new url:', self.info.dict['location']
                    redirect_url = info.dict['location']
                    return self.request(redirect_url, url)
            raise
    
    def _try_decode(self, content, charset):
        success = False
        for c in (charset,) + self.DEFAULT_CHARSETS:
            if not c:
                continue
            try:
                content = content.decode(c)
                success = True
                break
            except UnicodeDecodeError:
                pass
        if not success:
            content = content.decode(c, 'replace')
        return content
    
    def get_content(self, response):
        """get the reponse content(Unicode), decode with charset
        """
        content = response.read()
        charset = self.CHARSET_RE.search(response.headers['content-type']) # 自动检测charset，若有，则decode
        if not charset:
            charset = self.CHARSET_RE.search(content)
        if charset:
            charset = charset.groups()[0]
        content = self._try_decode(content, charset)
        return content
    
    def get_urls(self, content, express, callback=None):
        """获取urls"""
        url_pattern = re.compile(express, re.I | re.VERBOSE)
        rawurls = url_pattern.findall(content)
        urls = []
        for url in rawurls:
            if callback is not None and callable(callback):
                url = callback(self._current_url, url)
            if not url.startswith(u'http://'):
                url = urlparse.urljoin(self._current_url, url)
            urls.append(url.strip())
        return urls
    
    def _remove_html_tag(self, d):
        copy_d = d.copy()
        for k, v in copy_d.iteritems():
            if isinstance(v, basestring):
                d[k] = self.REMOVE_HTMLTAG_RE.sub(u'', v).strip()
    
    def get_info(self, content, express):
        info_pattern = re.compile(express, re.I | re.VERBOSE)
        m = info_pattern.search(content)
        if m:
            d = m.groupdict()
            if self.remove_html:
                self._remove_html_tag(d)
            return d
        else:
            return {}
    
#    @staticmethod
#    def urlencode(url):
#        """
#        A version of Python's urllib.quote() function that can operate on unicode
#        strings. The url is first UTF-8 encoded before quoting. The returned string
#        can safely be used as part of an argument to a subsequent iri_to_uri() call
#        without double-quoting occurring.
#        """
#        if isinstance(url, unicode):
#            return urllib.quote(url.encode(DEFAULT_CHARSETS[0]), ':/&=;@?+$,#%').encode(Defa)