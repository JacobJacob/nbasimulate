from django.conf import settings
from django.conf.urls.defaults import patterns, url, include

urlpatterns = patterns('',
    # Example:
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.STATIC_PATH}), 
    url(r'^$', 'views.index', name='index'),
    url(r'^development/$', 'views.development', name='development'),
    (r'^admin/', include('web.admin.urls')),
    (r'^user/', include('web.user.urls')),
    (r'^player/', include('web.player.urls')),
    (r'^match/', include('web.match.urls')),
    (r'^league/', include('web.league.urls')),
    (r'^team/', include('web.team.urls')),
    (r'^union/', include('web.union.urls')),
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/client/$', 'web.services.client'),
    (r'^services/match/$', 'web.services.match_service'),
    (r'^services/admin/$', 'web.services.admin_service'),
    (r'^services/player/$', 'web.services.player_service'),
    (r'^services/user/$', 'web.services.user_service'),
)
