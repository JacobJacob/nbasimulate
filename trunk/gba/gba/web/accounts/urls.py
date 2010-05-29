from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('gba.web.accounts.views',
    url(r'^login/$', 'login', name='login'),
    url(r'^logout/$', 'logout', name='logout'),
    url(r'^register/$', 'register', name='register'),
    url(r'^captcha/$', 'captcha', name='captcha'),
    url(r'^timeout/$', 'timeout', name='timeout'),
    url(r'^ucenter/$', 'ucenter', name='ucenter'),
    url(r'^check_email/$', 'check_email', name='check-email'),
)
