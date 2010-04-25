#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Client Base Class"""

import logging
import traceback
import time
import sys
import threading
import datetime

import gba
from gba.common import exception_mgr
from gba.business.client import ClientManager
from gba.common.constants import ClientStatus, STATUS_MAP, Command, SmartClientCommand
from gba.common import serverinfo

class WorkThread(threading.Thread):
    """工作线程"""
    def __init__(self, func, *args, **kwargs):
        threading.Thread.__init__(self)
        self.func = func
        self.args = args
        self.kwargs = kwargs
        
    def run(self):
        self.func(*self.args, **self.kwargs)
        
class BaseClient(object):
    """客户端基类"""
    
    REPORT_SLEEP_TIME = 20
    
    def __init__(self, client_type):
        self.client_type = client_type # 客户端名称
        self.status = ClientStatus.SLEEP # 初始状态
        self._last_report_status = None
        self.tmp_cmd = None # 临时指令存储
        self.work_thread = None # 工作线程
        
        # 子类可能会用到的几个参数，从 TaskScheduler 中读出（不回写）
        self.params = None # report() 函数成功运行后即有值
#        self.task_id = None # report() 函数成功运行后即有值
        self.client_id = None # register() 成功，即有值，服务器端负责分配，保证唯一
        self.current_info = None

        self.status_lock = threading.RLock()
        
        self.debug = False
        
        self.work_thread_stop = False # 支持 stop() 用的变量
        self.work_thread_pause = False # 支持 pause() 用的变量
        
        self.is_paused = False # 是否已经成功进入暂停状态
        self.sleep_time = self.REPORT_SLEEP_TIME
        self.ip = serverinfo.get_ip()

    def get_value_from_params(self, key, value_type, default):
        """从传回的params中获取指定参数的值
        self.params的格式为key:value,key:value...
        """
        if self.params:
            items = dict([i.split(':', 1) for i in self.params.split(',') if ':' in i])
            if key in items:
                try:
                    return value_type(items[key])
                except ValueError:
                    pass
        return default

    def suport_pause(self):
        if self.work_thread_pause:
            logging.warning("JUST DO in status PAUSE, ...")
            self.is_paused = True
            while self.work_thread_pause:
                time.sleep(5)
            self.is_paused = False
            logging.warning("JUST DO out status PAUSE, ...")
    
    def run(self):
        """"函数主体, 子类必须覆盖。"""
        raise NotImplementedError
    
    def mainbody(self):
        try:
            self.current_info = None
            while not self.work_thread_stop:
#                self._report() # 不由工作线程调用
                next = self.run()
                self.suport_pause() # 如果子类不支持 PAUSE，保证状态切换正确
                
                # 若是数字并且大于等于0，则表示是暂停时间
                if isinstance(next, (int, float)) and next >= 0:
                    while next > 0 and not self.work_thread_stop: # 避免接收到停止任务也没有被停止
                        sleep_seconds = 5
                        if next < sleep_seconds:
                            s = next
                        else:
                            s = sleep_seconds
                        self._sleep(s)
                        next = next - sleep_seconds

            if not self.work_thread_stop: # 不是被stop了，就算正常结束
                self.execute(Command.FINISH)
        except SystemExit:
            if self.work_thread_stop:
                logging.warning('work thread exit!')
            else:
                raise
        except:
            self.current_info = traceback.format_exc()
            logging.error('%r' % self.current_info)
            self.execute(Command.ERROR)

    def _report(self):
        """汇报函数, 子类可以覆盖，回调 report() 进行上报。"""
        now = datetime.datetime.now()
        info = '[%s]%s' % (now.strftime("%Y-%m-%d %H:%M:%S"), self.current_info)
        return self.report(self.status, info) # 这里 附加参数 将传到服务器端的 publicop 里面，具体定义由子类实现
        
    def _start(self):
        self.work_thread = WorkThread(self.mainbody)
        self.work_thread.setName('WorkThread.%s.%s.client_%s' % \
                                 (self.client_type, self.__class__.__name__, self.client_id))
        self.work_thread.setDaemon(False)
        self.work_thread_stop = False # 支持 stop() 用的变量
        self.work_thread_pause = False # 支持 pause() 用的变量
        self.work_thread.start()
        
    def _svnup(self):
        logging.warning("%s[Source Version = %s] SVNUP, exit 43" % (self.client_type, gba.VERSION, serverinfo.get_ip()))
        raise SystemExit(SmartClientCommand.SVNUP_RESTART) # 升级重启进程
        
    def _quit(self):
        raise SystemExit(SmartClientCommand.EXIT) # 退出进程
        
    def _restart(self):
        raise SystemExit(SmartClientCommand.RESTART)
    
    def _reboot(self):
        raise SystemExit(SmartClientCommand.MACHINE_RESTART)
        
    def _pause(self):
        while not self.work_thread_pause:
            self.work_thread_pause = True
            time.sleep(1)
            
    def _continue(self):
        while self.work_thread_pause:
            self.work_thread_pause = False
            time.sleep(1)
            
    def _stop(self):
        self.work_thread_stop = True
#        # 保证工作线程也停止
#        if self.work_thread and self.work_thread.isAlive():
#            self.work_thread.kill()

    def _execute(self, cmd):
        "命令间不能相互调用，否则会发生死锁。"
        if cmd not in Command.ALL_CONST:
            raise ValueError("Unkonw cmd(%r)" % cmd)
        
        if  self.status == ClientStatus.SLEEP: # 主要是 STOP 指令，不能保证客户端按照规定 退出 了
            count = 0
            while self.work_thread and self.work_thread.isAlive():
                count += 1
                if count % 10 == 1:
                    logging.warning('Exec cmd(%r), BUT last task is not yet ended, WAITING...' % cmd)
                # 继续上报，避免“久无响应”错误
                self._report()
                self._sleep()
        self._change_status(cmd) # 状态切换
        
        if Command.START == cmd:
            self._start()
        elif Command.SVNUP == cmd:
            self._report()
            self._svnup()
        elif Command.QUIT == cmd:
            self._report()
            self._quit()
        elif Command.REREG == cmd: # 重启 进程，相当于 重新注册
            self._report()
            self._restart()
        elif Command.ERROR == cmd:
            pass
        elif Command.FINISH == cmd:
            pass
        elif Command.STOP == cmd:
            self._report()
            self._stop()
        elif Command.PASS == cmd:
            pass
        elif Command.PAUSE == cmd:
            self._report()
            self._pause()
        elif Command.CONTINUE == cmd:
            self._continue()
        elif Command.REBOOT == cmd:
            self._report()
            self._reboot()
            
        
    def _change_status(self, cmd):
        self.status = STATUS_MAP[self.status][cmd]

    def execute(self, cmd):
        logging.info('cmd: %r, params: %r' % (cmd, self.params))
        self.status_lock.acquire()
        try:
            if cmd == Command.START and self.status == ClientStatus.ACTIVE:
                return # 已运行无须启动
            if self.debug:
                logging.info("STATUS %r EXECUTE CMD={%r}" % (self.status, cmd))
            if self.status not in STATUS_MAP: 
                logging.warning("ASSERT FAILD: STATUS(%s) in (%s)" % (self.status, STATUS_MAP))
                return
            if cmd not in STATUS_MAP[self.status]: 
                logging.warning("ASSERT FAILD: cmd(%s) in (%s)" % (cmd, STATUS_MAP[self.status]))
                return
            self._execute(cmd)
            if Command.PASS in STATUS_MAP[self.status]: # 如果有 PASS，即跳到下个状态
                self._execute(Command.PASS)
            if self.debug:
                logging.info("%r current STATUS=%r" % (self.__class__.__name__, self.status))
        finally:
            self.status_lock.release()

    def report(self, status, description): # self._report() 成功运行后，self.params 就有值了
        """模块上报状态并接受指令，以及运行参数、Task ID 等。"""
        while True:
            try: 
                cmd, params = ClientManager.update_status(self.client_id, status, description)
                data = {'cmd': cmd, 'params': params}
                break
            except KeyboardInterrupt:
                raise
            except Exception, e:
                print e
                logging.warning('STATUS:%s, REPORT Server ERROR: %r' % (status, traceback.format_exc()))
                exception_mgr.on_except()
            self._sleep()
        
        result = data
        if self.debug: 
            logging.info("status: %s cmd: %r" % (status, result))
        if result:
            self.tmp_cmd = result.get('cmd', None)
            # 若没有命令并且处于空闲状态，又没被手动停止，则继续
            # 防止工作线程和主线程同时report导致服务器端无法发送start命令
            if self.tmp_cmd is None and self.status == ClientStatus.SLEEP \
                    and not self.work_thread_stop: 
                self.tmp_cmd = Command.START
            self.params = result.get('params', self.params)
            
        self._last_report_status = status # 设置最后上报状态
    
    def _sleep(self, seconds=None):
        if seconds is None:
            seconds = self.sleep_time
        time.sleep(seconds)
    
    def _register(self):
        """模块注册，并得到由服务器端分配的 Client ID。"""
        while True:
            try:
                self.client_id = ClientManager.register(self._id, self.client_type, gba.VERSION, self.ip)
                logging.info('%s REGISTER SUCCEED(client_id %s)' % \
                                (self.client_type, self.client_id))
                return True
            except Exception, e:
                exception_mgr.on_except()
                self._sleep()
        
    def _get_id(self):
        """获取客户端id，从sys参数获取，若没有，则默认1"""
        if len(sys.argv) > 1:
            return sys.argv[1]
        return 's0'
    
    def main(self):
        """客户端的主循环入口"""
        self._id = self._get_id()
        logging.info("%s[Source Version = %s] START" % (self.client_type, gba.VERSION))
        if not self._register(): # 只有注册成功，才能进入主循环
            return
        while True:
            try:
                self._report() # 具体上报内容可以由子类实现
                if self.tmp_cmd is not None:
                    self.execute(self.tmp_cmd) # 执行命令
                    self.tmp_cmd = None
            except SystemExit:
                raise
            except:
                exception_mgr.on_except()
            self._sleep()