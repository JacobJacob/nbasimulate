#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端定义"""

CLIENTS = {
    "URL_AUTH_SANDBOX": "webauth.client.webshield.sandbox_authclient.main",
    "URL_TASK_CREATOR": "webauth.client.url_task.url_task_creator.main",
    "URL_TASK_EXPIRER": "webauth.client.url_task.url_task_expired.main",
    "DEMO_CLIENT": "webauth.client.base_test.main",  
    "BAIDU_TOP_URL": "webauth.client.urlsource.baidu_top_spider.main",  
    "GOOGLE_REBANG_URL": "webauth.client.urlsource.google_rebang_spider.main",
    "GATEWAY_DATA_SPIDER": "webauth.client.urlsource.gateway_data_spider.main" ,
    
    "WEBSHIELD_URL_IMPORTER": "webauth.client.webshield.import_webshield_source.main" ,
    "URL_SOURCE_STATISTIC": "webauth.client.statistics.url_source_statistic.main" ,
    "DOMAIN_IMPORTER": "webauth.client.urlsource.domain_importer.main" ,
}