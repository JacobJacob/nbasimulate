#-*- coding:utf-8 -*-

import urllib, httplib

class Post(object):
    """Post类用于发送一个post请求"""
    
    def __init__(self, data, host, path):
        """
        @param data: 发post的数据，为字典格式化
        @param host: 连接的网站host,如 http://www.sina,com.cn
        @param path: 提交的路径,如/post.php?action=submit  
        """
        self._data = data
        self._host = host
        self._path = path
        
    def submit(self, result_keyword=u"发表成功", get_content=False, debug=False):
        """提交
        @param result_keyword: 判断是否提交成功的关键字 
        """
        params = urllib.urlencode(self._data)
        headers = {"Content-Type": "application/x-www-form-urlencoded",   
                   "Connection": "Keep-Alive",
                    "Referer": "%s%s" % (self._host, self._path),
                    "Accept-Charset": "utf-8"}
        
        try:
            conn = httplib.HTTPConnection(self._host);
            conn.request(method="POST",url=self._path, body=params, headers=headers);   
            response = conn.getresponse();
        
            if response.status == 200:
                data = response.read()
                if not data:
                    return False
                try:
                    data = data.decode('gb2312')
                except:
                    pass
                if debug:
                    print data
                if get_content:
                    return data
                if data.find(result_keyword) != -1:
                    return True
                else:
                    return False
            else:
                return False
        except Exception, e:
            print e
        finally:
            conn.close()
        
