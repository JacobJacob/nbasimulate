#!/usr/bin/python
# -*- coding: utf-8 -*-
"""通用的webservice"""
from gba.common.db import connection
from gba.common.db.reserve_convertor import ReserveLiteral

def get_runtime_config(config_key, program=None, ip="all"):
    """获取运行时配置"""
    sql = "select config_value from runtime_config where ip=%s and program=%s and config_key=%s"
    cursor = connection.cursor()
    try:
        data = cursor.fetchone(sql, (ip, program, config_key))
        if data:
            return data[0]
        return None
    finally:
        cursor.close()

def get_runtime_data(client_name, process_id, data_key):
    """获取运行时数据"""
    sql = "select data_content from runtime_data " \
        + "where client=%s and process_id=%s and data_key=%s"
    cursor = connection.cursor()
    try:
        r = cursor.fetchone(sql, (client_name, process_id, data_key))
        if r:
            return r[0]
        return None
    finally:
        cursor.close()

def set_runtime_data(client_name, process_id, data_key, data_content):
    """设置运行时数据"""
    cursor = connection.cursor()
    try:
        info = {'client': client_name,
                'process_id': process_id,
                'data_key': data_key,
                'data_content': data_content,
                'created_time': ReserveLiteral('now()'),}
        return cursor.insert(info, 'runtime_data', True, ['created_time'])
    finally:
        cursor.close()

def get_task_status(index=0, limit=50):
    """获取任务运行信息"""
    sql = """SELECT * FROM task_status ORDER BY update_time DESC LIMIT %s, %s"""
    cursor = connection.cursor()
    try:
        rs = cursor.fetchall(sql, (index, limit))
        if rs:
            rs = rs.to_list()
            for r in rs:
                desc = r['description']
                if desc:
                    desc_list = desc.encode('utf-8').split('\n')
                    if len(desc_list) > 20:
                        r['description'] = '... more than 20 ...\n%s' % '\n'.join(desc_list[-20:]) # 只要最后的20个
            return rs
        return None
    finally:
        cursor.close()
        
_INSERT_TASK_STATUS = """
        insert into task_status(name, status, 
            description, client_ip, start_time, update_time)
        values(%s, %s, %s, %s, now(), now())
        on duplicate key update 
            name=values(name), status=values(status), description=concat(description,"\n",values(description)),
            client_ip=values(client_ip), update_time=values(update_time)"""
def save_task_status(name, status, description, client_ip):
    """保存任务运行信息"""
    data = (name, status, description, client_ip)
    cursor = connection.cursor()
    try:
        return cursor.execute(_INSERT_TASK_STATUS, data)
    finally:
        cursor.close()