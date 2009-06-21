#!/usr/bin/python
# -*- coding: utf-8 -*-
"""每日报表"""
from datetime import datetime, timedelta

from webauth.web.render import render_to_response
from webauth.business import url_statistic
from webauth.common.constants.url_task_const import URL_FROM_TYPE_MAP

def generate_report(request):
    """生成网站安全认证昨日报表"""
    datas = {}
    total_data = url_statistic.get_total_url_data()
    lastday_data = url_statistic.get_day_url_source()
    
    datas["total_data"] = {}
    datas["total_data"]["url_count"] = total_data["all"][0]
    datas["total_data"]["black_url_count"] = total_data["all"][1]
    datas["total_data"]["percentage"] = "%.2f" % (100.0 * total_data["all"][1] / total_data["all"][0])
    total_type_data = []
    for from_type, count in total_data.iteritems():
        if from_type == "all":
            continue
        d = {}
        d["from_type"] = URL_FROM_TYPE_MAP[from_type]
        d["url_count"] = count[0]
        d["black_url_count"] = count[1]
        if count[0] and count[1]:
            d["percentage"] = "%.2f" % (100.0 * count[1] / count[0])
        total_type_data.append(d)
    datas["total_type_data"] = total_type_data
    
    datas["lastday_data"] = {}
    datas["lastday_data"]["url_count"] = lastday_data["all"][0]
    datas["lastday_data"]["black_url_count"] = lastday_data["all"][1]
    if lastday_data["all"][0] and lastday_data["all"][1]:
        datas["lastday_data"]["percentage"] = "%.2f" % (100.0 * lastday_data["all"][1] / lastday_data["all"][0])
    lastday_type_data = []
    for from_type, count in lastday_data.iteritems():
        if from_type == "all":
            continue
        d = {}
        d["from_type"] = URL_FROM_TYPE_MAP[from_type]
        d["url_count"] = count[0]
        d["black_url_count"] = count[1]
        if count[0] and count[1]:
            d["percentage"] = "%.2f" % (100.0 * count[1] / count[0])
        lastday_type_data.append(d)
    datas["lastday_type_data"] = lastday_type_data
    last_day = datetime.now() - timedelta(days=1)
    datas["lastday"] = last_day.strftime("%Y-%m-%d")

    top_black_domain = url_statistic.get_top_black_domain()
    datas["top10_domain"] = top_black_domain

    lastday_black_domain = url_statistic.get_day_black_domain()
    datas["lastday_domain"] = lastday_black_domain

    return render_to_response(request, 'daily_report.html', datas)