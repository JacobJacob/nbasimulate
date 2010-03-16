from django.conf import settings
from django.conf.urls.defaults import patterns, url, include

urlpatterns = patterns('',
    # Example:
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.STATIC_PATH}), 
    url(r'^$', 'views.index', name='index'),
    url(r'^left/$', 'views.left', name='left'),
    url(r'^right/$', 'views.right', name='right'),
    url(r'^admintop/$', 'views.admin_top', name='admin-top'),
    (r'^admin/', include('web.admin.urls')),
    (r'^user/', include('web.user.urls')),
    (r'^player/', include('web.player.urls')),
    (r'^match/', include('web.match.urls')),
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/match/$', 'web.services.match_service'),
    (r'^services/admin/$', 'web.services.admin_service'),
)
