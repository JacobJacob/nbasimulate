#-*- coding:utf-8 -*-

from xba.web.render import render_to_response
from xba.business import xbatop_manager
from xba.model import Article

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