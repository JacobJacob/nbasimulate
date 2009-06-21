#-*- coding:utf-8 -*-
from threading import Thread, Lock, Condition
import exception_mgr

class ThreadPoolException(Exception):
    pass

class ThreadPool:
    def __init__(self, size):
        self.has_error = False
        self.on_error = lambda e: exception_mgr.on_except(None, 1)
        self._size = size
        self._free_threads = set()
        self._threads = []
        self._waiter = Condition(Lock())

    def add_job(self, method, *args, **kwargs):
        """on_error: exception handler
           error_args: additional error args"""
        self._waiter.acquire()
        try:
            if self._set_thread_job(method, args, kwargs):
                return
            if len(self._threads) < self._size:
                t = _ThreadItem(self)
                t.set_job(method, args, kwargs)
                t.start()
                self._threads.append(t)
                return
            if not self._free_threads:
                self._waiter.wait()
            if not self._set_thread_job(method, args, kwargs):
                raise ThreadPoolException("no free thread")
        finally:
            self._waiter.release()

    def done(self):
        while self._threads:
            t = self._threads.pop(0)
            t.join()
        self._free_threads.clear()

    def _notify(self, t):
        self._waiter.acquire()
        try:
            self._free_threads.add(t)
            self._waiter.notifyAll()
        finally:
            self._waiter.release()

    def _set_thread_job(self, method, args, kwargs):
        if self._free_threads:
            t = self._free_threads.pop()
            t.set_job(method, args, kwargs)
            return True
        return False

class _ThreadItem(Thread):
    def __init__(self, pool):
        Thread.__init__(self)
        self._job = None
        self.setDaemon(True)
        self._doing = True
        self._pool = pool
        self._waiter = Condition(Lock())
        self.is_free = lambda: (self._job == None)

    def set_job(self, method, args, kwargs):
        self._waiter.acquire()
        try:
            if not self.is_free():
                raise ThreadPoolException("thread is busy now")
            self._job = (method, args, kwargs)
            self._waiter.notifyAll()
        finally:
            self._waiter.release()

    def run(self):
        self._waiter.acquire()
        try:
            while self._doing or self._job:
                if self._job:
                    method, args, kwargs = self._job
                    on_error = kwargs.pop("on_error", self._pool.on_error)
                    error_args = kwargs.pop("error_args", ())
                    try:
                        method(*args, **kwargs)
                    except Exception, e:
                        self._pool.has_error = True
                        try:
                            on_error(e, *error_args)
                        except:
                            pass
                    finally:
                        self._job = None
                        self._pool._notify(self)
                elif self._doing:
                    self._waiter.wait()
        finally:
            self._waiter.release()

    def join(self, timeout = None):
        self._waiter.acquire()
        self._doing = False
        self._waiter.notifyAll()
        self._waiter.release()
        Thread.join(self, timeout)
