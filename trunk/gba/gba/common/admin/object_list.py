#-*- coding:utf-8 -*-
from entity.module import modules

class ObjectList():
    
    def __init__(self, cls, page=1, size=10, condition=None):
        self._cls = cls
        self._page = page
        self._size = size
        self._condition = condition
        self._objs = self.query()
    
    def query(self):
        objs = self._cls.query(limit='%s, %s' % (self._page, self._size))
        return objs
    
    def __str__(self):
        if self._cls is None:
            return 'cls is none'
        admin_cls = modules[self._cls]
        display_fields = admin_cls._display
        meta = self._cls.get_meta()
        columns = meta.columns
        s = '<table>'
        cols = []
        for column in columns:
            s += '<tr>'
            if column.field in display_fields:
                s += '<th>'
                s += column.field
                s += '</th>'
                cols.append(column.field)
            s += '</tr>'
        for obj in self._objs:
            s += '<tr>'
            for col in cols:
                s += '<td>'
                s += getattr(obj, col)
                s += '</td>'
        return s