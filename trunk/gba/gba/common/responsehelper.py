#!/usr/bin/python
# -*- coding: utf-8 -*-
"""response辅助方法
"""

from gba.common import tenjin


TEMPLATE = """<?xml version="1.0"?>
<!DOCTYPE hibernate-mapping PUBLIC
    "-//Hibernate/Hibernate Mapping DTD//EN"
    "http://hibernate.sourceforge.net/hibernate-mapping-3.0.dtd" >

<hibernate-mapping package="com.ts.dt.po">
    <class name="%s" table="%s">
        <id
            name="id"
            type="java.lang.Long"
            column="id"
        >
            <generator class="native"/>
        </id><?py
for i in items: 
?>
        <property
          name="${i['name']}"
          column="${i['column']}"
          type="${i['type']}"
        />
<?py #endfor ?>
</class>
</hibernate-mapping>"""

_result_tpl = None

def render_to_xml(data, cls, table):
    global _result_tpl, escape, to_str
    if _result_tpl is None:
        _result_tpl = tenjin.Template(escapefunc='escape',
                                      tostrfunc='to_str',
                                      smarttrim=True)
        _result_tpl.convert(TEMPLATE % (cls, table))
        escape = tenjin.helpers.escape
        to_str = tenjin.helpers.to_str
    output = _result_tpl.render({'items': data})
    return output
