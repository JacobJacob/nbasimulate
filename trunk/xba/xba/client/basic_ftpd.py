#!/usr/bin/python
# -*- coding: utf-8 -*-
"""A basic FTP server which uses a DummyAuthorizer for managing 'virtual
users', setting a limit for incoming connections.
"""
import os
if os.name != 'nt':
    import pwd

import time

from xba.common.pyftplib import ftpserver
from xba.common.single_process import SingleProcess
from xba.common.exception_mgr import on_except
from xba.common import file_utility
from xba.config import PathSettings
from xba.common.mailutil import send_to


class UnixAuthorizer(ftpserver.DummyAuthorizer):

    def __init__(self):
        ftpserver.DummyAuthorizer.__init__(self)
        # the uid/gid the daemon runs under
        self.PROCESS_UID = os.getuid()
        self.PROCESS_GID = os.getgid()
    
    def impersonate_user(self, username, password):
        uid = pwd.getpwnam('mercury').pw_uid
        gid = pwd.getpwnam('mercury').pw_gid
        os.setegid(gid)
        os.seteuid(uid)

    def terminate_impersonation(self):
        os.setegid(self.PROCESS_GID)
        os.seteuid(self.PROCESS_UID)



def main():
    # Instantiate a dummy authorizer for managing 'virtual' users
    authorizer = ftpserver.DummyAuthorizer()
#    if DEBUG:
#        authorizer = UnixAuthorizer()

    # Define a new user having full r/w permissions and a read-only
    # anonymous user
#    authorizer.add_user('mercuryrevert', 'mer@c1u2r3yrevert', os.getcwd(), perm='elradfmw')
    path = os.path.join(PathSettings.WORKING_FOLDER, "ftp_root")
    file_utility.ensure_dir_exists(path)
    authorizer.add_user('xbaftp', 'xbaftp123', path, perm='elradfmw')
    #    authorizer.add_anonymous('f:/ftp/soft')

    # Instantiate FTP handler class
    ftp_handler = ftpserver.FTPHandler
    ftp_handler.authorizer = authorizer

    # Define a customized banner (string returned when client connects)
    ftp_handler.banner = "pyftpdlib %s based ftpd ready." %ftpserver.__ver__

    # Specify a masquerade address and the range of ports to use for
    # passive connections.  Decomment in case you're behind a NAT.
    #ftp_handler.masquerade_address = '151.25.42.11'
    #ftp_handler.passive_ports = range(60000, 65535)

    # Instantiate FTP server class and listen to 0.0.0.0:21
    address = ('0.0.0.0', 21)
    ftpd = ftpserver.FTPServer(address, ftp_handler)

    # set a limit for connections
    ftpd.max_cons = 256
#    ftpd.max_cons_per_ip = 5

    # start ftp server
    ftpd.serve_forever()

if __name__ == '__main__':
    s = SingleProcess('xba ftp server')
    s.check()
    send_count = 0
    while True: # 防止未知异常死掉
        try:
            main()
        except KeyboardInterrupt:
            raise
        except:
            send_count += 1
            if send_count <= 5:
                msg = on_except(track_index=1)
                send_to('ftpd异常!', msg, subtype='plain')
                time.sleep(10)
            else:
                msg = on_except(track_index=1)
                time.sleep(60)