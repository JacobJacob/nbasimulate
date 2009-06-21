# -*- coding: utf-8 -*-
import sys
sys.coinit_flags = 0          # specify free threading
import win32com.client 
import pythoncom
from threading import Lock, Thread
import pywintypes
import exception_mgr
from win32process_killer import kill_processname
from constants import IEStatus, HTTPStatus
import time
import win32event
import urlparse

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
    
class ExplorerEvents:
    def __init__(self, *args, **kwargs):
        self._cevent = win32event.CreateEvent(None, 0, 0, None)
        self._qevent = win32event.CreateEvent(None, 0, 0, None)
        self._lock = Lock()
        self.re_init()
    
    def re_init(self):
        self.is_download = False
        self.need_clear = False
        self._last_web = None
        self.status_code = None
        self._error_url = None
        self.real_url = None
        win32event.ResetEvent(self._cevent)
        win32event.ResetEvent(self._qevent)
    
    def wait_complete(self, timeout = -1):
        if self.real_url is None:
            timeout = 2
        if timeout > -1:
            timeout = timeout * 1000
        rc = win32event.WaitForSingleObject(self._cevent, timeout)
        if self.real_url is None:
            return True
        else:
            return rc == win32event.WAIT_OBJECT_0
    
    def wait_quit(self, timeout = -1):
        if timeout > -1:
            timeout = timeout * 1000
        rc = win32event.WaitForSingleObject(self._qevent, timeout)
        return rc == win32event.WAIT_OBJECT_0
    
    def OnDocumentComplete(self,
                           pDisp=pythoncom.Empty,
                           URL=pythoncom.Empty):
        if self._last_web and self._last_web == pDisp:
            win32event.SetEvent(self._cevent)

    def OnBeforeNavigate2(self, pDisp=pythoncom.Empty, URL=pythoncom.Empty, Flags=pythoncom.Empty, TargetFrameName=pythoncom.Empty, PostData=pythoncom.Empty, Headers=pythoncom.Empty, Cancel=pythoncom.Empty):
        self._lock.acquire()
        try:
            if not self.real_url:
                self.real_url = URL
        finally:
            self._lock.release()
    
    def OnNavigateComplete2(self,
                           pDisp=pythoncom.Empty,
                           URL=pythoncom.Empty):
        self._lock.acquire()
        try:
            if not self._last_web:
                self._last_web = pDisp
                self.real_url = URL
        finally:
            self._lock.release()
        
    def OnNavigateError(self, pDisp = pythoncom.Missing,URL = pythoncom.Missing,TargetFrameName =pythoncom.Missing, StatusCode =pythoncom.Missing,Cancel =pythoncom.Missing):
        self._lock.acquire()
        try:
            if self.real_url and self.real_url == URL:
                self.status_code = StatusCode
        finally:
            self._lock.release()
            
    def OnFileDownload(self, bActiveDocument = pythoncom.Missing, bCancel = pythoncom.Missing):
        if bActiveDocument:
            return False
        else:
            self.is_download = True
            self.need_clear = True
            win32event.SetEvent(self._cevent)
            return True
        
#    def OnNewWindow3(self, ppDisp = pythoncom.Missing, Cancel = pythoncom.Missing,
#                     dwFlags = pythoncom.Missing, bstrUrlContext = pythoncom.Missing,
#                     bstrUrl = pythoncom.Missing):
#        self.need_clear = True
    
    def OnQuit(self):
        win32event.SetEvent(self._qevent)

class IE(object):
    def __init__(self, need_quit = True, timeout = 45):
        self.timeout = timeout
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
            timeouter = Timeouter(self._navigate, url)
            if timeouter.track(10, self.quit):
                return False, None
            else:
                return self._wait()
        except:
            exception_mgr.on_except()
            self.quit()
            return False, None
    
    def _navigate(self, url):
        try:
            if not self._ie or self.need_quit:
                self._ie = win32com.client.DispatchWithEvents(
                    "InternetExplorer.Application", ExplorerEvents)
            else:
                self._ie.re_init()
            self._ie.Visible = 1
            self._ie.Silent = 1
            self._ie.Navigate(url)
        except:
            exception_mgr.on_except()
            self.quit()
    
    def _wait(self):
        success = True
        ie = self._ie
        if not ie:
            return False, None
        if not ie.wait_complete(self.timeout):
            success = False
            self.is_timeout = True
        timeouter = Timeouter(self._get_title, ie)
        timeouter.track(10, self.quit)
        
        if self._is_404(self.real_url):
            status_code = HttpStatus.NOT_FOUND
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
        if status_code and not status_code in (HttpStatus.OK, 
                                               HttpStatus.NOT_MODIFIED, 
                                               HttpStatus.MOVED_PERMANENTLY, 
                                               HttpStatus.FOUND):
            self.title = None
#        if not success:
#            self.quit()
        return success, status_code
    
    def quit(self):
        return
        self._ie = None
        try:
            kill_processname(self._to_kill)
        except:
            pass
    
    def stop(self):
        timeouter = Timeouter(self._stop)
        timeouter.track(10, self.quit)
    
    def _stop(self):
        pass
    
    def _get_title(self, ie):
        """
        description:
        http://www.phpied.com/getelementbyid-description-in-ie/
        """
        try:
            self.real_url = ie.LocationURL
            if not ie.Document:
                return
            title = ie.Document.title
            if title:
                if not isinstance(title, unicode):
                    charset = ie.Document.charset
                    if charset:
                        title = title.decode(charset).encode("utf-8")
                    else:
                        title = title.decode("utf-8").encode("utf-8")
                self.title = title
            element = ie.Document.getElementById('description')
            if element and element.content:
                description = element.content
                if not isinstance(title, unicode):
                    charset = ie.Document.charset
                    if charset:
                        description = description.decode(charset).encode("utf-8")
                    else:
                        description = description.decode("utf-8").encode("utf-8")
                self.description = description
        except:
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
    
if __name__ == '__main__':
    ie = IE()
    ie.visit('http://baidu.com')
    time.sleep(100)