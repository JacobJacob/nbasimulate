# -*- coding: utf-8 -*-
from datetime import datetime
import traceback
import time

from dtspider.common import md5mgr
from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral
from dtspider.common.single_process import SingleProcess
from dtspider.business import runtime_data
from dtspider.common.constants import ClientType
from dtspider.client.base import BaseClient

class ZhuhaiEmailImporter(BaseClient):
    
    def __init__(self, round_time=5):
        super(ZhuhaiEmailImporter, self).__init__(ClientType.EMAIL_IMPORTER)
        self._round_time = round_time # 爬取一轮的时间间隔
        self._last_id = 0
        
    def run(self):
        
        self._load_status()
        email_infos = self._fetch_emial()
        if not email_infos:
            now = datetime.now()
            self.current_info = "%s: sleep 60s..." % now.strftime("%Y-%m-%d %H:%M:%S")
            return 60
        
        now = datetime.now()
        self.current_info = "%s:get %s emails" % (now.strftime("%Y-%m-%d %H:%M:%S"), len(email_infos))
        self._last_id = email_infos[-1]['id']
        for email_info in email_infos:
            del email_info['id']
            email_info['created_at'] = ReserveLiteral('now()')
            email_info['email_md5'] = md5mgr.mkmd5fromstr(email_info['email'])
            email_info['from_type'] = 1
            now = datetime.now()
            self.current_info = "%s:email:%s" % (now.strftime("%Y-%m-%d %H:%M:%S"), email_info['email'])
            self._save_email(email_info)
        
        self._save_status()
        now = datetime.now()
        self.current_info = "%s: sleep 5s..." % now.strftime("%Y-%m-%d %H:%M:%S")
        return self._round_time
    
    def _fetch_emial(self):
        """fetch email from zhuhai info table"""
        sql = "select id, email from zhuhai_info where email is not null and " + \
                  "email like '%s' and id > %s limit 100" % ('%@%', self._last_id)
        while True:
            cursor = connection.cursor()
            try:
                data = cursor.fetchall(sql)
                if data:
                    return data.to_list()
                else:
                    return []
            except:
                self.current_info = u'%s' % traceback.format_exc()
            finally:
                cursor.close()
            self._sleep()
   
    def _save_email(self, email_info):
        """save the email"""
        while True:
            cursor = connection.cursor()
            try:
                cursor.insert(email_info, 'ads_email', True, ['created_at'])
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
            finally:
                cursor.close()
            self._sleep()
            
    def _load_status(self):
        """load status"""
        while True:
            try:
                data = runtime_data.load_runtime_data(self.__class__.__name__, 'last_id')
                if data:
                    self._last_id = int(data['value'])
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
            self._sleep()
    
    def _save_status(self):
        """save status"""
        while True:
            try:
                runtime_data.save_runtime_data(self.__class__.__name__, 'last_id', self._last_id)
                break
            except:
                self.current_info = u'%s' % traceback.format_exc()
            self._sleep()
def main():
    importer = ZhuhaiEmailImporter()
    importer.main()
        
if __name__ == '__main__':
    main()
    
    



