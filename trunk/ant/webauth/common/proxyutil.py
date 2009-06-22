#coding:utf-8
import logging, traceback
import time
import urllib2

def visit_check_raise(site, proxy = '127.0.0.1:8118'):
    ai = "$"
    for i in range(3):
        time.sleep(i * 15)
        try:
            ai = visit(site)
            break
        except Exception:
            pass
    errinfo = ''
    bi = "$"
    for i in range(1):
        time.sleep(i * 15)
        try:
            bi = visit_proxy(site, proxy=proxy)
        except Exception:
            errinfo = '%r' % traceback.format_exc()
    blank = "\n" + ("#" * 250) + "\n"
    k = len(ai) - len(bi)
    if k < 0: k = k * -1
    if k >= max(len(ai), len(bi)) / 200 + 2: # 相差太大了，因为有的 允许 细微差别，差别控制在 0.5%
        info = ("""visit(site) != visitProxy(site)
%s
%s
%s""" % (blank + site, blank + ai + (" @%d" % len(ai)), blank + bi + (" @%d" % len(bi)) + blank))
        #logging.warning(info)
        raise Exception(info + "\n" + site + "\n" + errinfo)
#visitAssertEqual = visitCheckRaise

def _visit(site, opener, headers=None):
    if site.find("://") == -1:
        site = "http://" + site    
    urllib2.socket.setdefaulttimeout(90)
    
    request = urllib2.Request(site)
    request.add_header('User-Agent', 'Mozilla/5.0 (Windows; U; Windows NT 5.1; zh-CN; rv:1.8.1.14) Gecko/20080404 (FoxPlus) Firefox/2.0.0.14')
    request.add_header('Accept', "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/xaml+xml, application/vnd.ms-xpsdocument, application/x-ms-xbap, application/x-ms-application, */*")
    request.add_header("Accept-Language","zh-cn") # 加入头信息，这样可以避免403错误
    request.add_header("Content-Type","text/html; charset=utf8") 
    if headers != None:
        for key, value in headers:
            request.add_header(key, value)
            
    page = opener.open(request)
    pageData = page.read()
    page.close()
    return pageData

def visit(site):
    i = 0
    while True:
        time.sleep(i * 5)
        try:
            _opener = urllib2.build_opener(urllib2.HTTPHandler, 
                                           urllib2.HTTPCookieProcessor())
            return _visit(site, _opener)
        except Exception, e:
            if i == 3: 
                raise e
        i += 1

def is_net_ok():
    try:
        data = visit("http://www.kingsoft.com")
        # print " ".join(data[:1000].split())
        return data.find("WPS Office") != -1
    except Exception:
        return False
if __name__ == "__main__":
    print is_net_ok()

def visit_proxy(site, proxy='127.0.0.1:8118', times=4, loggingwarning=True, headers=None, nocookie=False): 
    
    if not nocookie:
        if proxy:
            _openerProxy = urllib2.build_opener(urllib2.ProxyHandler({'http': 'http://' + proxy}), 
                                            urllib2.HTTPHandler, 
                                            urllib2.HTTPCookieProcessor())
        else:
            _openerProxy = urllib2.build_opener(urllib2.HTTPHandler, 
                                            urllib2.HTTPCookieProcessor()) 
    else:
        if proxy:
            _openerProxy = urllib2.build_opener(urllib2.ProxyHandler({'http': 'http://' + proxy}), 
                                            urllib2.HTTPHandler,)
        else:
            _openerProxy = urllib2.build_opener(urllib2.HTTPHandler,) 
    
    opencount = 0
    while True:
        try:
            return _visit(site, _openerProxy, headers=headers)
        except Exception, e:
            if opencount >= times: # TOR 重启的问题
                if is_net_ok():
                    if loggingwarning:
                        logging.warning("%s, %s" % (site, proxy))
                    raise e # URLError: Connection refused
                else: # 公司网络 -- 断网
                    logging.warning('Net not ok...')
            time.sleep(opencount * 15) # 一共90秒 [0, 15, 30, 45]
            if loggingwarning:
                logging.warning('%r' % traceback.format_exc())
            opencount += 1

def check_proxy(proxy):
    """验证代理可用性
    """
    import time
    timea = int(time.time())
    timeb = timea + 60 * 5
    try:
        visit_proxy('http://www.adobe.com/', proxy = proxy, times = 1)
        visit_proxy('http://www.sun.com/', proxy = proxy, times = 1)
        data = ""
        data = visit_proxy('http://www.cnn.com/', proxy = proxy, times = 1)
        timeb = int(time.time())
        return data.find('<title>CNN.com') != -1 or data.find('<title>CNN.COM') != -1, (timeb - timea) / 3
    except Exception:
        return False, timeb - timea
    
# Table mapping response codes to messages; entries have the
# form {code: (shortmessage, longmessage)}.
responses = {
    100: ('Continue', 'Request received, please continue'),
    101: ('Switching Protocols',
          'Switching to new protocol; obey Upgrade header'),

    200: ('OK', 'Request fulfilled, document follows'),
    201: ('Created', 'Document created, URL follows'),
    202: ('Accepted',
          'Request accepted, processing continues off-line'),
    203: ('Non-Authoritative Information', 'Request fulfilled from cache'),
    204: ('No Content', 'Request fulfilled, nothing follows'),
    205: ('Reset Content', 'Clear input form for further input.'),
    206: ('Partial Content', 'Partial content follows.'),

    300: ('Multiple Choices',
          'Object has several resources -- see URI list'),
    301: ('Moved Permanently', 'Object moved permanently -- see URI list'),
    302: ('Found', 'Object moved temporarily -- see URI list'),
    303: ('See Other', 'Object moved -- see Method and URL list'),
    304: ('Not Modified',
          'Document has not changed since given time'),
    305: ('Use Proxy',
          'You must use proxy specified in Location to access this '
          'resource.'),
    307: ('Temporary Redirect',
          'Object moved temporarily -- see URI list'),

    400: ('Bad Request',
          'Bad request syntax or unsupported method'),
    401: ('Unauthorized',
          'No permission -- see authorization schemes'),
    402: ('Payment Required',
          'No payment -- see charging schemes'),
    403: ('Forbidden',
          'Request forbidden -- authorization will not help'),
    404: ('Not Found', 'Nothing matches the given URI'),
    405: ('Method Not Allowed',
          'Specified method is invalid for this server.'),
    406: ('Not Acceptable', 'URI not available in preferred format.'),
    407: ('Proxy Authentication Required', 'You must authenticate with '
          'this proxy before proceeding.'),
    408: ('Request Timeout', 'Request timed out; try again later.'),
    409: ('Conflict', 'Request conflict.'),
    410: ('Gone',
          'URI no longer exists and has been permanently removed.'),
    411: ('Length Required', 'Client must specify Content-Length.'),
    412: ('Precondition Failed', 'Precondition in headers is false.'),
    413: ('Request Entity Too Large', 'Entity is too large.'),
    414: ('Request-URI Too Long', 'URI is too long.'),
    415: ('Unsupported Media Type', 'Entity body in unsupported format.'),
    416: ('Requested Range Not Satisfiable',
          'Cannot satisfy request range.'),
    417: ('Expectation Failed',
          'Expect condition could not be satisfied.'),

    500: ('Internal Server Error', 'Server got itself in trouble'),
    501: ('Not Implemented',
          'Server does not support this operation'),
    502: ('Bad Gateway', 'Invalid responses from another server/proxy.'),
    503: ('Service Unavailable',
          'The server cannot process the request due to a high load'),
    504: ('Gateway Timeout',
          'The gateway server did not receive a timely response'),
    505: ('HTTP Version Not Supported', 'Cannot fulfill request.'),}
