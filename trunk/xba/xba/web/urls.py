from django.conf import settings
from django.conf.urls.defaults import patterns, url, include

urlpatterns = patterns('',
    # Example:
     url(r'^$', 'xba.web.views.index', name='index'),
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.STATIC_PATH}), 
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/player/$', 'web.services.player_service'),
)
