

from gba.entity import ProfessionPlayer
from gba.common import responsehelper

def db_to_cls(name, table=True):
    to_upper = table
    cls_name = ''
    for n in name:
        if to_upper:
            cls_name += n.upper()
            to_upper = False
        else:
            if n == '_':
                to_upper = True
            else:
                cls_name += n
    return cls_name
    
def dbtype_to_javatype(dbtype):
    javatype = ''
    if dbtype.startswith('float'):
        javatype = "java.lang.Float"
    elif dbtype.startswith('int') or dbtype.startswith('bigint'):
        javatype = "java.lang.Long"
    elif dbtype.startswith('smallint'):
        javatype = "java.lang.Integer"
    elif dbtype.startswith('tinyint'):
        javatype = "java.lang.Boolean"
    elif dbtype.startswith('varchar'):
        javatype = "java.lang.String"
    elif dbtype.startswith("datetime") or dbtype.startswith("timestamp"):
        javatype = "java.lang.Date"
    elif dbtype.startswith('double'):
        javatype = "java.lang.Double"
    return javatype
        
if __name__ == '__main__':
    
    meta = ProfessionPlayer.get_meta()
    infos = []
    columns = meta.columns
    for c in columns:
        name = db_to_cls(c.field, table=False)
        column = c.field
        type = dbtype_to_javatype(c.type)
        infos.append({'name': name, 'column': column, 'type': type})
    data = {'items': infos}
    print responsehelper.render_to_xml(data, db_to_cls(meta.table), meta.table)
    