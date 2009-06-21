#!/usr/bin/python
# -*- coding: utf-8 -*-
"""ie wrapper test"""

import time

from ie_wrapper import IE


def clear_file(path): # 主要是：网盾升级会删除文件
    assert path.endswith(".dat")
    while True: # 擦掉结果
        try:
            f = file(path, "w")
            f.write("")
            f.close()
        except Exception, e: # 如果读错，回滚
            print e
            continue
        break

def test_get_description():
    """
    http://www.phpied.com/getelementbyid-description-in-ie/
    """
    urls = ['http://duba.net/', 'http://google.com',
            'http://www.baidu.com/', 'http://pc120.com',
            'http://labs.duba.net/']
    ie = IE(True, 20)
    for url in urls:
        ie.visit(url)
        print url
        print ie.title
        print ie.description
        
def test_cases():
    path = "C:/Program Files/Kingsoft/KSWebShieldSVC/urlindex.dat"
    
    ie = IE(False, 30, True, True)
    urls = [
            # 确保OnBeforeNavigate2事件被触发
            'http://jscsj.gov.cn/',
            'http://www.ybtc.edu.cn/',
            'http://cdigf.gov.cn/',
            'http://secretariat.nsysu.edu.tw/',
            'http://www.jianghai.gov.cn/',
            'http://re-zone.gov.cn/',

            # 误报
#            'http://gsec.gov.cn/',
#            'http://pc120.duba.net:8080/queryportal/querysingleurl/?mid=mid&crc=crc&version=version&retformat=json&guid=guid&url=http://dfe44d1f.cn/wm/iyear.html',
#            'http://dreyersink.com/', # 过滤
#            'http://videogamecententral.com/', # 114过滤
#            'http://www.jinmao114.cn/tradeinfo/offerdetail/info.asp?info_id=227534', # 超恶意网址，触发好多项！
#            'http://blog.duba.net/test/test.html',
#            'http://dianwanmi.com/',
#            'http://dsj.zjyq.gov.cn/',
##            # 此url会造成ie打开卡死
##            'http://www.szgtzy.gov.cn/',
#            'http://www.teacherblog.com.cn/blog/56888/archives/2009/200931819407.html',
#            'http://www.jx-njc.gov.cn/',
#            'http://www.fso.fudan.edu.cn/',
#            # download, 不点击不会发生
#            'http://code13.keyrun.com/code/oa.php?krcid=227579&username=yymm123&webtype=4&adid_ary=3870,4395,4401,4416,4436,4437,4405,4442,4337,4343,4423||2||&ad_replace=1&width=0&height=0&codetype=1&krview=&isnum=12&strreferrer=http://www.97gao97mo.cn/',
##            'html/33005/list_17_23.html',
#            'http://2217.com',
#            'http://www.6ddxx.net/html/kp/99026461001.html',
#            'http://fjqzss-l-tax.gov.cn/admin/index.asp',
#            'http://zbwtuis.cn:6868/b165439/',
#            'http://www.ayfood.cn/',
#            'http://www.rbdz.com/',
#            'http://www.eglobalpurchase.com/company_content.asp?id=3630',
#            'http://222.218.130.38/wuliguangxue/syzd/index.html',
#            'http://9981edu.com/',
#            'http://a.07ss.com/',
#            'http://www.gm-kj.cn/1.htm?jdfwkey=wgcxq2',
#            'http://www.lgbx.cn/',
#            'http://www.lgbx.cn/article/',
#            'http://duba.net', 
#            'http://jjs.sdili.edu.cn/', 
#            'http://pc120.com',
#            'http://2217.com',
#            'http://10.20.238.169/ad',
            ]
    eyurl = 'http://blog.duba.net/test/test.html'
    ie.visit(eyurl)
    reason = open(path).read()
    assert reason
    print reason
    for url in urls:
        clear_file(path)
        start_time = time.time()
        print ie.visit(url)
        print 'use time:', time.time() - start_time, 
        print ie.is_timeout
        print ie.title
        try:
            print ie.description
        except:
            print `ie.description`
        
        r = open(path).read()
        print r
        print '-' * 60


if __name__ == "__main__":
    test_cases()
    time.sleep(1000)