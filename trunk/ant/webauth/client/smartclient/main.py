#!/usr/bin/python
# -*- coding: utf-8 -*-
"""Smart Client

负责svn更新
"""
from __future__ import absolute_import

import threading
import time
import sys
import logging
import os
import traceback
import optparse
import ConfigParser

try:
    import multiprocessing
except ImportError:
    import processing as multiprocessing

from webauth.common import init_log
from webauth.common.svnutils import svn_update
from webauth.client.smartclient.deploy import CLIENTS
from webauth.common.constants import SmartClientCommand
try:
    from webauth.common.process import reboot
except ImportError: # reboot 暂时不支持linux
    reboot = None


gExitSign = None
Sign_Update = "update"
Sign_Restart = "restart"
Sign_Stop = "stop"
globalparmas = ""

def load_func(sFunc):
    try:
        dot = sFunc.rindex('.')
    except ValueError:
        logging.error('no dot in func name: %s' % sFunc)
        raise
    
    sModule = sFunc[:dot]
    sFunc = sFunc[dot+1:]
    
    return getattr(__import__(sModule, {}, {}, ['']), sFunc)


class Config(object):
    def __init__(self):
        self.accept_sections = ['DEFAULT']
        self.trans_funcs = {}
        self.cp = ConfigParser.ConfigParser()
        self.cp.optionxform = str
    
    def load(self, ifile):
        self.cp.readfp(ifile)
        
        sections = self.cp.sections()
        sections.append('DEFAULT')
        
        for x in sections:
            if x not in self.accept_sections:
                continue
            for k, v in self.cp.items(x):
                k = k.replace('-', '_')
                trans = self.trans_funcs.get(k, str)
                setattr(self, k, trans(v))

    def dump(self, ofile):
        self.cp.write(ofile)
        
    def set(self, section, option, value):
        self.cp.set(section, option, value)
        

class DaemonProcess(multiprocessing.Process):
    def __init__(self, name, memory, func, args, kwargs):
        if not isinstance(memory, int):
            raise TypeError, 'type of memory is not int: %r' % (type(memory))

        multiprocessing.Process.__init__(self)
        self.name = name
        self.func = func
        self.memory = memory
        self.args = args
        self.kwargs = kwargs
        
    def copy(self):
        return DaemonProcess(self.name, self.memory, self.func, self.args, self.kwargs)

    def run(self):            
        threading.currentThread().setName(self.name)
        
        init_log(prefix=self.name)
        
        if self.memory > 0:
            try:
                import resource
                resource.setrlimit(resource.RLIMIT_AS, 
                                   (self.memory* 1024 * 1024, -1))
            except ImportError:
                logging.warning('no resource module')
            except:
                # 实现新的升级代码方案后，这里会出现：ValueError: not allowed to raise maximum limit
                # 通过 os.system() 启动，父进程的最大限制也是 这个数，因此这里不用设置也是可以的
                info = '%r' % traceback.format_exc()
                if info.find("ValueError: not allowed to raise maximum limit") == -1:
                    logging.error(info)
        
        try:
            #reload(self.func.__module__)  
            self.func(*self.args, **self.kwargs)
        except Exception:
            logging.error('%r' % traceback.format_exc())
            raise
        
# 支持，函数带参数，格式为 -> func:-1,,a=2,c=
# 解析并调用 func("-1", "", a = "2", c = "")
# SMARTCLIENT 支持，函数带参数，格式为 -> func:-1,,a=2,c=
# 解析并调用 func("-1", "", a = "2", c = "")
# 注意：全部以字符串对待……因为无法识别参数类型，如果需要，可以在程序内部作一次转换
def parse_func(func):
    # func:-1,,a=2,c=
    pos = func.find(':')
    
    if pos == -1:
        return (func, [], {})
    
    funcName = func[:pos]
    other = func[pos+1:]
    
    if not other:
        return (funcName, [], {})
    
    other = other.split(',')
    
    args = []
    kwargs = {}
    for x in other:
        x = x.strip()
        pos = x.find('=')
        if pos == -1:
            args.append(x)
            continue
        
        key = x[:pos]
        value = x[pos+1:]
        kwargs[key] = value
    return (funcName, args, kwargs)
    
def Main(funcs = None, 
         autorestart=None,
         memory=None,
         interval=60):
    
    if autorestart is None:
        autorestart = True
    
    threading.currentThread().setName('SMARTCLIENT')
    init_log('SMARTCLIENT')

    procs = {}
    
    funcs = [parse_func(x) for x in funcs]
    funcs = [(x[0], load_func(CLIENTS[x[0]]), x[1], x[2]) for x in funcs]
    
    for name, func, args, kwargs in funcs:
        if func:
            logging.warning('start %s' % name) # 在初始化 Log 前不写日志
            dt = DaemonProcess(name, memory, func, args, kwargs)
            dt.start()
            pid = dt.getPid()
            if pid is None:
                logging.error('start %s failed' % name)
            else:
                procs[pid] = dt 
            
    while True:        
        pid = None
        for _k, v in procs.items():
            if v.isAlive():
                continue
            pid = v.getPid()
            status = v.getExitCode()
            x = v
            break
        
        if pid is None:
            time.sleep(5)
            continue
        
        x = procs[pid]
        if not x.isAlive():
            code = status
            logging.warning('process %s died with code %s' % (x.name, code))
            x.join() # 等待其结束
            
            del procs[pid]
            if code == SmartClientCommand.SVNUP_RESTART: # 43 升级重启
                print 'svn update ....'
                while True:
                    success, result = svn_update()
                    if success:
                        logging.warning('svn update: %s' % result)
                        print 'svn update success, %s' % result
                        break
                    print 'svn update error, %s' % result
                    time.sleep(5)
                cmd = 'python %s' % " ".join(globalparmas if globalparmas else sys.argv)
                print cmd
                # 以前的方法可以升级硬盘上的代码，但是不能升级内存中的代码（更新，重新加载）
                # 新方法都可以实现升级，这里完成后，启动一个进程，然后自己自杀（在下面退出循环）
                # 这里应该有逻辑错误（在下面会跳出），但却一直可用，就让人不解了
                # 因为在 这里 生成 了 一个 子进程 的缘故
                os.system(cmd) 
#                newProc = x.copy()
#                newProc.start()
#                procs[newProc.getPid()] = newProc
            elif code == SmartClientCommand.MACHINE_RESTART: # 机器重启
                print 'machine restart'
                reboot()
                
            elif code == 42 or (code > 0 and autorestart): # 42 重启
                newProc = x.copy()
                newProc.start()
                procs[newProc.getPid()] = newProc
        if not procs:
            break
                
def _pprint(funcs):
    """
print _pprint(['a','b','c','d','e']) 的打印效果
       a                               d
       b                               e
       c
    """
    clients = ''
    n = (len(funcs)+1)//2 #  // 取整除部分
    for i in range(0, n):
        if i + n >= len(funcs):
            clients += '\t%s\n' % funcs[i]
        else:
            clients += '\t%-28s\t%s\n' % (funcs[i], funcs[i+n])
    return clients

def list_clients(ofile):
    clients = sorted(CLIENTS.keys())

    sClients = _pprint(clients)
            
    ofile.write('''\
Clients:
%(clients)s
''' % {
       'clients': sClients.rstrip()
       }
    )
        
def main():
    global globalparmas
    globalparmas = sys.argv

    parser = optparse.OptionParser(usage='%prog [options] CLIENT...')
    
    parser.set_defaults(config='smartclient.cfg',
                        memory=1000,
                        autorestart=True,
                        interval=60)
    
    parser.add_option('-l', '--list-clients',
                      action='store_true',
                      help='list clients and exit')
    parser.add_option('-c', '--config',
                      metavar='FILE',
                      help='read config from FILE, default is %default')
    parser.add_option('-d', '--daemon',
                      action='store_true',
                      help='daemon mode')
    parser.add_option('-M', '--memory',
                      type='int',
                      metavar='N',
                      help='limit memory to N megabyte, default is %default.'
                           ' if you want to turn off memory restriction, use -M -1')
    parser.add_option('-m', '--manager',
                      metavar='IP[:PORT]',
                      help='set manager IP and PORT')
    parser.add_option('-a', '--autorestart',
                      action='store_true',
                      help='auto restart, this is default')
    parser.add_option('-n', '--noautorestart',
                      dest='autorestart',
                      action='store_false',
                      help='do not auto restart')
    parser.add_option('-i', '--interval',
                      type='int',
                      help='check interval, in seconds, default is %default'
                      )
    
    opts, args = parser.parse_args()
    if opts.list_clients:
        list_clients(sys.stdout)
        sys.exit(0)
        
    if not args:
        parser.print_help(sys.stderr)
        sys.exit(2)
    current_dir = os.path.dirname(os.path.realpath(__file__))
    configFile = os.path.join(current_dir, opts.config)
    if not os.path.exists(configFile):
        if opts.config != parser.defaults['config']:
            parser.error('can not find config: %r' % opts.config)
        configFile = None
            
    config = Config()
    if configFile is not None:
        config.load(file(configFile))
    
    config.daemon = opts.daemon
    config.manager = opts.manager
        
#    if hasattr(config, 'daemon') and config.daemon:
#        from django.utils import daemonize
#        daemonize.become_daemon('/')
        
    if config.manager is not None:
        from webauth import client
        os.environ['URLAUTH_MANAGER'] = config.manager
        client.set_default_manager(config.manager)
    
    Main(args, opts.autorestart, opts.memory, opts.interval)

if __name__ == '__main__':
    main()