#!/usr/bin/python
# -*- coding: utf-8 -*-
"""docstring"""

from webauth.common.db.connection_wrapper import Connection
from webauth.common.db import connection
from webauth.business import url_source
from webauth.common.constants import UrlFromType
from webauth.common import urlutil


def dump():
    conn = Connection(
                          user = 'readonly',
                          db = 'webobserver',
                          passwd = '123456',
                          host = '192.168.47.12',
                          port = 3306,
                          charset = "utf8"
                          )
    f = open('webobserver_urls.dat', 'ab')
    cursor = conn.cursor()
    sql = 'SELECT id, rawurl FROM analyzeapp_wangduncheckurl where id>%s order by id asc limit 10000;'
    last_id = 0
    try:
        while True:
            rs = cursor.fetchall(sql, (last_id,))
            if not rs:
                break
            for r in rs:
                url = r[1].encode('utf-8')
                f.write('%s\n' % url)
            max_id = rs[-1][0]
            print '%d -> %d' % (last_id, max_id)
            last_id = max_id
    finally:
        f.close()
        cursor.close()
        
def dump_webobserver_blackurl(filepath):
    conn = Connection(user = 'readonly',
                          db = 'webobserver',
                          passwd = '123456',
                          host = '192.168.47.12',
                          port = 3306,
                          charset = "utf8"
                          )
    f = open(filepath, 'ab')
    cursor = conn.cursor()
    sql = 'SELECT id, url FROM dmapp_urlresult where id>%s order by id asc limit 10000;'
    last_id = 0
    try:
        while True:
            rs = cursor.fetchall(sql, (last_id,))
            if not rs:
                break
            for r in rs:
                url = r[1].encode('utf-8')
                f.write('%s\n' % url)
            max_id = rs[-1][0]
            print '%d -> %d' % (last_id, max_id)
            last_id = max_id
    finally:
        f.close()
        cursor.close()
        
def dump_important_url(filepath):
    conn = Connection(
                          user = 'readonly',
                          db = 'webobserver',
                          passwd = '123456',
                          host = '192.168.47.12',
                          port = 3306,
                          charset = "utf8"
                          )
    f = open(filepath, 'ab')
    cursor = conn.cursor()
    sql = 'SELECT id, rawurl FROM analyzeapp_urlmonitor where id>%s and type<>3 and classify<>0 order by id asc limit 10000;'
    last_id = 0
    try:
        while True:
            rs = cursor.fetchall(sql, (last_id,))
            if not rs:
                break
            for r in rs:
                split_result = urlutil.standardize(r[1])
                if not split_result:
                    continue
                url = split_result.host
                f.write('%s\n' % url)
            max_id = rs[-1][0]
            print '%d -> %d' % (last_id, max_id), rs[-1][1]
            last_id = max_id
    finally:
        f.close()
        cursor.close()
        
def dump_host(filepath):
    conn = Connection(
                          user = 'readonly',
                          db = 'webobserver',
                          passwd = '123456',
                          host = '192.168.47.12',
                          port = 3306,
                          charset = "utf8"
                          )
    f = open(filepath, 'ab')
    cursor = conn.cursor()
    sql = 'SELECT id, backup_host FROM thirdapp_thirddata where id>%s order by id asc limit 10000;'
    last_id = 0
    try:
        while True:
            rs = cursor.fetchall(sql, (last_id,))
            if not rs:
                break
            for r in rs:
                split_result = urlutil.standardize(r[1])
                if not split_result:
                    continue
                url = split_result.host
                f.write('%s\n' % url)
            max_id = rs[-1][0]
            print '%d -> %d' % (last_id, max_id), rs[-1][1]
            last_id = max_id
    finally:
        f.close()
        cursor.close()
        
def dump_host_from_url(filepath):
    f = open(filepath, 'ab')
    cursor = connection.cursor()
    sql = 'SELECT id, url FROM url_source where id>%s order by id asc limit 10000;'
    last_id = 0
    try:
        while True:
            rs = cursor.fetchall(sql, (last_id,))
            if not rs:
                break
            for r in rs:
                split_result = urlutil.standardize(r[1])
                if not split_result:
                    continue
                f.write('%s\n' % split_result.host_url)
                if not split_result.is_domain:
                    f.write('%s\n' % split_result.domain_url)
            max_id = rs[-1][0]
            print '%d -> %d' % (last_id, max_id), rs[-1][1]
            last_id = max_id
    finally:
        f.close()
        cursor.close()
        
def load(filepath, from_type):
    f = open(filepath, 'rb')
    try:
        urls = []
        i = 0
        for l in f:
            urls.append(l.strip())
            i += 1
            if len(urls) > 1000:
                u = urlutil.standardize(urls[-1])
                if u:
                    print i, u.geturl()
                url_source.add_url(urls, from_type)
                urls = []
        if urls:
            print i, urls[-1]
            url_source.add_url(urls, from_type)
    finally:
        f.close()


if __name__ == '__main__':
#    dump_important_url('important_white_urls.dat')
#    dump_host_from_url('dump_host_from_url.dat')
#    load('dump_host_from_url.dat', UrlFromType.SPLIT_URL)
#    load('important_white_urls.dat', UrlFromType.IMPORTANT_SITE)

#    dump_webobserver_blackurl('webobserver_blackurl.dat')
#    load('webobserver_blackurl.dat', UrlFromType.WEBSHIELD)
    
#    dump_host('thirdapp_thirddata_host.dat')
    load('thirdapp_thirddata_host.dat', UrlFromType.INIT)