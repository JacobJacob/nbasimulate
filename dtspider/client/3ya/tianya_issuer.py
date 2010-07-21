# -*- coding: utf-8 -*-
from datetime import datetime
import time

from common.db import connection
from common.net.post import Post
from business import zhuhai_info
from business import type_mapping

select_sql = 'select id, url_md5, title, content, issuer, phone, price, issue_date, effect_date, type ' + \
                      'from zhuhai_info where status <> 1 and id > %s order by id asc limit 10'

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
            print 'issue success! title:%s' % info['title']
        else:
            print 'issue failure! title:%s' % info['title']
          
        time.sleep(5)

        
def main():
    
    task_count = 0
    last_id = 0
    while True:
        
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
    print 'finish taks:%s' % task_count
        
if __name__ == '__main__':
    main()
    
    



