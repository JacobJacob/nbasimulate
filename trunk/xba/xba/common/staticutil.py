#!/usr/bin/python
# -*- coding: utf-8 -*-

import os

from xba.config import DjangoSettings
from xba.common import file_utility

def dump(response, path):
    """生成静态文件"""
    if path[0] == "/":
        path = path[1:]
    html = response.content
    full_path = os.path.join(DjangoSettings.WEB_ROOT, path)
    print "dump", full_path
    file_utility.ensure_dir_exists(os.path.dirname(full_path))
    f = open(full_path, "wb")
    try:
        f.write(html)
    finally:
        f.close()
        
def clean(path=DjangoSettings.WEB_ROOT):
    """清除静态文件"""
    file_utility.remove_dir_tree(path)

def delete(path):
    if path[0] == "/":
        path = path[1:]
    full_path = os.path.join(DjangoSettings.WEB_ROOT, path)
    if os.path.exists(full_path):
        os.remove(full_path)
   

if __name__ == "__main__":
    response = "test"
    dump(response, "arti/ddfsdf/s/html")
    print os.path.join("ss", "dddd")