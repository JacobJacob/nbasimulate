#coding:utf-8

from webauth.client.httpproxy import source
from webauth.business import http_proxy
from webauth.common.threadutil import FuncThread

def add_proxy(obj): 
    "接受 单个 str proxy 和 多个 str proxy 列表"
    [add_proxy(x) for x in obj] if isinstance(obj, list) else http_proxy.save_proxy(obj)

def main():
    http_proxy.check_all_db_proxy()
    for func in source.ProxyDigger(add_proxy).list_self():
        FuncThread(func, 1).start()    

if __name__ == '__main__':
    main()
