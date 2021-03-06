#!/usr/bin/python
# -*- coding: utf-8 -*-
"""client manager"""

from gba.common.db import connection
from gba.common.constants import ClientStatus
from gba.common.db.reserve_convertor import ReserveLiteral
from gba.business import common_business
from gba.common.constants.client import Command


class ClientManager(object):
    
    SELECT_CLIENTS = "select *, unix_timestamp(now()) - unix_timestamp(updated_time) as last_time from client order by ip"
    
    SELECT_CLIENT = """select id from client where client_id=%s and type=%s and ip=%s
    """
    SELECT_CLIENT_BY_ID = """select * from client where id=%s
    """
    REGISTER_SQL = """insert into client(client_id, type, ip, status, version, cmd, description, created_time)
        values(%s, %s, %s, %s, %s, 'start', 'init', now()) 
        ON DUPLICATE KEY update status=values(status), version=values(version), 
            cmd=values(cmd), description=values(description)
    """
    UPDATE_STATUS_SQL = """update client set status=%s, description=%s, cmd=null, updated_time=now()
        where id=%s
    """
    UPDATE_STATUS_WITH_CMD_SQL = """update client set status=%s, description=%s, cmd=%s, updated_time=now()
        where id=%s
    """
    UPDATE_TIME_ONLY_SQL = """update client set cmd=null, updated_time=now() where id=%s
    """
    SET_COMMAND = """update client set cmd=%s, params=%s, updated_time=now()
        where id=%s
    """
    SET_COMMAND_TO_ALL = """update client set cmd=%s, params=%s, updated_time=now()
        where type = 'url_auth_sandbox'
    """
    DELETE_CLIENT = 'delete from client where id=%s'
    
    @classmethod
    def register(cls, id, client_type, version, ip):
        """根据客户端编号id，类型，ip返回服务器注册的id"""
        status = ClientStatus.SLEEP
        cursor = connection.cursor()
        try:
            cursor.execute(cls.REGISTER_SQL, (id, client_type, ip, status, version))
            r = cursor.fetchone(cls.SELECT_CLIENT, (id, client_type, ip))
            return r[0]
        finally:
            cursor.close()
    
    @classmethod
    def update_status(cls, client_id, status, description):
        """客户端状态更新，返回执行命令和辅助参数"""
        cursor = connection.cursor()
        try:
            r = cursor.fetchone(cls.SELECT_CLIENT_BY_ID, (client_id,))
            if not r:
                return Command.QUIT, None
            cmd, params = r['cmd'], r['params']
            # 更改状态，清空命令
            if status == ClientStatus.FINISH:
                # 任务完成，自动增加启动命令
                cmd = 'start'
                cursor.execute(cls.UPDATE_STATUS_WITH_CMD_SQL,
                               (status, description, cmd, client_id))
            else:
                cursor.execute(cls.UPDATE_STATUS_SQL, (status, description, client_id))
            return cmd, params
        finally:
            cursor.close()
            
    @classmethod
    def get_clients(cls):
        """获取客户端信息"""
        cursor = connection.cursor()
        try:
            clients = cursor.fetchall(cls.SELECT_CLIENTS)
            return clients
        finally:
            cursor.close()
            
    @classmethod
    def get_client(cls, client_id):
        """获取客户端信息"""
        cursor = connection.cursor()
        try:
            client = cursor.fetchone(cls.SELECT_CLIENT_BY_ID, client_id)
            return client
        finally:
            cursor.close()
    
    @classmethod
    def set_command(cls, client_id, cmd, params):
        """获取客户端信息"""
        cmd = cmd.strip().lower()
        if not params:
            params = ReserveLiteral('params')
        cursor = connection.cursor()
        try:
            if cmd == 'delete':
                cursor.execute(cls.DELETE_CLIENT, (client_id,))
            else:
                cursor.execute(cls.SET_COMMAND, (cmd, params, client_id))
        finally:
            cursor.close()
    
    @classmethod
    def set_command_to_all(cls, cmd, params):
        """获取客户端信息"""
        cmd = cmd.strip().lower()
        if not params:
            params = ReserveLiteral('params')
        cursor = connection.cursor()
        try:
            cursor.execute(cls.SET_COMMAND_TO_ALL, (cmd, params,))
        finally:
            cursor.close()
    SELECT_MSN_ACCOUNT = 'select email, password from msnbot where client_ip=%s'
    @classmethod
    def get_msn_account(cls, client_ip):
        cursor = connection.cursor()
        try:
            r = cursor.fetchone(cls.SELECT_MSN_ACCOUNT, (client_ip,))
            return r.to_dict()
        finally:
            cursor.close()
            
    SELECT_RUNTIME_DATA = """select data_content from runtime_data 
        where client=%s and process_id=%s and data_key=%s"""
    @classmethod
    def get_runtime_data(cls, client_name, process_id, data_key): #已废弃，请调用common_business.get_runtime_data()
        """获取运行时数据"""
        return common_business.get_runtime_data(client_name, process_id, data_key)

    @classmethod
    def set_runtime_data(cls, client_name, process_id, data_key, data_content):#已废弃，请调用common_business.set_runtime_data()
        """设置运行时数据"""
        return common_business.set_runtime_data(client_name, process_id, data_key, data_content)
            
