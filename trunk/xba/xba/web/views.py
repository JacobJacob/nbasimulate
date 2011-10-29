#-*- coding:utf-8 -*-

from xba.web.render import render_to_response
from xba.business import xbatop_manager
from xba.model import Article

def index(request):
    '''首页'''
    
    notices = Article.query(condition="status=1 and category='notice'", limit=6, order="created_time desc")
    print notices
    #notices = Article.query(condition="status=1", limit=6, order="created_time desc")
    
    top_accounts = xbatop_manager.get_top10_account()

    datas = {"notices": notices, "top_accounts": top_accounts}
    return render_to_response(request, "index.html", datas)


