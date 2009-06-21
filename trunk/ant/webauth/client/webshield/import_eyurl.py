#!/usr/bin/python
# -*- coding: utf-8 -*-
"""定时导入毒窝

http://svn.rdev.kingsoft.net/KXEngine/WebShield/trunk/Document/eyURL.xls
"""
import shutil
import logging
import os

from webauth.common.pyExcelerator import parse_xls
from webauth.common import svnutils
from webauth.common.db import connection
from webauth.common.single_process import SingleProcess
from webauth.common.md5mgr import mkmd5fromstr
from webauth.common.constants import UrlFromType, BlackURLCheckType
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common import urlutil
from webauth.common import init_log
from webauth.config import PathSettings
from webauth.business import url_source
from webauth.business import black_url


class MaliciousUrlImporter(object):
    """定时将网盾的黑url导入到eyurl表"""
    
    def _get_eyurl_infos(self, filepath):
        sheets = parse_xls(filepath)
        ey_urls = sheets[0][1]
        eyurl_infos = {}
        for row in xrange(1, 100000):
            if ey_urls.get((row, 0)) == None:
                break
            ey_url = ey_urls.get((row, 0))
            url_split = urlutil.standardize(ey_url)
            if url_split is None:
                continue
            ey_url = ey_url.strip()
            url = url_split.geturl()
            url_md5 = mkmd5fromstr(url)
            eyurl_info = {
                'url': ey_url, 
                'url_md5': url_md5,
                'type': int(ey_urls.get((row, 1))), # 1 网马, 2 木马, 3 钓鱼, 4 广告
                # 0：不需要域名匹配（2个点以上）2：两级匹配（xxx.com）3：三级匹配（aaa.com.cn）
                'domain_match': int(ey_urls.get((row, 2))), 
                # 1：主机名需要全部匹配（域名匹配必须有值）0：不需要主机名匹配
                'host_match': int(ey_urls.get((row, 3))),
                # 1：全部URL需要匹配（域名匹配和主机匹配必须有值）0：不需要URL全部匹配
                'all_match': int(ey_urls.get((row, 4))),
                'reporter': 'human',
                'created_time': ReserveLiteral('now()'),
            }
            check_type = 0
#            if eyurl_info['type'] == 3:
            if eyurl_info['domain_match'] > 0:
                check_type = BlackURLCheckType.DOMAIN_MATCH # domian
            elif eyurl_info['host_match'] > 0:
                check_type = BlackURLCheckType.HOST_MATCH # host级别
            eyurl_infos[url_md5] = [eyurl_info, check_type]
        return eyurl_infos
    
    def _compare(self, news, olds):
        """对比新旧库，返回差异
        return add_infos, delete_infos
        """
        add_infos, delete_infos = [], []
        for url_md5, info in news.iteritems():
            if url_md5 not in olds:
                add_infos.append(info)
        for url_md5, info in olds.iteritems():
            if url_md5 not in news:
                delete_infos.append(info)
        return add_infos, delete_infos
    
    def run(self):
#        svnpath = 'http://svn.rdev.kingsoft.net/KXEngine/WebShield/trunk/Document'
        last_eyurl_path = os.path.join(PathSettings.WORKING_FOLDER, 'last_webshield_eyurl.xls')
        localpath = os.path.join(PathSettings.WORKING_FOLDER, 'webshield_eyurl')
        # 第一次需要手工check out svnpath
        # svn co http://svn.rdev.kingsoft.net/KXEngine/WebShield/trunk/Document /data/webauth_working/webshield_eyurl --username mercury_upgrader --password mercury123
        eyurl_path = os.path.join(localpath, 'eyURL.xls')
        svnutils.svn_update(eyurl_path)
        
        eyurl_infos = self._get_eyurl_infos(eyurl_path)
        if os.path.exists(last_eyurl_path):
            last_eyurl_infos = self._get_eyurl_infos(last_eyurl_path)
        else:
            last_eyurl_infos = {}
        add_infos, delete_infos = self._compare(eyurl_infos, last_eyurl_infos)
        logging.info('total %s, last %s, add %s, del %s' % \
                    (len(eyurl_infos), len(last_eyurl_infos), len(add_infos), len(delete_infos)))
#        if not add_infos and not delete_infos:
#            return
        malicious_hosts = []
        malicious_host_md5s = []
        if add_infos: # 有add_infos
            malicious_infos = add_infos
        else: # 没有则使用全的
            malicious_infos = eyurl_infos.values()
        for info, check_type in malicious_infos:
            if check_type > 0:
                malicious_hosts.append({'host': info['url'], 
                                        'from_type': UrlFromType.WEBSHIELD_EYURL, 
                                        'ey_type': info['type'],
                                        'check_type': check_type,
                                        })
                malicious_host_md5s.append('"%s"' % mkmd5fromstr(info['url']))
        if delete_infos: # 需要删除的数据
            delete_url_md5s = ','.join(['"%s"' % info[0]['url_md5'] for info in delete_infos])
            delete_host_md5s = ','.join(['"%s"' % mkmd5fromstr(info[0]['url']) for info in delete_infos])
        else:
            delete_url_md5s = None
            delete_host_md5s = None
        
        cursor = connection.cursor()
        try:
            # 添加到eyurl
            if add_infos:
                lastrowid, effected = cursor.insert([info[0] for info in add_infos], 'eyurl', True, ['created_time', 'url_md5'])
                logging.info('add %s eyurls to eyurl' % effected)
                # 更新url_source来源
                logging.info('add to url_source')
                url_source.add_url([info['url'] for info, check_type in add_infos], UrlFromType.WEBSHIELD_EYURL, 
                                   cursor, change_source=True)
            # 添加host到黑库
            if malicious_hosts:
                if add_infos:
                    lastrowid, row_effected = black_url.add_malicious_hosts(malicious_hosts, cursor)
                    logging.info('add %s hosts to black_url' % row_effected)
                # 删除黑库 和结果表中相关的url, 必须是check_type == 0，否则会删除切割出来的host
                malicious_host_md5s = ','.join(malicious_host_md5s)
                effected = cursor.execute("""delete from black_url 
                    where (host_md5 in (%s) or domain_md5 in (%s)) and check_type = 0
                    """ % (malicious_host_md5s, malicious_host_md5s))
                logging.info('delete %s urls from black_url by malicious_host_md5s' % effected)
            # 删除
            if delete_url_md5s:
                # 删除eyurl
                effected = cursor.execute('delete from eyurl where url_md5 in (%s)' % delete_url_md5s)
                logging.info('delete %s eyurls from eyurl' % effected)
                # 删除black url
                effected = cursor.execute("""delete from black_url 
                    where host_md5 in (%s) or domain_md5 in (%s)""" % (delete_host_md5s, delete_host_md5s))
#                effected = black_url.remove_black_urls('%s,%s' % (delete_host_md5s, delete_url_md5s), cursor)
                logging.info('delete %s hosts from black_url' % effected)
        finally:
            cursor.close()
        # 移动当前eyurl_paht 到 last_eyurl_path
        if os.path.exists(last_eyurl_path):
            os.remove(last_eyurl_path)
        shutil.copy2(eyurl_path, last_eyurl_path)


if __name__ == '__main__':
    s = SingleProcess('import eyurl')
    s.check()
    init_log()
    
    importer = MaliciousUrlImporter()
    importer.run()