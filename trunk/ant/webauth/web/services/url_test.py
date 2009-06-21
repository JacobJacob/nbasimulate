#!/usr/bin/python
# -*- coding: utf-8 -*-
"""url相关service api"""

from webauth.common.jsonrpclib import ServerProxy
from webauth.common.constants import UrlFromType

if __name__ == '__main__':
    s = ServerProxy('http://10.20.238.169/services/url/')
    
    urls = 'http://www.kingsoft.com'
    s.add_url(urls, UrlFromType.HOT_SITE)
    
    urls = ['http://www.kingsoft.com',
            'www.duba.net',
            '81715239.qzone.qq.com',
           'a1.dd.aa.dfs.sdfsd.sdfsdf.www.duba.net',
            'http://leak.pc120.com/search/?searchtype=leak&q=flash',
            'http://www.2217.com',
            'http://www.yksdj.gov.cn/shownews.asp?newsid=92',
            'http://labs.duba.net/',
            'http://jjs.sdili.edu.cn/',
            'http://labs.duba.net/wd.shtml',
            'http://008.wzhe123.cn/bbs/',
            'http://sll1209.cn/a69/',
            'http://www.lgbx.cn/article/',
            'http://www.zzmld.cn/',
            'http://www.feiyubooks.com/ordercheck.aspx',
            'http://85gov.cn/box/b02/olze.htm',
            'http://703sese.cn/a2/fx.htm',
            ' http://www.gm95588.cn/1.htm?jdfwkey=hrfez2',
            'http://a.07ss.com/html/linglei/38584.html ',
            'http://a.07ss.com/html/llsn/40049.html',
            'http://wmg.hdd32.cn/1/11/',
            'http://www.gm-kj.cn/1.htm?jdfwkey=wgcxq2',
            'http://g.igf85.cn/d1/03/p1.html',
            'http://z.ks630.cn/d1/05/index2.htm',
            'http://wmg.dsa32.cn/1/14/index2.htm',
            'http://baidusib.cn/05/ytxxz.htm',
            'http://wmg.hdd32.cn/ ',
            'http://www.gg8721.cn/a319/fxx.htm',
            'http://code.feichang71.com/code/960_60_042201.html?id=960_60_042201&webownerid=10581&childid=10000',
            'http://haoma.qq.com.buy-qq.vip-6qq-qm-kkn-haoma.cn/imagestyle/img/qb/haoma.gif',
            'http://www.motu.cn/365loving/hd/zz.jsp?y=wen&c=haoyeck62',
            'http://union.115ku.cn/auto/auto.htm',
            'http://www.tvnet.com.cn/',
            'http://eeiorea.info/lf/in.php',
            'http://www.kaokaoni.org/-----nokia-n95_______.htm?keyrunget=ndu5mnwymzg1otb8bhv6b25ncwl8mxxodhrwoi8vd3d3lmrtnxguy29tlw%3d%3d&keyrunoed=a1939f3559798d38c491adf7ba3b05c4ny44mjy5odm4odgynkurmti%3d',
            'http://wwww.icouu.com:81/html/index.html?pid=15&mid=22159&cid=-1&pn=gd&clientid=1243289269&channel=1&stn=-1&extra=-1',
            'http://www.gg8721.cn/a319/fxx.htm',
            'http://www.91qianming.cn/131.js',
            'http://www.zjqsy.cn/      ',
            'http://008.wzhe123.cn/bbs/            ',
            'http://zhidao.baidu.com/question/13258522.html?fr=qrl',
            'http://sina.com.cn',
            'http://163.com',
            'http://google.com',
            'http://g.cn',
            'http://tudou.com',
            'http://youku.com',
            'http://a.07ss.com',
            ]
    
    print s.add_url(urls, UrlFromType.HOT_SITE)
    