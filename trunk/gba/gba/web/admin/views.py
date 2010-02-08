#!/usr/bin/python
# -*- coding: utf-8 -*-
""""""

from web.render import render_to_response
from entity.module import Tables, Columns
from common.db import connection
from common import json

def list(request):
    """list"""
    page = int(request.GET.get('page', 1))
    index = (page - 1) * 10
    tables = Tables.query(limit='%s, %s' % (index, 10))
    total = Tables.count()
    
    if total == 0:
        totalpage = 0
    else:
        totalpage = (total -1) / 10 + 1
    
    datas = {'tables': tables, 'totalpage': totalpage, 'page': page, \
             'nextpage': page + 1, 'prevpage': page - 1}
    return render_to_response(request, 'admin/list.html', datas)

def add(request):
    action = request.POST.get('action', None)
    data = {}
    if action == 'save':
        add_data = _save(request)
        for k, v in add_data.items():
            data[k] = v
    return render_to_response(request, 'admin/add.html', data)

_CREATE_TABLE_SCRIPT =  """
                        CREATE TABLE `%s` (
                          `Id` int(11) NOT NULL AUTO_INCREMENT,
                          PRIMARY KEY (`Id`)
                        ) ENGINE=InnoDB DEFAULT CHARSET=utf8;
                        """
def _save(request):
    name = request.POST.get('name', None)
    table = Tables()
    table.name = name
    
    column = Columns()
    column.name_s = 'id'
    column.type_s = 'int'
    column.length_s = 11
    column.key_s = 'PRI'
    column.extra_s = json.dumps(['auto_increment', ])
    try:
        Columns.transaction()
        column.persist()
        table.persist()
        Columns.commit()
    except:
        Columns.rollback()
        raise
    
    id = table.id
    data = {'name': name, 'id': id}
    cursor = connection.cursor()
    try:
        cursor.execute(_CREATE_TABLE_SCRIPT % name)
    finally:
        cursor.close()
    return data

def edit(request):
    
    id = int(request.GET.get('id', -1))
    table = Tables.load(id=id)
    datas = {'name': table.name, 'id': id}
    return render_to_response(request, 'admin/add.html', datas)

   