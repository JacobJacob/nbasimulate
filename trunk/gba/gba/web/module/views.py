#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from entity.module import module_list, module_admin_list
from web.render import render_to_response



def index(request):
    return render_to_response(request, 'module/index.html', {'modules': module_list})


def list(request, module):
    
    module_cls = module_list[module]
    page = int(request.GET.get('page', 1))
    index = (page - 1) * 10
    modules = module_cls.query(limit='%s, %s' % (index, 10))
    total = module_cls.count()
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / 10 + 1
    
    datas = {'modules': modules, 'totalpage': totalpage, 'page': page, \
             'nextpage': page + 1, 'prevpage': page - 1, 'module': module}
    return render_to_response(request, 'module/list.html', datas)

def edit(request, module):
    
    id = int(request.GET.get('id', -1))
    module_cls = module_list[module]
    if id != -1:
        module_obj = module_cls.load(id=id)
    else:
        module_obj = module_cls()
        
    datas = {'module': module_obj}
    return render_to_response(request, 'module/edit.html', datas)

def save(requset):
    
    
    return render_to_response(request, 'module/edit.html', datas)
   