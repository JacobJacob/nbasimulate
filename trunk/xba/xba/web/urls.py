from django.conf import settings
from django.conf.urls.defaults import patterns, url, include

urlpatterns = patterns('',
    # Example:
     url(r'^$', 'xba.web.views.index', name='index'),
    (r'^site_media/(?P<path>.*)$', 'django.views.static.serve', {'document_root': settings.STATIC_PATH}), 
    (r'^player/', include('web.player.urls')),
    (r'^game/', include('web.game.urls')),
    (r'^club/', include('web.club.urls')),
    (r'^cup/', include('web.cup.urls')),
    (r'^account/', include('web.account.urls')),
)

# json rpc settings
jsonrpc_urlpatterns = patterns('',
    (r'^services/player/$', 'web.services.player_service'),
    (r'^services/account/$', 'web.services.account_service'),
    (r'^services/game/$', 'web.services.game_service'),
)
