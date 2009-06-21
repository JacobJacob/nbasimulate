#coding:utf-8
#Quanhai Yang @ 2008-9-6
from webauth.web.app.proxy.source import ProxyDigger, _res

_cnt = 0
def _printList(lProxy):
    if lProxy: print "\n".join(lProxy[:3])
    print "-" * 70
    for x in lProxy:
        global _cnt
        import re
        _cnt += 1 if re.findall("^%(proxy)s$" % _res, x) else 0
     
import unittest
class __Test(unittest.TestCase):
    def test_go(self):
        funcs = ProxyDigger(_printList, 1).list_self()
        cnt = 0
        for func in funcs:
            global _cnt
            _cnt = 0
            cnt += 1
            tmp = "%s. %s" % (cnt, func.__name__)
            print "%s[ %s ]%s" % ("#" * 6, tmp, "#" * (60 - len(tmp)))
            func(debug = True)
            self.assertTrue(_cnt > 0)

if __name__ == "__main__":
    unittest.main()
