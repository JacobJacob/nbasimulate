#!/usr/bin/python
# -*- coding: utf-8 -*-
"""URL info search"""

from django.http import HttpResponseRedirect
from django.core.urlresolvers import reverse

from webauth.web.render import render_to_response
from webauth.common import urlutil
from webauth.business import url_source
from webauth.business import url_task
from webauth.business import black_url
from webauth.business import url_third_data
from webauth.business import white_host


def search(request):
    """url 查询方法，根据请求的url，查询所有的相关信息
    包括url基本信息，url安全信息，url检测历史信息，第三方信息等
    """
    url = request.GET.get('url', '').strip()
    if not url:
        return HttpResponseRedirect(reverse('index'))
    
    url_split = urlutil.standardize(url)
    url = url_split.geturl()
    url_info = url_source.get_url(url)
    # 检测url
    url_check_info = black_url.check_url(url)
    
    check_histories = url_task.get_check_history(url)
    
    third_datas = url_third_data.get_data(url)
    
    datas = {
             'current_url': url,
             'url_info': url_info,
             'url_check_info': url_check_info,
             'check_histories': check_histories,
             'third_datas': third_datas,
            }
    
    if not url_split.is_host and url_split.host != url_split.domain:
        host_url = url_split.host_url
        host_info = url_source.get_url(host_url)
        datas['host_info'] = host_info
        datas['host_check_info'] = black_url.check_url(host_url)
    if not url_split.is_domain:
        domain_url = url_split.domain_url
        domain_info = url_source.get_url(domain_url)
        datas['domain_info'] = domain_info
        datas['domain_check_info'] = black_url.check_url(domain_url)
        
    related_blackurls = black_url.get_urls_by_host(url_split.host)
    datas['related_blackurls'] = related_blackurls
    
    return render_to_response(request, 'urlinfo/item.html', datas)

def white_hosts(request):
    return render_to_response(request, 'urlinfo/white_host.html',
                              {'white_hosts': white_host.get_white_hosts_from_db()})