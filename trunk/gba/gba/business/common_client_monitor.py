#!/usr/bin/python
# -*- coding: utf-8 -*-
"""
统计客户端执行情况
"""

from __future__ import with_statement

from datetime import datetime, timedelta
from business.dboperator import DBOperator

class CommonClientInfo:
    """普通客户端信息"""
    def __init__(self):
        """初始化客户端初始值"""
        self.id = 0
        self.ip = ""
        self.status = -2
        self.update_time = None
        self.client_id = ""
        self.is_active = False
        self.now_datetime = None
        self.lapse_seconds = 0
        self.count1 = 0
        self.count2 = 0
        self.count3 = 0
        self.count4 = 0
           
    def __repr__(self):
        return "(%d,%s,%d,%s,%s)" % (self.id, self.client_id, self.status, str(self.update_time), str(self.is_active))
        
class CommonClientMonitor(DBOperator):
    """
    统计客户端检测URL的数量，速度等
    """
    GET_CLIENT_INFO = """SELECT id, ip, status, updated_time, client_id, now() as now_time FROM `client` where type = %s;"""
    GET_CLIENT_RUN_STATUS = """SELECT lapse_seconds, report_count1, report_count2, report_count3, report_count4 FROM 
                            client_run_status_log where client_id = %s and report_datetime = %s"""
    
    def __init__(self, client_type, judge_type=0, time_type=2, time_length=1):
        """初始化类
        @param client_type:字符串，客户端类型
        @param judge_tpye:整数，从低位到高位以bit表示count1,count2,count3,count4
                          bit为0表示比比较，bit为1表示对应值要大于0
        @param time_type:整数，时间跨度类型，1： 以天为单位跨度查询
                                         2： 以小时为单位跨度查询
                                         3： 以分钟为单位跨度查询
        @param time_length:整数，时间跨度的长度 
        """
        self.client_type = client_type
        self.judge_type = judge_type
        self.time_type = time_type
        self.time_length = time_length
        self._clients_info = {}
            
    def get_client_info(self):
        """获取并初始化客户端基本信息，结果保存在self._clients_info内"""
        with self.cursor() as cursor:
            clients_info = cursor.fetchall(self.GET_CLIENT_INFO, (self.client_type,))
            for client_info in clients_info:
                client = CommonClientInfo()
                client.id = client_info["id"]
                client.ip = client_info["ip"]
                client.status = client_info["status"]
                client.update_time = client_info["updated_time"]
                client.client_id = client_info["client_id"]
                client.now_datetime = client_info["now_time"]
                self._clients_info[client_info["id"]] = client
            
            for id, client_info in self._clients_info.items():
                now_datetime = client_info.now_datetime - timedelta(seconds=300) #比当前时间提前5分钟，避免刚好跨小时时，数据还没更新上来
                if self.time_type == 1:
                    stat_datetime = datetime(now_datetime.year, now_datetime.month, (now_datetime.day / self.time_length) * self.time_length)
                elif self.time_type == 2:
                    stat_datetime = datetime(now_datetime.year, now_datetime.month, now_datetime.day, (now_datetime.hour / self.time_length) * self.time_length)
                elif self.time_type == 3:
                    stat_datetime = datetime(now_datetime.year, now_datetime.month, now_datetime.day, now_datetime.hour, (now_datetime.minute / self.time_length) * self.time_length)
                else:
                    stat_datetime = now_datetime 
                    
                run_status = cursor.fetchone(self.GET_CLIENT_RUN_STATUS, (id, str(stat_datetime)))
                if run_status:
                    if run_status["lapse_seconds"]:
                        client_info.lapse_seconds = run_status["lapse_seconds"]
                    if run_status["report_count1"]:
                        client_info.count1 = run_status["report_count1"]
                    if run_status["report_count2"]:
                        client_info.count2 = run_status["report_count2"]
                    if run_status["report_count3"]:
                        client_info.count3 = run_status["report_count3"]
                    if run_status["report_count4"]:
                        client_info.count4 = run_status["report_count4"]
           
            for id, client_info in self._clients_info.items():
                if not client_info.now_datetime or not client_info.update_time: #时间错
                    continue
                
                if client_info.status == -1 or client_info.status == -2: #状态错
                    continue
                
                if client_info.status != 1: #正常休眠状态，或者完成状态
                    client_info.is_active = True
                    continue
                
                if client_info.now_datetime < client_info.update_time or \
                    (client_info.now_datetime - client_info.update_time).seconds < 60:
                    
                    if client_info.lapse_seconds <= 0: #工作状态没干活，说明挂了
                        continue
                    
                    if self.judge_type & 0x01 and client_info.count1 <= 0:
                        continue
                    
                    if self.judge_type & 0x02 and client_info.count2 <= 0:
                        continue
                    
                    if self.judge_type & 0x04 and client_info.count3 <= 0:
                        continue
                    
                    if self.judge_type & 0x08 and client_info.count4 <= 0:
                        continue
                    
                    client_info.is_active = True                        

        return self._clients_info

if __name__ == '__main__':
#    stat_info = CommonClientMonitor("google_pr_spider", 0)
    stat_info = CommonClientMonitor("alexa_spider", 0)   
    print stat_info.get_client_info()
    
