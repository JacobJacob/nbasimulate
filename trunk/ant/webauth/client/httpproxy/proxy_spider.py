#coding:utf-8
#Quanhai Yang @ 2008-9-6
import re, time, logging
_res = {
    "host": r"([0-9a-zA-Z-_]+\.[\.0-9a-zA-Z-_]+)",
    "blank": r"\s*",
    "digit": r"[0-9]+",
    "port": r"([0-9]+)",
    "portxp": r"\+([a-z+]+)",
    "proxy": r"([0-9a-zA-Z-_]+\.[\.0-9a-zA-Z-_]+:[0-9]+)",
    "urlname": r"[0-9a-zA-Z-_%]*?"}

class ProxyDigger:
    def __init__(self, saveFunc, sleepTime = 60 * 1): # 1 分钟下载一个页面
        self.saveFunc = saveFunc
        self.sleepTime = sleepTime
                    
    def list_self(self):
        return [getattr(self, x) for x in dir(self) if x.startswith("get") and callable(getattr(self, x))]
    
    def _visit(self, site, proxy = None):
        from webauth.common.proxyutil import visit_proxy
        try:
            return visit_proxy(site, proxy = proxy, times = 1)
        except Exception, e:
            logging.warning("%s %s -- %s" % (__file__, site, e))
        return ""

    def _get_single(self, url, regular, transform, debug):
        if debug: print url
        page = self._visit(url)
        tmp = transform(re.findall(regular, page)) if regular else transform(page)
        self.saveFunc(tmp)
        return tmp
        
    def _get_single_list(self, urls, regular, transform, debug):
        urls = list(set(urls))
        for url in urls:
            self._get_single(url, regular, transform, debug)
            time.sleep(self.sleepTime if not debug else 0)
        
    def _get_by_id(self, urltemplet, regular, transform, debug):
        cnt = 0
        while True:
            cnt += 1
            url = urltemplet % cnt
            
            tmp = self._get_single(url, regular, transform, debug)
            if debug and cnt >= 3 or not tmp: break
            time.sleep(self.sleepTime if not debug else 0)
            
    def _getByPage(self, urlseed, _regular, regular, transform, debug):
        page = self._visit(urlseed)
        urlroot = urlseed
        while urlroot and not urlroot.endswith("/"):
            urlroot = urlroot[:-1]
        urls = [urlroot + ("".join(x) if isinstance(x, tuple) else x) for x in re.findall(_regular, page)]
        urls = list(set(urls))
        
        cnt = 0
        for url in urls:
            cnt += 1
            
            _tmp = self._get_single(url, regular, transform, debug)
            #if debug and cnt >= 3: break
            time.sleep(self.sleepTime if not debug else 0)
            
    def get51proxy(self, debug = False): # http://51proxy.net/http_fast.html
        self._getByPage("http://51proxy.net/http_fast.html", 
                        r"<li><a href=\"(.*?\.html)\"><span>", 
                        r"#ffffff';\">%(blank)s<td width=\"30\">%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td width=\"60\">%(port)s</td>" % _res, 
                        lambda tmp: [":".join(x) for x in tmp], 
                        debug)
        
    def getCnproxy(self, debug = False): # http://www.cnproxy.com/proxy1.html
        def analyse(page):
            tmp = re.findall(r"<tr><td>%(host)s<SCRIPT type=text/javascript>document.write\(\":\"%(portxp)s\)</SCRIPT></td>" % _res, page)
            # z="3";j="4";r="2";l="9";c="0";x="5";i="7";a="6";p="8";s="1";
            dictmap = {}
            for a in re.findall("[a-z]=\"[0-9]\";", page):
                dictmap[a[0]] = a[3]
            # <tr><td>66.98.152.190<SCRIPT type=text/javascript>document.write(":"+a+a+x+j)</SCRIPT></td>
            return [x[0] + ":" + "".join([dictmap[y] for y in x[1].split("+")]) for x in tmp]
        self._getByPage("http://www.cnproxy.com/proxy1.html",
                        r"<li><a href=\"(proxy.*?\.html)\">",
                        None,
                        analyse,
                        debug)
        
    def _getDheart(self, debug = False): # http://www.dheart.net/proxy/index.php?lphydo=list&port=&type=&country=&page=1
        self._get_by_id("http://www.dheart.net/proxy/index.php?lphydo=list&port=&type=&country=&page=%s",
                      r"this.className='cells'\">%(blank)s<td >%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td>%(port)s</td>" % _res,
                      lambda tmp: [":".join(x) for x in tmp],
                      debug)
        
    def getIpcn(self, debug = False): # http://proxy.ipcn.org/proxylist.html, http://proxy.ipcn.org/proxylist2.html
        self._get_single_list(["http://proxy.ipcn.org/proxylist.html", "http://proxy.ipcn.org/proxylist2.html"],
                        r"%(proxy)s" % _res,
                        lambda tmp: tmp,
                        debug)

    def getOrzin(self, debug = False): # http://orzin.com/socks/index.php?act=list&port=&type=&country=&page=1
        self._get_by_id("http://orzin.com/socks/index.php?act=list&port=&type=&country=&page=%s",
                      r"this.className='cells'\">%(blank)s<td >%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td>%(port)s</td>" % _res,
                      lambda tmp: [":".join(x) for x in tmp],
                      debug)
        
    def getPassE(self, debug = False): # http://www.pass-e.com/proxy/index.php?page=1
        self._get_by_id("http://www.pass-e.com/proxy/index.php?page=%s",
                      r"list\('%(host)s','%(port)s','%(digit)s'," % _res,
                      lambda tmp: [":".join(x) for x in tmp],
                      debug)
        
#    def getProxycn(self, debug = False): # http://www.proxycn.com/html_proxy/
#        self._getByPage("http://www.proxycn.com/html_proxy/", 
#                        r"<A HREF=\"(.*?\.html)\">", 
#                        r"clip\('%(proxy)s'\);" % _res, 
#                        lambda tmp: tmp, 
#                        debug)
        
    def getSamair(self, debug = False): # http://www.samair.ru/proxy/proxy-01.htm
        def analyse(page):
            tmp = re.findall(r'<td>%(host)s<script type="text/javascript">document.write\(":"%(portxp)s\)</script></td>' % _res, page)
            # w=2;v=1;n=0;t=5;d=4;x=6;a=8;q=9;p=7;m=3;
            dictmap = {}
            for a in re.findall("([a-z]=[0-9];)", page):
                dictmap[a[0]] = a[2]
            # <td>83.246.88.141<script type="text/javascript">document.write(":"+a+v)</script></td>
            return [x[0] + ":" + "".join([dictmap[y] for y in x[1].split("+")]) for x in tmp]
        self._get_by_id("http://www.samair.ru/proxy/proxy-%02d.htm",
                      None,
                      analyse,
                      debug)

    def getSooip(self, debug = False): # http://www.sooip.cn/Article_search.asp
        self._getByPage("http://www.sooip.cn/Article_search.asp", 
                        r"<a href='(Article_Show.asp\?ArticleID=%(digit)s)'><b>" % _res, 
                        r"%(host)s %(port)s" % _res, 
                        lambda tmp: [":".join(x) for x in tmp], 
                        debug)

    def getIpbbs(self, debug = False): # http://www.ipbbs.com/index.php?num=1
        self._get_by_id("http://www.ipbbs.com/index.php?num=%s",
                      r"list\('%(host)s','%(port)s'," % _res,
                      lambda tmp: [":".join(x) for x in tmp],
                      debug)

    def getHaozs(self, debug = False): # http://www.haozs.net/
        self._get_single_list(["http://www.haozs.net/ip.htm", "http://www.haozs.net/ircproxy.htm"],
                        r"%(proxy)s" % _res,
                        lambda tmp: tmp,
                        debug)
        self._get_by_id("http://www.haozs.net/proxyip/index.php?num=%s",
                      r"</tr>%(blank)s<tr>%(blank)s<td>%(proxy)s</td>" % _res,
                      lambda tmp: tmp,
                      debug)
        self._getByPage("http://www.haozs.net/", 
                        r'(?:<a href="http://www\.haozs\.net/(%(urlname)s\.php)")|(?:<a href="(dailiip%(digit)s\.htm)" target="_blank">)' % _res, 
                        r"(?:<tr .*?><td align=center>&nbsp;%(digit)s</td>%(blank)s<td align=center>&nbsp;%(host)s</td>%(blank)s<td align=center>&nbsp;%(port)s</td>)|(?:%(host)s%(blank)s%(port)s)" % _res, 
                        lambda tmp: [":".join([y for y in x if y]) for x in tmp], 
                        debug)
        
    def get868dns(self, debug = False): # http://www.868dns.com/
        def clear(x):
            tmp = ":".join(x)
            while tmp.find(" ") != -1: tmp = tmp.replace(" ", "")
            while tmp.find("::") != -1: tmp = tmp.replace("::", ":")
            while tmp.startswith(":"): tmp = tmp[1:]
            while tmp.endswith(":"): tmp = tmp[:-1]
            return tmp
        self._getByPage("http://www.868dns.com/", 
                        r"<a href =(.*?-proxy-server-list-0.phtml)>",
                        r"(?:height=10>%(blank)s<td>%(host)s</td>%(blank)s<td>%(port)s</td>)|\
(?:<td>%(proxy)s</td>)|\
(?:<td>IP: %(host)s Port: %(port)s</td>)" % _res, 
                        lambda tmp: [clear(x) for x in tmp], 
                        debug)

def _print(proxys):
    print "\n".join(proxys[:5])
if __name__ == "__main__":
    ProxyDigger(_print, 1).get51proxy(debug = True)
