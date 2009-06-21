
from webauth.business import domain_info as domain
from webauth.common import md5mgr

f = open('hosts.dat', 'rb')
domain_infos = []
for i, host in enumerate(f):
    if host:
        domain_info = {}
        domain_info['domain'] = host.replace('\r\n', '')
        print 'domain:%s' % domain_info['domain']
        domain_info['domain_md5'] = md5mgr.mkmd5fromstr(domain_info['domain'])
        domain_infos.append(domain_info)
    if i % 1000 == 0:
        domain.insert_domain(domain_infos)
        domain_infos =  []

domain.insert_domain(domain_infos)