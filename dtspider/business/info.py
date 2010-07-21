
from dtspider.common.db import connection
from dtspider.common.db.reserve_convertor import ReserveLiteral

def save_info(info):
    info['created_at'] = ReserveLiteral('now()')
    cursor = connection.cursor()
    try:
        return cursor.insert(info, 'info', True, ['create_at'])
    finally:
        cursor.close()
        
