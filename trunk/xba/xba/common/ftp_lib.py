#!/usr/bin/env python
# -*- coding: utf-8 -*-

import os
from threading import Lock
import ftplib
import urllib
from datetime import datetime
import shutil
import socket
from cStringIO import StringIO

from xba.config import PathSettings
from xba.common import file_utility
from xba.common.md5mgr import mkmd5fromstr, AsyncMD5

class FtpEx(ftplib.FTP):
    SplitStr = "==perfectSplit=="
    Locker = Lock()
    ConfigFolder = os.path.join(PathSettings.WORKING_FOLDER, "ftp_config")
    def __init__(self, host='', user='', passwd='', acct=''):
        self.timeout = 60 #None to use socket default
        socket.setdefaulttimeout(self.timeout)
        self.__connected = False
        self.__fileList = []
        self.user = user
        self.password = passwd
        self.account = acct
        self.beforeDownload = None
        ftplib.FTP.__init__(self, host, user, passwd, acct)
        self.blocksize = 1024 ** 2

    def connect(self, host = '', port = 0):
        result = ftplib.FTP.connect(self, host, port)
        if self.timeout != None:
            self.sock.settimeout(self.timeout)
        self.__connected = True
        return result
    
    def login(self, user = '', passwd = '', acct = ''):
        self.user = user
        self.password = passwd
        self.account = acct
        return ftplib.FTP.login(self, user, passwd, acct)

    def ensure_login(self):
        if not self._connected():
            self.close()
            try:
                self.connect()
                self.login(self.user, self.password, self.account)
            except:
                self.close()
                raise
    
    def close(self):
        ftplib.FTP.close(self)
        self.__connected = False

    def download(self, remote_path, local_path, resume = True):
        if local_path[-1] in ("/", "\\"):
            local_folder = local_path
            file_name = os.path.basename(remote_path)
            local_file_path = os.path.join(local_folder, file_name)
        else:
            local_file_path = local_path
            local_folder = os.path.dirname(local_file_path)
        if not os.path.exists(local_file_path):
            file_utility.ensure_dir_exists(local_folder)
            tmp = local_file_path + "._tf"
            rest = 0
            if resume and os.path.exists(tmp):
                rest = os.path.getsize(tmp)
            if resume:
                file_obj = open(tmp, "ab")
            else:
                file_obj = open(tmp, "wb")
            try:
                self.retrbinary('RETR ' + remote_path, file_obj.write, self.blocksize, rest)
                file_obj.close()
                os.rename(tmp, local_file_path)
            except:
                if not resume and os.path.exists(tmp):
                    file_obj.close()
                    os.remove(tmp)
                raise
            finally:
                file_obj.close()
        return local_file_path
    
    def exists(self, remote_path):
        """判断文件是否存在"""
        try:
            r = self.size(remote_path)
            if r:
                return True
        except:
            pass
        return False
    
    def check_md5(self, remote_path, md5):
        fp = AsyncMD5()
        self.retrbinary('RETR ' + remote_path, fp.write, self.blocksize)
        return fp.get_md5() == md5

    def upload(self, local_path, remote_path = ''):
        is_stream = hasattr(local_path, "close")
        remote_folder = ""
        if remote_path:
            if remote_path[-1] == "/":
                if is_stream:
                    raise Exception("remote_path is required if local_path is a stream")
                remote_folder = remote_path
                filename = os.path.basename(local_path)
                remote_path = remote_folder + filename
            else:
                remote_folder = os.path.dirname(remote_path)
        else:
            if is_stream:
                raise Exception("remote_path is required if local_path is a stream")
            remote_path = os.path.basename(local_path)
            
        if is_stream:
            stream = local_path            
        else:
            stream = open(local_path, "rb")
        try:
            try:
                self.storbinary("STOR " + remote_path, stream, self.blocksize)
            except ftplib.error_perm, e:
                if remote_folder and e.args[0][:3] in ("553", "550"):
                    self.mkd(remote_folder)
                    stream.seek(0)
                    self.storbinary("STOR " + remote_path, stream, self.blocksize)
                else:
                    raise
        finally:
            if not is_stream:
                stream.close()
        return remote_path
    
    def create_file(self, remote_path):
        sio = StringIO("")
        try:
            self.upload(sio, remote_path)
        finally:
            sio.close()
    
    def mkd(self, dirname):
        try:            
            ftplib.FTP.mkd(self, dirname)
        except ftplib.error_perm, e:
            if e.args[0][:3] == "550":
                cwd = self.pwd()
                try:
                    folders = dirname.split('/')
                    if dirname[0] == '/':
                        self.cwd('/')
                    for folder in folders:
                        if folder:
                            try:
                                if not folder in ('..', '.'):
                                    ftplib.FTP.mkd(self, folder)
                            except ftplib.error_perm, e1:
                                if e1.args[0][:3] != "550":
                                    raise
                            self.cwd(folder)
                finally:
                    self.cwd(cwd)
            else:
                raise

    def get_folders(self, baseDir = ""):
        return self.get_entries(baseDir)[1]

    def get_files(self, baseDir = ""):
        return self.get_entries(baseDir)[0]

    def get_entries(self, baseDir):
        files = []
        folders = []
        list = []
        try:
            entries = self.nlst(baseDir)
        except ftplib.error_perm, e:
            if e.args[0][:3] == "550":
                self.dir(baseDir, list.append)
                if len(list) == 2:
                    if list[0][-2:] == ' .' and list[1][-3:] == ' ..':
                        return [], []
                elif len(list) == 0:
                    return [], []
            raise
        oldDir = self.pwd()
        try:
            self.cwd(baseDir)
            self.dir("", list.append)
        finally:
            self.cwd(oldDir)
        if list:
            for eachEntry in entries:
                eachEntry = eachEntry.replace("//", "/").replace("\\", "/")
                isFile = False
                matched = False
                if eachEntry not in ('.', '..'):
                    entryName = os.path.basename(eachEntry)
                    index = -1 * len(entryName)
                    for eachList in list:
                        if eachList[index:] == entryName:
                            matched = True
                            if eachList[:1] == "-" \
                               or (eachList[:1] != 'd' and eachList.find(' <DIR> ') == -1):
                                isFile = True
                                break
                if matched:
                    if entryName == eachEntry:
                        if baseDir[-1:] != "/":
                            baseDir += "/"
                        eachEntry = baseDir + eachEntry
                    if isFile:
                        files.append(eachEntry)
                    else:
                        folders.append(eachEntry)
        return files, folders

    def get_mtime(self, filePath):
        s = self.sendcmd("MDTM %s" % filePath)
        if len(s) != 18:
            raise ftplib.error_temp, s
        s = s[4:]
        year = int(s[:4])
        month = int(s[4:6])
        day = int(s[6:8])
        hour = int(s[8:10])
        minute = int(s[10:12])
        second = int(s[12:])
        n = datetime(year, month, day, hour, minute, second)
        return n
    
    def walk(self, baseDir, sort = False):
        files, folders = self.get_entries(baseDir)
        if sort:
            files.sort()
            folders.sort()
        yield baseDir, folders, files
        self.ensure_login()
        for each_folder in folders:
            for entry in self.walk(each_folder, sort):
                yield entry
    
    def is_windows(self):
        s = self.sendcmd("SYST")
        if s:
            s = s.lower()
            return s.find("windows") >= 0
        else:
            return False

    @classmethod
    def delete_local(cls, filePath):
        config_path = cls._get_config_path(filePath)
        if os.path.exists(filePath):
            os.remove(filePath)
        if os.path.exists(config_path):
            os.remove(config_path)
    
    @classmethod
    def move_local(cls, srcFilePath, destFilePath):
        shutil.move(srcFilePath, destFilePath)
        srcConfigPath = cls._get_config_path(srcFilePath)
        if os.path.exists(srcConfigPath):
            local_path, ftpInfo = cls._read_config_path(srcConfigPath)
            destConfigPath = cls._get_config_path(destFilePath)
            cls._write_config(destConfigPath, 
                                     ftpInfo.host, 
                                     ftpInfo.port,
                                     ftpInfo.user, 
                                     ftpInfo.password, 
                                     ftpInfo.path, 
                                     local_path)
    
    @classmethod
    def resume_download(cls, local_path, beforeDownload = None):
        config_path = cls._get_config_path(local_path)
        if not os.path.exists(config_path) and local_path[-4:] == "._tf":
            config_path = cls._get_config_path(local_path[:-4])
        if os.path.exists(config_path):
            localPath, ftpInfo = cls._read_config_path(config_path)
            ftp = cls()
            ftp.host = ftpInfo.host
            ftp.user = ftpInfo.user
            ftp.password = ftpInfo.password
            ftp.port = ftpInfo.port
            ftp.beforeDownload = beforeDownload
            ftp.ensure_login()
            try:
                local_folder = os.path.dirname(localPath)
                return ftpInfo, ftp.download(ftpInfo.path, local_folder)
            finally:
                ftp.close()
        return None, None
    
    @classmethod
    def read_ftp_info(cls, local_path):
        config_path = cls._get_config_path(local_path)
        if not os.path.exists(config_path) and local_path[-4:] == "._tf":
            config_path = cls._get_config_path(local_path[:-4])
        if os.path.exists(config_path):
            ftpInfo = cls._read_config_path(config_path)[1]
            return ftpInfo
        return None
    
    def _connected(self):
        result = self.__connected
        if result:
            try:
                self.pwd()
                result = True
            except:
                result = False
        return result
    
    def _get_unique_tmppath(self, sourcePath, folder):
        """获取目录下唯一的临时文件名"""
        fileName = os.path.basename(sourcePath)
        if folder:
            fullPath = os.path.join(folder, fileName)
        else:
            fullPath = fileName
        pathSegments = os.path.splitext(fullPath)
        i = 1
        is_downloaded = False
        isResume = False
        while os.path.exists(fullPath) or os.path.exists(fullPath + "._tf"):
            isResume = self._is_resume(sourcePath, fullPath)
            if isResume:
                if os.path.exists(fullPath):
                    is_downloaded = True
                break
            pathName = pathSegments[0] + str(i)
            fullPath = pathName + pathSegments[1]
            i = i + 1
        config_path = FtpEx._get_config_path(fullPath)
        if not is_downloaded:
            fullPath = fullPath + "._tf"
        return is_downloaded, fullPath, config_path
    
    def _is_resume(self, sourcePath, fullPath):
        config_path = FtpEx._get_config_path(fullPath)
        if os.path.exists(config_path):
            localPath, ftpInfo = FtpEx._read_config_path(config_path)
            resume = (self.host == ftpInfo.host \
                     and ftpInfo.path == sourcePath \
                     and localPath == fullPath)
            return resume
        return False
    
    @classmethod
    def _get_config_path(cls, local_path):
        md5 = mkmd5fromstr(repr(local_path))
        config_path = os.path.join(FtpEx.ConfigFolder, md5[:2], md5 + ".cfg")
        return config_path
    
    @classmethod
    def _write_config(cls, config_path, host, port, user, password, remote_path, local_path):
        file_utility.ensure_dir_exists(os.path.dirname(config_path))
        configFile = file(config_path, 'w')
        try:
            configStr = "%s%s" % (host, cls.SplitStr)
            configStr += remote_path + os.linesep
            configStr += local_path[0:-4] + os.linesep
            userStr = user
            if not userStr: userStr = ""
            pwdStr = password
            if not pwdStr: pwdStr = ""
            configStr += userStr + os.linesep
            configStr += pwdStr + os.linesep
            configStr += str(port) + os.linesep
            configFile.write(configStr)
        finally:
            configFile.close()
    
    @classmethod
    def _read_config_path(cls, local_path):
        f = file(local_path, "r")
        try:
            ftpInfo = FtpInfo()
            serverInfo = f.readline().strip().split(cls.SplitStr)
            serverInfo[1] = serverInfo[1]
            ftpInfo.host = serverInfo[0]
            ftpInfo.path = serverInfo[1]
            localPath = f.readline().strip()
            ftpInfo.user = f.readline().strip()
            ftpInfo.password = f.readline().strip()
            ftpInfo.port = f.readline().strip()
            if not ftpInfo.port:
                ftpInfo.port = 21
            else:
                ftpInfo.port = int(ftpInfo.port)
        finally:
            if f: f.close()
        return localPath, ftpInfo
    
    def is_folder(self, f, baseDir = ""):
        result = True
        currentDir = self.pwd()
        if baseDir:
            f = os.path.join(baseDir, f)
            f = f.replace("\\", "/")
        try:
            try:
                self.cwd(f)
            except ftplib.error_perm, e:
                if e.args[0][:3] != "550":#no such file or directory
                    raise
                result = False
            else:
                result = True
        finally:
            self.cwd(currentDir)
        return result
    
    def __del__(self):
        try:
            self.close()
        except:
            pass
    
    def size(self, filename):
        '''Retrieve the size of a file.
        Fixed: 550 SIZE not allowed in ASCII mode.
        '''
        self.voidcmd('TYPE I')
        return ftplib.FTP.size(self, filename)
    

class FtpInfo:
    def __init__(self, ftpUrl = None, user = None, password = None):
        self.host = ""
        self.port = ftplib.FTP_PORT
        self.path = ""
        self.user = user
        self.password = password
        if not ftpUrl:
            return
        if len(ftpUrl) > 4 and ftpUrl[:4].lower() == "ftp:":
            ftpUrl = ftpUrl[4:]
        #get host, path
        if ftpUrl.startswith("//"):
            self.host, self.path = urllib.splithost(ftpUrl)
            if self.path:
                self.path = self.path[1:]
        else:
            firstSlash = ftpUrl.find("/")
            if firstSlash > 0:
                self.host = ftpUrl[:firstSlash]
                self.path = ftpUrl[firstSlash + 1:]
            else:
                raise Exception("Error url")
        #get port
        colonIndex = self.host.rfind(":")
        if colonIndex > 0:
            portStr = self.host[colonIndex + 1:]
            if portStr:
                self.port = int(portStr)
        if self.path and self.path[:1] != "/":
            self.path = "/" + self.path