#!/usr/bin/env python
# -*- encoding: utf-8 -*-
"""
分类统计日志的错误信息，并发送email到指定邮箱
"""
import gzip
import re
import smtplib
import time
from types import ListType

from webauth.common.md5mgr import mkmd5fromstr
from webauth.common.svnutils import svninfo
from webauth.common.mailutil import Mail

LOG_FILENAME = r"/data/logs/webauth.log.1.gz"
#LOG_FILENAME = r'e:/webauth.log.1.gz'

MAIL_TO_LIST = ["wangbin2@kingsoft.com", "yuanfeng@kingsoft.com", \
                "yangquanhai@kingsoft.com","shixiangwen@kingsoft.com",\
                "panjianbo@kingsoft.com"]
#MAIL_TO_LIST = ["yuanfeng@kingsoft.com"]

MAIL_CC_LIST = []
MAIL_HOST = "bjmail.kingsoft.com"
MAIL_USER = "gateway_datasync_report"
MAIL_PASSWORD = "8Q2xT3Pt2121"
MAIL_POSTFIX = "kingsoft.com"

LOG_RE = re.compile("""
    ^
    (\w+)\s(\d{2})\s              #[0][1] month day
    (\d{2}):(\d{2}):(\d{2})\s     #[2][3][4] hour:minute:second 
    ([\d\.]+)\s                   #[5]client ip
    (\d+)\|                       #[6]process id
    ([^\|]+)\|                    #[7]thread name
    (\w+)\|                       #[8]level
    ([^\|]+?)\|                   #[9]pathname
    (\d+)\|                       #[10]lineno
    ([^\|]+)\|                    #[11]func name
    ([\s\S]*)                     #[12]message
    $ 
    """, re.IGNORECASE | re.VERBOSE)

REPR_RE = re.compile("""^'([\s\S]*)'$""",re.IGNORECASE | re.VERBOSE)

def analyse_log(filename=None):
    """分类统计日志文件错误信息
    @param filename: 需分析的路径和文件名
    @raise IOError,EOFError
    @return 字典，格式如 {md5content:{occur_times:?,error_content:?,first_occur_time:?,error_filename:?,client_ip:?}}
    """
    ERROR_STAMP = "ERROR"
    ERROR_LABLE = "|" + ERROR_STAMP + "|" 
    
    analyse_result = {}                    #format {key:[count,content]}
    zfile_handle = gzip.GzipFile(filename)
    try:
        while True:
            input_line = zfile_handle.readline()
            if not input_line:                #文件结束，读到EOF了
                break
            
            if ERROR_LABLE not in input_line:   #搜素特征串，如果没有这个，就不用再比较了
                continue
            
            line_split_result = LOG_RE.match(input_line)
            if not line_split_result:         #分析行出错，抛出异常？还是继续干活？
                continue                      #先继续干吧
            
            line_split_result = line_split_result.groups()
            if line_split_result[8] == ERROR_STAMP: #确认获取了一个错误
                error_occur_time = "%s %s %s:%s:%s" % \
                                (line_split_result[0], \
                                line_split_result[1], \
                                line_split_result[2], \
                                line_split_result[3], \
                                line_split_result[4])
                error_filename = line_split_result[9]
                error_client_ip = line_split_result[5]
                
                #为了兼容，增加一个变量
                error_content = line_split_result[12]
                if not REPR_RE.match(error_content):#判断没有用repr()格式化的，自己格式化把
                    error_content = repr(error_content)
                error_content_and_ip = "%s__%s" % (error_client_ip, error_content)
                md5_stamp = mkmd5fromstr(error_content_and_ip)
                if analyse_result.has_key(md5_stamp):
                    analyse_result[md5_stamp]["occur_times"] += 1
                    continue                        #如果找到同样的错误，错误数+1
                
                analyse_result[md5_stamp] = {"occur_times" : 1, \
                                              "error_content" : error_content, \
                                              "first_occur_time" : error_occur_time, \
                                              "error_filename" : error_filename, \
                                              "error_client_ip" : error_client_ip}
                
        return analyse_result
    
    except EOFError:
        pass    
    finally:
        zfile_handle.close()

def sort_analyse_result(result):
    """查询结果排序，逆序，把最多的异常放前边
    @param result: 字典，格式如 {md5content:{occur_times:?,error_content:?,first_occur_time:?,error_filename:?,error_client_ip:?}}
    @return 排序后列表，逆序，格式如 [(occur_times,error_label,code_author,first_occur_time,error_content,error_client_ip)]
    """
    TRACE_STAMP = "Traceback (most recent call last):\n" #注意一定要有\n，避免下面出异常
    error_label = ""
    analyse_list = []
    code_author_buffer = {}     #这个过程比较耗时，如果文件重复，用字典缓冲下结果，就不要再去服务器查询了
    for key in result.keys():
        error_content = eval(result[key]["error_content"])
        if TRACE_STAMP in error_content:
            error_label = error_content.split("\n")[1].lstrip()
        else:
            error_label = result[key]["error_filename"]

        code_author = ""
        if not code_author_buffer.has_key(result[key]["error_filename"]):
            code_author, code_version = svninfo(result[key]["error_filename"])
            code_author_buffer[result[key]["error_filename"]] = code_author
        else:
            code_author = code_author_buffer[result[key]["error_filename"]]
        analyse_list.append((result[key]["occur_times"], error_label, \
                              code_author, result[key]["first_occur_time"], \
                              result[key]["error_content"], \
                              result[key]["error_client_ip"]))
        
    return sorted(analyse_list,key=lambda d:d[0],reverse=True)

def send_mail(mail_subject, mail_content, mail_to_list, mail_cc_list=[], mail_attachment=None):
    """
    发送邮件到列表邮箱
    @param mail_subject:主题
    @param mail_content:内容
    @param mail_to_list:to mailbox list
    @param mail_cc_list:cc mailbox list
    @param mail_attachment: 附件
    @return 发送成功，true 失败，false
    """
    if not type(mail_to_list) is ListType or len(mail_to_list) < 1:
        raise Exception("[mail to] type error or empty") 
    
    send_address = MAIL_USER + "<" + MAIL_USER + "@" + MAIL_POSTFIX + ">"
  
    mail = Mail(MAIL_HOST, MAIL_USER + "@" + MAIL_POSTFIX, MAIL_PASSWORD)
    try:
        mail.send(mail_subject, mail_content, send_address, mail_to_list, mail_cc_list, mail_attachment)
        return True
    except smtplib.SMTPException:  #这里隐藏了smtp认证过程中的错误
        return False
    except smtplib.socket.error:   #这里隐藏了网络故障引发的部分错误
        return False

def run(mail_to_list=MAIL_TO_LIST, mail_cc_list=MAIL_CC_LIST, has_attachment=False):
    """启动日志错误统计功能，如果发送邮件失败，每隔10分钟重发，重发5次，仍发送失败引发异常
    @raise Exception,socket.error
    """
    MSG_HEAD_BEGIN = "-------------------- abstract begin -----------------------------------------"
    MSG_HEAD_END = "-------------------- abstract end ------------------------------------------"
    MSG_CONTENT_BEGIN = "---------------------- descript begin -----------------------------------------"
    MSG_CONTENT_END = "---------------------- descript end ----------------------------------------"

    analyse_result = analyse_log(LOG_FILENAME)
    analyse_list = sort_analyse_result(analyse_result)
    mail_subject = "log file:%s; analyse time:%s;" % (LOG_FILENAME, time.strftime("%Y-%m-%d %H:%M:%S", time.localtime()))
    
    msg_title = []
    error_index = 0
    for occur_times, error_label, code_author, first_occur_time, error_content, error_client_ip in analyse_list:
        error_index += 1
        msg_title.append("%4d. times %-5d mender:%s; %s; %s" % \
                         (error_index, occur_times, code_author, error_client_ip, error_label))
    msg_title = "\n".join(msg_title)

    msg_content = []
    error_index = 0
    for occur_times, error_label, code_author, first_occur_time, error_content, error_client_ip in analyse_list:
        error_index += 1
        msg_content.append("%d. times %d mender: %s; %s; first occur time %s \n%s\n %s" % \
          (error_index, occur_times, code_author, error_client_ip, first_occur_time, error_label, eval(error_content)))
    msg_content = "\n".join(msg_content)
       
    mail_content = "\n".join([mail_subject, MSG_HEAD_BEGIN, msg_title, \
                          MSG_HEAD_END, MSG_CONTENT_BEGIN, msg_content, MSG_CONTENT_END])
    mail_attachment = None
    if has_attachment:
        file_handle = open(LOG_FILENAME, "rb")
        try:
            attachment_content = file_handle.read()
        finally:
            file_handle.close()
        mail_attachment = [{'content': attachment_content, 'subject': LOG_FILENAME}]

    #邮件发送由于网络原因可能造成发送失败，所以这里要检测发送结果，如果发送失败，重发
    try_send_mail_times = 0
       
    while True:
        if send_mail(mail_subject, mail_content, mail_to_list, mail_cc_list, mail_attachment):
            break
        
        try_send_mail_times += 1
        print try_send_mail_times
        if try_send_mail_times < 6:
            time.sleep(10*60)
        else:
            raise Exception("send report to mailbox error")
    
if __name__ == '__main__':
    run(has_attachment = False) 