#!/usr/bin/python
# -*- coding: utf-8 -*-
"""default veiws"""

from django.http import HttpResponse

def report_eyurl(request):
    """上报恶意url，毒窝
    
    调用url: /eyurl/report/
    
    POST参数: 
    urls: 每行一个，间隔使用tab分隔
        url1 type1 domain_match1 host_match1 all_match1
        url2 type2 domain_match2 host_match2 all_match2
        ...
        
    返回 0 表示调用成功
    -1 表示失败
    """
    urls = request.POST.get('urls', '')
    if not urls:
        return HttpResponse('-1')
    for u in urls.split('\n'):
        if not u:
            continue
        url, type, domain_match, host_match, all_match = u.split('\t')
        try:
            type = int(type)
            domain_match = int(domain_match)
            host_match = int(host_match)
            all_match = int(all_match)
        except ValueError:
            return HttpResponse('-1')
        url_md5 = md5(url).hexdigest()
        try:
            eyurl = EYUrl.objects.get(md5=url_md5)
        except EYUrl.DoesNotExist:
            eyurl = EYUrl(url=url, type=type, md5=url_md5,
                          domain_match=domain_match,
                          host_match=host_match, all_match=all_match)
        eyurl.save()
    return HttpResponse('0')