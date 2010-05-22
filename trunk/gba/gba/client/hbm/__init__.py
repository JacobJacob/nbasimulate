

from gba.entity import ProfessionPlayer
from gba.common import responsehelper

def tbname_to_clsname(name):
    to_upper = True
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
        javatype = "java.util.Float"
    elif dbtype.startswith('int') or dbtype.startswith('bigint'):
        javatype = "java.util.Long"
    elif dbtype.startswith('smallint'):
        javatype = "java.util.Integer"
    elif dbtype.startswith('tinyint'):
        javatype = "java.util.Boolean"
    elif dbtype.startswith('varchar'):
        javatype = "java.util.String"
    elif dbtype.startswith("datetime") or dbtype.startswith("timestamp"):
        javatype = "java.util.Date"
    return javatype
        
if __name__ == '__main__':
    
    meta = ProfessionPlayer.get_meta()
    infos = []
    columns = meta.columns
    for c in columns:
        name = c.field
        column = name
        type = dbtype_to_javatype(c.type)
        infos.append({'name': name, 'column': column, 'type': type})
    data = {'items': infos}
    print responsehelper.render_to_xml(data, tbname_to_clsname(meta.table), meta.table)
    