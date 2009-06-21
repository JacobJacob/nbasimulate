#!/usr/bin/python
# -*- coding: utf-8 -*-
"""调用ie com的封装类"""

import logging
import sys
sys.coinit_flags = 0          # specify free threading
import time
from threading import Lock, Thread
import urlparse

import win32com.client 
import pythoncom
import pywintypes
import win32event
import win32gui
import win32con

from constants import IEStatus, HTTPStatus
from win32process_killer import kill_processname
import winguiauto
from __init__ import log_execption
from threadutil import timeout as timeout_func, Timeout
import urlutil


class Timeouter:
    def __init__(self, method, *args):
        if args:
            self._args = args
        else:
            self._args = ()
        self._method = method
    
    def track(self, timeout, on_timeout, *args, **kwargs):
        """if timeout, return True"""
        t = Thread(target = self._method, args = self._args)
        t.setDaemon(True)
        t.start()
        rest = timeout
        while t.isAlive() and rest > 0:
            rest -= 0.01
            time.sleep(0.01)
        is_timeout = False
        if rest <= 0 and t.isAlive():
            is_timeout = True
            on_timeout(*args, **kwargs)
        t.join()
        return is_timeout
    
    
class ExplorerEvents(object):
    def __init__(self, *args, **kwargs):
        self._cevent = win32event.CreateEvent(None, 0, 0, None)
        self._qevent = win32event.CreateEvent(None, 0, 0, None)
        self._lock = Lock()
        self.re_init()
        self.debug = False
    
    def re_init(self):
        self.is_timeout = False
        self.is_download = False
        self.need_clear = False
        self._last_web = None
        self.status_code = HTTPStatus.OK
        self._error_url = None
        self.real_url = None
        win32event.ResetEvent(self._cevent)
        win32event.ResetEvent(self._qevent)
    
    def wait_complete(self, timeout=45):
        sleep_count = 5
        while sleep_count: # 确保OnBeforeNavigate2事件被触发
            if self.real_url is not None:
                break
            sleep_count -= 1
            time.sleep(1)
        if self.real_url is None:
            timeout = 2
        if self.debug:
            print 'wait_complete: ', timeout, self.real_url
        rc = win32event.WaitForSingleObject(self._cevent, int(timeout * 1000))
        if self.real_url is None:
            pass
        elif rc != win32event.WAIT_OBJECT_0:
            if self.debug:
                print 'wait_complete: want %s, now %s' % (win32event.WAIT_OBJECT_0, rc)
            self.is_timeout = True
            return False
        return True
    
    def wait_quit(self, timeout = -1):
        if timeout > -1:
            timeout = timeout * 1000
        rc = win32event.WaitForSingleObject(self._qevent, timeout)
        return rc == win32event.WAIT_OBJECT_0
    
    def OnDocumentComplete(self, pDisp=pythoncom.Empty, URL=pythoncom.Empty):
        self._lock.acquire()
        if self.debug:
            print '-' * 60
            print 'OnDocumentComplete', URL
            print 'last_url:', self._last_web
            print 'real_url:', self.real_url
        try:
            if self._last_web and (self._last_web == URL or self.real_url == URL):
                print 'OnDocumentComplete, set cevent.'
                win32event.SetEvent(self._cevent)
        finally:
            self._lock.release()

    def OnBeforeNavigate2(self, pDisp=pythoncom.Empty, URL=pythoncom.Empty, 
                          Flags=pythoncom.Empty, TargetFrameName=pythoncom.Empty, 
                          PostData=pythoncom.Empty, Headers=pythoncom.Empty, 
                          Cancel=pythoncom.Empty):
        self._lock.acquire()
        if self.debug:
            print 'OnBeforeNavigate2', URL, Headers
        try:
            if not self.real_url:# and URL != "about:blank":
                self.real_url = URL
        finally:
            self._lock.release()
    
    def OnNavigateComplete2(self,
                           pDisp=pythoncom.Empty,
                           URL=pythoncom.Empty):
        self._lock.acquire()
        if self.debug:
            print 'OnNavigateComplete2', URL
        try:
#            if not self._last_web:
            self._last_web = URL
        finally:
            self._lock.release()
        
    def OnNavigateError(self, pDisp=pythoncom.Missing, URL=pythoncom.Missing, 
                        TargetFrameName=pythoncom.Missing, StatusCode=pythoncom.Missing, 
                        Cancel=pythoncom.Missing):
        self._lock.acquire()
        if self.debug:
            print 'OnNavigateError', URL
        try:
            if self.real_url and self.real_url == URL:
                self.status_code = StatusCode
        finally:
            self._lock.release()
            
    def OnFileDownload(self, bActiveDocument=pythoncom.Missing, bCancel=pythoncom.Missing):
        self._lock.acquire()
        if self.debug:
            print 'OnFileDownload', bActiveDocument
        try:
            if bActiveDocument:
                return False
            else:
                self.is_download = True
                self.need_clear = True
                win32event.SetEvent(self._cevent)
                return True
        finally:
            self._lock.release()
        
#    def OnNewWindow3(self, ppDisp = pythoncom.Missing, Cancel = pythoncom.Missing,
#                     dwFlags = pythoncom.Missing, bstrUrlContext = pythoncom.Missing,
#                     bstrUrl = pythoncom.Missing):
#        self.need_clear = True
    
    def OnQuit(self):
        win32event.SetEvent(self._qevent)

class IE(object):
    
    BLANK_URL = "about:blank"
    
    def __init__(self, need_quit=True, timeout=None, visible=True, debug=False):
        self.timeout = timeout
        self.visible = visible
        self.debug = debug
        self.is_download = False
        self.is_timeout = False
        self.need_quit = need_quit
        self.title = None
        self.description = None
        self._ie = None
        self.real_url = None
        self._to_kill = ("iexplore.exe", "wmplayer.exe", "notepad.exe", "mspaint.exe")
        self._skip_error_code = set([-2147467260,
                                 IEStatus.CODE_DOWNLOAD_DECLINED,
                                 IEStatus.CODE_INSTALL_BLOCKED_BY_HASH_POLICY,
                                 IEStatus.CODE_INSTALL_SUPPRESSED,
                                 IEStatus.INVALID_CERTIFICATE,
                                 IEStatus.NO_VALID_MEDIA,
                                 IEStatus.REDIRECT_FAILED,
                                 IEStatus.REDIRECT_TO_DIR,
                                 IEStatus.REDIRECTING,
                                 IEStatus.RESULT_DISPATCHED,
                                 IEStatus.SECURITY_PROBLEM,
                                 IEStatus.TERMINATED_BIND,#
                                 IEStatus.CANNOT_LOAD_DATA,
                                 IEStatus.CANNOT_LOCK_REQUEST,
                                 IEStatus.DATA_NOT_AVAILABLE,
                                 IEStatus.DOWNLOAD_FAILURE,
                                 IEStatus.INVALID_REQUEST,
                                 IEStatus.INVALID_URL,
                                 IEStatus.UNKNOWN_PROTOCOL,
                                 IEStatus.RESOURCE_NOT_FOUND,
                                 IEStatus.DOWNLOAD_FAILURE,])
        self._skip_error_code.update(HTTPStatus.ALL_CONST)
    
    def __del__(self):
        self.quit()
    
    @timeout_func(60)
    def _visit(self, url):
        """带超时的访问方法，超时会抛出Timeout异常
        http://www.szgtzy.gov.cn/会导致ie卡死
        """
        try:
            timeouter = Timeouter(self._navigate, url)
            if timeouter.track(10, self.quit):
                return False, None
            else:
                return self._wait(url)
        except:
            log_execption()
            self.quit()
            return False, None
    
    def visit(self, url):
        """返回值：success, status_code. 
        如果success=True，则判断status_code是否为404，并作相应处理。
        如果success=False，则访问URL失败.
        可以通过is_timeout判断是否超时"""
        self.is_timeout = False
        self.is_download = False
        self.title = None
        self.description = None
        self.real_url = None
        try:
            return self._visit(url)
        except Timeout:
            logging.error('%r ie long time out' % url)
            self.is_timeout = True
            self.quit()
            return False, None
    
    def _navigate(self, url):
        try:
            if not self._ie or self.need_quit:
                if self.debug:
                    print 'Create IE Dispatch'
                self._ie = win32com.client.DispatchWithEvents(
                    "InternetExplorer.Application", ExplorerEvents)
            else:
                self._ie.re_init()
            self._ie.debug = self.debug
            self._ie.Visible = self.visible
            self._ie.Silent = 1
            if self.debug:
                print 'Start navigate', url
            self._ie.Navigate(url)
            if self.debug:
                print 'End navigate', url
        except pythoncom.com_error, e:
            if self.debug:
                print 'Navigate %s error:' % url, e
            if url != self.BLANK_URL:
                log_execption()
            self.quit()
    
    def _clean_cookies(self, ie):
        if self.debug:
            print 'Clear cookies:', ie.Document.cookie
        items = ['%s;expires=Thu, 01-Jan-1970 00:00:00 GMT;' % i.strip() for i in ie.Document.cookie.split(';')]
        for i in items:
            ie.Document.cookie = i
    
    def _wait(self, url):
        ie = self._ie
        if not ie:
            return False, None
        success = ie.wait_complete(self.timeout)
        self.is_timeout = ie.is_timeout
        
        doc_avaible = True
        try:
            if not ie.Document:
                doc_avaible = False
        except pythoncom.com_error, e:
            if self.debug:
                print 'get ie.Docment error', e
            doc_avaible = False
        
        self.real_url = ie.real_url
        if doc_avaible:
            try:
                print 'Getting LocationURL...'
                if ie.LocationURL and ie.LocationURL != self.BLANK_URL:
                    self.real_url = ie.LocationURL
                print 'Clean cookies...'
                self._clean_cookies(ie)
                need_title = True
                if self.real_url and url != self.real_url:
                    # 若不是调到广东dns的域则，获取， autosearch.gd.vnet.cn
                    url_split = urlutil.standardize(self.real_url)
                    if url_split:
                        host = url_split.host
                        if 'autosearch.gd.vnet.cn' in host:
                            need_title = False
                        elif '114search.118114.cn' in host:  # 过滤 114search.118114.cn
                            need_title = False
                if need_title:
                    print 'Getting title and description...'
                    self._get_title(ie)
            except:
                log_execption()
        if self._is_404(self.real_url):
            status_code = HTTPStatus.NOT_FOUND
        else:
            status_code = ie.status_code
        self.is_download = ie.is_download
        
        if self.need_quit or ie.need_clear:
            self.quit()
        else:
            self.stop()
        if status_code:
            if status_code in self._skip_error_code:
                success = True
            elif status_code < 100 or status_code >= 600:
                success = False
        # 不在指定的http状态，获取的标题和描述是无效的
        if status_code and not status_code in (HTTPStatus.OK, 
                                               HTTPStatus.NOT_MODIFIED, 
                                               HTTPStatus.MOVED_PERMANENTLY, 
                                               HTTPStatus.FOUND):
            self.title = None
            self.description = None
        return success, status_code
    
    def quit(self):
        self._ie = None
        try:
            kill_processname(self._to_kill)
        except:
            pass
    
    def stop(self):
        timeouter = Timeouter(self._stop)
        timeouter.track(10, self.quit)
    
    def _stop(self):
        self._close_dialog()
        try:
            if self._ie:
                self._ie.Stop()
                self._navigate(self.BLANK_URL)
                if self._ie:
                    if not self._ie.wait_complete(2) \
                            or self._ie.LocationURL != self.BLANK_URL:
                        self.quit()
                    else:
                        self._close_all_ie()
        except pywintypes.com_error:
            self.quit()
    
    def _get_title(self, ie):
        """
        description:
        http://www.phpied.com/getelementbyid-description-in-ie/
        """
        try:
            title = ie.Document.title
            if title:
                if isinstance(title, str):
                    charset = ie.Document.charset
                    if charset:
                        title = title.decode(charset)
                    else:
                        title = title.decode('utf-8', 'replace')
                if u'输入的域名或网址无法访问' in title:
                    title = None
                self.title = title
            element = ie.Document.getElementById('description')
            if element and element.content:
                description = element.content
                if isinstance(title, str):
                    charset = ie.Document.charset
                    if charset:
                        description = description.decode(charset)
                    else:
                        description = description.decode('utf-8', 'replace')
                self.description = description
        except Exception, e:
            print 'get title error: %s' % e
            pass
    
    def _is_404(self, url):
        if not url:
            return False
        site = urlparse.urlsplit(url)[1]
        if site:
            site = site.lower()
            if site.endswith("gd.vnet.cn"):
                return True
        return False
    
    def _close_dialog(self):
        try:
            wnd = winguiauto.findTopWindows(wantedClass="#32770")
            for sw in wnd:
                win32gui.PostMessage(sw,win32con.WM_CLOSE, 0, 0)
        except:
            pass
    
    def _close_all_ie(self):
        try:
            wnd = winguiauto.findTopWindows(wantedClass="IEFrame")
            if len(wnd) > 1:
                self.quit()
        except:
            pass
