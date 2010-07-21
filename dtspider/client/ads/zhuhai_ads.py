#-*- coding:utf-8 -*-

from business import ads_log
from common.util import md5mgr

from paitr import IE


class ZhuhaiAds(object):
    
    
    def __init__(self, start_url, max_page=100):
        """zhuhai ads init"""
        self._current_page = 1
        self._start_url = start_url
        self._max_page = max_page
    
    def run(self):
        
        
        while self._current_page < self._max_page:
            ie = IE.attach(self._start_url % self._current_page, 'url')
            print ie.title
            links = ie.links('infoView\.aspx\?infoId=\d+')
            for link in links:
                print link.href
                print link.title
                url = link.href
                url_md5 = md5mgr.mkmd5fromstr(url)
                log_info = ads_log.load_log(url_md5)
                if log_info:
                    print 'this page arredy issue ads '
                    continue
                link.click()
                link_ie = IE.attach(link.href)
                if not link_ie:
                    continue
                self._issue_ads(link_ie)
                log_info = {'url': url, 'url_md5': url_md5}
                ads_log.log(log_info)
                link_ie.close()
                
            self._current_page += 1
            self._next_page(ie)

    def _issue_ads(self, ie):
        """发布信息"""
        try:
           frame = ie.frame(2)
           title_input = frame.input('txt_subject')
           content_textarea = frame.textarea('txt_content')
           title_input.set_value(u'珠海分类信息,更新更全的资讯')
           content_textarea.set_value(u'珠海分类信息,更新更全的资讯:http://www.0756info.com')
           button = frame.button('btn_save')
           button.click()
        except Exception, e:
            print e

    def _next_page(self, ie):
        links = ie.links(self._start_url % self._current_page)
        links[0].click()
        
def main():
    zhuhai_ads = ZhuhaiAds('http://mark.zhuhai.gd.cn/classInfo.aspx?page=%s')
    zhuhai_ads.run()

if __name__ == '__main__':
    main()
