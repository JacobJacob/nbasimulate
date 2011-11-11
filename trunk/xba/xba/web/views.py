#-*- coding:utf-8 -*-

from xba.web.render import render_to_response, json_response
from xba.business import xbatop_manager, account_manager
from xba.model import Article, PromotionHistory

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
    user_id = request.REQUEST.get("user_id")
    
    try:
        user_id = int(user_id)
    except:
        user_id = 0
    
    if user_id > 0:
        ip = request.META['REMOTE_ADDR']
        history = PromotionHistory.load(user_id=user_id, ip=ip)
        if not history:
            history = PromotionHistory()
            history.user_id = user_id
            history.ip = ip
            history.count = 1
            account_manager.add_promotion(user_id)
        else:
            history.count = history.count + 1
            
        history.persist()
    
    return json_response(1)