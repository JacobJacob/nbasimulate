
from webauth.common.spider import Spider

_res = {
    "host": r"([0-9a-zA-Z-_]+\.[\.0-9a-zA-Z-_]+)",
    "blank": r"\s*",
    "digit": r"[0-9]+",
    "port": r"([0-9]+)",
    "portxp": r"\+([a-z+]+)",
    "proxy": r"([0-9a-zA-Z-_]+\.[\.0-9a-zA-Z-_]+:[0-9]+)",
    "urlname": r"[0-9a-zA-Z-_%]*?"}

url = u'http://www.5uproxy.net/http_fast.html'
templates = [
    {
      'info': ur"""
      <td width=\"30\">([0-9]+)</td>
      """
     }
]
spider = Spider(url, templates)

response = spider.request(url)
content = spider.get_content(response)

#info = spider.get_info(content, templates[0]['info'])
import re
proxys = re.findall(r"#ffffff';\">%(blank)s<td width=\"30\">%(digit)s</td>%(blank)s<td>%(host)s</td>%(blank)s<td width=\"60\">%(port)s</td>" % _res, content)


for proxy in proxys:
    print proxy

print content
print 'finish spider'