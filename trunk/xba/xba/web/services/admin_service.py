#!/usr/bin/python
# -*- coding: utf-8 -*-
"""文章相关rpc"""

from xba.common.jsonrpcserver import jsonrpc_function
from xba.model import Article

@jsonrpc_function
def set_article_status(request, id, status):
    """设置公告状态"""
    article = Article.load(id=id)
    article.status = status
    return article.persist()
    
@jsonrpc_function
def delete_article(request, id):
    """删除文章"""
    article = Article.load(id=id)
    return article.delete()