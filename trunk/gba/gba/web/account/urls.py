from django.conf.urls.defaults import patterns, url

urlpatterns = patterns('django_study.web.account.views',
    url(r'^login/$', 'login', name='login'),
    url(r'^logout/$', 'logout', name='logout'),
    url(r'^register/$', 'register', name='register'),
    url(r'^captcha/$', 'captcha', name='captcha'),
)
