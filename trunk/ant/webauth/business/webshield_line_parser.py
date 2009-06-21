#coding:utf-8
from webauth.common import urlutil
import re

class WebshieldLineError(Exception):
    """网盾日志行格式错误"""
    pass

class WebshieldURLError(WebshieldLineError):
    """网盾日志行中URL格式错误"""
    pass

class WebshieldReasonError(WebshieldLineError):
    """网盾日志行中原因格式错误"""
    pass

class WebshieldGroupError(WebshieldLineError):
    """网盾日志行中分组格式错误"""
    pass

class WebshieldEncodeError(WebshieldLineError):
    """网盾日志行编码错误"""
    pass

class WebshieldLineParser:
    _pattern = re.compile(r"\[.+?\]\s*\[.+?\]\s*\[.+?\]\s*\[.+?\]\s*\[\d+\].*")
    def __init__( self, text_line ):
        """text_line类似于[2009-02-12 14:01:59][iexplore.exe][20090113][11][11]http://example.net/
        数据格式：[时间][程序][网盾版本][挂马原因代号][URL所属分组]url
        """
        text_line = text_line.strip()
        if not text_line or not self._pattern.match(text_line):
            raise WebshieldLineError(text_line)
        self._line = text_line
        self._url_spliter = None
    
    def get_url(self):
        """第5个]之后的内容,即网盾日志行中的url"""
        if self._url_spliter is None:
            url_begin = 0
            for i in range(5):
                url_begin = self._line.find("]", url_begin) + 1
                if url_begin < 1:
                    raise WebshieldLineError(self._line)
            if url_begin > 1:
                url = self._line[url_begin:]
                self._url_spliter = urlutil.standardize(url)
        if not self._url_spliter:
            raise WebshieldURLError(self._line)
        return self._url_spliter.geturl()
    
    def get_host(self):
        """获取主机"""
        if self._url_spliter is None:
            self.get_url()
        return self._url_spliter.host
    
    def get_time(self):
        """第1个[]
        return:日志行的生成时间"""
        return self._get_brackets(0)
    
    def get_program(self):
        """第2个[]
        return:客户端浏览器"""
        return self._get_brackets(1)

    def get_version(self):
        """第3个[]
        return:网盾日志版本"""
        return self._get_brackets(2)
    
    def get_reason(self):
        """第4个[]
        return:网盾上报的原因"""
        reason = self._get_brackets(3)
        try:
            return int(reason, 16)
        except (ValueError, TypeError):
            pass
        raise WebshieldReasonError(self._line)
    
    def get_group_id(self):
        """第5个[]
        return:url分组，标记着一批URL是否来自同一次访问"""
        group_id = self._get_brackets(4)
        try:
            return int(group_id, 10)
        except (ValueError, TypeError):
            pass
        raise WebshieldGroupError(self._line)
    
    def _get_brackets(self, index):
        """取日志行中第index个括号的内容
        index:取第几个括号的内容, 从0开始
        return:第index个括号的内容"""
        count = 0
        begin = 0
        while count <= index:
            count += 1
            begin = self._line.find("[", begin) + 1
            if begin < 1:
                raise WebshieldLineError(self._line)
        end = self._line.find("]", begin)
        if end < begin:
            raise WebshieldLineError(self._line)
        return self._line[begin: end]
