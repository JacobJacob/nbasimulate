# -*- coding: utf-8 -*-

from smtplib import SMTP
from email.MIMEText import MIMEText
from email.MIMEMultipart import MIMEMultipart
from email.mime.application import MIMEApplication

class Mail:
    def __init__(self, sSmtpServer, sUser, sPwd):
        self.CRLF = "\r\n"
        self.smtpServer = sSmtpServer
        self.user = sUser
        self.pwd = sPwd
        
    def _encode2utf8(self, text):
        """判断text是否为unicode字符串，如果是，转成utf-8"""
        if(isinstance(text, unicode)):
            return text.encode("utf-8")
        return text        
        
    def send(self, sSubject, sMsg, sFrom, lsTo, lsCc=[],lsPlugin=None):
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
        #body
        body = MIMEText(sMsg, 'plain')
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
        server = SMTP(self.smtpServer)
        try:
            server.sendmail(sFrom, lsTo, msg_all)
        finally:
            server.quit()
        return True
    
def send_to(subject, msg, to=['yuanfeng@kingsoft.com', 'wangbin2@kingsoft.com'], 
            cc=[], attachments=[]):
    """发邮件给指定的开发员"""
    sender = 'webauth_report@kingsoft.com'
    if isinstance(to, basestring):
        to = [to]
    m = Mail("bjmail.kingsoft.com", sender, '8Q2xT3Pt')
    return m.send(subject, msg, sender, to, cc, attachments)


if __name__ == '__main__':
    sender = 'gateway_datasync_report@kingsoft.com'
    to = ['yuanfeng@kingsoft.com']
    cc = []
    m = Mail("bjmail.kingsoft.com", sender, '8Q2xT3Pt2121')
    attachment = [ {'content': '文件测试', 'subject':'t1.txt'},
                   {'content': u'文件测试', 'subject':'t2.txt'}]
    
    print m.send('test', '你好', sender, to, cc, attachment)
    