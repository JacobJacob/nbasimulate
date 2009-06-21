#RE = '''
#<PHONE NUMBER="+1 650 318 0200, Fax: +1 650 618 1499"/>
#<OWNER NAME="Larry Page"/>
#'''
#
#RE2 = '''
#<PHONE NUMBER="+1 650 318 0200, Fax: +1 650 618 1499"/>
#'''
#
#exp = ur"""
#     <PHONE\s*NUMBER="(?P<phone>.*?)"\s*/>[\S\s]
#     (<OWNER\s*NAME="(?P<author>.*?)"\s*/>)?
#   """
#
#print RE
#
#from webauth.common.spider import Spider
#
#spider = Spider('',[])
#
#info = spider.get_info(RE, exp)
#print info
#
#info = spider.get_info(RE2, exp)
#print info

a = 'a'
b = None

print ' '.join((a, b))

