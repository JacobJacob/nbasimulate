# -*- coding: utf-8 -*-


import urllib2
import re

class Spider(object):
    """a spider
    """
    REMOVE_HTMLTAG_RE = re.compile(r'(<.*?>)', re.I)
    DEFAULT_CHARSETS = ('utf-8', 'gb2312', 'gbk')
    CHARSET_RE = re.compile(ur'(?:<meta\s[^<>]*?\s)?charset=([\w\d\-]*)(?:[^<>]*?>)?', re.I)
    DEFAULT_TIMEOUT = 60
    
    def __init__(self, proxy=None, remove_html=True):
        """init the base param"""
        self._proxy = proxy
        self.init_spider()
        self.remove_html = remove_html
    
    def init_spider(self):
        """初始化"""
        if self._proxy:
            self._opener = urllib2.build_opener(urllib2.ProxyHandler({'http': 'http://%s' % self._proxy}))
        else:
            self._opener = urllib2.build_opener()
    
    def request(self, url, referer=None):
        """request"""
        request = urllib2.Request(url)
        old_timeout = urllib2.socket.getdefaulttimeout()
        urllib2.socket.setdefaulttimeout(30)
        request.add_header('User-Agent', 'Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.1; SV1; .NET CLR 2.0.50727; MAXTHON 2.0)')
        if referer:
            request.add_header('Referer', referer)
    
        try:
            return self._opener.open(request)
        except urllib2.URLError, e:
            raise
    def read(self, url, referer=None):
        """访问url并返回其内容"""
        try:
            response = self.request(url, referer)
            content = response.read()
        except:
            print 'error, url:%s' % url
            return ''
        if 'content-type' in response.headers:
            charset = self.CHARSET_RE.search(response.headers['content-type']) # 自动检测charset，若有，则decode
        else:
            charset = None
        if not charset:
            charset = self.CHARSET_RE.search(content)
        if charset:
            charset = charset.groups()[0]
        content = self._try_decode(content, charset)
        return content

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
    
    def get_urls(self, content, express, callback=None):
        url_pattern = re.compile(express, re.I | re.VERBOSE)
        raw_urls = url_pattern.findall(content)
        return raw_urls
            
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

def main():
    spider = Spider()
    content = spider.read('http://www.163.com')
    spider.get_urls(content, r'''href="(.*?)"''')
    
if __name__ == '__main__':
    main()
    