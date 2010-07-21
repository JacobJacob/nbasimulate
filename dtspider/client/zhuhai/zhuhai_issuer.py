# -*- coding: utf-8 -*-
from datetime import datetime
import time

from dtspider.common.db import connection
from dtspider.common.net.post import Post
from dtspider.common.single_process import SingleProcess
from dtspider.business import zhuhai_info
from dtspider.business import type_mapping
from dtspider.business import runtime_data

select_sql = 'select id, url_md5, title, content, issuer, phone, price, issue_date, effect_date, type ' + \
                      'from zhuhai_info where status <> 1 and to_days(updated_at)>= to_days(now()) and id > %s order by id asc limit 10'

import sys
reload(sys)
sys.setdefaultencoding('utf-8')

def issue(infos):
    
    for info in infos:
        data = {}
        if not info['title']:
            continue
        
        type = info['type']
        
        #得到类别与我们网站中类别的映射，如找不到则返回
        mapping_info = type_mapping.get_type_mapping_value(type, 'ZH-ZH')
        if not mapping_info or not mapping_info['type_mapping']:
            print 'not type mapping'
            continue
        else:
            data['fid'] = mapping_info['type_mapping']
       
        data['postdb[title]'] = info['title'].encode('gb2312', 'ignore')
        data['postdb[content]'] = info['content'].encode('gb2312', 'ignore')
        if len(info['phone']) == 11:
            data['postdb[mobphone]'] = info['phone']
        else:
            data['postdb[telephone]'] = info['phone']
        
        data['postdb[my_price]'] = info['price'] if info['price'] != -1 else '0'
        data['postdb[city_id]'] = '1'
        data['id'] = '0'
        data['postdb[username]'] = info['issuer'].encode('gb2312', 'ignore')
        
        now = datetime.now()
        issue_date = info['issue_date']
        if now.year == issue_date.year and now.month == issue_date.month \
            and now.day == issue_date.day:
            effect_date = info['effect_date']
            diff_effect = effect_date - issue_date
            data['postdb[showday]'] = diff_effect.days
        else:
            continue
        
        post = Post(data, 'jackyshi.vip4.es163.net', '/post.php?action=postnew')
        
        success = post.submit()
        if success:
            info['status'] = 1
            zhuhai_info.save_info(info)
            print u'issue success! title:%s' % info['title']
        else:
            pass
            print u'issue failure! title:%s' % info['title']
          
        time.sleep(5)

        
def main():
    
    task_count = 0

    while True:
        
        data = runtime_data.load_runtime_data('zhuhai_issuer', "id")
        if data:
            last_id = int(data['value'])
        else:
            last_id = 0
        
        cursor = connection.cursor()
        try:
            rs = cursor.fetchall(select_sql % last_id)
            if not rs:
                break
        except Exception, e:
            print e
        finally:
            cursor.close()
    
        infos = rs.to_list()
        last_id = infos[-1]['id']
        task_count += len(infos)
        issue(infos)
        
        data_info = {'programe': 'zhuhai_issuer', 'value_key': 'id', 'value': last_id}
        runtime_data.insert_runtime_data(data_info)
        
    print 'finish taks:%s' % task_count
        
if __name__ == '__main__':
    single = SingleProcess('zhuhai issuer')
    single.check()
    main()
    
    



