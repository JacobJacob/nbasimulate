#!/usr/bin/python
# -*- coding: utf-8 -*-
"""S8 MSN bot"""

import socket
import select
import urllib
import time
import email as Email

from webauth.common import msnlib
from webauth.common import msncb
from webauth.client.rpc_proxy import client_service_proxy


class MSNBot(object):
    """msn机器人
    提供关机功能
    """
    
    def __init__(self, email, pwd, status='online'):
        self._msn = msnlib.msnd()
        self._msn.cb = msncb.cb()
        self._msn.email = email
        self._msn.pwd = pwd
        self._msn.cb.add = self._cb_add
        self._msn.cb.msg = self._cb_msg
        self.status = status
        self._auth_users = {}
        self._password = 'webauth.123'
        
        self.CMD = {
            'help': self._help,
            'auth': self._auth,
            'reboot': self._reboot,
        }
        
    def login(self):
        print "Logging In"
        self._msn.login()

        print "Sync"
        # this makes the server send you the contact list, and it's recommended that
        # you do it because you can get in trouble when getting certain events from
        # people that are not on your list; and it's not that expensive anyway
        self._msn.sync()
        
        local_ip = socket.gethostbyname(socket.gethostname()) # 这个得到本地ip
        self._msn.change_nick('webauth %s' % local_ip)

        print "Changing Status"
        # any non-offline status will do, otherwise we'll get an error from msn when
        # sending a message
        self._msn.change_status(self.status)

    def quit(self):
        try:
            self._msn.disconnect()
        except:
            pass
        print "Exit"
    
    HELP = """
help 帮助
auth pwd 验证
reboot 重启
"""

    def _help(self, email, params=None):
        if email not in self._auth_users:
            self._msn.sendmsg(email, 'Auth first!')
        else:
            self._msn.sendmsg(email, self.HELP)
    
    def _auth(self, email, params):
        if params and params[0] == self._password:
            self._auth_users[email] = True
            self._msn.sendmsg(email, 'Auth Success!!!\r\n%s' % self.HELP)
        else:
            self._msn.sendmsg(email, 'Auth Fail!!!')
            self._help(email)
            
    def _reboot(self, email, params):
        from webauth.common.process import reboot
        self._msn.sendmsg(email, 'Bye, I\'m going to die.')
        reboot()
    
    def _execute(self, email, cmds):
        if not cmds or (email not in self._auth_users and cmds[0] != 'auth'):
            self._help(email)
        else:
            if cmds[0] not in self.CMD:
                self._msn.sendmsg(email, '%s not found.' % cmds[0])
                self._help(email)
            else:
                cmd = self.CMD[cmds[0]]
                cmd(email, cmds[1:])
    
    def _cb_msg(self, md, type, tid, params, sbd):
        """Get a message"""
        mime_message = Email.message_from_string(params)
        if mime_message.get_content_type() == 'text/plain':
            print `tid`
            email = tid.split(' ')[0]
            print 'charset', mime_message.get_content_charset()
            msg = mime_message.get_payload().strip()
            print msg
            cmds = msg.split(' ')
            self._execute(email, cmds)
#        elif mime_message.get_content_type() == 'text/x-msmsgscontrol':
#            print 'typing...'

    def _cb_add(self, md, type, tid, params):
        "Handles a user add; both you adding a user and a user adding you"
        t = params.split(' ')
        type = t[0]
        if type == 'RL':
            email = t[2]
            nick = urllib.unquote(t[3])
            msnlib.debug('ADD: %s (%s) added you, %s' % (nick, email, params))
#            # 自动添加为好友
            self._msn.useradd(email)
            
        elif type == 'FL':
            email = t[2]
            nick = urllib.unquote(t[3])
            gid = t[4]
            md.users[email] = msnlib.user(email, nick, gid)
            # put None in last_lst so BPRs know it's not coming from sync
            md._last_lst = None
            msnlib.debug('ADD: adding %s (%s), %s' % (email, nick, params))
        else:
            pass
        
    def _ensure_login(self):
        while True:
            try:
                self.login()
                break
            except Exception, e:
                print e

    def run(self):
        # we loop over the network socket to get events
        self._ensure_login()
        while True:
            try:
                # we get pollable fds
                t = self._msn.pollable()
                infd = t[0]
                outfd = t[1]
            
                # we select, waiting for events
                try:
                    fds = select.select(infd, outfd, [], 0)
                except:
                    self.quit()
                    self.login()
                    continue
                
                for i in fds[0] + fds[1]:       # see msnlib.msnd.pollable.__doc__
                    try:
                        self._msn.read(i)
                    except ('SocketError', socket.error), err:
                        if i != self._msn:
                            # user closed a connection
                            # note that messages can be lost here
                            self._msn.close(i)
                        else:
                            # main socket closed
                            self.quit()
                            self.login()
            except KeyboardInterrupt:
                self.quit()
                break
            except Exception, e:
                print e
                self.quit()
                self._ensure_login()
            # sleep a bit so we don't take over the cpu
            time.sleep(0.01)
            
if __name__ == '__main__':
    data = client_service_proxy.get_msn_account()
    print data
    account = data['result']
    msn = MSNBot(account['email'], account['password'])
#    msn = MSNBot('webauth2@hotmail.com', 'web.123auth')
    msn.run()