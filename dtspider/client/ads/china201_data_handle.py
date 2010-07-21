#!/usr/bin/python
# -*- coding: utf-8 -*-

import win32com.client 

NEXT = '<p align="center"><b><font color="#ff0000">[1]</font>&nbsp;[2]&nbsp;下一页 </b></p>'
PREV = '<p align="center"><b>上一页&nbsp;&nbsp;[1]&nbsp;<font color="#ff0000">[2]</font>&nbsp;</b></p>'

DELETE_WORDS = ['(中国性健康网)', '【中国性健康网】', '-中国性健康网', '[中国性健康网]',
                 '_[中国性健康网]', '( 中国性健康网 )']

import re

KEY_WORD_RE = re.compile(ur'([-_(（\[【]?中国性健康网[)）\]】：]?)', re.VERBOSE)

KEY_WORD_RE_B = re.compile(ur'(sexjk)', re.VERBOSE)

def remove(content):
    index = content.find('中国性健康网')
    if index != -1:
        print content[index - 10: index + 20]
        ms = KEY_WORD_RE.findall(content)
        if ms:
            for m in ms:
                content = content.replace(m, '')
        return -3, content
    index = content.find('Sexjk')
    if index != -1:
        content = content.replace('(www_Sexjk_com)', '')  
        content = content.replace('(www.Sexjk.com)', '')
        content = content.replace('_www.Sexjk.com', '')
        content = content.replace('www.Sexjk.com', '')
        content = content.replace('(Sexjk.com)', '')
        content = content.replace('-Sexjk.com', '')
        content = content.replace('_Sexjkcom', '')
        content = content.replace('Sexjk.com', '')
        content = content.replace('Sexjk', '')
        ms = KEY_WORD_RE_B.findall(content)
        if ms:
            for m in ms:
                print m
        return -3, content
    if len(content) < 100:
        pass
        return -1, content
    if content.find('下一页') != -1 or content.find('上一页') != -1:
        return -1, content
    
    return -2, content

def main():
    conn = win32com.client.Dispatch(r'ADODB.Connection') 
    DSN = 'PROVIDER=Microsoft.Jet.OLEDB.4.0;DATA SOURCE=F:/#epfwy.mdb;' 
    conn.Open(DSN)

    rs = win32com.client.Dispatch(r'ADODB.Recordset') 
    rs_name = 'Yao_Article'#
    rs.Open('[' + rs_name + ']', conn, 1, 3) 
    rs.MoveFirst() 
    count = 0 
    print 'start ....'
    while 1: 
        if rs.EOF: 
            break 
        count = count + 1
        content = rs.Fields.Item(7).Value
        result, content = remove(content)
        if result == -3:
            id = rs.Fields.Item(0).Value
            #sql = "Insert INTO [Yao_Article]  ([ID], [Content]) VALUES ('%d', '%s')" % (id, content)
            print '开始更新'
            #print content
            #sql = """update Yao_Article set content="%s" where id=%s""" % (content, id)
            content = content.replace("'", '"')
            sql = "update Yao_Article set content='%s' where id=%s" % (content, id)
            conn.Execute(sql)
            print '结束更新'
#            print '-' * 100
#            print 'id'
#            print rs.Fields.Item(0).Value
#            print 'id'
#            print rs.Fields.Item(1).Value
#            print 'content'
#            print rs.Fields.Item(7).Value
#            print 'flag'
#            print rs.Fields.Item(10).Value
#            print '-' * 100
        rs.MoveNext() 
        
if __name__ == '__main__':
    main()


