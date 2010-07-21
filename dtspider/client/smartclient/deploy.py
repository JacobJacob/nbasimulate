#!/usr/bin/python
# -*- coding: utf-8 -*-
"""客户端定义"""

CLIENTS = {
    "CNPROXY_SPIDER": "dtspider.client.httpproxy.cnproxy_spider.main",
    "5UPROXY_SPIDER": "dtspider.client.httpproxy.5uproxy_spider.main",
    "EMAIL_KEYWORD_SPIDER": "dtspider.client.msg.email_spider.main",
    "ZHUHAI_EMAIL_IMPORTER": "dtspider.client.email.zhuhai_email_importer.main",
    "CHINA201_ADS_SENDER": "dtspider.client.ads.china201_ads_sender.main",
    "SANYAXUNFANG_SPIDER": "dtspider.client.3ya.sanyaxunfang_spider.main",
    "INJECT_DETECT_CLIENT": "dtspider.client.inject.injection_detect_client.main",
    "INJECT_URL_SPIDER": "dtspider.client.inject.inject_url_spider.main",
}