#!/usr/bin/python
# -*- coding: utf-8 -*-
"""base url settings"""

from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('',
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', 
     {'document_root': settings.STATIC_PATH}),
    url(r'^$', 'webauth.web.views.index', name='index'),
    
    (r'^eyurl', include('webauth.web.eyurl.urls')),
    
    # plan tasks
	url(r'^plantasks/$', 'webauth.web.plantask_views.task_list', name='plantasks'),
    
    # url info search
    (r'^url', include('webauth.web.urlinfo.urls')),
    
    # system monitor
    (r'^monitor', include('webauth.web.monitor.urls')),
    
    #reporter
    (r'^reporter', include('webauth.web.reporter.urls')),
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/client/$', 'webauth.web.services.client'),
    (r'^services/url/$', 'webauth.web.services.url_source'),
    (r'^services/url_task/$', 'webauth.web.services.url_task'),
    (r'^services/proxy/$', 'webauth.web.services.http_proxy'),
    (r'^services/domain/$', 'webauth.web.services.domain_info'),
#    (r'^services/plan_task/$', 'webauth.web.services.plan_task'),
    (r'^services/plan_task/$', 'webauth.web.services.plan_task'),
)