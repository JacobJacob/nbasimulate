#!/usr/bin/python
# -*- coding: utf-8 -*-
from datetime import datetime, timedelta

from webauth.common.db import connection

def add_black_url_count(from_type, row_effected, url_created_time, is_black):
    """统计黑URL数量。在每次新增黑URL时调用
    param: from_type即URL来源, row_effected新增的黑URL个数, url_created_time即URL进入系统的时间
    return:没有返回值"""
    return
    if not row_effected:
        return
    sql = "insert into black_url_statistic (from_type, black_url_count, url_created_time, created_time) " \
        + "values(%s, %s, %s, now()) on duplicate key update black_url_count = black_url_count + values(black_url_count)"
    created_hour = datetime(url_created_time.year, 
                            url_created_time.month, 
                            url_created_time.day, 
                            url_created_time.hour)
    cursor = connection.cursor()
    try:
        cursor.execute(sql, (from_type, row_effected, created_hour))
    finally:
        cursor.close()
        
def add_total_url_count(statistic_hour):
    select_sql = "select from_type, count(*) as url_count from url_source " \
            + "where created_time >= %s and created_time < %s " \
            + "group by from_type"
    insert_sql = "insert into url_statistic (from_type, url_count, url_created_time, created_time) " \
                + "values(%s, %s, %s, now())"
    h = statistic_hour
    begin_hour = datetime(h.year, h.month, h.day, h.hour)
    end_hour = begin_hour + timedelta(hours=1)
    
    cursor = connection.cursor()
    cursor.execute("start transaction")
    try:
        rs = cursor.fetchall(select_sql, (begin_hour, end_hour))
#        values = []
        for r in rs:
            from_type = r["from_type"]
            url_count = r["url_count"]
            value = (from_type, url_count, begin_hour)
            cursor.execute(insert_sql, value)
#            values.append(value)
#        cursor.executemany(insert_sql, values)
        cursor.execute("commit")
    except:
        cursor.execute("rollback")
        raise
    finally:
        cursor.close()

def get_url_source_begin():
    cursor = connection.cursor()
    try:
        last_time = None
        rs = cursor.fetchone("select max(url_created_time) from url_statistic")
        if rs[0]:
            last_time = datetime(rs[0].year, rs[0].month, rs[0].day, rs[0].hour)
        else:
            rs = cursor.fetchone("select min(created_time) from url_source")
            if rs[0]:
                last_time = datetime(rs[0].year, rs[0].month, rs[0].day, rs[0].hour)
                last_time -= timedelta(hours=1)
    finally:
        cursor.close()
    if last_time:
        return last_time + timedelta(hours=1)
    return None

def get_url_source_stop():
    cursor = connection.cursor()
    try:
        stop_hour = cursor.get_db_time()
    finally:
        cursor.close()
    if stop_hour.minute < 15:
        stop_hour -= timedelta(hours=1)
    stop_hour = datetime(stop_hour.year,
                         stop_hour.month,
                         stop_hour.day,
                         stop_hour.hour)
    return stop_hour

def count_black_url():
    last_id = -1
    select_sql = "select id, from_type, url_created_time, domain from url_check_result where id>%s order by id limit 1000"
    insert_url_sql = "insert into black_url_statistic (from_type, black_url_count, url_created_time, created_time) " \
                 + "values(%s, %s, %s, now())"
    insert_domain_sql = "insert into black_domain_statistic (domain, from_type, black_url_count, url_created_time, created_time) " \
                 + "values(%s, %s, %s, %s, now())"
    url_data = {}
    domain_data = {}
    cursor = connection.cursor()
    try:
        while True:
            rs = cursor.fetchall(select_sql, (last_id))
            if not rs:
                break
            for r in rs:
                last_id = r["id"]
                from_type = r["from_type"]
                url_created_time = r["url_created_time"]
                domain = r["domain"]
                if not url_created_time or from_type is None:
                    continue
                url_created_hour = datetime(url_created_time.year,
                                            url_created_time.month,
                                            url_created_time.day,
                                            url_created_time.hour)
                url_key = (from_type, url_created_hour)
                if url_data.has_key(url_key):                    
                    url_data[url_key] += 1
                else:
                    url_data[url_key] = 1

                domain_key = (from_type, url_created_hour, domain)
                if domain_data.has_key(domain_key):
                    domain_data[domain_key] += 1
                else:
                    domain_data[domain_key] = 1
        cursor.execute("truncate black_url_statistic")
        for key in url_data.iterkeys():
            from_type, url_created_hour = key
            cursor.execute(insert_url_sql, (from_type, url_data[key], url_created_hour))

        cursor.execute("truncate black_domain_statistic")
        for key in domain_data.iterkeys():
            from_type, url_created_hour, domain = key
            cursor.execute(insert_domain_sql, (domain, from_type, domain_data[key], url_created_hour))
    finally:
        cursor.close()

def get_total_url_data():
    data = {}
    url_sql = "select from_type, sum(url_count) as url_count from url_statistic group by from_type"
    black_url_sql = "select from_type, sum(black_url_count) as url_count from black_url_statistic group by from_type"    
    cursor = connection.cursor()
    try:
        total_url_rs = cursor.fetchall(url_sql)
        total_black_rs = cursor.fetchall(black_url_sql)
    finally:
        cursor.close()
    all_count = 0
    for r in total_url_rs:
        from_type = r["from_type"]
        url_count = int(r["url_count"])
        all_count += url_count
        data[from_type] = [url_count, None]
    all_black_count = 0
    for r in total_black_rs:
        from_type = r["from_type"]
        url_count = int(r["url_count"])
        all_black_count += url_count
        if data.has_key(from_type):
            data[from_type][1] = url_count
        else:
            data[from_type] = [None, url_count]
    data["all"] = [all_count, all_black_count]
    return data

def get_day_url_source(count_day = None):
    if not count_day:
        count_day = datetime.now() - timedelta(days=1)
    data = {}
    begin_day = datetime(count_day.year, count_day.month, count_day.day)
    end_day = begin_day + timedelta(days=1)
    url_sql = "select from_type, sum(url_count) as url_count from url_statistic where url_created_time>=%s and url_created_time<%s group by from_type"
    black_url_sql = "select from_type, sum(black_url_count) as url_count from black_url_statistic where url_created_time>=%s and url_created_time<%s group by from_type"
    cursor = connection.cursor()
    try:
        url_rs = cursor.fetchall(url_sql, (begin_day, end_day))
        black_rs = cursor.fetchall(black_url_sql, (begin_day, end_day))
    finally:
        cursor.close()
    all_count = 0
    for r in url_rs:
        from_type = r["from_type"]
        url_count = int(r["url_count"])
        all_count += url_count
        data[from_type] = [url_count, None]
    all_black_count = 0
    for r in black_rs:
        from_type = r["from_type"]
        url_count = int(r["url_count"])
        all_black_count += url_count
        if data.has_key(from_type):
            data[from_type][1] = url_count
        else:
            data[from_type] = [None, url_count]
    data["all"] = [all_count, all_black_count]
    return data

def get_top_black_domain(top_n = 10):
    sql = "select domain, sum(black_url_count) as black_url_count from black_domain_statistic group by domain order by black_url_count desc limit %s" % top_n
    url_count_sql = "select url_count from domain_info where domain=%s limit 1"
    eyurl_sql = "select 1 from eyurl where url=%s limit 1"
    data = []
    cursor = connection.cursor()
    try:
        domain_rs = cursor.fetchall(sql)    
        for r in domain_rs:
            domain = r["domain"]
            black_url_count = int(r["black_url_count"])
            url_count = black_url_count
            domain_rs = cursor.fetchone(url_count_sql, (domain))
            percentage = None
            if domain_rs:
                url_count = domain_rs[0]
            if url_count < black_url_count:
                url_count = black_url_count
            if cursor.fetchone(eyurl_sql, (domain)):
                black_url_count = url_count
            if url_count:
                percentage = 100.0 * black_url_count / url_count
                percentage = "%.2f" % percentage
            data.append((domain, url_count, black_url_count, percentage))
    finally:
        cursor.close()
    return data

def get_day_black_domain(count_day = None, top_n = 10):
    if not count_day:
        count_day = datetime.now() - timedelta(days=1)
    begin_day = datetime(count_day.year, count_day.month, count_day.day)
    end_day = begin_day + timedelta(days=1)
    sql = "select domain, sum(black_url_count) as black_url_count from black_domain_statistic " \
        + "where url_created_time>=%s and url_created_time<%s " \
        + "group by domain order by black_url_count desc limit %s" % top_n
    url_count_sql = "select url_count from domain_info where domain=%s limit 1"
    eyurl_sql = "select 1 from eyurl where url=%s limit 1"
    cursor = connection.cursor()
    try:
        domain_rs = cursor.fetchall(sql, (begin_day, end_day))
        data = []
        for r in domain_rs:
            domain = r["domain"]
            black_url_count = int(r["black_url_count"])
            url_count = black_url_count
            domain_rs = cursor.fetchone(url_count_sql, (domain))
            percentage = None
            if domain_rs:
                url_count = domain_rs[0]
            if url_count < black_url_count:
                black_url_count = url_count
            if cursor.fetchone(eyurl_sql, (domain)):
                black_url_count = url_count
            if url_count:
                percentage = 100.0 * black_url_count / url_count
                percentage = "%.2f" % percentage
            data.append((domain, url_count, black_url_count, percentage))
    finally:
        cursor.close()
    return data

        
    
