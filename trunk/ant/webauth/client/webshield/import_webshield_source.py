#!/usr/bin/python
# -*- coding: utf-8 -*-
"""接收网盾上报的URL"""
import os
import bsddb
import traceback

from webauth.common.single_process import SingleProcess
from webauth.client.base import BaseClient
from webauth.common.constants import ClientType
from webauth.common import file_utility
from webauth.config import PathSettings
from webauth.business.webshield_line_parser import WebshieldLineParser, WebshieldLineError, WebshieldEncodeError
from webauth.common.md5mgr import mkmd5fromstr
from webauth.client.rpc_proxy import url_source_proxy
from webauth.common import log_execption, get_logging
#from webauth.business.url_source import import_webshield_url

class ImportWebshieldUrl(BaseClient):
    """接收网盾上报的URL客户端
    http://trac.rdev.kingsoft.net/mercury/wiki/网盾数据上报
    """
    CUR_VERSION = '20090421'
    def __init__(self):
        super(ImportWebshieldUrl, self).__init__(ClientType.WEBSHIELD_URL_IMPORTER)
        self._root_dir = os.path.join(os.path.expanduser('~'), u'data/wangdun/2/2/')
        self._db_path = os.path.join(PathSettings.WORKING_FOLDER, "import_webshield_url.db")
        self._logging = get_logging()

    def run(self):
        """轮询网盾上报目录,将URL入库"""
        last_folder = None
        self.current_info = "begin"
        try:
            error_count = 0
            self._handled_url = set()
            date_dirs = os.listdir(self._root_dir)
            date_dirs.sort()
            date_dirs_count = len(date_dirs)
            db = bsddb.btopen(self._db_path)
            try:
                for i in range(date_dirs_count):
                    date_dir = os.path.join(self._root_dir, date_dirs[i])
                    encoded_date_dir = date_dir.encode("utf-8")
                    if db.has_key(encoded_date_dir) or not os.path.isdir(date_dir):
                        continue
                    minute_dirs = os.listdir(date_dir)
                    minute_dirs.sort()
                    minute_dirs_count = len(minute_dirs)
                    for j in range(minute_dirs_count):
                        minute_dir = os.path.join(date_dir, minute_dirs[j])
                        encoded_minute_dir = minute_dir.encode("utf-8")
                        if (i >= date_dirs_count - 1 and j >= minute_dirs_count - 1) \
                           or db.has_key(encoded_minute_dir) \
                           or not os.path.isdir(minute_dir):
                            continue
                        last_folder = minute_dir
                        self.current_info = minute_dir
                        
                        walker = file_utility.walk(minute_dir)
                        for top, dirs, files in walker:
                            del dirs
                            for each_file in files:
                                file_path = os.path.join(top, each_file)
                                encoded_file_path = file_path.encode("utf-8")
                                if not db.has_key(encoded_file_path):
                                    try:
                                        file_handler = open(file_path, "rb")
                                    except IOError, e:
                                        if e.errno == 2: #文件不存在
                                            continue
                                        raise
                                    datas = []
                                    try:
                                        for line in file_handler:
                                            try:
                                                data = self._parse_line(line)
                                                if data:
                                                    datas.append(data)
                                            except (WebshieldLineError, ValueError):
                                                if error_count % 100 == 0:
                                                    log_execption()
                                                error_count += 1
                                            if len(datas) > 100:
                                                url_source_proxy.import_webshield_url(datas)
                                                datas = []
                                        if datas:
                                            url_source_proxy.import_webshield_url(datas)
                                            datas = []
                                    finally:
                                        file_handler.close()
                                    db[encoded_file_path] = "f"
                                    db.sync()
                        db[encoded_minute_dir] = "m"
                        db.sync()
                    if i < date_dirs_count - 1:
                        db[encoded_date_dir] = "d"
                        db.sync()
            finally:
                self._handled_url = None
                db.close()
        except:
            log_execption()
            s = traceback.format_exc()
            self.current_info = "sleep 300s." + s
            return 300
        self.current_info = "last_folder: %s, sleep 600s..." % repr(last_folder)
        return 600
    
    def _parse_line(self, line):
        """将网盾数据行导入到数据表webshield_url和url_source中
        line:网盾日志的数据行
        返回值:没有返回值"""
        try:
            line = line.decode("gbk").encode("utf-8")
        except:
            raise WebshieldEncodeError(line)
        if line.count("[") != 5 or line.count("]") != 5:
            return None
        parser = WebshieldLineParser(line)
        reason = parser.get_reason()
        if reason is None:
            return None
        version = parser.get_version()
        if version >= self.CUR_VERSION:
            if reason & 0x80000000:
                return None
        elif reason in (0x11, 0x12, 0x14):
            return None
        url = parser.get_url()
        host = parser.get_host()
        if host.endswith("duba.net"):
            return None
        url_md5 = mkmd5fromstr(url)
        if url_md5 in self._handled_url:
            return None
        host_md5 = mkmd5fromstr(host)
        report_time = parser.get_time()
        client_program = parser.get_program()
        group_id = parser.get_group_id()
        data = {
                "url": url,
                "url_md5": url_md5,
                "host": host,
                "host_md5": host_md5,
                "report_time": report_time,
                "client_program": client_program,
                "webshield_version": version,
                "reason": reason,
                "group_id": group_id,
                "raw_data": line,
                }
#        import_webshield_url(data)
#        url_source_proxy.import_webshield_url(data)
        self._handled_url.add(url_md5)
        return data

def main():
    mutex_process = SingleProcess("import_webshield_source_client")
    mutex_process.check()
    client = ImportWebshieldUrl()
    client.main()

if __name__ == '__main__':
    main()
