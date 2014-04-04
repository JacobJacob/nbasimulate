#-*- coding:utf-8 -*-

import os
import random
import cPickle as pickle

from xba.web.render import render_to_response, json_response
from xba.business import xbatop_manager
from xba.business import match_total_manager
from xba.model import Article
from xba.config import PathSettings
from xba.common import get_logging

logging = get_logging()


from xba.common import staticutil

def index(request):
    '''首页'''
    
    notices = Article.query(condition="status=1 and category='notice'", limit=6, order="created_time desc")

    strategys = Article.query(condition="status=1 and category='strategy'", limit=6, order="created_time desc")
    
    experiences = Article.query(condition="status=1 and category='experience'", limit=6, order="created_time desc")
    
    top_accounts = xbatop_manager.get_top10_account()

    datas = {"notices": notices, "top_accounts": top_accounts, "strategys": strategys, "experiences": experiences}
    
    response = render_to_response(request, "index.html", datas)

    #静态化    
    staticutil.dump(response, "index.html")
    
    return response


def total(request):
    '''统计'''
    user_id = request.REQUEST.get("u")
    referer = request.REQUEST.get("r")
    
    try:
        user_id = int(user_id)
    except:
        user_id = 0
    
    if user_id > 0:
        
        ip = request.META['REMOTE_ADDR']
        
        path = os.path.join(PathSettings.PROMOTION_LOG_PATH, "%s_%s_%s" % (user_id, ip, random.random()))
        f = open(path, "wb")
        try:
            pickle.dump((user_id, ip, referer), f)
        finally:
            f.close()
    
    return json_response(1)

def match_report(request):
    
    logging.debug("request")

    url = request.REQUEST.get("url")
    if url:
        type_id, match_id = match_total_manager.get_report_info(url)
        if type != 0:
            datas = match_total_manager.view_total_with_match_id(type_id, match_id)
        if not datas:
            datas = {'error_msg': u'比赛详细战况不存在'}
    else:
        datas = {'error_msg': u'请输入战报地址!!'}
    
    response = render_to_response(request, "match_report.html", datas)
    return response
    
