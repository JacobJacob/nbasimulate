#!/usr/bin/python
# -*- coding: utf-8 -*-
import traceback

from webauth.common.db import connection
from webauth.common.db.reserve_convertor import ReserveLiteral
from webauth.common.single_process import SingleProcess
from webauth.client.base import BaseClient
from webauth.common.constants import ClientType
from webauth.common import log_execption, get_logging
from webauth.common.db.connection_wrapper import MySQLError

class DomainImporter(BaseClient):
    """从url_source表提取domain数据"""

    def __init__(self):
        super(DomainImporter, self).__init__(ClientType.DOMAIN_IMPORTER)
        self._logging = get_logging()

    def run(self):
        last_id = -1
        self.current_info = "begin"
        try:
            last_id = self._load_last_id()
            select_sql = "select id, domain, domain_md5 from url_source where id>%s order by id limit 1000"
            domain_sql = "insert into domain_info (domain, domain_md5, url_count, created_time) " \
                         + "values(%s, %s, %s, %s) " \
                         + "on duplicate key update url_count = url_count + values(url_count)"
            literal_now = ReserveLiteral("now()")
            cursor = connection.cursor()
            try:
                connection.autocommit(False)
                while True:
                    rs = cursor.fetchall(select_sql, (last_id))
                    if not rs:
                        break
                    domain_data = {}
                    for r in rs:
                        last_id = r["id"]
                        domain = r["domain"]
                        domain_md5 = r["domain_md5"]
                        if domain_data.has_key(domain_md5):
                            domain_data[domain_md5][1] += 1
                        else:
                            domain_data[domain_md5] = [domain, 1]
                    del rs
                    domain_values = []
                    for domain_md5, domain_count in domain_data.iteritems():
                        domain = domain_count[0]
                        count = domain_count[1]
                        domain_value = (domain, domain_md5, count, literal_now)
                        domain_values.append(domain_value)
                    del domain_data
                    cursor.executemany(domain_sql, domain_values)
                    self._save_last_id(cursor, last_id)
                    connection.commit()
                    del domain_values
            finally:
                connection.close()
                cursor.close()
        except MySQLError:
            s = traceback.format_exc(3)
            self.current_info = "sleep 300s." + s
            return 300
        self.current_info = "last_id: %s, sleep 600s..." % last_id
        return 600
        
    def _load_last_id(self):
        sql = "select data_content from runtime_data where client='domain_importer' and process_id=0 and data_key='last_id'"
        last_id = -1
        cursor = connection.cursor()
        try:
            rs = cursor.fetchone(sql)
            if rs:
                last_id = rs[0]
        finally:
            cursor.close()
        return last_id
    
    def _save_last_id(self, cursor, last_id):
        sql = "insert into runtime_data (client, process_id, data_key, data_content, created_time) " \
            + "values('domain_importer', 0, 'last_id', %s, now()) " \
            + "on duplicate key update data_content=values(data_content)"
        cursor.execute(sql, (last_id))

def main():
    mutex_process = SingleProcess("domain_importer_client")
    mutex_process.check()
    importer = DomainImporter()
    importer.main()
    
if __name__ == "__main__":
    main()
    