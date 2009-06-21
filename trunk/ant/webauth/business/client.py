#!/usr/bin/python
# -*- coding: utf-8 -*-
"""client manager"""

from webauth.business.dboperator import DBOperator
from webauth.common.constants import ClientStatus
from webauth.common.db.reserve_convertor import ReserveLiteral


class ClientManager(DBOperator):
    
    SELECT_CLIENTS = "select * from client order by ip"
    
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
    
    @classmethod
    def register(cls, id, client_type, version, ip):
        """根据客户端编号id，类型，ip返回服务器注册的id"""
        status = ClientStatus.SLEEP
        cursor = cls.cursor()
        try:
            cursor.execute(cls.REGISTER_SQL, (id, client_type, ip, status, version))
            r = cursor.fetchone(cls.SELECT_CLIENT, (id, client_type, ip))
            return r[0]
        finally:
            cursor.close()
    
    @classmethod
    def update_status(cls, client_id, status, description):
        """客户端状态更新，返回执行命令和辅助参数"""
        cursor = cls.cursor()
        try:
            r = cursor.fetchone(cls.SELECT_CLIENT_BY_ID, (client_id,))
            cmd, params = r['cmd'], r['params']
            # 更改状态，清空命令
            if status == ClientStatus.FINISH:
                # 任务完成，自动增加启动命令
                cmd = 'start'
                cursor.execute(cls.UPDATE_STATUS_WITH_CMD_SQL, 
                               (status, description, cmd, client_id))
#            elif status == ClientStatus.ACTIVE and description:
#                # 在运行状态并且有描述，则修改
#                cursor.execute(cls.UPDATE_STATUS_SQL, (status, description, client_id))
#            elif r['status'] == status: # 状态未发生改变，则只修改时间信息
#                cursor.execute(cls.UPDATE_TIME_ONLY_SQL, (client_id,))
            else:
                cursor.execute(cls.UPDATE_STATUS_SQL, (status, description, client_id))
            return cmd, params
        finally:
            cursor.close()
            
    @classmethod
    def get_clients(cls):
        """获取客户端信息"""
        cursor = cls.cursor()
        try:
            clients = cursor.fetchall(cls.SELECT_CLIENTS)
            return clients
        finally:
            cursor.close()
            
    @classmethod
    def get_client(cls, client_id):
        """获取客户端信息"""
        cursor = cls.cursor()
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
        cursor = cls.cursor()
        try:
            cursor.execute(cls.SET_COMMAND, (cmd, params, client_id))
        finally:
            cursor.close()
    
    SELECT_MSN_ACCOUNT = 'select email, password from msnbot where client_ip=%s'
    @classmethod
    def get_msn_account(cls, client_ip):
        cursor = cls.cursor()
        try:
            r = cursor.fetchone(cls.SELECT_MSN_ACCOUNT, (client_ip,))
            return r.to_dict()
        finally:
            cursor.close()
            
    SELECT_RUNTIME_DATA = """select data_content from runtime_data 
        where client=%s and process_id=%s and data_key=%s"""
    @classmethod
    def get_runtime_data(cls, client_name, process_id, data_key):
        """获取运行时数据"""
        cursor = cls.cursor()
        try:
            r = cursor.fetchone(cls.SELECT_RUNTIME_DATA, 
                                (client_name, process_id, data_key))
            if r:
                return r[0]
            return None
        finally:
            cursor.close()

    @classmethod
    def set_runtime_data(cls, client_name, process_id, data_key, data_content):
        """设置运行时数据"""
        cursor = cls.cursor()
        try:
            info = {'client': client_name,
                    'process_id': process_id,
                    'data_key': data_key,
                    'data_content': data_content,
                    'created_time': ReserveLiteral('now()'),}
            return cursor.insert(info, 'runtime_data', True, ['created_time'])
        finally:
            cursor.close()
            
if __name__ == '__main__':
    client_name = 'test'
    process_id = 100
    data_key = 'data_key'
    data_content = 'data_content'
    print ClientManager.set_runtime_data(client_name, process_id, data_key, data_content)
    print ClientManager.get_runtime_data(client_name, process_id, data_key)