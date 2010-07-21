from django.conf.urls.defaults import *

urlpatterns = patterns('dtspider.web.spider',
    # Example:
    url(r'^$', 'views.index', name='spider-index'), 
)
