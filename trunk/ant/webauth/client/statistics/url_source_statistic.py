#!/usr/bin/python
# -*- coding: utf-8 -*-
"""分类型、时间统计URL来源"""
from datetime import timedelta
import traceback

from webauth.business import url_statistic
from webauth.common.single_process import SingleProcess
from webauth.client.base import BaseClient
from webauth.common.constants import ClientType
from webauth.common import log_execption, get_logging
from webauth.common.db.connection_wrapper import MySQLError

class UrlSourceStatistic(BaseClient):
    def __init__(self):
        super(UrlSourceStatistic, self).__init__(ClientType.URL_SOURCE_STATISTIC)
        self._logging = get_logging()
    
    def run(self):
        self.current_info = "begin"
        try:
            self._count_url_source()
            self._count_black_url()
        except MySQLError:
            s = traceback.format_exc(3)
            self.current_info = "sleep 300s." + s
            return 300
        self.current_info = "sleep 1 hour..."
        return 3600
    
    def _count_url_source(self):
        self.current_info = "count_url_source"
        begin_hour = url_statistic.get_url_source_begin()
        self.current_info = "begin_hour %s" % begin_hour
        if not begin_hour:
            return
        stop_hour = url_statistic.get_url_source_stop()
        self.current_info = "stop_hour %s" % stop_hour
        while begin_hour < stop_hour:
            self.current_info = "begin_hour %s..." % begin_hour
            url_statistic.add_total_url_count(begin_hour)
            begin_hour += timedelta(hours=1)
    
    def _count_black_url(self):
        self.current_info = "count_black_url"
        url_statistic.count_black_url()

def main():
    mutex_process = SingleProcess("url_source_statistic_client")
    mutex_process.check()
    runner = UrlSourceStatistic()
    runner.main()
    
if __name__ == "__main__":
    main()
    