# -*- coding: utf-8 -*-

from smtplib import SMTP
from email.MIMEText import MIMEText
from email.MIMEMultipart import MIMEMultipart
from email.mime.application import MIMEApplication

class Mail:
    def __init__(self, smtp_server='mail.3-ya.com', user='china201@3-ya.com', password='821015'):
        self.CRLF = "\r\n"
        self.smtpServer = smtp_server
        self.user = user
        self.pwd = password
        
    def _encode2utf8(self, text):
        """判断text是否为unicode字符串，如果是，转成utf-8"""
        if(isinstance(text, unicode)):
            return text.encode("gb2312")
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
        body = MIMEText(sMsg, subtype, "gb2312")
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
    
def send_to(subject, msg, to, cc=[], attachments=[], subtype='html', bcc=[]):
    """发邮件给指定的开发员"""
    if isinstance(to, basestring):
        to = [to]
    m = Mail()
    sender = m.user
    return m.send(subject, msg, sender, to, cc, attachments, subtype, bcc)


if __name__ == '__main__':
    sender = 'info@3-ya.com'
    to = ['shixiangwen03@gmail.com']
    cc = []
    m = Mail()
    attachment = [ {'content': '文件测试', 'subject':'t1.txt'},
                   {'content': u'文件测试', 'subject':'t2.txt'}]
    
    print m.send('test', '你好\n测试换行', sender, to, cc, attachment, subtype='plain')
    print send_to('test', '你好\n测试换行', to, cc, subtype='plain')
    