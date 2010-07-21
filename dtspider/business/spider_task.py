#-*- coding:utf-8 -*-
from dtspider.common.db import connection

_SELECT_SPIDER_TASK = 'select id, task_name, start_url, is_content_page, url_pattern ' + \
                     'created_at from spider_task limit %s, %s'
                     
_SELECT_SPIDER_TASK_COUNT = 'select count(*) from spider_task'
                     
def get_spider_tasks(page, pagesize):
    """get the spider tasks"""
    cursor = connection.cursor()
    if page <= 0:
        page = 1
    index = (page - 1) * pagesize
    try:
        rs = cursor.fetchall(_SELECT_SPIDER_TASK, (index, pagesize))
        if rs:
            count = cursor.fetchone(_SELECT_SPIDER_TASK_COUNT)[0]
            return rs.to_list(), count
        else:
            return [], 0
    finally:
        cursor.close()