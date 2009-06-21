#!/usr/bin/python
# -*- coding: utf-8 -*-
"""网盾检测客户端"""

import logging
import time
from datetime import datetime
import os
import traceback
import re

from webauth.client.base import BaseClient
from webauth.client.rpc_proxy import url_task_proxy
from webauth.common.ie_wrapper import IE
from webauth.common.constants import ClientType
from webauth.common.file_utility import remove_dir_tree
from webauth.common import log_execption
from webauth.common import json
from webauth.config import PathSettings


def _on_remove_dir_error(e):
    pass

class WebShieldUnValidError(Exception):
    """网盾无效"""
    pass

class URLAuthClient(BaseClient):
    """URL检测客户端
    http://trac.rdev.kingsoft.net/mercury/wiki/urlauth-url2.0
    """
    
    def __init__(self):
        super(URLAuthClient, self).__init__(ClientType.URL_AUTH)
        self.task_service = url_task_proxy
        self.tasks = None
        self.finish_task_count = 0
        self.ie = None
        self.result_path = 'C:\\Program Files\\Kingsoft\\KSWebShieldSVC\\urlindex.dat'
        self.total_used_time = 0.0 # 总使用时间
        self.debug = False
        
    def before_run(self):
        self.tasks = self._get_tasks()
        msg = None
        if self.tasks:
            speed = 0
            if self.total_used_time > 0:
                speed = self.total_used_time / self.finish_task_count
            msg = '[%s] Tasks[%d] %s - %s, had finished %d(%.1f sec), %.1f sec/url.' % \
                (datetime.now(), len(self.tasks), self.tasks[0]['url_md5'], 
                 self.tasks[-1]['url_md5'], self.finish_task_count, self.total_used_time, speed)
        if self.debug:
            if not msg:
                print 'No tasks'
            else:
                print msg
        return msg
    
    def _get_timeout(self):
        """从服务器传回的参数列表中获取timeout，若没有，则使用默认值"""
        return self.get_value_from_params('timeout', int, 45)
    
    def _vaild_webshield(self, timeout):
        """验证网盾是否有效
        包括正向校验和反向校验
        """
        dangerous_url ='http://blog.duba.net/test/test.html'
        safe_url = 'http://www.baidu.com/'
        error_url = None
        for _i in range(10):
            reason = self.check(dangerous_url, timeout)[0]
            if reason and len(reason[0]) == 3: # 3项
                print reason
                reason_safe = self.check(safe_url, timeout)[0]
                if not reason_safe:
                    return True
                else:
                    error_url = safe_url
            else:
                error_url = dangerous_url
            logging.warning('!!!Web shield vaild fail!!!')
            self.ie.quit() # 确保ie推出
            self._sleep()
        raise WebShieldUnValidError(error_url)
            
    def _get_tasks(self):
        try:
            data = self.task_service.fetch_task()
        except:
            logging.warning('%r' % traceback.format_exc())
            return None
        if data['error']:
            logging.warning('get task error: %r' % data['error']['stack'])
            self.current_info = data['error']['stack']
        return data['result']
        
    REASON_RE = re.compile(r"""
        ^
        \[[\w\-:\s]+\]
        \s
        \[.*?\]    # process name
        \[.*?\]    # proxy version
        \[(\d+)\]    # reason code
        \[(.+?)\]    # reason description
        \[\d+\]      # random code
        (.*)         # other info (e.g.: url)
        $
        """, re.I | re.VERBOSE) 
    def _format_reason(self, reason):
        """
        http://trac.rdev.kingsoft.net/mercury/wiki/urlauth-url2.0
旧格式
[2009-05-21 11:35:31] [iexplore.exe][20090114][0][3086]http://2117.com/
[2009-05-21 11:35:31] [iexplore.exe][20090114][16][3086]http://2217.com/
[2009-05-21 11:38:28] [iexplore.exe][20090114][0][3680]http://www.yksdj.gov.cn/shownews.asp?newsid=92
[2009-05-21 11:38:28] [iexplore.exe][20090114][9][3680]ccj7.cn/

新格式
[2009-05-21 11:35:31] [iexplore.exe][20090114][0][发现xxx进程正在访问xxx网页, 已成功阻止][3086]http://2117.com/
[2009-05-21 11:35:31] [iexplore.exe][20090114][16][发现xxx进程正在访问xxx网页, 已成功阻止][3086]http://2217.com/
[2009-05-21 11:38:28] [iexplore.exe][20090114][0][发现xxx进程正在访问xxx网页, 已成功阻止][3680]http://www.yksdj.gov.cn/shownews.asp?newsid=92
[2009-05-21 11:38:28] [iexplore.exe][20090114][9][发现xxx进程正在访问xxx网页, 已成功阻止][3680]ccj7.cn/
        """
        if not reason:
            return None
        reasons = []
        keys = set()
        lines = reason.split('\n')
        for line in lines:
            line = line.strip()
            m = self.REASON_RE.match(line)
            if m:
                code, url, info = m.group(1), m.group(3), m.group(2)
                if code != '0':
                    url = self._ensure_utf_8_str(url)
                    info = self._ensure_utf_8_str(info)
                    key = '%s_%s' % (code, url)
                    if key not in keys:
                        keys.add(key)
                        if not url or url == 'no_url_error':
                            url = None
                        reasons.append([code, url, info])
        if not reasons:
            reasons = None
        return reasons
    
    def _visit(self, url, timeout):
        """"""
        if not self.ie:
            visible = self.get_value_from_params('ie_visible', int, 1)
            if visible == 1:
                visible = True
            else:
                visible = False
            self.ie = IE(False, timeout, visible)
            self.ie.visit('about:blank') # 先打开空白页
        starttime = time.time()
        is_success, status_code = self.ie.visit(url)
        usetime = time.time() - starttime
        is_timeout = self.ie.is_timeout
        is_download = self.ie.is_download
        title = self.ie.title
        description = self.ie.description
        real_url = self.ie.real_url
        return is_success, status_code, is_timeout, is_download, title, \
            description, real_url, usetime
    
    def _clear_ie(self): 
        """删除 IE cookies，临时文件: http://www.jaycn.com/"""
        username = os.environ['username']
        for path in [r'C:\Documents and Settings\%s\Cookies',
                     r'C:\Documents and Settings\%s\Local Settings\Temporary Internet Files',
                     r'C:\Documents and Settings\%s\Local Settings\History',
                     r'C:\Documents and Settings\%s\Local Settings\Temp',]:
            try:
                remove_dir_tree(path % username, on_error=_on_remove_dir_error)
            except:
                print traceback.format_exc()
                
    def check(self, url, timeout):
        if os.path.exists(self.result_path):
            os.remove(self.result_path)
        self._clear_ie()
        is_success, status_code, is_timeout, is_download, title, \
            description, real_url, usetime = self._visit(url, timeout)
        reason = None
        if os.path.exists(self.result_path):
            f = open(self.result_path, 'rb')
            try:
                reason = f.read()
            finally:
                f.close()
            reason = self._format_reason(reason)
            try:
                os.remove(self.result_path)
            except:
                pass
        self._clear_ie()
        title = self._ensure_utf_8_str(title)
        description = self._ensure_utf_8_str(description)
        real_url = self._ensure_utf_8_str(real_url)
        return reason, is_success, status_code, is_timeout, is_download, \
            title, description, real_url, usetime
    
    CHART_SETS = ('utf-8', 'gb2312', 'gbk')
    def _ensure_utf_8_str(self, s):
        if isinstance(s, basestring):
            if isinstance(s, str):
                error = False
                for charset in self.CHART_SETS:
                    try:
                        s = s.decode(charset)
                        error = False
                        break
                    except UnicodeDecodeError:
                        error = True
                # 都无法正确编码，则只能使用replace
                if error:
                    s = s.decode(self.CHART_SETS[0], 'replace')
            if isinstance(s, unicode):
                s = s.encode('utf-8')
        return s
        
    def run(self):
        timeout = self._get_timeout()
        if self.finish_task_count % 1000 == 0: # 每验证完1000个url，就检验一次网盾是否有效
            self._vaild_webshield(timeout)
            logging.info('web shield valid success!')
        dangerous_count, safe_count = 0, 0
        for task in self.tasks:
            url = task['url']
            if isinstance(url, unicode):
                url = url.encode('utf-8')
            print '--------------start---------------------'
            print 'Client %s' % self._id, task['url_md5'], datetime.now()
            print url
            reason, is_success, status_code, is_timeout, is_download, \
                title, description, real_url, usetime = self.check(url, timeout)
            task['title'] = title
            task['description'] = description
            task['reason'] = reason
            if reason:
                dangerous_count += 1
            else:
                safe_count += 1
            task['is_timeout'] = is_timeout
            task['is_download'] = is_download
            task['real_url'] = real_url
            task['usetime'] = usetime
            task['status_code'] = status_code
            task['visit_success'] = is_success
            try:
                print 'real url:', real_url.decode('utf-8').encode('gbk')
            except:
                print 'real url:', `real_url`
            print 'use time: %s seconds' % usetime
            self.total_used_time += usetime
            print 'success:', is_success
            print 'status code:', status_code
            print 'is timeout:', is_timeout
            try:
                print 'title:', title.decode('utf-8').encode('gbk')
            except:
                print 'title:', `title`
            try:
                print 'description:', description.decode('utf-8').encode('gbk')
            except:
                print 'description:', `description`
            print 'reason:', reason
            print '----------------end--------------------'
        print 'Report result to server: %d dangerous, %d safe' % \
            (dangerous_count, safe_count)
        self._finish_tasks(self.tasks)
        self.finish_task_count += len(self.tasks)
        logging.info('Had finish %d tasks!' % self.finish_task_count)
        self.tasks = None
    
    def _finish_tasks(self, task_results):
        while task_results:
            try:
                data = self.task_service.finish_task(task_results)
                if not data['error']: # 保存成功，则返回
                    break
                logging.warning('Finish tasks error, %r' % data['error']['stack'])
                self.current_info = data['error']['stack']
#                self._save_results(task_results)
#                if 'ValueError' in data['error']['name']: # 64位系统会出现“ValueError: Invalid \uXXXX\uXXXX surrogate pair: line 1 column 3322 (char 3322)”异常
#                    for result in task_results:
#                        data2 = self.task_service.finish_task([result])
#                        if not data2['error']: # 保存成功，则返回
#                            task_results.remove(result)
#                        else:
#                            logging.warning('Finish tasks rpr error, %r' % data['error'])
#                            self.current_info = data['error']['stack']
#                            self._save_results(result)
            except:
                log_execption()
            self._sleep()
            
    def _save_results(self, task_results):
        print 'save result to %s' % os.path.join(PathSettings.WORKING_FOLDER, 'last_result.dat')
        f = open(os.path.join(PathSettings.WORKING_FOLDER, 'last_result.dat'), 'wb')
        try:
            json.dump(task_results, f)
        finally:
            f.close()
    
    
if __name__ == '__main__':
    client = URLAuthClient()
    client.main()
    r = """
[2009-05-21 11:35:31] [iexplore.exe][20090114][0][发现xxx进程正在访问xxx网页, 已成功阻止][3086]http://2117.com/
[2009-05-21 11:35:31] [iexplore.exe][20090114][16][发现xxx进程正在访问xxx网页, 已成功阻止][3086]http://2217.com/
[2009-05-21 11:38:28] [iexplore.exe][20090114][0][发现xxx进程正在访问xxx网页, 已成功阻止][3680]http://www.yksdj.gov.cn/shownews.asp?newsid=92
[2009-05-21 11:38:28] [iexplore.exe][20090114][9][发现xxx进程正在访问xxx网页, 已成功阻止][3680]ccj7.cn/
        """
    reason = client._format_reason(r)
    for code, url, info in reason:
        print code, url, info