# -*- coding: utf-8 -*-

import base64
import email
from smtplib import SMTP
from email.MIMEText import MIMEText
from email.MIMEMultipart import MIMEMultipart
from email.mime.application import MIMEApplication
from poplib import POP3

class Mail:
    def __init__(self, smtp_server='smtp.163.com', user='shixiangwen03@163.com', password='821015'):
        self.CRLF = "\r\n"
        self.smtpServer = smtp_server
        self.user = user
        self.pwd = password
        
    def _encode2utf8(self, text):
        """判断text是否为unicode字符串，如果是，转成utf-8"""
        if(isinstance(text, unicode)):
            return text.encode("utf-8")
        return text        
        
    def send(self, sSubject, sMsg, sFrom, lsTo, lsCc=[], lsPlugin=None, subtype='html', lsBcc=[]):
        """lsPlugin是附件，格式为"""
        #encode
        sSubject = self._encode2utf8(sSubject)
        sMsg = self._encode2utf8(sMsg)
        sFrom = self._encode2utf8(sFrom)
        if lsTo:
            index = len(lsTo)
            while index > 0:
                index -= 1
                lsTo[index] = self._encode2utf8(lsTo[index])
        if lsCc:
            index = len(lsCc)
            while index > 0:
                index -= 1
                lsCc[index] = self._encode2utf8(lsCc[index])
        
        if lsBcc:
            index = len(lsBcc)
            while index < 0:
                index -= 1
                lsBcc[index] = self._encode2utf8(lsBcc[index])
        
        if lsPlugin:
            for plugin in lsPlugin:
                plugin['content'] = self._encode2utf8(plugin['content'])
                plugin['subject'] = self._encode2utf8(plugin['subject'])
        #header
        msg = MIMEMultipart()
        msg["from"] = sFrom
        msg["to"] = ','.join(lsTo)
        msg["subject"] = sSubject
        if lsCc:
            msg["cc"] = ','.join(lsCc)
        
        if lsBcc:
            msg["bcc"] = ','.join(lsBcc)
        
        #body
        body = MIMEText(sMsg, subtype, "utf-8")
        msg.attach(body)
        #attachments
        if lsPlugin:
            for plugin in lsPlugin:
                mimeFile = MIMEApplication(plugin['content'])
                mimeFile.add_header('content-disposition', 'attachment', filename=plugin['subject'])
                msg.attach(mimeFile)
        msg_all = msg.as_string()
        if lsCc:
            lsTo.extend(lsCc)
        
        if lsBcc:
            lsTo.extend(lsBcc)    
            
        server = SMTP(self.smtpServer)
        try:
            server.login(self.user, self.pwd)
            server.sendmail(sFrom, lsTo, msg_all)
        finally:
            server.quit()
        return True
    
class Receiver:
    
    def __init__(self, smtp_server='zhmail.kingsoft.com', user='PC120', password='NWm64d'):
        self.CRLF = "\r\n"
        self.smtpServer = smtp_server
        self.user = user
        self.pwd = password
        self.pop = POP3(self.smtpServer)
        self.pop.user(self.user)
        self.pop.pass_(self.pwd)
        
    def mail_list(self, limit=-1):
        '''获取邮件列表'''
        _, mails, _ = self.pop.list()
        mail_ids = [int(m.split(' ')[0]) for m in mails]
        
        emails = []
        count = 0      
        for mail_id in mail_ids:
            if limit != -1 and count > limit:
                break
            else:
                count += 1
            _, content, _ = self.pop.retr(mail_id)
            content = '\n'.join(content)
            m = email.message_from_string(content)
            headers = email.Header.decode_header(m['subject'])
            if headers[0][1]:
                subject = headers[0][0].decode(headers[0][1])
            else:
                subject = headers[0][0]
            emails.append((mail_id, subject))
        return emails

    def mail_from_list(self, limit=-1):
        '''获取邮件列表'''
        _, mails, _ = self.pop.list()
        mail_ids = [int(m.split(' ')[0]) for m in mails]
        
        emails = []
        count = 0      
        for mail_id in mail_ids:
            if limit != -1 and count > limit:
                break
            else:
                count += 1
            _, content, _ = self.pop.retr(mail_id)
            content = '\n'.join(content)
            m = email.message_from_string(content)
            mail_from = email.Header.decode_header(m['from'])
            if len(mail_from) >= 2 and len(mail_from[1]) >= 1:
                mail_from = mail_from[1][0]
                emails.append((mail_id, mail_from))
        return emails
    
    def del_mail(self, id):
        """删除某个邮件"""
        try:
            self.pop.dele(id)
            return True
        except:
            return False
        
    def get_content(self, id, _from):
        _, content, _ = self.pop.retr(id)
        content = '\n'.join(content)
        m = email.message_from_string(content)
        headers = email.Header.decode_header(m['subject'])
        content = ""
        payload = m.get_payload()
        while True:
            if isinstance(payload, str):
                content = base64.decodestring(payload)
                break;
            else:
                payload = payload[0].get_payload()
#        if _from == "kingsoft":
#            content = base64.decodestring(m.get_payload())
#        elif _from == "cnnic":
#            payload1 = m.get_payload()
#            payload2 = payload1(0).get_payload()
#            content = base64.decodestring(m.get_payload())
##            content = base64.decodestring(m.get_payload(0).get_payload(0).get_payload())
#        elif _from == "snda":
#            content = base64.decodestring(m.get_payload(0).get_payload(0).get_payload())
#        else:
#            content = ""
            
        if headers[0][1]:
            try:
                content = content.decode(headers[0][1])
            except:
                pass
        return content
    
    def get_attachs(self, id, handle_type=['txt']):
        '''获取某个文件的附件
        @param handle_type: 要处理的类型，不在该列表中的文件不处理,目前txt的附件是以字符串的形式返回,其它类型的文件可能创建临时
        文件返回路径会好点，目前没有实现 
        '''
        _, content, _ = self.pop.retr(id)
        content = '\n'.join(content)
        m = email.message_from_string(content)
        attachs = []
        for part in m.walk():
            content_type = part.get_content_type()  
            file_name = part.get_filename()
            try:
                if file_name and file_name.startswith('=?'):
                    encoding = file_name[2:file_name.find("B?")-1]
                    file_name = base64.decodestring(file_name[file_name.find('B?')+2:])
                    file_name = file_name.decode(encoding)
            except:
                pass
            
            if not file_name:
                continue
            ext_name = file_name[file_name.rfind('.')+1:]
            if ext_name not in handle_type:
                continue

            if file_name and content_type == "application/octet-stream":
                s = part.get_payload(decode=True)
                attachs.append((file_name, s))
        
        return attachs
    
    def __del__(self):
        if self.pop:
            self.pop.quit()
                        
def send_to(subject, msg, to=['shixiangwen03@gmail.com'], cc=[], attachments=[], subtype='html', bcc=[], user="shixiangwen03@163.com"):
    """发邮件给指定的开发员"""
    if isinstance(to, basestring):
        to = [to]
    m = Mail(user=user)
    sender = m.user
    return m.send(subject, msg, sender, to, cc, attachments, subtype, bcc)

if __name__ == '__main__':
    send_to("ttt", "tttt") 