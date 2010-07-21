from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('',
    # Example:
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.STATIC_PATH}), 
    url(r'^$', 'views.index', name='index'),
    (r'^spider/', include('dtspider.web.spider.urls')),
    (r'^monitor/', include('dtspider.web.monitor.urls')),
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/common_service/$', 'dtspider.web.services.common_service'),
    (r'^services/client/$', 'dtspider.web.services.client'),
    (r'^services/proxy/$', 'dtspider.web.services.http_proxy'),
)
