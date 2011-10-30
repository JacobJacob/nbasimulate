#!/usr/bin/python
# -*- coding: utf-8 -*-
"""文章相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.model import Article
from xba.common import staticutil

@jsonrpc_function
def set_article_status(request, id, status):
    """设置文章状态"""
    article = Article.load(id=id)
    article.status = status
    ret_val = article.persist()
    staticutil.clean()
    return ret_val
    
@jsonrpc_function
def delete_article(request, id):
    """删除文章"""
    article = Article.load(id=id)
    ret_val = article.delete()
    staticutil.clean()
    return ret_val