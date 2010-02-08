from django.conf import settings
from django.conf.urls.defaults import *

urlpatterns = patterns('',
    # Example:
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.STATIC_PATH}), 
    url(r'^$', 'views.index', name='index'),
    (r'^admin/', include('web.admin.urls')),
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/common_service/$', 'web.services.common_service'),
    (r'^services/client/$', 'web.services.client'),
)
